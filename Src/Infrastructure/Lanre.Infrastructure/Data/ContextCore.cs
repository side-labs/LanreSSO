// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Lanre.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;

    public abstract class ContextCore : DbContext
    {
        protected ContextCore(DbContextOptions options, AppSettings settings)
            : base(options)
        {
            this.Settings = settings;
        }

        public AppSettings Settings { get; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChanges();
        }

        protected void UpdateTimestampOfEntities()
        {
            // var entries = this.ChangeTracker.Entries().Where(p =>
            //                                                 p.State == EntityState.Modified
            //                                                 && p.Entity.GetType().Assembly.DefinedTypes.Any(x => typeof(ITimestamp).IsAssignableFrom(x)));
            // foreach (var entry in entries)
            // {
            //     if (entry.Metadata.FindProperty("FechaUltimoAcceso") != null)
            //     {
            //         entry.Property("FechaUltimoAcceso").CurrentValue = DateTimeExtensions.Now;
            //     }

            //     if (entry.Metadata.FindProperty("LastAccess") != null)
            //     {
            //         entry.Property("LastAccess").CurrentValue = DateTimeExtensions.Now;
            //     }
            // }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var mappings = this.GetType().Assembly.DefinedTypes.Where(t =>
                typeof(IEntityMappingConfiguration).IsAssignableFrom(t));

            foreach (var type in mappings.Where(m => !m.IsAbstract && !m.IsInterface))
            {
                var builder = (IEntityMappingConfiguration)Activator.CreateInstance(type);
                builder.Map(modelBuilder);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.Settings.Settings.DetailedErrors)
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
