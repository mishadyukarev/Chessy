using Chessy.Common.Enum;
using Chessy.Common.Model.Component;

namespace Chessy.Common.Model.Entity
{
    public class BookE
    {
        public PageBookTC PageBookTC;
        public bool IsOpenedBook;

        public BookE(in PageBookTypes pageBookT, in bool isOpenedBook)
        {
            PageBookTC.PageBookT = pageBookT;
            IsOpenedBook = isOpenedBook;
        }
    }
}