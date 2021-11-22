using System;
using System.Collections.Generic;
using static Game.Game.CellValuesC;

namespace Game.Game
{
    public static class CellSpace
    {
        public static List<byte[]> GetXyAround(byte[] xyStartCell)
        {
            var xyAvailableCells = new List<byte[]>();
            var xyResultCell = new byte[XY_FOR_ARRAY];

            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                var xyDirectCell = GetXyDirect(dir);

                xyResultCell[X] = (byte)(xyStartCell[X] + xyDirectCell[X]);
                xyResultCell[Y] = (byte)(xyStartCell[Y] + xyDirectCell[Y]);

                xyAvailableCells.Add((byte[])xyResultCell.Clone());
            }

            return xyAvailableCells;
        }
        public static void TryGetXyAround(byte[] xyStartCell, out Dictionary<DirectTypes, byte[]> directs)
        {
            directs = new Dictionary<DirectTypes, byte[]>();
            var xyResultCell = new byte[XY_FOR_ARRAY];

            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                var xyDirectCell = GetXyDirect(dir);

                xyResultCell[X] = (byte)(xyStartCell[X] + xyDirectCell[X]);
                xyResultCell[Y] = (byte)(xyStartCell[Y] + xyDirectCell[Y]);

                directs.Add(dir, (byte[])xyResultCell.Clone());
            }
        }

        public static DirectTypes GetDirect(byte[] xy_from, byte[] xy_to)
        {
            TryGetXyAround(xy_from, out var xy_around);

            foreach (var item_1 in xy_around)
            {
                if (item_1.Value.Compare(xy_to)) return item_1.Key;
            }

            throw new Exception();
        }


        public static byte[] GetXyCellByDirect(byte[] xyStartCell, DirectTypes directType)
        {
            var xyResultCell = new byte[XY_FOR_ARRAY];

            var xyDirectCell = GetXyDirect(directType);

            xyResultCell[X] = (byte)(xyStartCell[X] + xyDirectCell[X]);
            xyResultCell[Y] = (byte)(xyStartCell[Y] + xyDirectCell[Y]);

            return xyResultCell;
        }
        public static sbyte[] GetXyDirect(DirectTypes direct)
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

                case DirectTypes.UpRight:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.UpLeft:
                    xyDirectCell[X] = -1;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.DownRight:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = -1;
                    break;

                case DirectTypes.DownLeft:
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
