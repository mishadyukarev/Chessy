using System;

namespace Game.Game
{
    public struct ProtectionC : ITWCellE
    {
        public int Protection;
        public bool Have => Protection > 0;

        public void Take(in int taking = 1)
        {
            if (taking <= 0) throw new Exception("Need positive number");

            Protection -= taking;
        }
        public void Reset() => Protection = 0;
    }
}