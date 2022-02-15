using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct IdxsC
    {
        readonly List<byte> _idxs;

        public List<byte> Idxs
        {
            get
            {
                var idxs = new List<byte>();
                foreach (var idx in _idxs) idxs.Add(idx);
                return idxs;
            }
        }
        public bool Contains(in byte idx) => _idxs.Contains(idx);

        public IdxsC(in List<byte> idxs) => _idxs = idxs;

        public void Add(in byte idx) => _idxs.Add(idx);
        public void Clear() => _idxs.Clear();
    }
}