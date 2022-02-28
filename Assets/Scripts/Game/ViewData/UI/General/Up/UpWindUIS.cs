using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class UpWindUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal UpWindUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            switch (E.DirectWindTC.Direct)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Right: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(); break;
                case DirectTypes.Left: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 180); break;
                case DirectTypes.Up: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 90); break;
                case DirectTypes.Down: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 270); break;
                case DirectTypes.UpRight: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 45); break;
                case DirectTypes.UpLeft: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 135); break;
                case DirectTypes.DownRight: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 315); break;
                case DirectTypes.DownLeft: UIEs.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 225); break;
                default: throw new Exception();
            }

            if (E.CurPlayerITC.Player == PlayerTypes.Second)
                UIEs.UpEs.WindTrC.EulerAngles += new Vector3(0, 0, 180);
        }
    }
}