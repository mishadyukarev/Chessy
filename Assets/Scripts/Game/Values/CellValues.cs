using System;

namespace Game.Game
{
    public struct CellValues
    {
        public static byte CellCount(XyzTypes xyz)
        {
            switch (xyz)
            {
                case XyzTypes.None: throw new Exception();
                case XyzTypes.X: return 15;
                case XyzTypes.Y: return 11;
                case XyzTypes.Z: throw new Exception();
                default: throw new Exception();
            }
        }
        public static byte AMOUNT_ALL_CELLS = (byte)(CellCount(XyzTypes.X) * CellCount(XyzTypes.Y));

        public const byte XY_FOR_ARRAY = 2;
        public const byte X = 0;
        public const byte Y = 1;
    }
}
