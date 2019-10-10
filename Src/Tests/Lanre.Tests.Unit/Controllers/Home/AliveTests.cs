// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration.Controllers.Home
{
    using Lanre.Clients.Api.Controllers.V1;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class AliveTests
    {
        private readonly Mock<HttpContext> _contextMock;

        public AliveTests()
        {
            this._contextMock = new Mock<HttpContext>();
        }

        [Fact]
        public void Call_To_Alive_Function_And_Return_Ok()
        {
            var homeController = new HomeController();
            homeController.ControllerContext.HttpContext = this._contextMock.Object;
            var response = homeController.Alive();

            Assert.IsType<OkObjectResult>(response);
        }
    }
}
