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
                eVCommon.Sound(ClipCommonTypes.CloseBook).Play();

                eMCommon.NeedUpdateView = true;
            });

            bookE.NextButtonC.AddListener(() =>
            {
                if (eMCommon.OpenedNowPageBookT < PageBookTypes.End - 1)
                {
                    eMCommon.OpenedNowPageBookT++;
                    eVCommon.Sound(ClipCommonTypes.ShiftBookSheet).Play();

                    eMCommon.NeedUpdateView = true;
                }
            });

            bookE.BackButtonC.AddListener(() =>
            {
                if (eMCommon.OpenedNowPageBookT > 0)
                {
                    eMCommon.OpenedNowPageBookT--;
                    eVCommon.Sound(ClipCommonTypes.ShiftBookSheet).Play();

                    eMCommon.NeedUpdateView = true;
                }
            });


            eUICommon.SettingsE.ExitButtonC.AddListener(() =>
            {
                eMCommon.SettingsC.IsOpenedBarWithSettings = false;
                //eVCommon.Sound(ClipTypes.Click).Play();

                eMCommon.NeedUpdateView = true;
            });


            eUICommon.ShopE.BuyButtonC.AddListener(sMC.BuyPremiumProduct);

        }
    }
}