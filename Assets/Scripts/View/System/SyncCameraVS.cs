using Chessy.Model.Model.Entity;
using Chessy.Model.View.System;
using System;
using UnityEngine;

namespace Chessy.Model
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
            if (_e.CurPlayerIT == PlayerTypes.None) throw new Exception();

            if (_e.CurPlayerIT == PlayerTypes.First)
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