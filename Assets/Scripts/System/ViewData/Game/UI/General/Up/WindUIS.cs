using System;
using UnityEngine;
using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    struct WindUIS : IEcsRunSystem
    {
        public void Run()
        {
            switch (Entities.WindE.DirectWind.Direct)
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

            if (Entities.WhoseMove.CurPlayerI == PlayerTypes.Second)
                DirectWind<TransformVC>().EulerAngles += new Vector3(0, 0, 180);
        }
    }
}