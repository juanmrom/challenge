using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.DAL.Dto
{
    public class PagedRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }  
}
