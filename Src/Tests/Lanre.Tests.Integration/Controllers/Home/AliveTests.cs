// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration.Controllers.Home
{
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;
    using static Lanre.Tests.Core.RoutesConstants;

    public class AliveTests : IntegrationTests
    {
        public AliveTests()
            : base(Home.Alive)
        {
        }

        [Fact]
        public async Task Call_To_Alive_Endpoint_And_Return_Ok()
        {
            await this.GetAsync<object>(
                expectedStatusCode: HttpStatusCode.OK,
                successStatusCode: true);
        }
    }
}
