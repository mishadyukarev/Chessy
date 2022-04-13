using Chessy.Common.Entity.View;
using Chessy.Game.Model.Entity;
using Chessy.Game.View.System;
using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncCameraVS : SystemViewGameAbs
    {
        readonly Vector3 _gamePosCamera = new Vector3(7.4f, 4.8f, -2);
        readonly EntitiesViewCommon _eVC;

        internal SyncCameraVS(in EntitiesViewCommon eVC, in EntitiesModelGame eMG) : base(eMG)
        {
            _eVC = eVC;
        }

        internal sealed override void Sync()
        {
            var curPlayer = e.CurPlayerIT;

            if (curPlayer == PlayerTypes.None) throw new Exception();

            if (curPlayer == PlayerTypes.First)
            {
                _eVC.CameraVC.Transform.position = _eVC.MainGOC.Transform.position + _gamePosCamera;
                _eVC.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                _eVC.CameraVC.Transform.position = _eVC.MainGOC.Transform.position + _gamePosCamera + new Vector3(0, 0.5f, 0);
                _eVC.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
    }
}