using System;

namespace Game.Game
{
    public struct ProtectionC : ITWCellE
    {
        public int Protection { get; internal set; }
        public bool Have => Protection > 0;

        internal void Take(in int taking = 1)
        {
            if (taking <= 0) throw new Exception("Need positive number");

            Protection -= taking;
        }
        internal void Set(ProtectionC shieldC) => Protection = shieldC.Protection;
        internal void Reset() => Protection = 0;
    }
}