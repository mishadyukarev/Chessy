﻿using UnityEngine;
using UnityEngine.UI;
using Chessy.View.UI.Entity;
using Chessy.View.UI.Component;

namespace Chessy.Model
{
    public struct SunsUIE
    {
        public ImageUIC RightSun;
        public ImageUIC LeftSun;

        internal SunsUIE(in Transform upZone)
        {
            RightSun = new ImageUIC(upZone.Find("SunRight").Find("Image").GetComponent<Image>());
            LeftSun = new ImageUIC(upZone.Find("SunLeft").Find("Image").GetComponent<Image>());
        }
    }
}