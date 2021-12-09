using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct DirWindUIC
    {
        Image Image => EntityUIPool.DirectWindUp<ImageUIC>().Image;

        public void SetEulerRot(PlayerTypes playerType, DirectTypes directType)
        {
            switch (directType)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Right: Image.rectTransform.eulerAngles = new Vector3(); break;
                case DirectTypes.Left: Image.rectTransform.eulerAngles = new Vector3(0, 0, 180); break;
                case DirectTypes.Up: Image.rectTransform.eulerAngles = new Vector3(0, 0, 90); break;
                case DirectTypes.Down: Image.rectTransform.eulerAngles = new Vector3(0, 0, 270); break;
                case DirectTypes.UpRight: Image.rectTransform.eulerAngles = new Vector3(0, 0, 45); break;
                case DirectTypes.UpLeft: Image.rectTransform.eulerAngles = new Vector3(0, 0, 135); break;
                case DirectTypes.DownRight: Image.rectTransform.eulerAngles = new Vector3(0, 0, 315); break;
                case DirectTypes.DownLeft: Image.rectTransform.eulerAngles = new Vector3(0, 0, 225); break;
                default: throw new Exception();
            }

            if (playerType == PlayerTypes.Second)
                Image.rectTransform.eulerAngles += new Vector3(0, 0, 180);
        }
    }
}