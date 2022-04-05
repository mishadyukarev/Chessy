using Chessy.Game.Model.Entity.Cell.Unit;
using Chessy.Game.Entity.View.Cell.Unit.Effect;
using Chessy.Common.Model.System;
using Chessy.Common.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class SyncFrozenArrawVS
    {
        readonly EntitiesModelGame _eMG;
        readonly EntitiesViewGame _eVG;

        internal SyncFrozenArrawVS(in EntitiesModelGame eMG, in EntitiesViewGame eVG)
        {
            _eMG = eMG;
            _eVG = eVG;
        }

        internal void SyncVision(in byte cell)
        {
            _eVG.CellEs(cell).UnitVEs.EffectVEs.FrozenArraw(true, true).Disable();
            _eVG.CellEs(cell).UnitVEs.EffectVEs.FrozenArraw(false, true).Disable();

            _eVG.CellEs(cell).UnitVEs.EffectVEs.FrozenArraw(true, false).Disable();
            _eVG.CellEs(cell).UnitVEs.EffectVEs.FrozenArraw(false, false).Disable();

            if (_eMG.UnitTC(cell).HaveUnit)
            {
                if (_eMG.UnitVisibleC(cell).IsVisible(_eMG.CurPlayerITC.PlayerT))
                {
                    if (_eMG.MainToolWeaponTC(cell).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        if (_eMG.FrozenArrawEffectC(cell).HaveShoots)
                        {
                            _eVG.CellEs(cell).UnitVEs.EffectVEs.FrozenArraw(_eMG.CellsC.IsSelectedCell, _eMG.UnitIsRightArcherC(cell).IsRight).Enable();
                        }
                    }
                }
            }
        }
    }
}