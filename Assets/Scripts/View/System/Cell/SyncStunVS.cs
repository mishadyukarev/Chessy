using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.System;

namespace Chessy.Model.System
{
    sealed class SyncStunVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _stunSRC;

        internal SyncStunVS(in SpriteRendererVC[] stunSRCs, in EntitiesModel eM) : base(eM)
        {
            _stunSRC = stunSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;

                if (_e.WhereViewDataUnitC(cellIdxCurrent).HaveDataReference)
                {
                    var dataIdxCell = _e.WhereViewDataUnitC(cellIdxCurrent).DataIdxCellP;


                    if (_e.UnitT(dataIdxCell).HaveUnit())
                    {
                        if (_e.UnitVisibleC(dataIdxCell).IsVisible(_e.CurrentPlayerIT))
                        {
                            _needActive[cellIdxCurrent] = _e.UnitEffectsC(dataIdxCell).IsStunned;
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _stunSRC[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}