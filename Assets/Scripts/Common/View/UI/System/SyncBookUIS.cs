using Chessy.Common.Component;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.Model.Entity;

namespace Chessy.Game.System.View.UI.Center
{
    public struct SyncBookUIS
    {
        public void Sync(in BookUIE bookUIE, in BookE bookE)
        {
            bookUIE.ParenGOC.SetActive(bookE.IsOpenedBook);

            if (bookE.IsOpenedBook)
            {
                for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
                {
                    bookUIE.PageGOC(pageT).SetActive(pageT == bookE.PageBookTC.PageBookT);
                }

                bookUIE.BackButtonC.SetActive(bookE.PageBookTC.PageBookT != PageBookTypes.Main);
                bookUIE.NextButtonC.SetActive(bookE.PageBookTC.PageBookT < PageBookTypes.End - 1);

                bookUIE.LeftPageTextC.TextUI.text = ((int)bookE.PageBookTC.PageBookT).ToString() + "/" + bookE.PageBookTC.PageBookT;
                bookUIE.RightPageTextC.TextUI.text = ((int)bookE.PageBookTC.PageBookT + 1).ToString() + "/" + bookE.PageBookTC.PageBookT;
            }
        }
    }
}