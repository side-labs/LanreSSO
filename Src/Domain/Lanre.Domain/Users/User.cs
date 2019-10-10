// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Domain.Entities
{
    using System;
    using Lanre.Infrastructure.Entities;

    public class User : AggregateRoot<Guid>
    {
        public User(string name, string surname)
            : base(Guid.NewGuid())
        {
            this.Name = name;
            this.Surname = surname;
        }

        private User()
            : base()
        {
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public void Update(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }
    }
}