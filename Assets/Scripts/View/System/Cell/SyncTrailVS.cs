using Chessy.Model;
using Chessy.Model.Component;
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
        readonly bool[,] _wasActivated = new bool[IndexCellsValues.CELLS, (byte)DirectTypes.End];
        readonly SpriteRenderer[,] _trailSRs;
        readonly GameObject[,] _gOs = new GameObject[IndexCellsValues.CELLS, (byte)DirectTypes.End];

        readonly Vector3 _vector0 = new Vector3(0, 0, 0);
        readonly Vector3 _vector1 = new Vector3(0, 0, 180);
        internal SyncTrailVS(in SpriteRenderer[,] trailSRs, in EntitiesModel eM) : base(eM)
        {
            _trailSRs = trailSRs;

            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                {
                    _gOs[cellIdx, (byte)directT] = trailSRs[cellIdx, (byte)directT].gameObject;
                }
            }
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

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                {
                    var directTbyte = (byte)directT;


                    if (_visibleTrailCs[cellIdxCurrent].IsVisible(_aboutGameC.CurrentPlayerIType))
                    {
                        _needActive[cellIdxCurrent, directTbyte] = _hpTrailCs[cellIdxCurrent].IsAlive(directT);
                    }

                    if (directT == DirectTypes.Up)
                    {
                        switch (_aboutGameC.CurrentPlayerIType)
                        {
                            case PlayerTypes.First:
                                _trailSRs[cellIdxCurrent, directTbyte].transform.parent.localEulerAngles = _vector0;
                                break;

                            case PlayerTypes.Second:
                                _trailSRs[cellIdxCurrent, directTbyte].transform.parent.localEulerAngles = _vector1;
                                break;

                            default: throw new Exception();
                        }
                    }


                    var needActive = _needActive[cellIdxCurrent, directTbyte];
                    ref var wasActivated = ref _wasActivated[cellIdxCurrent, directTbyte];

                    if(needActive != wasActivated) _gOs[cellIdxCurrent, directTbyte].SetActive(_needActive[cellIdxCurrent, directTbyte]);

                    wasActivated = needActive;
                }
            }
        }
    }
}