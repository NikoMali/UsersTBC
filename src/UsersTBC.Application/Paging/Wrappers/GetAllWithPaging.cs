using UsersTBC.Application.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsersTBC.Application.Helpers
{
    public class GetAllWithPaging<T,TE>
    {
        public TE PaginationFilter { get; set; } 
        public List<T> entities { get; set; }
        public int totalRecords { get; set; }
        public GetAllWithPaging(TE PaginationFilter, List<T> entities, int totalRecords)
        {
            this.PaginationFilter = PaginationFilter;
            this.entities = entities;
            this.totalRecords = totalRecords;
        }
    }
}
