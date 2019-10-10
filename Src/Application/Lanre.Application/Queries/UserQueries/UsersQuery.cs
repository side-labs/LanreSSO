// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Queries.UserQueries
{
    using System.Collections.Generic;
    using MediatR;

    public class UsersQuery : IRequest<IEnumerable<UserQueryResponse>>
    {
    }
}