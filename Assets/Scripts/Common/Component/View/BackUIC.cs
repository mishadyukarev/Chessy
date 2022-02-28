using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public struct BackUIC
    {
        static Image _frontBackgroundImage;
        static Image _backBackgroundImage;

        public BackUIC(bool isOnline, RectTransform zoneRectTrans)
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

        public static void SetActiveFrontImage(bool isActive) => _frontBackgroundImage.gameObject.SetActive(isActive);
    }
}
