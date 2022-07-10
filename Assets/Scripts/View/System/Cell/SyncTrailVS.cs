using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncTrailVS : SystemViewAbstract
    {
        readonly bool[,] _needActive = new bool[IndexCellsValues.CELLS, (byte)DirectTypes.End];
        readonly SpriteRendererVC[,] _trailSRCs;

        internal SyncTrailVS(in SpriteRendererVC[,] trailSRCs, in EntitiesModel eM) : base(eM)
        {
            _trailSRCs = trailSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte directT = 1; directT < (byte)DirectTypes.End; directT++)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    _needActive[cellIdxCurrent, directT] = false;
                }
            }


            for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
            {
                var directTbyte = (byte)directT;

                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.TrailVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                    {
                        _needActive[cellIdxCurrent, directTbyte] = _e.HealthTrail(cellIdxCurrent).IsAlive(directT);
                    }

                    if(directT == DirectTypes.Up)
                    {
                        switch (_e.CurrentPlayerIT)
                        {
                            case PlayerTypes.First:
                                _trailSRCs[cellIdxCurrent, directTbyte].ParentTransform.localEulerAngles = new Vector3(0, 0, 0);
                                break;

                            case PlayerTypes.Second:
                                _trailSRCs[cellIdxCurrent, directTbyte].ParentTransform.localEulerAngles = new Vector3(0, 0, 180);
                                break;

                            default: throw new Exception();
                        }
                    }

                    _trailSRCs[cellIdxCurrent, directTbyte].SetActiveGO(_needActive[cellIdxCurrent, directTbyte]);
                }
            }
        }
    }
}