using Microsoft.AspNetCore.WebUtilities;
using UsersTBC.Application.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Paging.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilterQuickSeach filter, string route);
        public Uri GetPageUri(PaginationFilterDetailSearch filter, string route);
    }

    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilterQuickSeach filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "SearchString", filter.SearchString.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetPageUri(PaginationFilterDetailSearch filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            //modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            if (!string.IsNullOrEmpty(filter.Search?.FirstName))
            {
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.Search)+"."+nameof(filter.Search.FirstName), filter.Search.FirstName.ToString());
            }
            if (!string.IsNullOrEmpty(filter.Search?.LastName))
            {
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.Search) + "." + nameof(filter.Search.LastName), filter.Search.LastName.ToString());
            }
            if (!string.IsNullOrEmpty(filter.Search?.PersonalNumber))
            {
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.Search) + "." + nameof(filter.Search.PersonalNumber), filter.Search.PersonalNumber.ToString());
            }
            if (!string.IsNullOrEmpty(filter.Search?.City?.Name))
            {
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.Search) + "." + nameof(filter.Search.City.Name), filter.Search.City?.Name.ToString());
            }
            if (!string.IsNullOrEmpty(filter.Search?.BirthDate.ToString()) && filter.Search.BirthDate != DateTime.MinValue && filter.Search.BirthDate != default(DateTime))
            {
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.Search) + "." + nameof(filter.Search.BirthDate), filter.Search.BirthDate.ToString());
            }
            if (filter.Search?.Gender != null)
            {
                if (Enum.IsDefined(typeof(Gender), filter.Search.Gender))
                {
                    modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.Search) + "." + nameof(filter.Search.Gender), filter.Search.Gender.ToString());
                }

            }
            return new Uri(modifiedUri);
        }
    }
}
