// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Repositories
{
    using System;
    using Domain.Users;
    using Lanre.Data.Contexts.Lanre;
    using Lanre.Data.Repositories.Core;

    public class UserRepository : Repository<User, Guid>
    {
        public UserRepository(LanreContext context)
            : base(context)
        {
        }
    }
}