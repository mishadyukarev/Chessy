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
            _bookUIE.ParenGOC.TrySetActive(_e.BookC.IsOpenedBook());

            if (_e.BookC.IsOpenedBook())
            {
                for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
                {
                    _bookUIE.PageGOC(pageT).TrySetActive(pageT == _e.OpenedNowPageBookT);
                }

                _bookUIE.BackButtonC.SetActive(_e.OpenedNowPageBookT != PageBookTypes.Main);
                _bookUIE.NextButtonC.SetActive(_e.OpenedNowPageBookT < PageBookTypes.End - 1);

                _bookUIE.LeftPageTextC.TextUI.text = ((int)_e.OpenedNowPageBookT).ToString() + "/" + _e.OpenedNowPageBookT;
                _bookUIE.RightPageTextC.TextUI.text = ((int)_e.OpenedNowPageBookT + 1).ToString() + "/" + _e.OpenedNowPageBookT;
            }
        }
    }
}