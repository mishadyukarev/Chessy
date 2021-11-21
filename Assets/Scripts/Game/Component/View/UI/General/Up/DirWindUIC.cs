using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct DirWindUIC
    {
        private static Image _image;

        public DirWindUIC(Transform upZone_Trans)
        {
            _image = upZone_Trans.Find("WindZone").Find("Direct_Image").GetComponent<Image>();
        }

        public static void SetEulerRot(PlayerTypes playerType, DirectTypes directType)
        {
            switch (directType)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Right: _image.rectTransform.eulerAngles = new Vector3(); break;
                case DirectTypes.Left: _image.rectTransform.eulerAngles = new Vector3(0, 0, 180); break;
                case DirectTypes.Up: _image.rectTransform.eulerAngles = new Vector3(0, 0, 90); break;
                case DirectTypes.Down: _image.rectTransform.eulerAngles = new Vector3(0, 0, 270); break;
                case DirectTypes.UpRight: _image.rectTransform.eulerAngles = new Vector3(0, 0, 45); break;
                case DirectTypes.UpLeft: _image.rectTransform.eulerAngles = new Vector3(0, 0, 135); break;
                case DirectTypes.DownRight: _image.rectTransform.eulerAngles = new Vector3(0, 0, 315); break;
                case DirectTypes.DownLeft: _image.rectTransform.eulerAngles = new Vector3(0, 0, 225); break;
                default: throw new Exception();
            }

            if (playerType == PlayerTypes.Second)
                _image.rectTransform.eulerAngles += new Vector3(0, 0, 180);
        }
    }
}