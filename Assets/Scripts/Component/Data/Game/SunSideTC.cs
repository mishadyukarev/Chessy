using System;

namespace Game.Game
{
    public struct SunSideTC
    {
        public SunSideTypes SunSide;

        public SunSideTC(in SunSideTypes sunSide) => SunSide = sunSide;
    }
}