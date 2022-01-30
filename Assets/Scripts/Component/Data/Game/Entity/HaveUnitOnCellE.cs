﻿using ECS;

namespace Game.Game
{
    public sealed class HaveUnitOnCellE : EntityAbstract
    {
        public ref HaveC HaveUnit => ref Ent.Get<HaveC>();

        public HaveUnitOnCellE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}