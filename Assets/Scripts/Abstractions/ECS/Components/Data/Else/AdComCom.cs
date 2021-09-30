using System;

namespace Assets.Scripts.ECS.Components.Data.Else.Common
{
    public struct AdComCom
    {
        public const int MINUTES_FOR_AD = 10;
        public static DateTime LastTimeAd;

        public AdComCom(DateTime dt)
        {
            LastTimeAd = dt;
        }
    }
}