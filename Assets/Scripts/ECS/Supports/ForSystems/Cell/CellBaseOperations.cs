using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConst;

namespace Assets.Scripts
{
    public sealed class CellBaseOperations
    {
        internal int[] CopyXY(int[] inArray)
        {
            int[] array = new int[XY_FOR_ARRAY];
            Array.Copy(inArray, array, array.Length);
            return array;
        }
        internal void CopyXYinTo(in int[] InXYCell, int[] ToXYCell)
        {
            ToXYCell[X] = InXYCell[X];
            ToXYCell[Y] = InXYCell[Y];
        }
        internal void CleanXY(int[] xy)
        {
            xy[X] = default;
            xy[Y] = default;
        }
        internal bool CompareXY(in int[] xyLeft, in int[] xyRight)
        {
            if (xyLeft[X] == xyRight[X]
                && xyLeft[Y] == xyRight[Y])
            {
                return true;
            }
            else { return false; }
        }
        internal List<int[]> CopyListXY(in List<int[]> inList)
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
        internal void CopyListXYinTo(List<int[]> inList, List<int[]> toList)
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
        internal bool TryFindCellInList(int[] xyCell, in List<int[]> list)
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