﻿using Chessy.Common.Entity.View.UI;
using Chessy.Common.View.UI.Entity;
using UnityEngine;

namespace Chessy.Common.View.UI
{
    public sealed class EntitiesViewUICommon
    {
        public readonly CanvasUIE CanvasE;
        public readonly BookUIE BookE;
        public readonly ShopUIE ShopE;
        public readonly SettingsUIE SettingsE;

        public EntitiesViewUICommon(in Canvas canvas, in Transform commonZone)
        {
            canvas.name = "Canvas";
            canvas.transform.SetParent(commonZone);
            CanvasE = new CanvasUIE(canvas);


            var commonZoneUI = canvas.transform.Find("Common+");

            SettingsE = new SettingsUIE(commonZoneUI.Find("Settings+"));
            BookE = new BookUIE(commonZoneUI);
            ShopE = new ShopUIE(commonZoneUI.Find("ShopZone"));
        }
    }
}