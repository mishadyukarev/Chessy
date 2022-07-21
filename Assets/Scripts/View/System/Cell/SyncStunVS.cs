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

                if (_unitWhereViewDataCs[cellIdxCurrent].HaveDataReference)
                {
                    var dataIdxCell = _unitWhereViewDataCs[cellIdxCurrent].DataIdxCellP;


                    if (_unitCs[dataIdxCell].HaveUnit)
                    {
                        if (_unitVisibleCs[dataIdxCell].IsVisible(_aboutGameC.CurrentPlayerIType))
                        {
                            _needActive[cellIdxCurrent] = _effectsUnitCs[dataIdxCell].IsStunned;
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