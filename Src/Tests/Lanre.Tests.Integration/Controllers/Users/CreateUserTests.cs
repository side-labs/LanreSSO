// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration.Controllers.Users
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Lanre.Application.Commands.UsersCrud;
    using Lanre.Application.Queries.UserQueries;
    using Xunit;
    using static Lanre.Tests.Core.RoutesConstants;

    [Collection("UserEndpoint")]
    public class CreateUserTests : IntegrationTestsWithData
    {
        private Guid _idToRemove;

        public CreateUserTests()
            : base(Users.Base)
        {
        }

        [Fact]
        public async Task Call_To_Create_Endpoint_And_Return_Ok()
        {
            var data = new UserCreateCommand()
            {
                Name = "RandomName1",
                Surname = " RandomSurname1",
            };
            var response = await this.PostAsync<UserIdResponse, UserCreateCommand>(
                            data: data,
                            expectedStatusCode: HttpStatusCode.OK,
                            successStatusCode: true);
            this._idToRemove = response.Id;
        }


        [Fact]
        public async Task Call_To_Create_Endpoint_And_Call_To_Get_By_Id()
        {
            var data = new UserCreateCommand()
            {
                Name = "RandomName1",
                Surname = " RandomSurname1",
            };
            var createResponse = await this.PostAsync<UserIdResponse, UserCreateCommand>(
                            data: data,
                            expectedStatusCode: HttpStatusCode.OK,
                            successStatusCode: true);
            this._idToRemove = createResponse.Id;

            var getResponse = await this.GetAsync<UserQueryResponse>(
                                url: $"{Users.Base}/{this._idToRemove}",
                                expectedStatusCode: HttpStatusCode.OK,
                                successStatusCode: true);

            getResponse.Should().NotBeNull();
            getResponse.Id.Should().Be(this._idToRemove);
            getResponse.Name.Should().Be(data.Name);
            getResponse.Surname.Should().Be(data.Surname);
        }

        protected override void SeedData()
        {
        }

        protected override void AddToContext()
        {
        }

        protected override void RemoveToContext()
        {
            var userToRemove = this.Context.Users.FirstOrDefault(u => u.Id == this._idToRemove);
            if (userToRemove != null)
            {
                this.Context.Users.Remove(userToRemove);
                this.Context.SaveChanges();
            }
        }
    }
}
