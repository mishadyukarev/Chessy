using Chessy.Model.Values;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model
{
    sealed class UpWindUIS : SystemUIAbstract
    {
        readonly Dictionary<DirectTypes, Vector3> _directs;
        readonly Vector3 _rotationForOtherPlayer = new Vector3(0, 0, 180);

        readonly EntitiesViewUI _eUI;

        internal UpWindUIS(in EntitiesViewUI entsUI, in Chessy.Model.Model.Entity.EntitiesModel ents) : base(ents)
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
                    case DirectTypes.LeftUp: _directs.Add(dirT, new Vector3(0, 0, 135)); break;
                    case DirectTypes.RightDown: _directs.Add(dirT, new Vector3(0, 0, 315)); break;
                    case DirectTypes.DownLeft: _directs.Add(dirT, new Vector3(0, 0, 225)); break;
                    default: throw new Exception();
                }
            }
            _eUI = entsUI;
        }

        internal override void Sync()
        {
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.ClickWindInfo)
            {
                _eUI.UpEs.ParentWindGOC.SetActive(true);

                _eUI.UpEs.WindTrC.EulerAngles = _directs[_e.WeatherE.WindC.DirectT];
                if (_e.CurPlayerIT == PlayerTypes.Second) _eUI.UpEs.WindTrC.EulerAngles += _rotationForOtherPlayer;
                _eUI.UpEs.WindTextC.TextUI.text = _e.WeatherE.WindC.Speed.ToString() + "/" + StartValues.MAX_SPEED_WIND;


            }
            else
            {
                _eUI.UpEs.ParentWindGOC.SetActive(false);
            }
        }
    }
}