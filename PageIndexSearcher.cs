using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WikipediaExtractor
{
    /// <summary>
    /// Searches for page index items by ID, title and regular expression in a Wikipedia data dump index and reports progress.
    /// </summary>
    public class PageIndexSearcher
    {
        public event EventHandler<PageIndexItemFoundEventArgs> PageIndexItemFound;
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public string[] WholeIndex;

        public PageIndexSearcher(string path)
        {
            WholeIndex = File.ReadAllLines(path);
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

        public List<PageIndexItem> Search(List<string> pageTitles, FileStream fs, int top_n_results = 6)
        {
            pageTitles = pageTitles.Distinct().ToList();
            List<PageIndexItem>[] pageIndexItems = new List<PageIndexItem>[pageTitles.Count];

            int SPLIT_AMOUNT = Environment.ProcessorCount / 2;
            int chunks_len = WholeIndex.Length / SPLIT_AMOUNT;

            List<string[]> split_titles = new List<string[]>();
            for (int i = 0; i < pageTitles.Count; i++) {
                pageIndexItems[i] = new List<PageIndexItem>();
                split_titles.Add(pageTitles[i].Trim().ToLower().Split(' '));
            }

            Parallel.For(0, SPLIT_AMOUNT, c => {
                int starti = c * chunks_len;
                int endi = Math.Min((c + 1) * chunks_len, WholeIndex.Length);
                for (int i=starti; i<endi; i++) {
                    string line = WholeIndex[i];
                    string low_line = line.ToLower();
                    for (int j = 0; j < split_titles.Count; j++) {
                        for (int k=0; k < split_titles[j].Length; k++) {
                            string search_term = split_titles[j][k];
                            if (low_line.Contains(search_term) == false || ContainsWholeWord(low_line, search_term) == false)
                                goto not_found;
                        }
                        // found a match!
                        string[] split = line.Split(':');
                        if (split.Length == 3) {
                            PageIndexItem item = new PageIndexItem() {
                                ByteStart = long.Parse(split[0]),
                                PageId = int.Parse(split[1]),
                                PageTitle = split[2],
                            };
                            var add_to = pageIndexItems[j];
                            lock (add_to) {
                                add_to.Add(item);
                            }
                        } not_found:;
                    }
                }
            });

            // sort the smallest titles to the top (likely the closest matches)
            foreach(var pii in pageIndexItems)
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