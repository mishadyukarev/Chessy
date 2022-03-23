﻿using Chessy.Common;

namespace Chessy.Game
{
    public struct HealthC
    {
        public float Health;

        public bool IsAlive => Health > 0;

        public HealthC(in int health) { Health = health; }

        public void Destroy() => Health = 0;
    }
}