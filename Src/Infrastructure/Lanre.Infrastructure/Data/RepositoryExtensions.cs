namespace Lanre.Infrastructure.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Lanre.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;

    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> IncludeProperties<TEntity, TKey>(this IQueryable<TEntity> query, string includeProperties)
            where TEntity : Entity<TKey>
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            return query;
        }

        public static IQueryable<TEntity> OrderByCustom<TEntity, TKey>(
            this IQueryable<TEntity> query,
            params OrderByCustom<TEntity>[] orderBy)
            where TEntity : Entity<TKey>
        {
            if (orderBy != null && orderBy.Any())
            {
                IOrderedQueryable<TEntity> ordered = null;

                var firstOrder = orderBy.First();
                if (firstOrder.OrderAscending)
                {
                    ordered = query.OrderBy(firstOrder.OrderBy);
                }
                else
                {
                    ordered = query.OrderByDescending(firstOrder.OrderBy);
                }

                foreach (var order in orderBy.Skip(1))
                {
                    if (order.OrderAscending)
                    {
                        ordered = ordered.ThenBy(order.OrderBy);
                    }
                    else
                    {
                        ordered = ordered.ThenByDescending(order.OrderBy);
                    }
                }

                query = ordered;
            }

            return query;
        }

        public static IQueryable<TEntity> WhereFilter<TEntity, TKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter)
            where TEntity : Entity<TKey>
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public static Task<TEntity> FirstOrDefaultFilterAsync<TEntity, TKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter)
            where TEntity : Entity<TKey>
        {
            if (filter != null)
            {
                return query.FirstOrDefaultAsync(filter);
            }

            return query.FirstOrDefaultAsync();
        }

        public static TEntity FirstOrDefaultFilter<TEntity, TKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter)
            where TEntity : Entity<TKey>
        {
            if (filter != null)
            {
                return query.FirstOrDefault(filter);
            }

            return query.FirstOrDefault();
        }

        public static IQueryable<TEntity> Paginate<TEntity, TKey>(this IQueryable<TEntity> query, int? page, int? pageSize)
           where TEntity : Entity<TKey>
        {
            if (page.HasValue && page.Value >= 0
                && pageSize.HasValue && pageSize.Value > 0)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            else
            {
                if (pageSize.HasValue && pageSize.Value > 0)
                {
                    query = query.Take(pageSize.Value);
                }
            }

            return query;
        }
    }
}