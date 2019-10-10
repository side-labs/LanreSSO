// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Repositories.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Lanre.Infrastructure.Data;
    using Lanre.Infrastructure.Entities;
    using Lanre.Infrastructure.Repository;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryReadOnly<TEntity, TKey> : IRepositoryReadOnly<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        internal IQueryable<TEntity> _query;

        public RepositoryReadOnly(ContextCore context)
        {
            this.Context = context;
            this.Set = context.Set<TEntity>();
            this.Query = this.Set;
        }

        protected ContextCore Context { get; }

        protected DbSet<TEntity> Set { get; }

        protected virtual IQueryable<TEntity> Query { get => this._query.AsNoTracking(); set => this._query = value; }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return this.Query.AnyAsync(filter);
            }

            return this.Query.AnyAsync();
        }

        public virtual Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includes = null,
            int? page = null,
            int? pageSize = null,
            params OrderByCustom<TEntity>[] orderBy)
        {
            return this.Query
                   .WhereFilter<TEntity, TKey>(filter)
                   .IncludeProperties<TEntity, TKey>(includes)
                   .OrderByCustom<TEntity, TKey>(orderBy)
                   .Paginate<TEntity, TKey>(page, pageSize)
                   .ToListAsync()
                   ;
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.Query
                    .WhereFilter<TEntity, TKey>(filter)
                    .CountAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id, string includes = null)
        {
            return this.Query
                     .IncludeProperties<TEntity, TKey>(includes)
                     .FirstOrDefaultAsync(t => t.Id.Equals(id))
                     ;
        }

        public virtual Task<List<TEntity>> GetByIdsAsync(List<TKey> ids, string includes = null)
        {
            return this.Query
                     .IncludeProperties<TEntity, TKey>(includes)
                     .Where(t => ids.Contains(t.Id))
                     .ToListAsync()
                     ;
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string includes = null)
        {
            return this.Query
                    .IncludeProperties<TEntity, TKey>(includes)
                    .FirstOrDefaultFilterAsync<TEntity, TKey>(filter)
                    ;
        }
    }
}
