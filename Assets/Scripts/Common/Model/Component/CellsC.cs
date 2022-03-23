﻿namespace Chessy.Common
{
    public struct CellsC
    {
        public byte Current;
        public byte Selected;
        public byte PreviousSelected;
        public byte PreviousVision;

        public bool IsSelectedCell => Selected != 0;
    }
}