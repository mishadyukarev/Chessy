﻿using System;
namespace Chessy.Model
{
    public struct AdC
    {
        public const int MINUTES_TIME_ADD = 5;
        public DateTime LastTimeAd { get; internal set; }

        internal AdC(DateTime dt) => LastTimeAd = dt;
    }
}