using System;

namespace Assets.Scripts.ECS.Components.Data.Else.Common
{
    internal struct AdComCom
    {
        internal const int MINUTES_FOR_AD = 10;
        internal static DateTime LastTimeAd;

        internal AdComCom(DateTime dt)
        {
            LastTimeAd = dt;
        }
    }
}