using System;
using System.Collections.Generic;
using System.IO;

namespace SearchEngine
{
    class Program
    {

        static PageDB pageDB = new PageDB();
        static String path = @"Wikipedia\Words\Games";
        static String linksPath = @"Wikipedia\Links\Games";
        static String path2 = @"Wikipedia\Words\Programming";
        static String linksPath2 = @"Wikipedia\Links\Programming";

        static void Main(string[] args)
        {
            Loader.ReadDirectory(path, linksPath,pageDB);
            Loader.ReadDirectory(path2, linksPath2, pageDB);

            Scorer.PageRank(pageDB);

            List<Score> results = Query("nintendo");

            for (int i = 0; i < 5; i++){
                var strings = results[i].p.url.Split("/");
                Console.WriteLine(strings[strings.Length-1]+ "\t{0:N2}"+ "\t{1:N2}" + "\t{2:N2}"+ "\t{3:N2}", results[i].score, results[i].contentScore, results[i].locationScore, results[i].pageRankScore);
            }

        }


        static List<Score> Query(String query)
        {
            List<Score> result = new List<Score>();
            Scores scores = new Scores();

            //Calculate score for each page in the pages database
            foreach (Page p in pageDB.pages)
            {
                scores.content.Add(Scorer.GetFrequencyScore(p, query));
                scores.location.Add(Scorer.GetLocationScore(p, query));
                scores.pageRank.Add(p.pageRank);
            }

            //Normalize scores
            Normalize(scores.content, false);
            Normalize(scores.location, true);
            Normalize(scores.pageRank, false);
            //  Normalize(scores.location, true);
            Weigh(scores.location, 0.8);
            Weigh(scores.pageRank, 0.5);
            //Generate result list
            for (int i = 0; i < pageDB.pages.Count; i++)
            {
                Page p = pageDB.pages[i];
                Double score = scores.content[i] + scores.location[i]+ scores.pageRank[i];
                result.Add(new Score(p, scores.content[i],scores.location[i],scores.pageRank[i],score));
            }
            //Sort result list with highest score first
            Sort(result);
            //Return result list
           return result;
        }

        public static void Weigh(List<double> l, double factor)
        {
            for(int i=0; i<l.Count; i++)
            {
                l[i] = l[i] * factor;
            }
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
                    
                    if (scores[i]==0) scores[i]=0.0001;
                    scores[i] = min / scores[i];
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
    }

  
}



