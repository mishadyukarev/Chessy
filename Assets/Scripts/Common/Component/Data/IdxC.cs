using System;

namespace Game.Game
{
    public class IdxC
    {
        public byte Idx;

        public bool Is(params byte[] idxs)
        {
            if (idxs == default) throw new Exception();
            if (idxs.Length == 0) throw new Exception();

            foreach (var idx in idxs) if (idx == Idx) return true;
            return false;
        }


        public IdxC() { }
        public IdxC(in byte idx) => Idx = idx;


        public void Set(in byte idx) => Idx = idx;
    }
}