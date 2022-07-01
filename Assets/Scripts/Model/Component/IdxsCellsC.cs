using System.Collections.Generic;
using System.Linq;
namespace Chessy.Model
{
    public readonly struct IdxsCellsC
    {
        readonly List<byte> _idxs;

        public bool HaveAny => _idxs.Count > 0;

        public List<byte> Idxs
        {
            get
            {
                var idxs = new List<byte>();
                foreach (var idx in _idxs) idxs.Add(idx);
                return idxs;
            }
        }
        public byte[] IdxsByteClone
        {
            get
            {
                var idxs = new byte[_idxs.Count];
                var i = 0;
                foreach (var idx in _idxs) idxs[i] = _idxs[i++];
                return idxs;
            }
        }
        public byte IdxFirst => Idxs.First();
        public bool Contains(in byte idx) => _idxs.Contains(idx);

        internal IdxsCellsC(in HashSet<byte> idxs)
        {
            _idxs = new List<byte>();
        }

        internal void Add(in byte idx) => _idxs.Add(idx);
        internal void Clear() => _idxs.Clear();
        internal void Sync(in byte[] idxs)
        {
            _idxs.Clear();
            foreach (var item in idxs) _idxs.Add(item);
        }
    }
}