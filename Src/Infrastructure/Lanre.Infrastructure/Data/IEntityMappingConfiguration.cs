// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder b);
    }

    public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration
        where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}