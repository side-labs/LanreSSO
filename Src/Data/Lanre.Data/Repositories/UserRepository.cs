// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Repositories
{
    using System;
    using Lanre.Data.Contexts.Lanre;
    using Lanre.Data.Repositories.Core;
    using Lanre.Domain.Entities;

    public class UserRepository : Repository<User, Guid>
    {
        public UserRepository(LanreContext context)
            : base(context)
        {
        }
    }
}