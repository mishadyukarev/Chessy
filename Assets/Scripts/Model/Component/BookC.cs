﻿using Chessy.Model.Enum;

namespace Chessy
{
    public struct BookC
    {
        public PageBookTypes OpenedNowPageBookT { get; internal set; }
        public PageBookTypes WasOpenedLastPageBookT { get; internal set; }
    }
}