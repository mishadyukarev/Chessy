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
                eMCommon.BookC.IsOpenedBook = false;
                eVCommon.Sound(ClipTypes.CloseBook).Play();
            });

            bookE.NextButtonC.AddListener(delegate
            {
                if (eMCommon.BookC.PageBookT < PageBoookTypes.End - 1)
                {
                    eMCommon.BookC.PageBookT++;
                    eVCommon.Sound(ClipTypes.ShiftBookSheet).Play();
                }
            });

            bookE.BackButtonC.AddListener(delegate
            {
                if (eMCommon.BookC.PageBookT > 0)
                {
                    eMCommon.BookC.PageBookT--;
                    eVCommon.Sound(ClipTypes.ShiftBookSheet).Play();
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
            shopUIE.ExitButtonC.GameObject.SetActive(false);
        }
    }
}