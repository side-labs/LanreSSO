namespace Lanre.Infrastructure.Entities
{
    public class PaginatedRequest
    {
        public const int PageNumber_Default = 1;

        public const int PageSize_Default = 10;

        public const string OrderBy_Default = "Id";

        public const bool OrderIsAsc_Default = true;

        private int pageNumber = PageNumber_Default;

        public int PageNumber
        {
            get
            {
                return pageNumber <= 0 ? PageNumber_Default : pageNumber;
            }
            set
            {
                pageNumber = value;
            }
        }

        private int pageSize = PageSize_Default;

        public int PageSize
        {
            get
            {
                return pageSize <= 0 ? PageSize_Default : pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        public string OrderBy { get; set; } = OrderBy_Default;

        public bool OrderIsAsc { get; set; } = OrderIsAsc_Default;
    }

    public class PaginatedRequest<TRequestDto> : PaginatedRequest
        where TRequestDto : class
    {
        public TRequestDto AdvancedSearch { get; set; }

        public PaginatedRequest GetRequestTable()
        {
            return this;
        }
    }

}