using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class SyncBookUIS : SyncUISystem
    {
        readonly BookUIE _bookUIE;

        internal SyncBookUIS(in BookUIE bookUIE, in EntitiesModel eM) : base(eM)
        {
            _bookUIE = bookUIE;
        }

        internal override void Sync()
        {
            _bookUIE.ParenGOC.TrySetActive(bookC.IsOpenedBook());

            if (bookC.IsOpenedBook())
            {
                for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
                {
                    _bookUIE.PageGOC(pageT).TrySetActive(pageT == bookC.OpenedNowPageBookT);
                }

                _bookUIE.BackButtonC.SetActive(bookC.OpenedNowPageBookT != PageBookTypes.Main);
                _bookUIE.NextButtonC.SetActive(bookC.OpenedNowPageBookT < PageBookTypes.End - 1);

                _bookUIE.LeftPageTextC.TextUI.text = ((int)bookC.OpenedNowPageBookT).ToString() + "/" + bookC.OpenedNowPageBookT;
                _bookUIE.RightPageTextC.TextUI.text = ((int)bookC.OpenedNowPageBookT + 1).ToString() + "/" + bookC.OpenedNowPageBookT;
            }
        }
    }
}