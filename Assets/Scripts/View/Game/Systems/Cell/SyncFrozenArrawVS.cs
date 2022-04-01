using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.Entity.View.Cell.Unit.Effect;

namespace Chessy.Game
{
    static class SyncFrozenArrawVS
    {
        public static void SyncVision(this EffectVEs effectsVEs, in UnitEs unitEs, in bool isSelected, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            effectsVEs.FrozenArraw(true, true).Disable();
            effectsVEs.FrozenArraw(false, true).Disable();

            effectsVEs.FrozenArraw(true, false).Disable();
            effectsVEs.FrozenArraw(false, false).Disable();

            if (unitEs.MainE.UnitTC.HaveUnit)
            {
                if (unitEs.ForPlayer(e.CurPlayerITC.PlayerT).IsVisible)
                {
                    if (unitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow))
                    {
                        if (unitEs.EffectsE.FrozenArrawC.HaveShoots)
                        {
                            effectsVEs.FrozenArraw(isSelected, unitEs.MainE.IsRightArcherC.IsRight).Enable();
                        }
                    }
                }
            }
        }
    }
}