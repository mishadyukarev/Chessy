using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.System;

namespace Chessy.Model.System
{
    sealed class SyncStunVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[StartValues.CELLS];
        readonly SpriteRendererVC[] _stunSRC;

        internal SyncStunVS(in SpriteRendererVC[] stunSRCs, in EntitiesModel eM) : base(eM)
        {
            _stunSRC = stunSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;

                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                    {
                        _needActive[cellIdxCurrent] = _e.UnitEffectsC(cellIdxCurrent).IsStunned;
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _stunSRC[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}