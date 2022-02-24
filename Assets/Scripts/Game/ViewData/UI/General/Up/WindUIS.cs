using System;
using UnityEngine;
using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    sealed class WindUIS : SystemUIAbstract, IEcsRunSystem
    {
        public WindUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            switch (E.DirectWindTC.Direct)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Right: DirectWind<TransformVC>().EulerAngles = new Vector3(); break;
                case DirectTypes.Left: DirectWind<TransformVC>().EulerAngles = new Vector3(0, 0, 180); break;
                case DirectTypes.Up: DirectWind<TransformVC>().EulerAngles = new Vector3(0, 0, 90); break;
                case DirectTypes.Down: DirectWind<TransformVC>().EulerAngles = new Vector3(0, 0, 270); break;
                case DirectTypes.UpRight: DirectWind<TransformVC>().EulerAngles = new Vector3(0, 0, 45); break;
                case DirectTypes.UpLeft: DirectWind<TransformVC>().EulerAngles = new Vector3(0, 0, 135); break;
                case DirectTypes.DownRight: DirectWind<TransformVC>().EulerAngles = new Vector3(0, 0, 315); break;
                case DirectTypes.DownLeft: DirectWind<TransformVC>().EulerAngles = new Vector3(0, 0, 225); break;
                default: throw new Exception();
            }

            if (E.CurPlayerITC.Player == PlayerTypes.Second)
                DirectWind<TransformVC>().EulerAngles += new Vector3(0, 0, 180);
        }
    }
}