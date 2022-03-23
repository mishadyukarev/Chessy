using Chessy.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public readonly struct BackUIE
    {
        public readonly ImageUIC FrontImageC;
        public readonly ImageUIC BackImageC;

        public BackUIE(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                FrontImageC = new ImageUIC(zoneRectTrans.transform.Find("RightZone_Image").GetComponent<Image>());
                BackImageC = new ImageUIC(zoneRectTrans.transform.Find("Back_Image").GetComponent<Image>());
            }
            else
            {
                FrontImageC = new ImageUIC(zoneRectTrans.transform.Find("LeftZone_Image").GetComponent<Image>());
                BackImageC = new ImageUIC(zoneRectTrans.transform.Find("Back_Image").GetComponent<Image>());
            }
        }
    }
}
