using Assets.Scripts.ECS.Component.Game.General;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.Workers
{
    public static class CellBaseOperat
    {
        internal static byte GetIndexCell(this EcsFilter<XyCellComponent> xyCellFilter, byte[] xy)
        {
            for (byte idx = 0; idx < xyCellFilter.GetEntitiesCount(); idx++)
            {
                if (xyCellFilter.Get1(idx).XyCell.Compare(xy))
                {
                    return idx;
                }
            }
            throw new Exception();
        }

        internal static byte[] GetXyCell(this EcsFilter<XyCellComponent> xyCellFilter, int idx)
        {
            for (int curIdx = 0; curIdx < xyCellFilter.GetEntitiesCount(); curIdx++)
            {
                if (curIdx == idx)
                {
                    return xyCellFilter.Get1(curIdx).XyCell;
                }
            }
            throw new Exception();
        }

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
        internal static bool Compare(this byte[] xyLeft, in byte[] xyRight)
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
        internal static bool TryFindCellInListAndRemove(this List<int[]> list, int[] xyTaking)
        {
            for (int xyNumber = 0; xyNumber < list.Count; xyNumber++)
            {
                if (list[xyNumber].Compare(xyTaking))
                {
                    list.RemoveAt(xyNumber);
                    return true;
                }
            }

            return false;
        }
    }
}