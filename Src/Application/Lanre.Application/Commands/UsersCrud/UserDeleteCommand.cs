// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Commands.UsersCrud
{
    using System;
    using MediatR;

    public class UserDeleteCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
