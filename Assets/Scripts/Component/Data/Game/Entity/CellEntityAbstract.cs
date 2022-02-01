﻿using ECS;

namespace Game.Game
{
    public abstract class CellEntityAbstract : EntityAbstract
    {
        public readonly byte Idx;

        public CellEntityAbstract(in byte idx, in EcsWorld gameW) : base(gameW)
        {
            Idx = idx;
        }
    }
}