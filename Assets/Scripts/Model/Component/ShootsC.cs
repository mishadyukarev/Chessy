﻿namespace Chessy.Model
{
    public struct ShootsC
    {
        public int Shoots { get; internal set; }
        public bool HaveShoots => Shoots > 0;
    }
}