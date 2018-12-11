using SearchServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchServer
{
    public class SearchService : ISearchService
    {
        int noResults = 5;
        PageDB pageDB=new PageDB();
        Search s;

        String path = @"Wikipedia\Words\Games";
        String path2 = @"Wikipedia\Words\Programming";

        public SearchService()
        {
            Loader loader = new Loader();
            loader.ReadDirectory(path, pageDB);
            loader.ReadDirectory(path2, pageDB);
            s = new Search(pageDB);

        }

        public List<ShortenedScore> SearchFrequency(string query)
        {
            List<Score> result = s.QueryFrequency(query, pageDB);
            List<ShortenedScore> sScores = new List<ShortenedScore>();
            for (int i = 0; i < noResults; i++)
            {
                ShortenedScore ss = new ShortenedScore(result[i].p.url, result[i].contentScore, result[i].locationScore, result[i].pageRankScore, result[i].score);
                sScores.Add(ss);
            }

            return sScores;
        }

        public List<ShortenedScore> SearchFrequencyLocation(string query)
        {
            List<Score> result = s.QueryFrequencyLocation(query, pageDB);
            List<ShortenedScore> sScores = new List<ShortenedScore>();
            for (int i = 0; i < noResults; i++)
            {
                ShortenedScore ss = new ShortenedScore(result[i].p.url, result[i].contentScore, result[i].locationScore, result[i].pageRankScore, result[i].score);
                sScores.Add(ss);
            }

            return sScores;
        }


        public List<ShortenedScore> Search(string query)
        {
            
            List<Score> result = s.Query(query, pageDB);
            List<ShortenedScore> sScores = new List<ShortenedScore>();
            for(int i=0; i<noResults; i++)
            {
                ShortenedScore ss = new ShortenedScore(result[i].p.url, result[i].contentScore, result[i].locationScore, result[i].pageRankScore, result[i].score);
                sScores.Add(ss);
            }

            return sScores;
        }

       
    }
}
