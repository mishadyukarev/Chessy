using Chessy.Common.Component;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;

namespace Chessy.Game.System.View.UI.Center
{
    public struct SyncBookUIS
    {
        public void Sync(in BookUIE bookE, in BookC bookC)
        {
            bookE.ParenGOC.SetActive(bookC.IsOpenedBook);

            if (bookC.IsOpenedBook)
            {
                for (var pageT = PageBoookTypes.None + 1; pageT < PageBoookTypes.End; pageT++)
                {
                    bookE.PageGOC(pageT).SetActive(pageT == bookC.PageBookT);
                }

                bookE.BackButtonC.SetActive(bookC.PageBookT != PageBoookTypes.Main);
                bookE.NextButtonC.SetActive(bookC.PageBookT < PageBoookTypes.End - 1);

                bookE.LeftPageTextC.TextUI.text = ((int)bookC.PageBookT).ToString() + "/" + bookC.PageBookT;
                bookE.RightPageTextC.TextUI.text = ((int)bookC.PageBookT + 1).ToString() + "/" + bookC.PageBookT;
            }
        }
    }
}