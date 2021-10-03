using System;
using System.Collections.Generic;
using static Scripts.Game.CellValues;

namespace Scripts.Game
{
    internal static class CellSpaceSupport
    {
        internal static List<byte[]> TryGetXyAround(byte[] xyStartCell)
        {
            var xyAvailableCells = new List<byte[]>();
            var xyResultCell = new byte[XY_FOR_ARRAY];

            for (byte i = 1; i < (byte)DirectTypes.LeftDown + 1; i++)
            {
                var xyDirectCell = GetXyDirect((DirectTypes)i);

                xyResultCell[X] = (byte)(xyStartCell[X] + xyDirectCell[X]);
                xyResultCell[Y] = (byte)(xyStartCell[Y] + xyDirectCell[Y]);

                xyAvailableCells.Add((byte[])xyResultCell.Clone());
            }

            return xyAvailableCells;

        }
        internal static byte[] GetXyCellByDirect(byte[] xyStartCell, DirectTypes directType)
        {
            var xyResultCell = new byte[XY_FOR_ARRAY];

            var xyDirectCell = GetXyDirect(directType);

            xyResultCell[0] = (byte)(xyStartCell[0] + xyDirectCell[0]);
            xyResultCell[1] = (byte)(xyStartCell[1] + xyDirectCell[1]);

            return xyResultCell;
        }
        internal static sbyte[] GetXyDirect(DirectTypes direct)
        {
            var xyDirectCell = new sbyte[XY_FOR_ARRAY];

            switch (direct)
            {
                case DirectTypes.None:
                    throw new Exception();

                case DirectTypes.Right:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = 0;
                    break;

                case DirectTypes.Left:
                    xyDirectCell[X] = -1;
                    xyDirectCell[Y] = 0;
                    break;

                case DirectTypes.Up:
                    xyDirectCell[X] = 0;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.Down:
                    xyDirectCell[X] = 0;
                    xyDirectCell[Y] = -1;
                    break;

                case DirectTypes.RightUp:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.LeftUp:
                    xyDirectCell[X] = -1;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.RightDown:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = -1;
                    break;

                case DirectTypes.LeftDown:
                    xyDirectCell[X] = -1;
                    xyDirectCell[Y] = -1;
                    break;

                default:
                    throw new Exception();
            }

            return xyDirectCell;
        }
    }
}
