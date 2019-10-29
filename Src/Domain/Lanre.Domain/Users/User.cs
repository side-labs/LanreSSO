// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Domain.Users
{
    using System;
    using CSharpFunctionalExtensions;
    using Lanre.Infrastructure.Entities;

    public class User : AggregateRoot<Guid>
    {
        private User()
            : base()
        {
        }

        public string Name { get; private  set; }

        public string Surname { get; private set; }

        public static Result<User> Create(string name, string surname)
        {
            var userToBeCreated = new User();
            var constraints = Result.Combine("\n",userToBeCreated.SetName(name), userToBeCreated.SetSurname(surname));
            if (constraints.IsSuccess)
            {
                return Result.Ok<User>(userToBeCreated);
            }

            return Result.Failure<User>(constraints.Error);
        }

        public Result<User> SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<User>("Name could not be Empty");
            }

            this.Name = name;
            return Result.Ok<User>(this);
        }

        public Result<User> SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                return Result.Failure<User>("Surname could not be Empty");
            }

            this.Surname = surname;
            return Result.Ok<User>(this);
        }
    }
}