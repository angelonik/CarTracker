using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class PagedResult<T>
    {
        public Paging Paging { get; set; } = new Paging();
        public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();
    }
}
