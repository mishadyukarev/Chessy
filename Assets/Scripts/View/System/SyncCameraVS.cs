using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
using System;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncCameraVS : SystemViewAbstract
    {
        readonly Vector3 _gamePosCamera = new Vector3(7.4f, 4.8f, -2);
        readonly EntitiesView _eV;

        internal SyncCameraVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;
        }

        internal sealed override void Sync()
        {
            if (AboutGameC.CurrentPlayerIType == PlayerTypes.None) throw new Exception();

            if (AboutGameC.CurrentPlayerIType == PlayerTypes.First)
            {
                _eV.CameraVC.Transform.position = _eV.MainGOC.Transform.position + _gamePosCamera;
                _eV.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                _eV.CameraVC.Transform.position = _eV.MainGOC.Transform.position + _gamePosCamera + new Vector3(0, 0.5f, 0);
                _eV.CameraVC.Transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
    }
}