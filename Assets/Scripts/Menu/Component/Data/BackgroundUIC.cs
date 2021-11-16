using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public struct BackgroundUIC
    {
        private Image _frontBackgroundImage;
        private Image _backBackgroundImage;

        public BackgroundUIC(bool isOnline, RectTransform zoneRectTrans)
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

        public void SetActiveFrontImage(bool isActive) => _frontBackgroundImage.gameObject.SetActive(isActive);
    }
}
