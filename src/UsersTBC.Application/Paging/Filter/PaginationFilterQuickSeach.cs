using System;
using System.Collections.Generic;
using System.Text;

namespace UsersTBC.Application.Filter
{
    public class PaginationFilterQuickSeach
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public PaginationFilterQuickSeach()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilterQuickSeach(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }
        public PaginationFilterQuickSeach(string searchString, int pageNumber, int pageSize)
        {
            this.SearchString = searchString;
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }
    }
}
