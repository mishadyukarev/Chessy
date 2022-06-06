using Chessy.Common.Enum;
using Chessy.Common.Model.Component;

namespace Chessy.Common.Model.Entity
{
    public struct BookE
    {
        public PageBookTC PageBookTC;
        public bool IsOpenedBook;

        internal BookE(in PageBookTypes pageBookT, in bool isOpenedBook)
        {
            PageBookTC = new PageBookTC(pageBookT);
            IsOpenedBook = isOpenedBook;
        }
    }
}