using SearchServer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SearchServer.Controllers
{
    [Route("api/cluster")]
    public class SearchController : Controller
    {
        ISearchService searchService;

        public SearchController(ISearchService cs)
        {
            this.searchService = cs;
        }


        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Json(searchService.GetBlogs());
        }

        [HttpGet("blogs")]
        public ActionResult GetBlogs()
        {
            return Json(searchService.GetBlogs());
        }
    
    }
}
