// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Core
{
    public class RoutesConstants
    {
        public static class Home
        {
            public const string Base = "Home";
            public const string Alive = "/Alive";
        }

        public static class Users
        {
            public const string Base = "Api/Users";
            public const string Paginated = Users.Base + "/Paginated";
        }
    }
}
