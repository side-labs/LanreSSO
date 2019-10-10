// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.Repository
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}