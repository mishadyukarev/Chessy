using Chessy.Common;
using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class RotateAllVS : SystemViewAbstract, IEcsRunSystem
    {
        readonly Vector3 _gamePosCamera;

        internal RotateAllVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
            _gamePosCamera = new Vector3(7.4f, 4.8f, -2);
        }

        public void Run()
        {
            var curPlayer = E.CurPlayerITC.Player;

            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (curPlayer == PlayerTypes.None) throw new Exception();
                CellVEs(idx_0).CellSR.RotParent = curPlayer == PlayerTypes.First
                    ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

                switch (curPlayer)
                {
                    case PlayerTypes.None: throw new Exception();

                    case PlayerTypes.First:
                        //VEs.CellEs(idx_0).Tra CellTrailVEs.TrailCellVC<ParentTransformVC>(DirectTypes.Up, idx_0).Transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;

                    case PlayerTypes.Second:
                        //CellTrailVEs.TrailCellVC<ParentTransformVC>(DirectTypes.Up, idx_0).Transform.localEulerAngles = new Vector3(0, 0, 180);
                        break;

                    default: throw new Exception();
                }
            }


            if (curPlayer == PlayerTypes.None) throw new System.Exception();

            if (curPlayer == PlayerTypes.First)
            {
                VEs.CameraVC.Transform.position = MainGoVC.Pos + _gamePosCamera;
                VEs.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                VEs.CameraVC.Transform.position = MainGoVC.Pos + _gamePosCamera + new Vector3(0, 0.5f, 0);
                VEs.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 180);
            }

        }
    }
}