// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}