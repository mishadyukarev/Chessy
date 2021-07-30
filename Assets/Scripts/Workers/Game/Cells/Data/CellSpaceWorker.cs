using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.Workers.Cell
{
    internal class CellSpaceWorker : MainGeneralWorker
    {
        internal static List<int[]> TryGetXYAround(int[] xyStartCell)
        {
            var xyAvailableCells = new List<int[]>();
            var xyResultCell = new int[XY_FOR_ARRAY];

            for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
            {
                var xyDirectCell = GetXYDirect((DirectTypes)i);

                xyResultCell[X] = xyStartCell[X] + xyDirectCell[X];
                xyResultCell[Y] = xyStartCell[Y] + xyDirectCell[Y];

                if (CellViewWorker.IsActiveSelfParentCell(xyResultCell))
                {
                    xyAvailableCells.Add((int[])xyResultCell.Clone());
                }
            }

            return xyAvailableCells;

        }
        internal static int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
        {
            var xyResultCell = new int[XY_FOR_ARRAY];

            var xyDirectCell = GetXYDirect(directType);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            return xyResultCell;
        }
        internal static int[] GetXYDirect(DirectTypes direct)
        {
            var xyDirectCell = new int[XY_FOR_ARRAY];

            switch (direct)
            {
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
                    break;
            }

            return xyDirectCell;
        }
    }
}
