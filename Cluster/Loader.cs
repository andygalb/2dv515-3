using System;
using System.Collections.Generic;
using System.IO;

namespace SearchEngine
{
    public class Loader
    {

        public static void ReadDirectory(String path, PageDB pageDB)
        {

            if (Directory.Exists(path))
            {
                string[] fileEntries = Directory.GetFiles(path);
                foreach (string fileName in fileEntries)
                {
                    Page page = new Page();
                    page.url = fileName;
                    page.words = ProcessWords(fileName, pageDB);
                    pageDB.pages.Add(page);
                }

            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }

        }


        public static List<int> ProcessWords(string fileName, PageDB pageDB)
        {
            List<int> wordList = new List<int>();

            using (var reader = new StreamReader(fileName))
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var words = line.Split(' ');

                    foreach (String word in words)
                    {
                        int hashCode = word.GetHashCode();

                        if (!pageDB.wordToId.ContainsKey(word)) { pageDB.wordToId.Add(word, hashCode); }
                        wordList.Add(hashCode);
                    }
                }
            return wordList;
        }

    }
}
