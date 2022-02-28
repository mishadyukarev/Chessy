using System.Collections.Generic;
using System.Linq;

namespace Chessy.Game
{
    public readonly struct IdxsC
    {
        readonly HashSet<byte> _idxs;

        public bool HaveAny => _idxs.Count > 0;

        public HashSet<byte> Idxs
        {
            get
            {
                var idxs = new HashSet<byte>();
                foreach (var idx in _idxs) idxs.Add(idx);
                return idxs;
            }
        }
        public byte IdxFirst => Idxs.First();
        public bool Contains(in byte idx) => _idxs.Contains(idx);

        public IdxsC(in HashSet<byte> idxs) => _idxs = idxs;

        public void Add(in byte idx) => _idxs.Add(idx);
        public void Clear() => _idxs.Clear();

    }
}