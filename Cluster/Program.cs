using System;
using System.Collections.Generic;
using System.IO;

namespace SearchEngine
{
    class Program
    {

        static PageDB pageDB = new PageDB();
        static String path = "Wikipedia/Words/Games";
        static String path2 = "Wikipedia/Words/Programming";

        static void Main(string[] args)
        {
            ReadDirectory(path);
            ReadDirectory(path2);

            List<Score> results = Query("nintendo");

            for (int i = 0; i < 5; i++){
                Console.WriteLine(results[i].p.url+" "+results[i].score);   
            }

        }


        static List<Score> Query(String query)
        {
            List<Score> result = new List<Score>();
            Scores scores = new Scores();

            //Calculate score for each page in the pages database
            for (int i = 0; i < pageDB.pages.Count; i++)
            {
                Page p = pageDB.pages[i];
                scores.content.Add(GetFrequencyScore(p, query));
              //  scores.location.Add(Scorer.GetLocationScore(p, query));
            }

            //Normalize scores
            Normalize(scores.content, false);
          //  Normalize(scores.location, true);

            //Generate result list
            for (int i = 0; i < pageDB.pages.Count; i++)
            {
                Page p = pageDB.pages[i];
                Double score = 1.0 * scores.content[i]; //+ 0.5 * scores.location[i];
                result.Add(new Score(p, score));
            }
            //Sort result list with highest score first
            Sort(result);
            //Return result list
           return result;
        }

        public static double GetFrequencyScore(Page page, String query)
        {
            //          1. Declare a score variable and set it to 0
            //2. Split the search query into a list of words
            //3. For each word in the query:
            //1. Convert the word string to an id integer
            //2. For each Page:
            //1. For each Word in the page:
            //1. Increase score by 1 if the current word id matches the query word id
            //4. Return the score 
            double score = 0;
            var words = query.Split(" ");
            foreach (String s in words)
            {
                int id = s.GetHashCode();
                foreach (int i in page.words)
                {
                    if (id == i) { score++; }
                }
            }
            return score;
        }

        static void Normalize(List<Double> scores, bool smallIsBetter)
        {
            if (smallIsBetter)
            {
                //Smaller values shall be inverted to higher values
                //and scaled between 0 and 1
                //Find min value in the array
                double min = Min(scores);
                //Divide the min value by the score
                //(and avoid division by zero)
                for (int i = 0; i < scores.Count; i++)
                {
                //    scores[i] = min / Max(scores[i], 0.00001);
                }
                    
            }
              
            else
            {
                //Higher values shall be scaled between 0 and 1
                //Find max value in the array
                double max = Max(scores);

                //When we have a max value, divide all scores by it
                for (int i = 0; i < scores.Count; i++)
                {
                    scores[i] = scores[i] / max;
                } 
            }
           

        }

        static double Max(List<double> list){
            double max = Double.MinValue;
            foreach(double d in list){
                if (d > max) { max = d; }
            }
            return max;
        }

        static double Min(List<double> list)
        {
            double min = Double.MaxValue;
            foreach (double d in list)
            {
                if (d < min) { min = d; }
            }
            return min;
        }

        static void Sort(List<Score> result){
            result.Sort((x, y) => y.score.CompareTo(x.score));
        }


        private static void ReadDirectory(String path)
        {

            if(Directory.Exists(path)) 
            {
                string[] fileEntries = Directory.GetFiles(path);
                foreach (string fileName in fileEntries)
                {
                    Page page = new Page();
                    page.url = fileName;
                    page.words = ProcessWords(fileName);
                    pageDB.pages.Add(page);
                }
                   
            }
            else 
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }  

        }

 
        private static List<int> ProcessWords(string fileName)
        {
            List<int> wordList= new List<int>();

            using (var reader = new StreamReader(fileName))
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var words = line.Split(' ');

                    foreach(String word in words)
                    {
                        int hashCode = word.GetHashCode();

                        if (!pageDB.wordToId.ContainsKey(word)) { pageDB.wordToId.Add(word, hashCode); }
                        wordList.Add(hashCode);
                    }
                }
            return wordList;
        }

           


      

    }

    public class Score
    {

        public Score(Page page, Double score){
            this.p = page;
            this.score = score;
        }

        public Page p;
        public Double score;
    }

    public class Scores
    {
        public List<double> content= new List<double>();
        public List<double> location = new List<double>();
    }

    public class PageDB
    {
        public Dictionary<String, int> wordToId = new Dictionary<string, int>();
        public List<Page> pages = new List<Page>();
    }

    public class Page
    {
        public String url;
        public List<int> words; 
    }
}



