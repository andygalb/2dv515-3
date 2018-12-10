using System;
namespace SearchEngine
{
    public class Scorer
    {

        static int MAX_ITERATIONS = 10;
       
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


        public static double GetLocationScore(Page page, String query)
        {
            //  1.Declare a score variable and set it to 0
            //2.Split the search query into a list of words
            //3.For each word in the query:
            //1.Convert the word string to an id integer
            //2.For each Page:
            //1.For each Word in the page:
            //1.Increase the score by the current index if the current word id matches the query word id
            //2.Increase the score by a high value(100000) if the word was not found
            //4.Return the score
            double score = 0;
            var queries = query.Split(" ");
            foreach (String s in queries)
            {
                int hash = s.GetHashCode();
                foreach (int i in page.words)
                {
                    score += i;
                }
                if (score == 0) { score = int.MaxValue; }
            }
            return score;
        }

        public static void PageRank(PageDB pageDB)
        {
            //Iterate over all pages for a number of iterations

            for (int i = 0; i < MAX_ITERATIONS; i++)
                foreach (Page p in pageDB.pages)
            {
                iteratePR(p, pageDB);
            }
        }
        //Calculate page rank value for a page

        public static void iteratePR(Page p, PageDB pageDB)
        {

            //Calculate page rank value
            double pr = 0;
            foreach (Page po in pageDB.pages)
            {
                if (po.HasLinkTo(p))
                {
                    //Sum of all pages
                    pr += po.pageRank / po.links.Count;
                }

                //Calculate PR
                p.pageRank = 0.85 * pr + 0.15;
            }

        }

    }
}
