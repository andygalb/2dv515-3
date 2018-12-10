using System;
using System.Collections.Generic;
using System.IO;

namespace SearchServer
{
    public class Loader
    {
        String[] wordsList;
        public Loader()
        {
          
        }

        public String[] GetWordsList()
        {
            return wordsList;
        }

        public List<Blog> LoadBlogs(String file)
        {
            List<Blog> blogs = new List<Blog>();

            using (var reader = new StreamReader(file))
            {
                string firstLine = reader.ReadLine();
                string[] words = firstLine.Split('\t');

                wordsList = new string[words.Length - 1];

                for (int i = 1; i < words.Length; i++)
                {
                    wordsList[i - 1] = words[i];
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\t');

                    Blog b = new Blog();
                    b.title = values[0];
                    for (int i = 1; i < values.Length; i++)
                    {
                        Word newWord = new Word();
                        newWord.word = words[i];
                        newWord.count = Int32.Parse(values[i]);
                        b.words.Add(newWord);
                    }
                    blogs.Add(b);
                }
            }
            return blogs;
        }

    }
}
