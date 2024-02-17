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

        readonly Vector3 _rot0 = new(0, 0, 0);
        readonly Vector3 _rot1 = new(0, 0, 180);

        internal SyncRiverVS(in RiverVE[] riverVEs, in EntitiesModel eM) : base(eM)
        {
            _riverVEs = riverVEs;
        }

        internal sealed override void Sync()
        {
            var currentPlayerT = aboutGameC.CurrentPlayerIType;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (CellC(cellIdxCurrent).IsBorder) continue;

                var parentTrans = _riverVEs[cellIdxCurrent].ParentTransformVC.Transform;

                switch (currentPlayerT)
                {
                    case PlayerTypes.None: throw new Exception();
                    case PlayerTypes.First:
                        if (_rot0.z != parentTrans.localEulerAngles.z) parentTrans.localEulerAngles = _rot0;
                        break;

                    case PlayerTypes.Second:
                        if (_rot1.z != parentTrans.localEulerAngles.z) parentTrans.localEulerAngles = _rot1;
                        break;

                    default: throw new Exception();
                }


                if (RiverC(cellIdxCurrent).RiverT == RiverTypes.Start)
                {
                    for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                    {
                        if (dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Left)
                        {
                            _riverVEs[cellIdxCurrent].River(dir_1).TrySetEnabled(haveRiverAroundCellCs[cellIdxCurrent].HaveRive(dir_1));
                        }
                    }
                }
            }


        }
    }
}