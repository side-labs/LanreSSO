// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Commands.UsersCrud
{
    using System;
    using CSharpFunctionalExtensions;
    using Lanre.Domain.Users;

    public class UserIdResponse
    {
        public Guid Id { get; set; }
        public Result<User> Result { get;set;}
    }
}
