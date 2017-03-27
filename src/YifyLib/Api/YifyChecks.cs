using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YifyLib.Api
{
    internal static class YifyChecks
    {
        public static void CheckAppKey(this Yify y)
        {
            if (string.IsNullOrWhiteSpace(y.ApplicationKey))
            {
                throw new YifyMissingAppKeyException(
                "Application Key is missing." +
                " Make sure that you enter correct application key to Yify.ApplicationKey property. " +
                "If you do not posses an application key then request an application key from https://yts.ag/contact");
            }
        }
        public static void CheckLoggedIn(this Yify y)
        {
            if (!y.IsLoggedIn)
            {
                throw new YifyNotLoggedInException("Login required to access this functionality. User Yify.Login function first.");
            }
        }
    }
}
