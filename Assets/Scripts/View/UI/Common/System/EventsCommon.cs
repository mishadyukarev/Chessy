using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.Model.System;
using Chessy.Common.View.UI;

namespace Chessy.Common
{
    public sealed class EventsCommon
    {
        public EventsCommon(SystemsModelCommon sMC, EntitiesViewUICommon eUICommon, EntitiesViewCommon eVCommon, EntitiesModelCommon eMCommon)
        {
            eUICommon.ShopE.ExitButtonC.AddListener(() => { eMCommon.ShopC.IsOpenedShopZone = false; });


            var bookE = eUICommon.BookE;
            bookE.ExitButtonC.AddListener(() =>
            {
                eMCommon.IsOpenedBook = false;
                eVCommon.Sound(ClipCommonTypes.CloseBook).Play();
            });

            bookE.NextButtonC.AddListener(() =>
            {
                if (eMCommon.PageBookT < PageBookTypes.End - 1)
                {
                    eMCommon.PageBookTC.PageBookT++;
                    eVCommon.Sound(ClipCommonTypes.ShiftBookSheet).Play();
                }
            });

            bookE.BackButtonC.AddListener(() =>
            {
                if (eMCommon.PageBookT > 0)
                {
                    eMCommon.PageBookTC.PageBookT--;
                    eVCommon.Sound(ClipCommonTypes.ShiftBookSheet).Play();
                }
            });


            eUICommon.SettingsE.ExitButtonC.AddListener(() =>
            {
                eMCommon.IsOpenSettings = false;
                //eVCommon.Sound(ClipTypes.Click).Play();
            });


            eUICommon.ShopE.BuyButtonC.AddListener(sMC.BuyProductS.Buy);

        }
    }
}