// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Contexts.Lanre
{
    using global::Lanre.Domain.Entities;
    using global::Lanre.Infrastructure.Data;
    using global::Lanre.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;

    public class LanreContext : ContextCore
    {
        public LanreContext(DbContextOptions<LanreContext> options, AppSettings settings)
            : base(options, settings)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}