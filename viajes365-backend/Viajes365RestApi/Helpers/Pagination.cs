using System;
using System.Collections.Generic;
using Viajes365RestApi.Filters;
using Viajes365RestApi.Services;
using Viajes365RestApi.Wrappers;

namespace Viajes365RestApi.Helpers
{
    public class Pagination
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedElements, PaginationFilter validFilter, int totalElements, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedElements, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalElements / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalElements = totalElements;
            return respose;
        }
    }
}
