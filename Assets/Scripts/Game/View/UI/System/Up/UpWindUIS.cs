using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    sealed class UpWindUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Dictionary<DirectTypes, Vector3> _directs;
        readonly Vector3 _rotationForOtherPlayer = new Vector3(0, 0, 180);

        internal UpWindUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
            _directs = new Dictionary<DirectTypes, Vector3>();
            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                switch (dirT)
                {
                    case DirectTypes.None: throw new Exception();
                    case DirectTypes.Right: _directs.Add(dirT, new Vector3()); break;
                    case DirectTypes.Left: _directs.Add(dirT, new Vector3(0, 0, 180)); break;
                    case DirectTypes.Up: _directs.Add(dirT, new Vector3(0, 0, 90)); break;
                    case DirectTypes.Down: _directs.Add(dirT, new Vector3(0, 0, 270)); break;
                    case DirectTypes.UpRight: _directs.Add(dirT, new Vector3(0, 0, 45)); break;
                    case DirectTypes.UpLeft: _directs.Add(dirT, new Vector3(0, 0, 135)); break;
                    case DirectTypes.DownRight: _directs.Add(dirT, new Vector3(0, 0, 315)); break;
                    case DirectTypes.DownLeft: _directs.Add(dirT, new Vector3(0, 0, 225)); break;
                    default: throw new Exception();
                }
            }
        }

        public void Run()
        {
            eUI.UpEs.WindTrC.EulerAngles = _directs[eMGame.WeatherE.WindC.Direct];
            if (eMGame.CurPlayerITC.Player == PlayerTypes.Second) eUI.UpEs.WindTrC.EulerAngles += _rotationForOtherPlayer;
            eUI.UpEs.WindTextC.TextUI.text = eMGame.WeatherE.WindC.Speed.ToString() + "/" + eMGame.WeatherE.WindC.MaxSpeed;
        }
    }
}