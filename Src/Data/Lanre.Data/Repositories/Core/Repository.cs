// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Repositories.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Lanre.Infrastructure.Data;
    using Lanre.Infrastructure.Entities;
    using Lanre.Infrastructure.Repository;
    using Microsoft.EntityFrameworkCore;

    public abstract class Repository<TEntity, TKey> : RepositoryReadOnly<TEntity, TKey>, IRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
    {
        public Repository(ContextCore context)
            : base(context)
        {
        }

        protected override IQueryable<TEntity> Query { get => this._query; set => this._query = value; }

        public virtual void Remove(TEntity entityToDelete)
        {
            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.Set.Attach(entityToDelete);
            }

            this.Set.Remove(entityToDelete);
        }

        public void Remove(List<TEntity> entitiesToRemove)
        {
            foreach (var entityToDelete in entitiesToRemove)
            {
                this.Remove(entityToDelete);
            }
        }

        public void Add(TEntity aggregateRoot)
        {
            this.Set.AddAsync(aggregateRoot);
        }

        public void Add(IList<TEntity> aggregateRoots)
        {
            aggregateRoots.ToList().ForEach(this.Add);
        }

        public void Update(TEntity aggregateRoot)
        {
            this.Set.Update(aggregateRoot);
        }

        public void Update(IList<TEntity> aggregateRoots)
        {
            aggregateRoots.ToList().ForEach(this.Update);
        }
    }
}