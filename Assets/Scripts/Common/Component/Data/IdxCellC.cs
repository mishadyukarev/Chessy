﻿using System;

namespace Chessy.Game
{
    public struct IdxCellC
    {
        public byte Idx;

        public bool Is(params byte[] idxs)
        {
            if (idxs == default) throw new Exception();
            if (idxs.Length == 0) throw new Exception();

            foreach (var idx in idxs) if (idx == Idx) return true;
            return false;
        }


        public IdxCellC(in byte idx) => Idx = idx;
    }
}