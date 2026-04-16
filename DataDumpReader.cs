using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using TalkerFrontend;

namespace WikipediaExtractor
{
    /// <summary>
    /// Extracts XML page data from a multistream Wikipedia data dump using byte offset positions read from index items.
    /// </summary>
    public class DataDumpReader : IDisposable
    {
        public readonly FileStream DataDumpStream;

        public event EventHandler<DataDumpPageFoundEventArgs> DataDumpPageFound;

        public class WikipediaEntry {
            public string Title;
            public string WholeEntry, CappedEntry, TrimmedEntry;
            public List<string> InterestingParts = new List<string>();
        }

        public DataDumpReader(string path)
        {
            DataDumpStream = new FileStream(path, FileMode.Open);
        }

        public DataDumpReader(FileStream dataDumpStream)
        {
            DataDumpStream = dataDumpStream ?? throw new ArgumentNullException("dataDumpStream");
        }

        public List<WikipediaEntry> Grab(List<PageIndexItem> pageIndexItems, out List<string> redirects, int max_len = 4096)
        {
            List<WikipediaEntry> results = new List<WikipediaEntry>();
            redirects = new List<string>();
            var was_redirects = new List<PageIndexItem>();
            for (int i=0; i<pageIndexItems.Count; i++)
            {
                var page = pageIndexItems[i];
                byte[] streamBytes;
                try {
                    streamBytes = ReadStream(DataDumpStream, page.ByteStart);
                } catch { continue; }
                var streamText = System.Text.Encoding.Default.GetString(streamBytes);
                int start_pos = streamText.IndexOf("<id>" + page.PageId + "</id>");
                if (start_pos > -1) {
                    start_pos = streamText.IndexOf("<text", start_pos);
                    if (start_pos > -1) {
                        start_pos = streamText.IndexOf('>', start_pos);
                        if (start_pos > -1) {
                            int end_pos = streamText.IndexOf("</text>", start_pos);
                            if (end_pos > -1) {
                                string whole_text = WikiRAG.CleaupWikipediaArticle(streamText.Substring(start_pos + 1, end_pos - start_pos - 1));
                                if (whole_text.StartsWith("#REDIRECT") == false) {
                                    results.Add(new WikipediaEntry() {
                                        Title = page.PageTitle,
                                        CappedEntry = whole_text.Length > max_len ? whole_text.Substring(0, max_len) : whole_text,
                                        WholeEntry = whole_text
                                    });
                                } else {
                                    // oof, redirect...
                                    redirects.Add(whole_text.Split('[').Last().Replace("]", "").Trim());
                                    pageIndexItems.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }
                }
            }
            return results;
        }

        private byte[] ReadStream(Stream dataDumpStream, long offset)
        {
            dataDumpStream.Seek(offset, SeekOrigin.Begin);
            using (var decompressedStream = new MemoryStream())
            {
                BZip2.Decompress(dataDumpStream, decompressedStream, false);
                return decompressedStream.ToArray();
            }
        }

        public void Dispose()
        {
            DataDumpStream?.Dispose();
        }
    }
}