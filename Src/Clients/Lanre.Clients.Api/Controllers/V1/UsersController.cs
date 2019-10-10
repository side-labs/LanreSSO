// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Clients.Api.Controllers.V1
{
    using System;
    using System.Threading.Tasks;
    using Lanre.Application.Commands.UsersCrud;
    using Lanre.Application.Queries.UserQueries;
    using Lanre.Infrastructure.ControllersCore;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    public class UsersController : ControllerCore
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await this._mediator.Send(new UsersQuery());

            return this.Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            if (id == default)
            {
                return this.BadRequest();
            }

            var user = await this._mediator.Send(new UserQuery() { Id = id });

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetUsersPaginated([FromQuery]UsersQueryPaginated query)
        {
            var users = await this._mediator.Send(query);

            return this.Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand command)
        {
            var response = await this._mediator.Send(command);

            return this.Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateCommand command)
        {
            command.Id = id;
            await this._mediator.Send(command);

            return this.Ok(new { });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var command = new UserDeleteCommand()
            {
                Id = id,
            };

            await this._mediator.Send(command);

            return this.Ok(new { });
        }
    }
}