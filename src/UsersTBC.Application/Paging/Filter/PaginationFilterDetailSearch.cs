using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Application.Models;

namespace UsersTBC.Application.Filter
{
    public class PaginationFilterDetailSearch
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SearchDetailModel Search { get; set; }
        public PaginationFilterDetailSearch()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilterDetailSearch(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }
        public PaginationFilterDetailSearch(PaginationFilterDetailSearch paginationFilterDetailSearch)
        {
            this.Search = paginationFilterDetailSearch.Search;
            this.PageNumber = paginationFilterDetailSearch.PageNumber < 1 ? 1 : paginationFilterDetailSearch.PageNumber;
            this.PageSize = paginationFilterDetailSearch.PageSize;
        }
        public PaginationFilterDetailSearch(SearchDetailModel Search, int pageNumber, int pageSize)
        {
            this.Search = Search;
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }

    }
}
