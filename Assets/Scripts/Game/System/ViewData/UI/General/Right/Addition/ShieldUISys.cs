﻿using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ShieldUISys : IEcsRunSystem
    {
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            ref var selTwUnitC = ref _twUnitF.Get1(SelIdx.Idx);

            ExtraTWZoneUIC.DisableAll();

            if (selTwUnitC.HaveToolWeap)
            {
                ExtraTWZoneUIC.Toggle(selTwUnitC.ToolWeapon, selTwUnitC.LevelTWType, true);
            }
        }
    }
}