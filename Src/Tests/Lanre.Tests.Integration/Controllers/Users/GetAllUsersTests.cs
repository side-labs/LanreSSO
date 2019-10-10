﻿// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration.Controllers.Users
{
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
    public class GetAllUsersTests : IntegrationTestsWithData
    {
        private List<User> _users;

        public GetAllUsersTests()
            : base(Users.Base)
        {
        }

        [Fact]
        public async Task Call_To_GetAll_Endpoint_And_Return_Ok()
        {
            await this.GetAsync<object>(
                expectedStatusCode: HttpStatusCode.OK,
                successStatusCode: true);
        }

        [Fact]
        public async Task Call_To_GetAll_Endpoint_And_Return_5_Elments()
        {
            const int expected_count = 5;
            var response = await this.GetAsync<IEnumerable<UserQueryResponse>>(
                                expectedStatusCode: HttpStatusCode.OK,
                                successStatusCode: true);

            response.Should().HaveCount(expected_count);
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
