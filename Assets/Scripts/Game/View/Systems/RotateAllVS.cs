using Chessy.Common;
using Chessy.Common.Entity.View;
using Chessy.Game.Entity;
using System;
using UnityEngine;

namespace Chessy.Game
{
    static class RotateAllVS
    {
        static readonly Vector3 _gamePosCamera = new Vector3(7.4f, 4.8f, -2);

        public static void Rotate(in EntitiesViewGame eV, in Entity.Model.EntitiesModelGame e, in EntitiesViewCommon eVC)
        {
            var curPlayer = e.CurPlayerITC.Player;

            for (byte idx_0 = 0; idx_0 < e.LengthCells; idx_0++)
            {
                if (curPlayer == PlayerTypes.None) throw new Exception();
                eV.CellEs(idx_0).CellSR.ParentTransform.rotation = curPlayer == PlayerTypes.First
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


            if (curPlayer == PlayerTypes.None) throw new Exception();

            if (curPlayer == PlayerTypes.First)
            {
                eVC.CameraVC.Transform.position = eVC.MainGOC.Transform.position + _gamePosCamera;
                eVC.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                eVC.CameraVC.Transform.position = eVC.MainGOC.Transform.position + _gamePosCamera + new Vector3(0, 0.5f, 0);
                eVC.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 180);
            }

        }
    }
}