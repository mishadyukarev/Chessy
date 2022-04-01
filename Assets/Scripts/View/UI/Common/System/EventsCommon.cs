using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.View.UI;

namespace Chessy.Common
{
    public sealed class EventsCommon
    {
        public EventsCommon(EntitiesViewUICommon eUICommon, EntitiesViewCommon eVCommon, EntitiesModelCommon eMCommon)
        {
            eUICommon.ShopE.ExitButtonC.AddListener(delegate { ExitShop(eUICommon.ShopE); });


            var bookE = eUICommon.BookE;
            bookE.ExitButtonC.AddListener(delegate
            {
                eMCommon.BookE.IsOpenedBook = false;
                eVCommon.Sound(ClipCommonTypes.CloseBook).Play();
            });

            bookE.NextButtonC.AddListener(delegate
            {
                if (eMCommon.BookE.PageBookTC.PageBookT < PageBookTypes.End - 1)
                {
                    eMCommon.BookE.PageBookTC.PageBookT++;
                    eVCommon.Sound(ClipCommonTypes.ShiftBookSheet).Play();
                }
            });

            bookE.BackButtonC.AddListener(delegate
            {
                if (eMCommon.BookE.PageBookTC.PageBookT > 0)
                {
                    eMCommon.BookE.PageBookTC.PageBookT--;
                    eVCommon.Sound(ClipCommonTypes.ShiftBookSheet).Play();
                }
            });


            eUICommon.SettingsE.ExitButtonC.AddListener(delegate
            {
                eMCommon.IsOpenSettings = false;
                //eVCommon.Sound(ClipTypes.Click).Play();
            });


        }

        void ExitShop(in ShopUIE shopUIE)
        {
            shopUIE.ShopZoneGOC.SetActive(false);
        }
    }
}