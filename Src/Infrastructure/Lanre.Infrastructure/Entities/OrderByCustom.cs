// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Entities
{
    using System;
    using System.Linq.Expressions;

    public class OrderByCustom<TEntity>
    {
        public OrderByCustom(Expression<Func<TEntity, object>> orderBy)
        {
            this.OrderAscending = true;
            this.OrderBy = orderBy;
        }

        public OrderByCustom(bool orderAscending, Expression<Func<TEntity, object>> orderBy)
        {
            this.OrderAscending = orderAscending;
            this.OrderBy = orderBy;
        }

        public bool OrderAscending { get; set; }

        public Expression<Func<TEntity, object>> OrderBy { get; set; }
    }
}
