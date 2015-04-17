using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.UnitTests.Constants
{
    public class UserConstants
    {
        /// <summary>
        /// The default username to use in unit tests
        /// </summary>
        public const string TestUserName = "artie@test.com";

        public class ChartCreator
        {
            public const long Id = 1;
            public const long OAuthServiceId = 6;   
        }

        public class PointEarner
        {
            public const long Id = 2;
            public const long OAuthServiceId = 1;
        }
    }
}
