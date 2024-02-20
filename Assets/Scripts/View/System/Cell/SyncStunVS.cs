﻿using Chessy.Model.Entity;
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

                if (unitWhereViewDataCs[cellIdxCurrent].HaveDataReference)
                {
                    var dataIdxCell = unitWhereViewDataCs[cellIdxCurrent].DataIdxCellP;


                    if (unitCs[dataIdxCell].HaveUnit)
                    {
                        if (_unitVisibleCs[dataIdxCell].IsVisible(aboutGameC.CurrentPlayerIType))
                        {
                            _needActive[cellIdxCurrent] = effectsUnitCs[dataIdxCell].IsStunned;
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