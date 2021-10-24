using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct WindZoneUICom
    {
        private Image _directWind_Image;

        internal WindZoneUICom(Transform upZone_Trans)
        {
            _directWind_Image = upZone_Trans.Find("WindZone").Find("Direct_Image").GetComponent<Image>();
        }

        internal void SetEulerRot(DirectTypes directType)
        {
            switch (directType)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Right: _directWind_Image.rectTransform.eulerAngles = new Vector3(); return;
                case DirectTypes.Left: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 180); return;
                case DirectTypes.Up: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 90); return;
                case DirectTypes.Down: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 270); return;
                case DirectTypes.RightUp: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 45); return;
                case DirectTypes.LeftUp: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 135); return;
                case DirectTypes.RightDown: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 315); return;
                case DirectTypes.LeftDown: _directWind_Image.rectTransform.eulerAngles = new Vector3(0, 0, 225); return;
                default: throw new Exception();
            }
        }
    }
}