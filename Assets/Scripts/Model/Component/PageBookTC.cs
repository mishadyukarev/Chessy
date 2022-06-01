using Chessy.Common.Enum;

namespace Chessy.Common.Model.Component
{
    public struct PageBookTC
    {
        public PageBookTypes PageBookT;

        internal PageBookTC(in PageBookTypes pageBookT) => PageBookT = pageBookT;
    }
}