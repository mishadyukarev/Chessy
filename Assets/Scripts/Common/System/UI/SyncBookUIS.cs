using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;

namespace Chessy.Game.System.View.UI.Center
{
    public struct SyncBookUIS
    {
        public void Sync(in BookUIE bookE, in EntitiesModel eC)
        {
            bookE.ParenGOC.SetActive(eC.IsOpenedBook);

            if (eC.IsOpenedBook)
            {
                for (var pageT = PageBoookTypes.None + 1; pageT < PageBoookTypes.End; pageT++)
                {
                    bookE.PageGOC(pageT).SetActive(pageT == eC.CurrentPageBookT);
                }

                bookE.BackButtonC.SetActive(eC.CurrentPageBookT != PageBoookTypes.Main);
                bookE.NextButtonC.SetActive(eC.CurrentPageBookT < PageBoookTypes.End - 1);

                bookE.LeftPageTextC.TextUI.text = ((int)eC.CurrentPageBookT).ToString() + "/" + eC.CurrentPageBookT;
                bookE.RightPageTextC.TextUI.text = ((int)eC.CurrentPageBookT + 1).ToString() + "/" + eC.CurrentPageBookT;
            }
        }
    }
}