using Chessy.Common.Entity;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using Chessy.Common.Model;

namespace Chessy.Common.View.UI
{
    sealed class SyncBookUIS : SyncUISystem
    {
        readonly BookUIE _bookUIE;

        internal SyncBookUIS(in BookUIE bookUIE, in EntitiesModelCommon eMC) : base(eMC)
        {
            _bookUIE = bookUIE;
        }

        internal override void Sync()
        {
            _bookUIE.ParenGOC.SetActive(e.IsOpenedBook);

            if (e.IsOpenedBook)
            {
                for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
                {
                    _bookUIE.PageGOC(pageT).SetActive(pageT == e.PageBookT);
                }

                _bookUIE.BackButtonC.SetActive(e.PageBookT != PageBookTypes.Main);
                _bookUIE.NextButtonC.SetActive(e.PageBookT < PageBookTypes.End - 1);

                _bookUIE.LeftPageTextC.TextUI.text = ((int)e.PageBookT).ToString() + "/" + e.PageBookT;
                _bookUIE.RightPageTextC.TextUI.text = ((int)e.PageBookT + 1).ToString() + "/" + e.PageBookT;
            }
        }
    }
}