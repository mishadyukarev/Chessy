using Game.Common;
using System;
using UnityEngine;

namespace Game.Game
{
    sealed class RotateAllVS : SystemViewAbstract, IEcsRunSystem
    {
        public RotateAllVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var curPlayer = Es.WhoseMoveE.CurPlayerI;

            foreach (byte idx_0 in CellWorker.Idxs)
            {
                if (curPlayer == PlayerTypes.None) throw new Exception();
                CellVEs(idx_0).CellSR.RotParent = curPlayer == PlayerTypes.First
                    ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

                switch (curPlayer)
                {
                    case PlayerTypes.None: throw new Exception();

                    case PlayerTypes.First:
                        CellTrailVEs.TrailCellVC<ParentTransformVC>(DirectTypes.Up, idx_0).Transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;

                    case PlayerTypes.Second:
                        CellTrailVEs.TrailCellVC<ParentTransformVC>(DirectTypes.Up, idx_0).Transform.localEulerAngles = new Vector3(0, 0, 180);
                        break;

                    default: throw new Exception();
                }
            }

            CameraVC.SetPosRotClient(curPlayer, MainGoVC.Pos);
        }

        public void SetRotForClient(PlayerTypes player)
        {

        }
    }
}