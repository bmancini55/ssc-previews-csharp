using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Common
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> list, int page, int pagesize, int totalCount)
        {
            this.AddRange(list);
            this.PageNumber = page;
            this.PageSize = pagesize;
            this.TotalCount = totalCount;
        }

        public PagedList()
        {
            PageNumber = 1;
            PageSize = Int32.MaxValue;
            TotalCount = 0;
        }

        public PagedList(IEnumerable<T> list)
        {            
            this.AddRange(list);
            this.PageNumber = 1;
            this.PageSize = Int32.MaxValue;            
            this.TotalCount = list.Count();
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool IsFirstPage
        {
            get
            {
                return PageNumber == 1;
            }
        }

        public bool IsLastPage
        {
            get
            {
                return PageNumber == (int)Math.Ceiling((double)TotalCount / (double)PageSize);
            }
        }

    }
}
