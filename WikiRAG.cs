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

        public static string UpdateFiles() {
            string directory = Integration.MainForm.GetWikiDirectory;
            IndexFile = null; DumpFile = null; indexSearcher = null;
            if (Directory.Exists(directory)) {
                string[] index_file = Directory.GetFiles(directory, "*index.txt");
                string[] xml_file = Directory.GetFiles(directory, "*xml.bz2");
                if (index_file.Length > 0) {
                    IndexFile = index_file[0];
                    MessageBox.Show("Index found, click 'OK' to begin importing (wait a minute or less)...", "Importing Acknowledgement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    indexSearcher = new PageIndexSearcher(IndexFile);
                } else return "...index.txt file not found (did you uncompress it?)";
                if (xml_file.Length > 0) DumpFile = xml_file[0];
                else return "...xml.bz2 dump not found!";
            } else return "Directory not found!";
            return indexSearcher.WholeIndex.Length.ToString() + " entries indexed!";
        }

        public static string GatherInformation(List<string> pageTitles, int length_allowance = 2048) {
            if (IndexFile == null || DumpFile == null || indexSearcher == null || length_allowance <= 0) return "";
            List<DataDumpReader.WikipediaEntry> results;
            using (var dataDumpReader = new DataDumpReader(DumpFile)) {
                var pageIndexItems = indexSearcher.Search(pageTitles, dataDumpReader.DataDumpStream, Math.Min(6, pageTitles.Count));
                results = dataDumpReader.Grab(pageIndexItems);
            }

            int total_len = 0, total_int = 0;
            for (int i=0; i<results.Count; i++) {
                total_len += results[i].WholeEntry.Length;
            }
            float trim_by = (float)(length_allowance * 0.66f) / total_len;
            if (trim_by > 1f) trim_by = 1f;

            for (int i = 0; i < results.Count; i++) {
                int trim_stop = Math.Min(results[i].WholeEntry.Length, Math.Max(128, (int)(results[i].WholeEntry.Length * trim_by)));
                results[i].TrimmedEntry = results[i].WholeEntry.Substring(0, trim_stop);
                // do some internal searching for more information
                for (int j = 0; j < pageTitles.Count; j++) {
                    string keyword = pageTitles[j];
                    if (keyword.Equals(results[i].Title, StringComparison.CurrentCultureIgnoreCase)) continue;
                    int find_keyword = results[i].WholeEntry.IndexOf(keyword, trim_stop);
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
            }

            output = output.Replace("''''", "\"");
            output = output.Replace("'''", "\"");
            output = output.Replace("''", "\"");
            output = output.Replace("[[", "[");
            output = output.Replace("]]", "]");
            output = output.Replace("\n\n", "\n");

            return Regex.Replace(WebUtility.HtmlDecode(output.Trim()), "<.*?>", String.Empty);
        }

        public static void Test() {
            GatherInformation(new List<string> { "2026 Iran War", "Iran War 2026", "guard" });
        }
    }
}
