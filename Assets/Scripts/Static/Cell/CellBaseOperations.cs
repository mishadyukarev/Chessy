using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConst;

namespace Assets.Scripts.Static
{
    public static class CellBaseOperations
    {
        internal static int[] CopyXY(int[] inArray)
        {
            int[] array = new int[XY_FOR_ARRAY];
            Array.Copy(inArray, array, array.Length);
            return array;
        }
        internal static void CopyXYinTo(in int[] InXYCell, int[] ToXYCell)
        {
            ToXYCell[X] = InXYCell[X];
            ToXYCell[Y] = InXYCell[Y];
        }
        internal static void CleanXY(int[] xy)
        {
            xy[X] = default;
            xy[Y] = default;
        }
        internal static bool CompareXY(in int[] xyLeft, in int[] xyRight)
        {
            if (xyLeft[X] == xyRight[X]
                && xyLeft[Y] == xyRight[Y])
            {
                return true;
            }
            else { return false; }
        }
        internal static List<int[]> CopyListXY(in List<int[]> inList)
        {
            var toList = new List<int[]>();

            for (int i = 0; i < inList.Count; i++)
            {
                var array = new int[XY_FOR_ARRAY];

                var inArray = inList[i];

                array[X] = inArray[X];
                array[Y] = inArray[Y];

                toList.Add(array);
            }

            return toList;
        }
        internal static void CopyListXYinTo(List<int[]> inList, List<int[]> toList)
        {
            toList.Clear();

            for (int i = 0; i < inList.Count; i++)
            {
                var array = new int[XY_FOR_ARRAY];

                var inArray = inList[i];

                array[X] = inArray[X];
                array[Y] = inArray[Y];

                toList.Add(array);
            }
        }
        internal static bool TryFindCellInList(int[] xyCell, in List<int[]> list)
        {
            foreach (var xy in list)
            {
                if (CompareXY(xy, xyCell))
                {
                    return true;
                }
            }
            return false;

            //return (from xy in list where  select xy).Count() != 0;
        }
    }
}