// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration<T>
        where T : class
    {
        public abstract void Map(EntityTypeBuilder<T> entity);

        public void Map(ModelBuilder entityBuilder)
        {
            var entity = entityBuilder.Entity<T>();
            this.Map(entity);
        }
    }
}