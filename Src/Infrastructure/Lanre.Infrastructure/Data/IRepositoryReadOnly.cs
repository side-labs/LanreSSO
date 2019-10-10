// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Lanre.Infrastructure.Entities;

    public interface IRepositoryReadOnly<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includes = null,
            int? page = null,
            int? pageSize = null,
            params OrderByCustom<TEntity>[] orderBy);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> GetByIdAsync(TKey id, string includes = null);

        Task<List<TEntity>> GetByIdsAsync(List<TKey> ids, string includes = null);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string includes = null);
    }
}