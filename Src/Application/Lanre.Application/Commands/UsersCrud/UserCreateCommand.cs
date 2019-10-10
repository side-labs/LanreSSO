// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Commands.UsersCrud
{
    using MediatR;

    public class UserCreateCommand : IRequest<UserIdResponse>
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
