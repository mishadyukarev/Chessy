using Chessy.Common.Enum;

namespace Chessy.Common.Component
{
    public struct BookC
    {
        public PageBoookTypes PageBookT;
        public bool IsOpenedBook;

        public BookC(in PageBoookTypes pageBookT, in bool isOpen)
        {
            PageBookT = pageBookT;
            IsOpenedBook = isOpen;
        }
    }
}