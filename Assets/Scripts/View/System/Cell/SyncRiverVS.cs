using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Entity;
using System;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncRiverVS : SystemViewAbstract
    {
        readonly RiverVE[] _riverVEs;

        internal SyncRiverVS(in RiverVE[] riverVEs, in EntitiesModel eM) : base(eM)
        {
            _riverVEs = riverVEs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                switch (_e.CurrentPlayerIT)
                {
                    case PlayerTypes.None: throw new Exception();
                    case PlayerTypes.First:
                        _riverVEs[cellIdxCurrent].ParentTransformVC.LocalEulerAngles = new Vector3(0, 0, 0);
                        break;

                    case PlayerTypes.Second:
                        _riverVEs[cellIdxCurrent].ParentTransformVC.LocalEulerAngles = new Vector3(0, 0, 180);
                        break;

                    default: throw new Exception();
                }


                if (_e.RiverT(cellIdxCurrent) == RiverTypes.Start)
                {
                    for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                    {
                        if (dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Left)
                        {
                            _riverVEs[cellIdxCurrent].River(dir_1).TrySetEnabled(_e.HaveRiverC(cellIdxCurrent).HaveRive(dir_1));
                        }
                    }
                }
            }

            
        }
    }
}