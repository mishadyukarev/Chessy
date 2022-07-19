using Chessy.Model.Enum;
using System;

namespace Chessy.Model.Component
{
    public struct BookC
    {
        public PageBookTypes OpenedNowPageBookT { get; internal set; }
        public PageBookTypes WasOpenedBookT { get; internal set; }

        public bool IsOpenedBook => OpenedNowPageBookT != PageBookTypes.None;
        public bool WasOpenedAnytimeBook => WasOpenedBookT != PageBookTypes.None;

        internal bool TryCloseBook()
        {
            if (IsOpenedBook)
            {
                WasOpenedBookT = OpenedNowPageBookT;
                OpenedNowPageBookT = default;

                return true;
            }
            else return false;
        }
        internal bool TryOpenBook()
        {
            if (!IsOpenedBook)
            {
                OpenedNowPageBookT = WasOpenedAnytimeBook ? WasOpenedBookT : (PageBookTypes)1;
                return true;
            }
            else return false;
        }

        internal void CloseBook()
        {
            if (!IsOpenedBook) throw new Exception("Book's closed");

            WasOpenedBookT = OpenedNowPageBookT;
            OpenedNowPageBookT = default;
        }
        internal void OpenBook()
        {
            if (IsOpenedBook) throw new Exception("Book's opened");

            OpenedNowPageBookT = WasOpenedAnytimeBook ? WasOpenedBookT : (PageBookTypes)1;
        }
    }
}