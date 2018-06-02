using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Paging
    {
        public int CurrentPage { get; set; } = 1;
        public int ResultCount { get; set; }
        public int PageCount { get; set; }
    }
}
