using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.Menu
{
    internal struct BackgroundMenuUICom
    {
        private Image _frontBackgroundImage;
        private Image _backBackgroundImage;

        internal BackgroundMenuUICom(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                _frontBackgroundImage = zoneRectTrans.transform.Find("RightZone_Image").GetComponent<Image>();
                _backBackgroundImage = zoneRectTrans.transform.Find("Back_Image").GetComponent<Image>();
            }
            else
            {
                _frontBackgroundImage = zoneRectTrans.transform.Find("LeftZone_Image").GetComponent<Image>();
                _backBackgroundImage = zoneRectTrans.transform.Find("Back_Image").GetComponent<Image>();
            }
        }

        internal void SetActiveFrontImage(bool isActive) => _frontBackgroundImage.gameObject.SetActive(isActive);
    }
}
