// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Clients.Api.Controllers.V1
{
    using Lanre.Infrastructure.ControllersCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    public class HomeController : ControllerCore
    {
        [AllowAnonymous]
        [HttpGet("/alive")]
        public IActionResult Alive()
        {
            return this.Ok(new { });
        }
    }
}