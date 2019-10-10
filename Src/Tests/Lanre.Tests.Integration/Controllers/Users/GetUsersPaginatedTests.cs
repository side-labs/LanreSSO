// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration.Controllers.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Lanre.Application.Queries.UserQueries;
    using Lanre.Domain.Entities;
    using Lanre.Infrastructure.Entities;
    using Xunit;
    using static Lanre.Tests.Core.RoutesConstants;

    [Collection("UserEndpoint")]
    public class GetUsersPaginatedTests : IntegrationTestsWithData
    {
        private List<User> _users;

        public GetUsersPaginatedTests()
            : base(Users.Paginated)
        {
        }

        [Fact]
        public async Task Call_To_GetPaginated_Endpoint_And_Return_Ok()
        {
            await this.GetAsync<object>(
                expectedStatusCode: HttpStatusCode.OK,
                successStatusCode: true);
        }

        [Fact]
        public async Task Call_To_GetPaginated_Endpoint_Without_Parameteters_And_Return_5_Elments()
        {
            const int expected_count = 5;
            var response = await this.GetAsync<PaginatedResult<UserQueryResponse>>(
                                expectedStatusCode: HttpStatusCode.OK,
                                successStatusCode: true);

            response.Entities.Should().HaveCount(expected_count);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(1, 4, 1)]
        [InlineData(1, 5, 1)]
        [InlineData(1, 6, 0)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(2, 3, 1)]
        [InlineData(2, 4, 0)]
        [InlineData(5, 1, 5)]
        [InlineData(5, 2, 0)]
        public async Task Call_To_GetPaginated_Endpoint_With_Inline_PageSize_And_PageNumber_And_Return_N_Elments(int pageSize, int pageNumber, int expectedCount)
        {
            var url = $"{Users.Paginated}?pageSize={pageSize}&pageNumber={pageNumber}";

            var response = await this.GetAsync<PaginatedResult<UserQueryResponse>>(
                                url: url,
                                expectedStatusCode: HttpStatusCode.OK,
                                successStatusCode: true);

            response.Entities.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData("name", true, "Name1", "Name5")]
        [InlineData("name", false, "Name5", "Name1")]
        [InlineData("nAmE", true, "Name1", "Name5")]
        [InlineData("Surname", true, "Name1", "Name5")]
        public async Task Call_To_GetPaginated_Endpoint_With_Inline_Order_And_Return_Ordered_Users(string orderBy, bool orderIsAscending, string firstName, string lastName)
        {
            var pageSize = 5;
            var pageNumber = 1;
            var expectedCount = 5;
            var url = $"{Users.Paginated}?pageSize={pageSize}&pageNumber={pageNumber}&orderBy={orderBy}&orderIsAsc={orderIsAscending}";

            var response = await this.GetAsync<PaginatedResult<UserQueryResponse>>(
                                url: url,
                                expectedStatusCode: HttpStatusCode.OK,
                                successStatusCode: true);

            response.Entities.Should().HaveCount(expectedCount);
            response.Entities.First().Name.Should().Be(firstName);
            response.Entities.Last().Name.Should().Be(lastName);
        }

        protected override void SeedData()
        {
            this._users = new List<User>()
            {
                new User("Name1", "Surname1"),
                new User("Name2", "Surname2"),
                new User("Name3", "Surname3"),
                new User("Name4", "Surname4"),
                new User("Name5", "Surname5"),
            };
        }

        protected override void AddToContext()
        {
            this.Context.Users.AddRange(this._users);
            this.Context.SaveChanges();
        }

        protected override void RemoveToContext()
        {
            var ids = this._users.Select(u => u.Id).ToList();
            var usersToRemove = this.Context.Users.Where(u => ids.Contains(u.Id));
            if (usersToRemove.Any())
            {
                this.Context.Users.RemoveRange(usersToRemove);
                this.Context.SaveChanges();
            }
        }
    }
}
