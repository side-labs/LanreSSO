// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Repositories.Core
{
    using System.Threading.Tasks;
    using Lanre.Data.Contexts.Lanre;
    using Lanre.Infrastructure.Repository;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly LanreContext _context;

        public UnitOfWork(LanreContext context)
        {
            this._context = context;
        }

        public Task<int> SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }
    }
}