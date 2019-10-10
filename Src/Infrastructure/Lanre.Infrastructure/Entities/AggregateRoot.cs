// Copyright (c) Lanre. All rights reserved.
namespace Lanre.Infrastructure.Entities
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        protected AggregateRoot()
            : base()
        {
        }

        protected AggregateRoot(TKey id)
            : base(id)
        {
        }
    }
}