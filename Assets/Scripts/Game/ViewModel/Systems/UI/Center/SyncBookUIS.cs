using Chessy.Game.Entity.View.UI.Center;

namespace Chessy.Game.System.View.UI.Center
{
    public struct SyncBookUIS
    {
        public void Sync(in BookUIE bookE, in EntitiesModel e)
        {
            bookE.ParenGOC.SetActive(e.ZoneInfoC.IsOpenedBook);

            if (e.ZoneInfoC.IsOpenedBook)
            {
                for (byte idx_page = 0; idx_page < Values.Values.MAX_PAGES; idx_page++)
                {
                    bookE.PageGOC(idx_page).SetActive(idx_page == e.CurrentIdxPageBook);
                }

                bookE.BackButtonC.SetActive(e.CurrentIdxPageBook != 0);
                bookE.NextButtonC.SetActive(e.CurrentIdxPageBook != Values.Values.MAX_PAGES - 1);


                bookE.LeftPageTextC.TextUI.text = e.CurrentIdxPageBook.ToString();
                bookE.RightPageTextC.TextUI.text = (e.CurrentIdxPageBook + 1).ToString();
            }
        }
    }
}