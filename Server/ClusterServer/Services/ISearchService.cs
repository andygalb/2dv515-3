using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchServer.Services
{
   public interface ISearchService
    {
        List<ShortenedScore> Search(string query);
        List<ShortenedScore> SearchFrequency(string query);
        List<ShortenedScore> SearchFrequencyLocation(string query);

    }
}
