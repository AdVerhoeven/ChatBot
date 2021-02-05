using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ChatBot
{
    public class SimpleChatBot
    {
        public string Language { get; }
        public Dictionary<string, string> ResponseDictionary { get; }

        public SimpleChatBot(string lang, Dictionary<string, string> responseDict)
        {
            Language = lang;
            ResponseDictionary = responseDict;
        }
        public SimpleChatBot(string lang, string filePath)
        {
            Language = lang;
            ResponseDictionary = ResponseDictionaryFromFile(filePath);
        }

        /// <summary>
        /// Attempt to read a response dictionary from a file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private Dictionary<string, string> ResponseDictionaryFromFile(string filePath)
        {
            try
            {
                var sr = new StreamReader(filePath);
                var dict = new Dictionary<string, string>();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] kvp = line.Split(';');
                    if (kvp.Length != 2)
                    {
                        throw new InvalidDataException($"Expected 2 arguments, got {kvp.Length} arguments");
                    }
                    dict.Add(kvp[0].ToLower(), kvp[1]);
                }
                return dict;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RespondSimple(string input)
        {
            var key = TextOnly(input);
            try
            {
                return ResponseDictionary[key];
            }
            catch (KeyNotFoundException e)
            {
                return "I do not know how to respond to this.";
            }
        }

        public string TextOnly(string text)
        {
            text = text.ToLower().Trim();
            var regex = new Regex(@"^([a-z]+\s)*$");
            if (regex.IsMatch(text))
            {
                return text;
            }
            else
            {
                var matches = regex.Matches(text);
                StringBuilder sb = new StringBuilder();
                foreach (Match match in matches)
                {
                    sb.Append(match.Value);
                }
                return sb.ToString();
            }
        }
    }
}
