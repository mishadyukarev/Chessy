using Chessy.Game.Entity.View.Cell.Unit.Effect;
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

            if (_e.UnitTC(_currentCell).HaveUnit)
            {
                if (_e.UnitVisibleC(_currentCell).IsVisible(_e.CurPlayerITC.PlayerT))
                {
                    if (_e.MainToolWeaponTC(_currentCell).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        if (_e.FrozenArrawEffectC(_currentCell).HaveShoots)
                        {
                            _needActive[_e.UnitIsRightArcherC(_currentCell).IsRight ? 0 : 1] = true;
                        }
                    }
                }
            }

            _effectE.FrozenArraw(true).SetActive(_needActive[0]);
            _effectE.FrozenArraw(false).SetActive(_needActive[1]);
        }
    }
}