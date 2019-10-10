// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration.Controllers.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Lanre.Application.Queries.UserQueries;
    using Lanre.Domain.Entities;
    using Xunit;
    using static Lanre.Tests.Core.RoutesConstants;

    [Collection("UserEndpoint")]
    public class GetByIdUsersTests : IntegrationTestsWithData
    {
        private List<User> _users;

        public GetByIdUsersTests()
            : base(Users.Base)
        {
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Call_To_GetById_Endpoint_And_Return_Correct_User(int index)
        {
            var expected_user = this._users[index];
            var expected_id = expected_user.Id;
            var response = await this.GetAsync<UserQueryResponse>(
                            url: $"{Users.Base}/{expected_id}",
                            expectedStatusCode: HttpStatusCode.OK,
                            successStatusCode: true);

            response.Id.Should().Be(expected_id);
            response.Name.Should().Be(expected_user.Name);
            response.Surname.Should().Be(expected_user.Surname);
        }

        [Fact]
        public async Task Call_To_GetById_Endpoint_And_Return_NotFound()
        {
            var expected_id = Guid.NewGuid();
            await this.GetAsync<UserQueryResponse>(
                            url: $"{Users.Base}/{expected_id}",
                            expectedStatusCode: HttpStatusCode.NotFound,
                            successStatusCode: false);
        }

        [Fact]
        public async Task Call_To_GetById_Endpoint_And_Return_BadRequest()
        {
            var expected_id = Guid.Empty;
            await this.GetAsync<UserQueryResponse>(
                           url: $"{Users.Base}/{expected_id}",
                           expectedStatusCode: HttpStatusCode.BadRequest,
                           successStatusCode: false);
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
