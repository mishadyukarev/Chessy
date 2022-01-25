using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class CellUnitUniqueAbilityE : EntityAbstract
    {
        public ref AmountC Cooldown => ref Ent.Get<AmountC>();

        public CellUnitUniqueAbilityE(in EcsWorld gameW) : base(gameW) { }
    }
}