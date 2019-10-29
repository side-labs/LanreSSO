// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Repository
{
    using System.Collections.Generic;
    using Lanre.Infrastructure.Entities;

    public interface IRepository<TEntity, TKey> : IRepositoryReadOnly<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
    {
          void Remove(TEntity entityToDelete);

        void Remove(List<TEntity> entitiesToRemove);

        void Add(TEntity aggregateRoot);

        void Add(IList<TEntity> aggregateRoots);

        void Update(TEntity aggregateRoot);

        void Update(IList<TEntity> aggregateRoots);
    }
}