using System;

namespace Viajes365RestApi.Wrappers
{
    public class PagedResponse<R>
    {
        public R ListElements { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public PagedResponse() { }

        public PagedResponse(R data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.ListElements = data;
            Succeeded = true;
            Message = string.Empty;
            ErrorCode = 0;
            Errors = null;
        }
    }
}
