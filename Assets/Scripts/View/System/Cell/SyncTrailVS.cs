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
        readonly Dictionary<DirectTypes, bool[]> _needActive = new Dictionary<DirectTypes, bool[]>();
        readonly Dictionary<DirectTypes, SpriteRendererVC[]> _trailSRCs = new Dictionary<DirectTypes, SpriteRendererVC[]>();

        internal SyncTrailVS(in Dictionary<DirectTypes, SpriteRendererVC[]> trailSRCs, in EntitiesModel eM) : base(eM)
        {
            _trailSRCs = trailSRCs;

            for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
            {
                _needActive.Add(directT, new bool[IndexCellsValues.CELLS]);
            }
        }

        internal sealed override void Sync()
        {
            for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    _needActive[directT][cellIdxCurrent] = false;
                }
            }


            for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.TrailVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                    {
                        _needActive[directT][cellIdxCurrent] = _e.HealthTrail(cellIdxCurrent).IsAlive(directT);
                    }

                    if(directT == DirectTypes.Up)
                    {
                        switch (_e.CurrentPlayerIT)
                        {
                            case PlayerTypes.First:
                                _trailSRCs[directT][cellIdxCurrent].ParentTransform.localEulerAngles = new Vector3(0, 0, 0);
                                break;

                            case PlayerTypes.Second:
                                _trailSRCs[directT][cellIdxCurrent].ParentTransform.localEulerAngles = new Vector3(0, 0, 180);
                                break;

                            default: throw new Exception();
                        }
                    }

                    _trailSRCs[directT][cellIdxCurrent].SetActiveGO(_needActive[directT][cellIdxCurrent]);
                }
            }
        }
    }
}