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
            _bookUIE.ParenGOC.TrySetActive(_bookC.IsOpenedBook());

            if (_bookC.IsOpenedBook())
            {
                for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
                {
                    _bookUIE.PageGOC(pageT).TrySetActive(pageT == _bookC.OpenedNowPageBookT);
                }

                _bookUIE.BackButtonC.SetActive(_bookC.OpenedNowPageBookT != PageBookTypes.Main);
                _bookUIE.NextButtonC.SetActive(_bookC.OpenedNowPageBookT < PageBookTypes.End - 1);

                _bookUIE.LeftPageTextC.TextUI.text = ((int)_bookC.OpenedNowPageBookT).ToString() + "/" + _bookC.OpenedNowPageBookT;
                _bookUIE.RightPageTextC.TextUI.text = ((int)_bookC.OpenedNowPageBookT + 1).ToString() + "/" + _bookC.OpenedNowPageBookT;
            }
        }
    }
}