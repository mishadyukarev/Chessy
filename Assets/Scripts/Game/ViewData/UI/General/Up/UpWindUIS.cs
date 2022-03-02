using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class UpWindUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal UpWindUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            switch (E.DirectWindTC.Direct)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Right: UIE.UpEs.WindTrC.EulerAngles = new Vector3(); break;
                case DirectTypes.Left: UIE.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 180); break;
                case DirectTypes.Up: UIE.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 90); break;
                case DirectTypes.Down: UIE.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 270); break;
                case DirectTypes.UpRight: UIE.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 45); break;
                case DirectTypes.UpLeft: UIE.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 135); break;
                case DirectTypes.DownRight: UIE.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 315); break;
                case DirectTypes.DownLeft: UIE.UpEs.WindTrC.EulerAngles = new Vector3(0, 0, 225); break;
                default: throw new Exception();
            }

            if (E.CurPlayerITC.Player == PlayerTypes.Second)
                UIE.UpEs.WindTrC.EulerAngles += new Vector3(0, 0, 180);
        }
    }
}