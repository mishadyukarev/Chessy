using System.Linq;

namespace Chessy
{
    public struct IdxsArrayC
    {
        byte[] _idxs;

        public byte[] Idxs => (byte[])_idxs.Clone();
        public bool Any(byte idx) => _idxs.Any(i => i == idx);

        public IdxsArrayC(in byte[] idxs) => _idxs = idxs;
    }
}