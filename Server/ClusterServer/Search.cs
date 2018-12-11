using System;
using System.Collections.Generic;
using System.Text;

namespace SearchServer
{
    
    class Search
    {

        PageDB pageDB = new PageDB();
      
        Scorer scorer = new Scorer();
        Loader loader = new Loader();

        public Search(PageDB pageDB)
        {
          //Configure Page Rank
            scorer.PageRank(pageDB);
        }

        public List<Score> Query(String query, PageDB pageDB)
        {
            List<Score> result = new List<Score>();
            Scores scores = new Scores();

            //Calculate score for each page in the pages database
            foreach (Page p in pageDB.pages)
            {
                scores.content.Add(scorer.GetFrequencyScore(p, query));
                scores.location.Add(scorer.GetLocationScore(p, query));
                scores.pageRank.Add(p.pageRank);
            }

            //Normalize scores
            Normalize(scores.content, false);
            Normalize(scores.location, true);
            Normalize(scores.pageRank, false);
         
            //Weight Scores - content weighs 1
            Weigh(scores.location, 0.8);
            Weigh(scores.pageRank, 0.5);

            //Generate result list
            for (int i = 0; i < pageDB.pages.Count; i++)
            {
                Page p = pageDB.pages[i];
                Double score = scores.content[i] + scores.location[i] + scores.pageRank[i];
                result.Add(new Score(p, scores.content[i], scores.location[i], scores.pageRank[i], score));
            }
            //Sort result list with highest score first
            Sort(result);
            //Return result list
            return result;
        }

        public List<Score> QueryFrequency(String query, PageDB pageDB)
        {
            List<Score> result = new List<Score>();
            Scores scores = new Scores();

            //Calculate score for each page in the pages database
            foreach (Page p in pageDB.pages)
            {
                scores.content.Add(scorer.GetFrequencyScore(p, query));
            }

            //Normalize scores
            Normalize(scores.content, false);

            //Generate result list
            for (int i = 0; i < pageDB.pages.Count; i++)
            {
                Page p = pageDB.pages[i];
                Double score = scores.content[i];
                result.Add(new Score(p, scores.content[i], 0, 0, score));
            }
            //Sort result list with highest score first
            Sort(result);
            //Return result list
            return result;
        }

        public List<Score> QueryFrequencyLocation(String query, PageDB pageDB)
        {
            List<Score> result = new List<Score>();
            Scores scores = new Scores();

            //Calculate score for each page in the pages database
            foreach (Page p in pageDB.pages)
            {
                scores.content.Add(scorer.GetFrequencyScore(p, query));
                scores.location.Add(scorer.GetLocationScore(p, query));
            }

            //Normalize scores
            Normalize(scores.content, false);
            Normalize(scores.location, true);

            //Weight Scores - content weighs 1
            Weigh(scores.location, 0.8);

            //Generate result list
            for (int i = 0; i < pageDB.pages.Count; i++)
            {
                Page p = pageDB.pages[i];
                Double score = scores.content[i] + scores.location[i];
                result.Add(new Score(p, scores.content[i], scores.location[i], 0, score));
            }
            //Sort result list with highest score first
            Sort(result);
            //Return result list
            return result;
        }
        public void Weigh(List<double> l, double factor)
        {
            for (int i = 0; i < l.Count; i++)
            {
                l[i] = l[i] * factor;
            }
        }



        void Normalize(List<Double> scores, bool smallIsBetter)
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

                    if (scores[i] == 0) scores[i] = 0.0001;
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

        double Max(List<double> list)
        {
            double max = Double.MinValue;
            foreach (double d in list)
            {
                if (d > max) { max = d; }
            }
            return max;
        }

        double Min(List<double> list)
        {
            double min = Double.MaxValue;
            foreach (double d in list)
            {
                if (d < min) { min = d; }
            }
            return min;
        }

        void Sort(List<Score> result)
        {
            result.Sort((x, y) => y.score.CompareTo(x.score));
        }

    }
}
