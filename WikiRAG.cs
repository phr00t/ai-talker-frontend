using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WikipediaExtractor;

namespace TalkerFrontend {
    public class WikiRAG {

        public static string IndexFile, DumpFile;
        public static PageIndexSearcher indexSearcher;

        public static string UpdateFiles(bool show_box = true) {
            string directory = Integration.MainForm.GetWikiDirectory;
            IndexFile = null; DumpFile = null; indexSearcher = null;
            if (Directory.Exists(directory)) {
                string[] index_file = Directory.GetFiles(directory, "*index.txt");
                string[] xml_file = Directory.GetFiles(directory, "*xml.bz2");
                if (index_file.Length > 0) {
                    IndexFile = index_file[0];
                    if (show_box) MessageBox.Show("Index found, click 'OK' to begin importing (wait a minute or less)...", "Importing Acknowledgement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    indexSearcher = new PageIndexSearcher(IndexFile);
                } else return "...index.txt file not found (did you uncompress it?)";
                if (xml_file.Length > 0) DumpFile = xml_file[0];
                else return "...xml.bz2 dump not found!";
            } else return "Directory not found!";
            return indexSearcher.WholeIndex.Length.ToString() + " entries indexed!";
        }

        public static string GatherInformation(List<string> pageTitles, int length_allowance = 2048, int min_len_per_document = 192) {
            if (IndexFile == null || DumpFile == null || indexSearcher == null || length_allowance <= 0) return "";
            for (int i = 0; i < pageTitles.Count; i++) pageTitles[i] = pageTitles[i].Trim();
            List<DataDumpReader.WikipediaEntry> results = new List<DataDumpReader.WikipediaEntry>();
            List<PageIndexItem> pageIndexItems = new List<PageIndexItem>();
            using (var dataDumpReader = new DataDumpReader(DumpFile)) {
                List<string> searching_for = pageTitles;
                follow_redirects: List<PageIndexItem> new_finds = indexSearcher.Search(searching_for, dataDumpReader.DataDumpStream, Math.Min(6, searching_for.Count));
                results.AddRange(dataDumpReader.Grab(new_finds, out searching_for));
                pageIndexItems.AddRange(new_finds);
                // dont research stuff we already got
                for (int i=0; i<new_finds.Count; i++) searching_for.Remove(new_finds[i].PageTitle);
                if (Integration.MainForm.FolloWikiRedirects && searching_for.Count > 0) goto follow_redirects;
            }

            int total_len = 0, total_int = 0;
            for (int i=0; i<results.Count; i++) {
                total_len += results[i].CappedEntry.Length;
            }
            float trim_by = (float)(length_allowance * 0.66f) / total_len;
            if (trim_by > 1f) trim_by = 1f;

            for (int i = 0; i < results.Count; i++) {
                int trim_stop = Math.Min(results[i].CappedEntry.Length, Math.Max(min_len_per_document, (int)(results[i].CappedEntry.Length * trim_by)));
                results[i].TrimmedEntry = results[i].CappedEntry.Substring(0, trim_stop);
                // do some internal searching for more information
                for (int j = 0; j < pageTitles.Count; j++) {
                    string keyword = pageTitles[j];
                    if (keyword.Equals(results[i].Title, StringComparison.CurrentCultureIgnoreCase)) continue;
                    int find_keyword = results[i].WholeEntry.IndexOf(keyword, trim_stop, StringComparison.CurrentCultureIgnoreCase);
                    if (find_keyword > -1) {
                        string interesting = StringProcessor.ReturnAround(results[i].WholeEntry, find_keyword, Integration.MainForm.WordsPerRecall);
                        total_int += interesting.Length;
                        results[i].InterestingParts.Add(interesting);
                    }
                }
            }

            float trim_int_by = (float)(length_allowance * 0.33f) / total_int;
            if (trim_int_by > 1f) trim_int_by = 1f;

            // ok, we've gathered all of the data, now to consolidate it all inside the allowances
            if (results.Count > 0) {
                string retval = "\n\nWikipedia Research Topic Results (which may or may not be relevant):\n";
                for (int i = 0; i < results.Count; i++) {
                    var r = results[i];
                    retval += r.Title + ": " + r.TrimmedEntry + "...";
                    if (r.InterestingParts.Count > 0) {
                        for (int j = 0; j < Math.Max(1, (int)(trim_int_by * r.InterestingParts.Count)); j++)
                            retval += "\n..." + r.InterestingParts[j] + "...";
                    }
                    retval += "\n\n";
                }
                retval += "End of Wikipedia Research\n\n";
                return retval;
            }

            return "";
        }

        public static string CleaupWikipediaArticle(string input_string) {
            if (string.IsNullOrEmpty(input_string))
                return input_string;

            string output = input_string;

            Regex curly = new Regex(@"\{\{[^{}]*\}\}");
            Regex fileTag = new Regex(@"\[\[File:.*\]\]", RegexOptions.IgnoreCase);
            Regex categoryTag = new Regex(@"\[\[Category:.*\]\]", RegexOptions.IgnoreCase);

            bool changed = true;

            while (changed) {
                changed = false;

                string newOutput = curly.Replace(output, "");
                if (newOutput != output) {
                    output = newOutput;
                    changed = true;
                }

                newOutput = fileTag.Replace(output, "");
                if (newOutput != output) {
                    output = newOutput;
                    changed = true;
                }

                newOutput = categoryTag.Replace(output, "");
                if (newOutput != output) {
                    output = newOutput;
                    changed = true;
                }
            }

            output = output.Replace("''''", "\"");
            output = output.Replace("'''", "\"");
            output = output.Replace("''", "\"");
            output = output.Replace("[[", "[");
            output = output.Replace("]]", "]");
            output = output.Replace("\n\n", "\n");

            return Regex.Replace(WebUtility.HtmlDecode(output.Trim()), "<.*?>", String.Empty).Trim();
        }

        public static void Test() {
            string keywords = "Superbowl X, aftermath";
            UpdateFiles(false);
            GatherInformation(new List<string>(keywords.Split(',')));
        }
    }
}
