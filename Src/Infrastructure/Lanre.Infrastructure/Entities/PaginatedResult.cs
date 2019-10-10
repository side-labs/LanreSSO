using System;
using System.Collections.Generic;
using System.Linq;

namespace Lanre.Infrastructure.Entities
{
    public class PaginatedResult<TResponse> : PaginatedRequest
    where TResponse : class
    {
        public int MyProperty { get; set; }
        public int TotalItems { get; set; }

        public IEnumerable<TResponse> Entities { get; set; }

        public static PaginatedResult<TResponse> MapFromRequest(PaginatedRequest request)
        {
            return GenerateResponseTableFromRequest(request.PageNumber, request.PageSize, request.OrderBy, request.OrderIsAsc);
        }

        public static PaginatedResult<TResponse> MapFromRequest<TRequesTResponse>(PaginatedRequest<TRequesTResponse> request)
            where TRequesTResponse : class
        {
            return GenerateResponseTableFromRequest(request.PageNumber, request.PageSize, request.OrderBy, request.OrderIsAsc);
        }


        public static PaginatedResult<TResponse> MapFromRequestMappingEntities<TEntityOrigin>(
                PaginatedRequest request,
                IEnumerable<TEntityOrigin> entities,
                Func<TEntityOrigin, TResponse> entitiesMapper,
                int totalItems)
            where TEntityOrigin : class
        {
            return new PaginatedResult<TResponse>()
            {
                Entities = entities.Select(entitiesMapper),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                OrderBy = request.OrderBy,
                OrderIsAsc = request.OrderIsAsc,
                TotalItems = totalItems
            };
        }


        private static PaginatedResult<TResponse> GenerateResponseTableFromRequest(int pageNumber, int pageSize, string orderBy, bool orderIsAsc)
        {
            return new PaginatedResult<TResponse>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OrderBy = orderBy,
                OrderIsAsc = orderIsAsc,
            };
        }
    }
}