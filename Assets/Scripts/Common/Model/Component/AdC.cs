using System;

namespace Chessy.Common
{
    public struct AdC
    {
        public const int MINUTES_FOR_AD = 5;
        public DateTime LastTimeAd;

        public AdC(DateTime dt)
        {
            LastTimeAd = dt;
        }
    }
}