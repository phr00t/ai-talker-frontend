using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TalkerFrontend;

namespace WikipediaExtractor
{
    /// <summary>
    /// Searches for page index items by ID, title and regular expression in a Wikipedia data dump index and reports progress.
    /// </summary>
    public class PageIndexSearcher
    {
        public PageIndexInfo[] WholeIndex;
        public int ActualWholeIndexSize;

        public class PageIndexInfo {
            public string WholeLine;
            public int titleStartPosition, pageIndexSplitColon;
        }

        public PageIndexSearcher(string path)
        {
            // start with a really big list
            var AllLines = new List<string>(40000000);

            using (StreamReader sr = File.OpenText(path)) {
                while (!sr.EndOfStream) {
                    AllLines.Add(sr.ReadLine());
                }
            }

            ActualWholeIndexSize = 0;
            WholeIndex = new PageIndexInfo[AllLines.Count];
            int chunk_into = Environment.ProcessorCount / 2;
            int chunk_len = AllLines.Count / chunk_into;

            //Now parallel process each line in the file
            Parallel.For(0, chunk_into, x =>
            {
                int starti = x * chunk_len;
                int endi = Math.Min(AllLines.Count, (x + 1) * chunk_len);
                for (int i=starti; i<endi; i++) {
                    string l = AllLines[i];
                    int colonCount = 0, lastColon = 0, doubleLastColon = 0, colonSearch = -1;
                    do {
                        colonSearch = l.IndexOf(':', colonSearch + 1);
                        if (colonSearch > -1) {
                            doubleLastColon = lastColon;
                            lastColon = colonSearch;
                            colonCount++;
                        } else break;
                    } while (colonSearch > -1);
                    if (colonCount == 2) {
                        // good entry (not a template or category)
                        int addToIndex = Interlocked.Increment(ref ActualWholeIndexSize) - 1;
                        WholeIndex[addToIndex] = new PageIndexInfo {
                            WholeLine = l,
                            titleStartPosition = lastColon + 1,
                            pageIndexSplitColon = doubleLastColon,
                        };
                    }
                }
            });
        }

        public static bool ContainsWholeWord(string source, string word, bool ignoreCase = false) {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(word)) {
                return false;
            }

            string pattern = $@"\b{Regex.Escape(word)}(?:'s)?(?=\b|\W)"; //$@"\b{Regex.Escape(word)}\b"; // \b matches word boundaries
            return Regex.IsMatch(source, pattern, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        public static string FirstLetterToUpperCase(string s) {
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public List<PageIndexItem> Search(List<string> pageTitles, int top_n_results = 6, bool exact_match = false)
        {
            if (ActualWholeIndexSize <= 0) return new List<PageIndexItem>();

            pageTitles = pageTitles.Distinct().ToList();
            List<PageIndexItem>[] pageIndexItems = new List<PageIndexItem>[pageTitles.Count];

            int SPLIT_AMOUNT = Environment.ProcessorCount / 2;
            int chunks_len = ActualWholeIndexSize / SPLIT_AMOUNT;

            if (!exact_match) {
                // keyword loose search
                List<string[]> split_titles = new List<string[]>();
                for (int i = 0; i < pageTitles.Count; i++) {
                    pageIndexItems[i] = new List<PageIndexItem>();
                    split_titles.Add(pageTitles[i].Trim().ToLower().Split(' '));
                }

                Parallel.For(0, SPLIT_AMOUNT, c => {
                    int starti = c * chunks_len;
                    int endi = Math.Min((c + 1) * chunks_len, WholeIndex.Length);
                    for (int i = starti; i < endi; i++) {
                        var index = WholeIndex[i];
                        string line = index.WholeLine;
                        for (int j = 0; j < split_titles.Count; j++) {
                            for (int k = 0; k < split_titles[j].Length; k++) {
                                string search_term = split_titles[j][k];
                                if (line.IndexOf(search_term, index.titleStartPosition, StringComparison.CurrentCultureIgnoreCase) == -1 || ContainsWholeWord(line, search_term, true) == false)
                                    goto not_found;
                            }
                            // found a match!
                            PageIndexItem item = new PageIndexItem() {
                                ByteStart = long.Parse(line.Substring(0, index.pageIndexSplitColon)),
                                PageId = int.Parse(line.Substring(index.pageIndexSplitColon + 1, index.titleStartPosition - index.pageIndexSplitColon - 2)),
                                PageTitle = line.Substring(index.titleStartPosition),
                            };
                            var add_to = pageIndexItems[j];
                            lock (add_to) {
                                add_to.Add(item);
                            }
                            not_found:;
                        }
                    }
                });
            } else {
                // exact match search
                for (int i = 0; i < pageTitles.Count; i++)
                    pageIndexItems[i] = new List<PageIndexItem>();

                Parallel.For(0, SPLIT_AMOUNT, c => {
                    int starti = c * chunks_len;
                    int endi = Math.Min((c + 1) * chunks_len, WholeIndex.Length);
                    for (int i = starti; i < endi; i++) {
                        var index = WholeIndex[i];
                        string line = index.WholeLine;
                        for (int j = 0; j < pageTitles.Count; j++) {
                            if (line.EndsWith(":" + pageTitles[j])) {
                                PageIndexItem item = new PageIndexItem() {
                                    ByteStart = long.Parse(line.Substring(0, index.pageIndexSplitColon)),
                                    PageId = int.Parse(line.Substring(index.pageIndexSplitColon + 1, index.titleStartPosition - index.pageIndexSplitColon - 2)),
                                    PageTitle = line.Substring(index.titleStartPosition),
                                };
                                var add_to = pageIndexItems[j];
                                lock (add_to) {
                                    add_to.Add(item);
                                }
                                break;
                            }
                        }
                    }
                });
            }

            // sort the smallest titles to the top (likely the closest matches)
            foreach (var pii in pageIndexItems)
                pii.Sort((o1, o2) => { return o1.PageTitle.Length.CompareTo(o2.PageTitle.Length); });

            // make sure to take results from all things
            List<PageIndexItem> final_top_list = new List<PageIndexItem>();
            for (int i=0; i<top_n_results; i++) {
                foreach(var pii in pageIndexItems) {
                    if (pii.Count > i) {
                        // make sure we don't already have it
                        for (int j=0; j<final_top_list.Count; j++) {
                            if (final_top_list[j].PageId == pii[i].PageId) goto skip_this_id;
                        }
                        final_top_list.Add(pii[i]); skip_this_id:;
                    }
                }
            }

            return final_top_list.GetRange(0, Math.Min(top_n_results, final_top_list.Count));
        }
    }
}