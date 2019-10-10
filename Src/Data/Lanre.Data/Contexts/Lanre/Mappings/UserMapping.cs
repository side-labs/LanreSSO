// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Mappings
{
    using Lanre.Domain.Entities;
    using Lanre.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserMapping : EntityMappingConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(x => x.Id);
        }
    }
}