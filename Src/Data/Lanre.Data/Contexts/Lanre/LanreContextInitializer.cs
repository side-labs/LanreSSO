// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Contexts.Lanre
{
    using global::Lanre.Data.Contexts.Core;

    public class LanreContextInitializer : ContextInitializer<LanreContext>
    {
        public static LanreContextInitializer Instance => new LanreContextInitializer();
    }
}
