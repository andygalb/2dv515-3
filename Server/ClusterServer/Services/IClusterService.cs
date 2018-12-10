using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchServer.Services
{
   public interface ISearchService
    {
        List<Blog> GetBlogs();
      
    }
}
