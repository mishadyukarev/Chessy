using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellRiverDataC
    {
        public RiverTypes RiverType { get; set; }
        public List<DirectTypes> DirectTypes { get; private set; }
        public List<byte> IdxsNextCells { get; private set; }

        public bool HaveNearRiver => RiverType != default;

        public CellRiverDataC(List<byte> list) : this()
        {
            DirectTypes = new List<DirectTypes>();
            IdxsNextCells = list;
        }
    }
}