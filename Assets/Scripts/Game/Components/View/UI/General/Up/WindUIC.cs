using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct WindUIC
    {
        private static Image _directWind_Image;

        public WindUIC(Transform upZone_Trans)
        {
            _directWind_Image = upZone_Trans.Find("WindZone").Find("Direct_Image").GetComponent<Image>();
        }

        public static void SetEulerRot(PlayerTypes playerType, DirectTypes directType)
        {
            switch (directType)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Right: _directWind_Image.rectTransform.eulerAngles = new Vector3(); break;
                case DirectTypes.Left: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 180); break;
                case DirectTypes.Up: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 90); break;
                case DirectTypes.Down: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 270); break;
                case DirectTypes.UpRight: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 45); break;
                case DirectTypes.UpLeft: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 135); break;
                case DirectTypes.DownRight: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 315); break;
                case DirectTypes.DownLeft: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 225); break;
                default: throw new Exception();
            }

            if (playerType == PlayerTypes.Second)
                _directWind_Image.rectTransform.eulerAngles += new Vector3(0, 0, 180);
        }
    }
}