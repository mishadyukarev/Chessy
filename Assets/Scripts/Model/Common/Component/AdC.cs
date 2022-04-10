using System;

namespace Chessy.Common
{
    public struct AdC
    {
        public const int MINUTES_TIME_ADD = 5;
        public DateTime LastTimeAd;

        internal AdC(DateTime dt) => LastTimeAd = dt;
    }
}