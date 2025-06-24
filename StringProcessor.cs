using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TalkerFrontend {
    public class StringProcessor {

        public static List<string> GetMostUniqueStrings(List<string> stringList) {
            if (stringList == null || stringList.Count == 0) {
                return new List<string>();
            }

            int n = stringList.Count;

            // Get distinct strings to work with, as duplicates don't affect uniqueness score.
            var uniqueStrings = stringList.Distinct().ToList();

            // If the number of unique strings is less than or equal to N,
            // no need for complex calculations, just return them all.
            if (uniqueStrings.Count <= n) {
                return uniqueStrings;
            }

            // A thread-safe dictionary to store the uniqueness score for each string.
            var stringScores = new ConcurrentDictionary<string, double>();

            // Process the uniqueness score calculation in parallel for efficiency.
            Parallel.ForEach(uniqueStrings, currentString =>
            {
                double totalDistance = 0;
                for (int i = 0; i < uniqueStrings.Count; i++) {
                    // No need to compare a string to itself.
                    if (currentString != uniqueStrings[i]) {
                        totalDistance += LevenshteinDistance(currentString, uniqueStrings[i]);
                    }
                }

                // The score is the average distance to all other strings.
                double averageDistance = totalDistance / (uniqueStrings.Count - 1);
                stringScores[currentString] = averageDistance;
            });

            // Order the strings by their score in descending order and take the top N.
            var topNStrings = stringScores
                .OrderByDescending(pair => pair.Value)
                .Take(n)
                .Select(pair => pair.Key)
                .ToList();

            return topNStrings;
        }

        /// <summary>
        /// Calculates the Levenshtein distance between two strings.
        /// This represents the number of edits (insertions, deletions, substitutions)
        /// required to change one string into the other.
        /// </summary>
        /// <param name="s1">The first string.</param>
        /// <param name="s2">The second string.</param>
        /// <returns>The Levenshtein distance.</returns>
        private static int LevenshteinDistance(string s1, string s2) {
            int[,] d = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++) {
                d[i, 0] = i;
            }

            for (int j = 0; j <= s2.Length; j++) {
                d[0, j] = j;
            }

            for (int j = 1; j <= s2.Length; j++) {
                for (int i = 1; i <= s1.Length; i++) {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[s1.Length, s2.Length];
        }

        public static readonly List<string> Suffixes = new List<string>
        {
            "n't", // e.g., for "isn't", "don't"
            "'ll", // e.g., for "we'll"
            "'re", // e.g., for "you're"
            "'ve", // e.g., for "I've"
            "'d",  // e.g., for "he'd" (could be "would" or "had")
            "'m",  // e.g., for "I'm"
            "'s",  // Possessive/is/has (e.g., "it's", "John's")
            "s'"   // Possessive plural (e.g., "students'")
        };
        
        public static HashSet<string> common_words = new HashSet<string>()
        {
            // Articles
            "a", "an", "the",

            // Conjunctions
            "and", "but", "or", "nor", "for", "so", "yet", "if", "else", "then", "as", "because", "until", "while", "unless", "whether", "though", "although", "whereas",

            // Prepositions
            "about", "above", "across", "after", "against", "along", "among", "around", "at", "before", "behind", "below", "beneath", "beside", "besides", "between", "beyond", "by", "concerning", "despite", "down", "during", "except", "from", "in", "inside", "into", "like", "near", "of", "off", "on", "onto", "out", "outside", "over", "past", "regarding", "since", "through", "throughout", "to", "toward", "towards", "under", "underneath", "unto", "up", "upon", "with", "within", "without",

            // Pronouns
            "i", "me", "my", "mine", "myself",
            "you", "your", "yours", "yourself", "yourselves",
            "he", "him", "his", "himself",
            "she", "her", "hers", "herself",
            "it", "its", "itself",
            "we", "us", "our", "ours", "ourselves",
            "they", "them", "their", "theirs", "themselves",
            "what", "which", "who", "whom", "whose", "this", "that", "these", "those",
            "all", "any", "both", "each", "either", "enough", "every", "few", "former", "latter", "least", "little", "many", "more", "most", "much", "neither", "next", "none", "one", "other", "several", "some", "such", "various",

            // Auxiliary Verbs (helping verbs)
            "am", "is", "are", "was", "were", "be", "been", "being",
            "have", "has", "had", "having",
            "do", "does", "did", "doing",
            "will", "would", "shall", "should", "may", "might", "must", "can", "could",

            // Common Adverbs (many end in -ly, but these are very general)
            "also", "always", "ever", "never", "not", "often", "once", "rather", "seldom", "sometimes", "still", "then", "too", "very", "just", "quite", "really", "even", "here", "there", "when", "where", "why", "how", "now", "soon", "late", "early", "again", "further", "already", "yet", "almost", "perhaps", "maybe", "truly",

            // Common Modifiers/Qualifiers
            // "about", // Already in prepositions
            "approximately",
            // "around", // Already in prepositions
            "certainly", "completely", "definitely", "effectively", "essentially", "exactly", "extremely", "fairly", "fully", "generally", "greatly", "highly", "hopefully", "largely", "literally", "mainly", "mostly", "nearly", "necessarily", "obviously", "particularly", "possibly", "primarily", "probably", "purely", "simply", "slightly", "somewhat", "surely", "totally", "typically", "ultimately", "usually", "virtually",

            // Other common words that often form structure
            "get", "gets", "got", "gotten",
            "go", "goes", "went", "gone",
            "make", "makes", "made",
            "no",
            "only",
            "own",
            "same",
            // "some", // Already in pronouns
            "than",
            "well"
        };

        public static void CombineMemories(ConcurrentDictionary<string, List<StringPosition>> newstuff, ConcurrentDictionary<string, List<StringPosition>> existing) {
            Parallel.ForEach(newstuff, (pair) => {
                if (existing.TryAdd(pair.Key, pair.Value) == false) {
                    // need to merge info
                    List<StringPosition> newlist = pair.Value;
                    List<StringPosition> existing_list = existing[pair.Key];
                    for (int m = 0; m < newlist.Count; m++) {
                        for (int n = 0; n < existing_list.Count; n++) {
                            if (existing_list[n].Contains(newlist[m]))
                                goto got_this_already;
                        }
                        lock (existing_list) {
                            existing_list.Add(newlist[m]);
                        }
                        got_this_already:;
                    }
                }
            });
        }

        public static string RemovePunctuation(string input) {
            StringBuilder result = new StringBuilder();
            foreach (char c in input) {
                if (char.IsLetterOrDigit(c))
                    result.Append(c);
            }

            return result.ToString();
        }

        public static string GetCapitalWords(string largeString, bool actually_lower_case = false) {
            if (string.IsNullOrEmpty(largeString)) {
                return string.Empty;
            }

            // Using a StringBuilder for efficient string concatenation.
            StringBuilder capitalWordsBuilder = new StringBuilder();

            // Regular expression to find sequences of one or more uppercase letters (A-Z).
            // \b ensures whole word matching, although for "capital words" it's less critical
            // than just matching uppercase sequences.
            // [A-Z]+ matches one or more uppercase letters.
            MatchCollection matches = Regex.Matches(largeString, actually_lower_case ? @"\b[a-z]+\b" : @"\b[A-Z][\w]*\b");

            foreach (Match match in matches) {
                // Append the matched capital word.
                capitalWordsBuilder.Append(match.Value);
                // Append a space after each word.
                capitalWordsBuilder.Append(" ");
            }

            // Remove the trailing space if any capital words were found.
            if (capitalWordsBuilder.Length > 0) {
                capitalWordsBuilder.Length--; // Remove the last space
            }

            return capitalWordsBuilder.ToString();
        }

        public static List<string> GetInfo(string from, string request, ConcurrentDictionary<string, List<StringPosition>> memory, int max_chars, int fill_level = 0, HashSet<string> words_processed = null) {
            List<string> results = new List<string>();
            request = from + " " + request;
            string[] word_split = request.Split(new char[] { ' ', '.', '?', '!', ':', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (words_processed == null) words_processed = new HashSet<string>();
            List<List<StringPosition>> possible_recalls = new List<List<StringPosition>>();
            int total_recalls = 0;
            for (int i = 0; i < word_split.Length; i++) {
                string word = GetUsefulKeyword(RemovePunctuation(word_split[i]));
                if (word == null) continue;
                if (words_processed.Contains(word)) continue;
                words_processed.Add(word);
                if (memory.TryGetValue(word, out var known)) {
                    possible_recalls.Add(known);
                    total_recalls += known.Count;
                }
            }
            // ok, lets recall an evenly distributed set of the latest memories
            int WordsPerMemory = Integration.MainForm.WordsPerRecall;
            int recalls_to_grab = Math.Min(total_recalls, max_chars / WordsPerMemory);
            int looking_at_list = 0;
            while (recalls_to_grab > 0) {
                List<StringPosition> recallfrom = possible_recalls[looking_at_list];
                int last_index = recallfrom.Count - 1;
                if (last_index >= 0) {
                    results.Add(recallfrom[last_index].Text);
                    recallfrom.RemoveAt(last_index);
                    recalls_to_grab--;
                }
                looking_at_list = (looking_at_list + 1) % possible_recalls.Count;
            }
            // now lets add in the most unique memories, up to our limit
            var most_unique = GetMostUniqueStrings(results);
            List<string> final_results = new List<string>();
            int k = 0;
            while (k < most_unique.Count && max_chars > 0) {
                string line = most_unique[k++];
                final_results.Add(line);
                max_chars -= line.Length;
            }
            // we might have context still to fill, so let's find more stuff
            if (Integration.MainForm.FillContext) {
                if (max_chars > WordsPerMemory * 10 && ChatManager.SelectedCharacter is Character c) {
                    switch (fill_level) {
                        case 0:
                            final_results.InsertRange(0, GetInfo(from, GetCapitalWords(c.PersistentDescription), memory, max_chars, 1, words_processed));
                            break;
                        case 1:
                            final_results.InsertRange(0, GetInfo(from, GetCapitalWords(c.PersistentDescription, true), memory, max_chars, 2, words_processed));
                            break;
                    }
                }
            }
            return final_results;
        }

        public static string GetUsefulKeyword(string word_in) {
            word_in = word_in.ToLower();
            for (int i = 0; i < Suffixes.Count; i++)
                word_in = word_in.Replace(Suffixes[i], "");
            if (word_in.Length < 3) return null;
            if (common_words.Contains(word_in)) return null;
            return word_in;
        }

        public class WordInfo {
            public string Word;
            public int Index;
        }

        public class StringPosition : IComparable<StringPosition> {
            public int Position;
            public string Text;

            public bool Contains(StringPosition sp) {
                return sp.Text.Contains(Text);
            }

            public override string ToString() {
                return Text;
            }

            public override bool Equals(object obj) {
                return obj is StringPosition sp && sp.Text.Equals(Text);
            }

            public override int GetHashCode() {
                return Text.GetHashCode();
            }

            public int CompareTo(StringPosition other) {
                return other.Position.CompareTo(other.Position);
            }
        }

        public static ConcurrentDictionary<string, List<StringPosition>> GenerateLongTerm(string lotsOfInfo) {
            ConcurrentDictionary<string, List<StringPosition>> data = new ConcurrentDictionary<string, List<StringPosition>>();
            var matches = Regex.Matches(lotsOfInfo, @"\b\w+\b(?!: )");
            int WordsPerMemory = Integration.MainForm.WordsPerRecall;
            Parallel.ForEach(matches.Cast<Match>(), (wordInfo) =>
            {
                string word_lower = GetUsefulKeyword(wordInfo.Value);
                int search_pos = wordInfo.Index;
                if (word_lower != null) {
                    string to_add = ReturnAround(lotsOfInfo, search_pos, WordsPerMemory);
                    StringPosition to_add_sp = new StringPosition() { Position = search_pos, Text = to_add };
                    List<StringPosition> info = new List<StringPosition>() { to_add_sp };
                    if (data.TryAdd(word_lower, info) == false) {
                        List<StringPosition> existing = data[word_lower];
                        lock (existing) {
                            bool already_got_it = false;
                            for (int m = 0; m < existing.Count; m++) {
                                if (existing[m].Contains(to_add_sp)) {
                                    already_got_it = true;
                                    break;
                                }
                            }
                            if (already_got_it == false) existing.Add(to_add_sp);
                        }
                    }
                }
            });
            // sort by order
            Parallel.ForEach(data.Values, (stringposs) => {
                stringposs.Sort();
            });
            return data;
        }

        public static string GetBase64(string largeString, int startPosition) {
            // 1. Validate inputs
            if (string.IsNullOrEmpty(largeString) || startPosition < 0 || startPosition >= largeString.Length) {
                return string.Empty;
            }

            int endPosition = startPosition;
            bool hasPaddingStarted = false;

            // 2. Iterate through the string from the start position
            while (endPosition < largeString.Length) {
                char currentChar = largeString[endPosition];

                if (hasPaddingStarted) {
                    // After padding starts, we only accept more '=' characters
                    if (currentChar == '=') {
                        endPosition++;
                    } else {
                        // Any other character ends the Base64 string
                        break;
                    }
                } else {
                    // Check for valid Base64 body characters
                    if ((currentChar >= 'A' && currentChar <= 'Z') ||
                        (currentChar >= 'a' && currentChar <= 'z') ||
                        (currentChar >= '0' && currentChar <= '9') ||
                        currentChar == '+' || currentChar == '/') {
                        endPosition++;
                    } else if (currentChar == '=') {
                        // The first padding character has been found
                        hasPaddingStarted = true;
                        endPosition++;
                    } else {
                        // An invalid character was found, so the Base64 string ends here
                        break;
                    }
                }
            }

            // 3. Extract and return the substring
            return largeString.Substring(startPosition, endPosition - startPosition);
        }

        public static string ReturnAround(string largeString, int position, int n) {
            // 1. Input Validation
            if (string.IsNullOrEmpty(largeString) || n <= 0) {
                return string.Empty;
            }
            position = Math.Max(0, Math.Min(position, largeString.Length - 1));

            // 2. Find the boundaries of the initial word at the given position.
            // If the position is whitespace, it finds the word to the right.
            int centerWordStart = FindWordStart(largeString, position);
            int centerWordEnd = FindWordEnd(largeString, centerWordStart);

            // If no word can be found at all (e.g., string is all whitespace)
            if (centerWordStart < 0) {
                return string.Empty;
            }

            // 3. Initialize collection and search pointers
            var words = new LinkedList<string>();
            words.AddLast(largeString.Substring(centerWordStart, centerWordEnd - centerWordStart));

            int leftSearchPos = centerWordStart - 1;
            int rightSearchPos = centerWordEnd;

            // 4. Loop, expanding outward, until N words are collected
            bool take_more_from_right = true;
            while (words.Count < n) {
                bool wordAdded = false;

                // Tie-breaker: Always try to get a word from the right first.
                if (TryGetNextWordRight(largeString, ref rightSearchPos, out string rightWord)) {
                    words.AddLast(rightWord);
                    wordAdded = true;
                }

                if (words.Count >= n) break;

                // focus on taking words from right
                if (take_more_from_right) {
                    take_more_from_right = false;
                    continue;
                }

                // Then, try to get a word from the left.
                if (TryGetNextWordLeft(largeString, ref leftSearchPos, out string leftWord)) {
                    words.AddFirst(leftWord);
                    wordAdded = true;
                }

                // If no words could be added from either side, we're done.
                if (!wordAdded) {
                    break;
                }

                take_more_from_right = true;
            }

            return string.Join(" ", words);
        }

        public static string ReplaceWholeWord(string all_text, string look_for, string replace_with) {
            // Use Regex for word boundary matching to avoid partial word replacements.
            return Regex.Replace(all_text, @"\b" + look_for + @"\b", replace_with, RegexOptions.IgnoreCase);
        }

        public static string ReplaceFirstWholeWord(string all_text, string look_for, string replace_with) {
            string pattern = @"\b" + Regex.Escape(look_for) + @"\b";
            Match match = Regex.Match(all_text, pattern, RegexOptions.IgnoreCase);

            if (match.Success) {
                int index = match.Index;
                return all_text.Substring(0, index) + replace_with + all_text.Substring(index + match.Length);
            }

            return all_text; // Return original string if no match is found
        }

        public static string TruncateStringByWordCount(string inputString, int wordLimit) {
            if (string.IsNullOrEmpty(inputString) || wordLimit <= 0) {
                return ""; // Handle empty input or invalid word limit
            }

            string[] words = inputString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= wordLimit) {
                return inputString; // String is already within the limit
            }

            List<string> truncatedWords = new List<string>();
            int wordCounter = 0;
            int openParenthesisCount = 0; // Counter for nested parentheses

            foreach (string word in words) {
                wordCounter++;
                bool addWord = openParenthesisCount > 0;

                // Check for parenthesis changes in the current word
                foreach (char c in word) {
                    if (c == '(') {
                        openParenthesisCount++;
                    } else if (c == ')') {
                        openParenthesisCount--;
                        if (openParenthesisCount < 0) openParenthesisCount = 0; // Safety to avoid negative count if unbalanced closing parenthesis.
                    }
                }

                addWord |= wordCounter <= wordLimit || openParenthesisCount > 0;

                if (addWord) truncatedWords.Add(word);
            }

            return string.Join(" ", truncatedWords);
        }

        // Finds the beginning of a word by searching backwards from a position.
        private static int FindWordStart(string text, int searchPos) {
            // If we start on whitespace, scan forward to the next word start
            if (char.IsWhiteSpace(text[searchPos])) {
                while (searchPos < text.Length && char.IsWhiteSpace(text[searchPos])) {
                    searchPos++;
                }
                if (searchPos == text.Length) return -1; // No word found
            }

            // Scan backwards to the beginning of the current word
            while (searchPos > 0 && !char.IsWhiteSpace(text[searchPos - 1])) {
                searchPos--;
            }
            return searchPos;
        }

        // Finds the end of a word by searching forwards from its start.
        private static int FindWordEnd(string text, int wordStartPos) {
            int searchPos = wordStartPos;
            while (searchPos < text.Length && !char.IsWhiteSpace(text[searchPos])) {
                searchPos++;
            }
            return searchPos;
        }

        // Scans right from a position to find the next complete word.
        private static bool TryGetNextWordRight(string text, ref int searchPos, out string word) {
            word = null;
            int wordStart = searchPos;

            // Skip whitespace to find the start of the next word
            while (wordStart < text.Length && char.IsWhiteSpace(text[wordStart])) {
                wordStart++;
            }

            if (wordStart >= text.Length) return false;

            // Find the end of that word
            int wordEnd = wordStart;
            while (wordEnd < text.Length && !char.IsWhiteSpace(text[wordEnd])) {
                wordEnd++;
            }

            word = text.Substring(wordStart, wordEnd - wordStart);
            searchPos = wordEnd; // Update search position for the next call
            return true;
        }

        // Scans left from a position to find the next complete word.
        private static bool TryGetNextWordLeft(string text, ref int searchPos, out string word) {
            word = null;
            int wordEnd = searchPos;

            // Skip whitespace to find the end of the next word
            while (wordEnd >= 0 && char.IsWhiteSpace(text[wordEnd])) {
                wordEnd--;
            }

            if (wordEnd < 0) return false;

            // Find the start of that word
            int wordStart = wordEnd;
            while (wordStart >= 0 && !char.IsWhiteSpace(text[wordStart])) {
                wordStart--;
            }

            word = text.Substring(wordStart + 1, wordEnd - wordStart);
            searchPos = wordStart; // Update search position for the next call
            return true;
        }
    }
}
