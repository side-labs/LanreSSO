// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Queries.UserQueries
{
    using Lanre.Infrastructure.Entities;
    using MediatR;

    public class UsersQueryPaginated : PaginatedRequest<PaginatedResult<UserQueryResponse>>, IRequest<PaginatedResult<UserQueryResponse>>
    {
    }
}