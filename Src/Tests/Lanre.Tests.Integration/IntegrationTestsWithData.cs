// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration
{
    using System;
    using Lanre.Tests.Core;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class IntegrationTestsWithData : IntegrationTestCore, IDisposable
    {
        public IntegrationTestsWithData(string url)
            : base(url)
        {
            this.SeedData();
            this.RemoveToContext();
            this.AddToContext();
        }

        protected IntegrationTestsWithData(string url, Type startup)
            : base(url, startup)
        {
            this.SeedData();
            this.RemoveToContext();
            this.AddToContext();
        }

        protected IntegrationTestsWithData(string url, Type startup, Action<IServiceCollection> configureServices)
            : base(url, startup, configureServices)
        {
            this.SeedData();
            this.RemoveToContext();
            this.AddToContext();
        }

        public void Dispose()
        {
            this.RemoveToContext();
        }

        protected abstract void SeedData();

        protected abstract void AddToContext();

        protected abstract void RemoveToContext();
    }
}