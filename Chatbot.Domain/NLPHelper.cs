using System;
using System.Collections.Generic;
using System.Text;
using Annytab.Stemmer;


namespace Chatbot.Domain
{
    public static class NLPHelper
    {
        public static int[] BagOfWords(string sentence, string[] words)
        {
            var bag = new int[words.Length];

            var s_words = Tokenize(sentence);
            s_words = Stemmerize(s_words);
            foreach (var se in s_words)
            {
                var i = 0;
                foreach (var wrd in words)
                {
                    var tempwrd = wrd.Trim('"');
                    i++;
                    if (se == tempwrd)
                    {
                        bag[i] = 1;
                    }
                }
            }
            return bag;
        }
        public static string[] Tokenize(string sentence)
        {
            char[] delims = new char[] { ' ', ',', '.', ':', '!', '?' };
            return sentence.Split(delims, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] Stemmerize(string[] words)
        {
            var stemmer = new EnglishStemmer();
            return stemmer.GetSteamWords(words);
        }
    }
}
