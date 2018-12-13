using SearchServer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SearchServer.Controllers
{
    [Route("api/search")]
    public class SearchController : Controller
    {
        ISearchService searchService;

        public SearchController(ISearchService cs)
        {
            this.searchService = cs;
        }

        [HttpGet("{query}")]
        public ActionResult Search(string query)
        {
            string cleaned = CleanString(query);
            return Json(searchService.Search(cleaned));
        }

        [HttpGet("frequency/{query}")]
        public ActionResult SearchFrequency(string query)
        {
            string cleaned=CleanString(query);
            return Json(searchService.SearchFrequency(cleaned));
        }

        [HttpGet("frequencylocation/{query}")]
        public ActionResult SearchFrequencyAndLocation(string query)
        {
            string cleaned = CleanString(query);
            return Json(searchService.SearchFrequencyLocation(cleaned));
        }

        private string CleanString(string dirty)
        {
            string clean1=dirty.Replace("+", " ");
            string clean2 = dirty.Replace("%20", " ");
            string lowerCase = clean2.ToLower();
            return lowerCase;
        }

    }
}
