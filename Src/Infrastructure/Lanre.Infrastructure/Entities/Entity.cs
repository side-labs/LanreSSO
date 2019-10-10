// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        protected Entity()
        {
        }

        protected Entity(TKey id)
            : this()
        {
            this.Id = id;
        }

        public TKey Id { get; protected set; }
    }
}