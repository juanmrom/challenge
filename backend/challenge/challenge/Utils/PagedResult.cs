using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Utils
{
    public sealed class PagedResult<T>
    {
        public PagedResult()
        {
            Results = new List<T>();
        }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
