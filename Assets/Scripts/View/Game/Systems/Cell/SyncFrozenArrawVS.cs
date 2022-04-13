﻿using Chessy.Game.Entity.View.Cell.Unit.Effect;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class SyncFrozenArrawVS : SystemViewCellGameAbs
    {
        readonly EffectVE _effectE;

        readonly bool[] _needActive = new bool[2];

        internal SyncFrozenArrawVS(in EffectVE effectVE, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _effectE = effectVE;
        }

        internal sealed override void Sync()
        {
            _needActive[0] = false;
            _needActive[1] = false;

            if (e.UnitTC(_currentCell).HaveUnit)
            {
                if (e.UnitVisibleC(_currentCell).IsVisible(e.CurPlayerITC.PlayerT))
                {
                    if (e.MainToolWeaponTC(_currentCell).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        if (e.FrozenArrawEffectC(_currentCell).HaveShoots)
                        {
                            _needActive[e.UnitIsRightArcherC(_currentCell).IsRight ? 0 : 1] = true;
                        }
                    }
                }
            }

            _effectE.FrozenArraw(true).SetActive(_needActive[0]);
            _effectE.FrozenArraw(false).SetActive(_needActive[1]);
        }
    }
}