﻿using System;

namespace Game.Common
{
    public struct AdComCom
    {
        public const int MINUTES_FOR_AD = 5;
        public static DateTime LastTimeAd;

        public AdComCom(DateTime dt)
        {
            LastTimeAd = dt;
        }
    }
}