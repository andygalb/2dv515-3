using System;
using System.Collections.Generic;
using System.IO;

namespace SearchServer
{
    public class Loader
    {
        String path = @"Wikipedia\Words\Games";
        String linksPath = @"Wikipedia\Links\Games";
        String path2 = @"Wikipedia\Words\Programming";
        String linksPath2 = @"Wikipedia\Links\Programming";

        public void ReadDirectory(String wordsPath, PageDB pageDB)
        {

            if (Directory.Exists(wordsPath))
            {
                string[] fileEntries = Directory.GetFiles(wordsPath);
                foreach (string fileName in fileEntries)
                {
                    Page page = new Page();
                    page.url = fileName;
                    page.words = ProcessWords(fileName, pageDB);
                    string newString=fileName.Replace("Words", "Links");
                    page.links = ProcessLinks(newString, pageDB);
                    pageDB.pages.Add(page);
                }

            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", wordsPath);
            }

        }


        public List<int> ProcessWords(string fileName, PageDB pageDB)
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

        public  List<String> ProcessLinks(string fileName, PageDB pageDB)
        {
            List<String> linkList = new List<String>();
            if (!File.Exists(fileName)){ return new List<string>(); }
            using (var reader = new StreamReader(fileName))
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                        linkList.Add(line);
                    
                }

            return linkList;
        }

    }
}
