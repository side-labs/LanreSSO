// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Integration
{
    using System;
    using Lanre.Tests.Core;

    public class IntegrationTests : IntegrationTestCore
    {
        public IntegrationTests(string url)
            : base(url, typeof(TestApiStartup))
        {
        }

        protected IntegrationTests(string url, Type startup)
            : base(url, startup)
        {
        }
    }
}