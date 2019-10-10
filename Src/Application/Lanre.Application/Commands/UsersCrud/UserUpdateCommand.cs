// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Commands.UsersCrud
{
    using System;

    public class UserUpdateCommand : UserCreateCommand
    {
        public Guid Id { get; set; }
    }
}
