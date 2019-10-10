// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.ControllersCore
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    [Authorize]
    public abstract class ControllerCore : ControllerBase
    {
    }
}