﻿namespace Chessy.Game
{
    public struct ExtractE
    {
        public ResourcesC ResourcesC;

        public void Set(in float resources)
        {
            ResourcesC.Resources = resources;
        }
    }
}