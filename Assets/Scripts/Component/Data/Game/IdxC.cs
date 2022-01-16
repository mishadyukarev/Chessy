﻿using System;

namespace Game.Game
{
    public struct IdxC : ISelectedIdx, ICurrectIdx, IPreVisionIdx, ICloud
    {
        public byte Idx;

        public bool Is(params byte[] idxs)
        {
            if (idxs == default) throw new Exception();
            if (idxs.Length == 0) throw new Exception();

            foreach (var idx in idxs)
            {
                if (idx == Idx) return true;
            }
            return false;
        }

        public IdxC(in byte idx) => Idx = idx;

        public void Reset() => Idx = 0;
    }
}