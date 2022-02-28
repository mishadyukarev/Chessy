using System;

namespace Chessy.Common
{
    public struct AdComC
    {
        public const int MINUTES_FOR_AD = 5;
        public static DateTime LastTimeAd;

        public AdComC(DateTime dt)
        {
            LastTimeAd = dt;
        }
    }
}