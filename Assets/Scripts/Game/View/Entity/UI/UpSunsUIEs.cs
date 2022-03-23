using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct UpSunsUIEs
    {
        public ImageUIC RightSun;
        public ImageUIC LeftSun;

        public UpSunsUIEs(in Transform upZone)
        {
            RightSun = new ImageUIC(upZone.Find("SunRight").Find("Image").GetComponent<Image>());
            LeftSun = new ImageUIC(upZone.Find("SunLeft").Find("Image").GetComponent<Image>());
        }
    }
}