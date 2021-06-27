using Assets.Scripts.Abstractions;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    internal sealed class CellBaseOperations
    {
        private const int _x = ValuesConst.X;
        private const int _y = ValuesConst.Y;
        private const int _xy = ValuesConst.XY_FOR_ARRAY;


        internal int[] CopyXY(int[] inArray)
        {
            int[] array = new int[_xy];
            Array.Copy(inArray, array, array.Length);
            return array;
        }
        internal void CopyXYinTo(in int[] InXYCell, int[] ToXYCell)
        {
            ToXYCell[_x] = InXYCell[_x];
            ToXYCell[_y] = InXYCell[_y];
        }
        internal void CleanXY(int[] xy)
        {
            xy[_x] = default;
            xy[_y] = default;
        }
        internal bool CompareXY(in int[] xyLeft, in int[] xyRight)
        {
            if (xyLeft[_x] == xyRight[_x]
                && xyLeft[_y] == xyRight[_y])
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
                var array = new int[_xy];

                var inArray = inList[i];

                array[_x] = inArray[_x];
                array[_y] = inArray[_y];

                toList.Add(array);
            }

            return toList;
        }
        internal void CopyListXYinTo(List<int[]> inList, List<int[]> toList)
        {
            toList.Clear();

            for (int i = 0; i < inList.Count; i++)
            {
                var array = new int[_xy];

                var inArray = inList[i];

                array[_x] = inArray[_x];
                array[_y] = inArray[_y];

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