// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Queries.UserQueries
{
    using System;
    using MediatR;

    public class UserQuery : IRequest<UserQueryResponse>
    {
        public Guid Id { get; set; }
    }
}