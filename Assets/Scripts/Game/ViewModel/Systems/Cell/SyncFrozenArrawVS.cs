using Chessy.Game.Entity.Cell.Unit;
using Chessy.Game.Entity.View.Cell.Unit.Effect;
using UnityEngine;

namespace Chessy.Game
{
    static class SyncFrozenArrawVS
    {
        public static void SyncVision(this EffectVEs effectsVEs, in UnitEs unitEs, in bool isSelected)
        {
            effectsVEs.FrozenArraw(true, true).Disable();
            effectsVEs.FrozenArraw(false, true).Disable();

            effectsVEs.FrozenArraw(true, false).Disable();
            effectsVEs.FrozenArraw(false, false).Disable();

            if (unitEs.MainE.UnitTC.HaveUnit)
            {
                if (unitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow))
                {
                    if (unitEs.EffectsE.FrozenArrawC.HaveEffect)
                    {
                        effectsVEs.FrozenArraw(isSelected, unitEs.MainE.IsRightArcherC.IsRight).Enable();
                    }
                }
            }
        }
    }
}