using System;
namespace SearchServer
{
    public class Scorer
    {

        static int MAX_ITERATIONS = 10;
       
        public double GetFrequencyScore(Page page, String query)
        {
           
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


        public double GetLocationScore(Page page, String query)
        {
           
            double score = 0;
            var queries = query.Split(" ");
            foreach (String s in queries)
            {
                int hash = s.GetHashCode();
                for (int j=0; j<page.words.Count; j++)
                {
                    if (page.words[j] == hash)
                    {
                        score += j+1;
                        break;
                    }
                    
                }
                if (score == 0) { score += 100000; }
            }
            return score;
        }

        public void PageRank(PageDB pageDB)
        {
            //Iterate over all pages for a number of iterations

            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                foreach (Page p in pageDB.pages)
                {
                    iteratePR(p, pageDB);
                }
            }
        }
        //Calculate page rank value for a page

        public void iteratePR(Page p, PageDB pageDB)
        {

            //Calculate page rank value
            double pr = 0;
            foreach (Page po in pageDB.pages)
            {
                
                    if (po.HasLinkTo(p))
                    {
                        //Sum of all pages
                        pr += (po.pageRank / po.links.Count);
                    }
                
                //Calculate PR
               
            }
            p.pageRank = 0.85 * pr + 0.15;
        }

    }
}
