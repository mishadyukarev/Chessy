﻿namespace Chessy.Common
{
    public struct CellsC
    {
        public byte Current { get; internal set; }
        public byte Selected { get; internal set; }
        public byte PreviousSelected { get; internal set; }
        public byte PreviousVision { get; internal set; }

        public bool IsSelectedCell => Selected != 0;
    }
}