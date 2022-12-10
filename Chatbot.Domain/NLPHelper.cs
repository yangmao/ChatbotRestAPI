using System;
using System.Collections.Generic;
using System.Text;
using Annytab.Stemmer;


namespace Chatbot.Domain
{
    public static class NLPHelper
    {
        public static string[] BagOfWords(string sentence, string[] words)
        {
            var bag = new string[words.Length];
            char[] delims = new char[] { ' ', ',', '.', ':','!','?' };
            string[] s_words = sentence.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            var stemmer =  new EnglishStemmer();
            s_words = stemmer.GetSteamWords(s_words);
            foreach (var se in s_words)
            {
                var i = 0;
                foreach (var wrd in words)
                {
                    i++;
                    if (se == wrd)
                    {
                        bag[i] = se;
                    }
                }
            }
            return bag;
        }
    }
}
