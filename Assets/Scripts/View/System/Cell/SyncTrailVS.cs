using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using System;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncTrailVS : SystemViewAbstract
    {
        readonly bool[,] _needActive = new bool[IndexCellsValues.CELLS, (byte)DirectTypes.End];
        readonly bool[,] _wasActivated = new bool[IndexCellsValues.CELLS, (byte)DirectTypes.End];
        readonly SpriteRenderer[,] _trailSRs;
        readonly GameObjectVC[,] _gOs = new GameObjectVC[IndexCellsValues.CELLS, (byte)DirectTypes.End];
        readonly Transform[,] _parentTrans = new Transform[IndexCellsValues.CELLS, (byte)DirectTypes.End];

        readonly Vector3 _vector0 = new(0, 0, 0);
        readonly Vector3 _vector1 = new(0, 0, 180);

        internal SyncTrailVS(in SpriteRenderer[,] trailSRs, in EntitiesModel eM) : base(eM)
        {
            _trailSRs = trailSRs;

            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                {
                    var go = trailSRs[cellIdx, (byte)directT].gameObject;

                    _gOs[cellIdx, (byte)directT] = new GameObjectVC(go);
                    _parentTrans[cellIdx, (byte)directT] = go.transform.parent;
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

            var currentPlayerT = AboutGameC.CurrentPlayerIType;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                {
                    var directTbyte = (byte)directT;


                    if (TrailVisibleC(cellIdxCurrent).IsVisible(currentPlayerT))
                    {
                        _needActive[cellIdxCurrent, directTbyte] = TrailHealthC(cellIdxCurrent).IsAlive(directT);
                    }

                    if (directT == DirectTypes.Up)
                    {
                        switch (currentPlayerT)
                        {
                            case PlayerTypes.First:
                                if (_vector0.z != _parentTrans[cellIdxCurrent, directTbyte].localEulerAngles.z)
                                {
                                    _parentTrans[cellIdxCurrent, directTbyte].localEulerAngles = _vector0;
                                }
                                break;

                            case PlayerTypes.Second:
                                if (_vector1.z != _parentTrans[cellIdxCurrent, directTbyte].localEulerAngles.z)
                                {
                                    _parentTrans[cellIdxCurrent, directTbyte].localEulerAngles = _vector1;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }

                    _gOs[cellIdxCurrent, directTbyte].TrySetActive2(_needActive[cellIdxCurrent, directTbyte], ref _wasActivated[cellIdxCurrent, directTbyte]);
                }
            }
        }
    }
}