using Chessy.Model;
using System;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncRiverVS : SystemViewCellGameAbs
    {
        readonly RiverVE _riverVE;

        internal SyncRiverVS(in RiverVE riverVE, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _riverVE = riverVE;
        }

        internal sealed override void Sync()
        {
            switch (_e.CurPlayerIT)
            {
                case PlayerTypes.None: throw new Exception();
                case PlayerTypes.First:
                    _riverVE.Parents.LocalEulerAngles = new Vector3(0, 0, 0);
                    break;

                case PlayerTypes.Second:
                    _riverVE.Parents.LocalEulerAngles = new Vector3(0, 0, 180);
                    break;

                default: throw new Exception();
            }


            if (_e.RiverT(_currentCell) == RiverTypes.Start)
            {
                for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                {
                    if (dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Left)
                    {
                        _riverVE.River(dir_1).SetEnabled(_e.HaveRiverC(_currentCell).HaveRive(dir_1));
                    }
                }
            }
        }
    }
}