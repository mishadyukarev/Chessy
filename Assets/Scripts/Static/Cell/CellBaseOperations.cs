using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.Static
{
    public static class CellBaseOperations
    {
        //internal static int[] CopyXy(int[] inArray)
        //{
        //    int[] array = new int[XY_FOR_ARRAY];
        //    Array.Copy(inArray, array, array.Length);
        //    return array;
        //}
        //internal static void CopyXyInTo(in int[] InXYCell, int[] ToXYCell)
        //{
        //    ToXYCell[X] = InXYCell[X];
        //    ToXYCell[Y] = InXYCell[Y];
        //}
        internal static void Clean(this int[] xy)
        {
            xy[X] = default;
            xy[Y] = default;
        }
        internal static bool Compare(this int[] xyLeft, in int[] xyRight)
        {
            if (xyLeft[X] == xyRight[X]
                && xyLeft[Y] == xyRight[Y])
            {
                return true;
            }
            else return false;
        }

        internal static List<int[]> Copy(this List<int[]> inList)
        {
            if (inList == default) throw new Exception();

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
        internal static void CopyListXYinTo(this List<int[]> inList, List<int[]> toList)
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
        internal static bool TryFindCell(this List<int[]> list, int[] xyCell)
        {
            foreach (var xy in list)
            {
                if (Compare(xy, xyCell))
                {
                    return true;
                }
            }
            return false;

            //return (from xy in list where  select xy).Count() != 0;
        }
    }
}