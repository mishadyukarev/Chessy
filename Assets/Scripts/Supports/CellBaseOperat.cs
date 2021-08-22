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

        internal static byte[] GetXyCell(this EcsFilter<XyCellComponent> xyCellFilter, byte idx)
        {
            for (byte curIdx = 0; curIdx < xyCellFilter.GetEntitiesCount(); curIdx++)
            {
                if (curIdx == idx)
                {
                    return xyCellFilter.Get1(curIdx).XyCell;
                }
            }
            throw new Exception();
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

        internal static List<byte[]> Copy(this List<byte[]> inList)
        {
            if (inList == default) throw new Exception();

            var toList = new List<byte[]>();

            for (ushort i = 0; i < inList.Count; i++)
            {
                var array = new byte[XY_FOR_ARRAY];

                var inArray = inList[i];

                array[X] = inArray[X];
                array[Y] = inArray[Y];

                toList.Add(array);
            }

            return toList;
        }
        internal static List<byte> Copy(this List<byte> inList)
        {
            if (inList == default) throw new Exception();

            var toList = new List<byte>();

            for (var i = 0; i < inList.Count; i++)
            {
                toList.Add(inList[i]);
            }

            return toList;
        }

        internal static void CopyListXyInTo(this List<byte[]> inList, List<byte[]> toList)
        {
            toList.Clear();

            for (byte i = 0; i < inList.Count; i++)
            {
                var array = new byte[XY_FOR_ARRAY];

                var inArray = inList[i];

                array[X] = inArray[X];
                array[Y] = inArray[Y];

                toList.Add(array);
            }
        }
        internal static bool TryFindCell(this List<byte[]> list, byte[] xyCell)
        {
            foreach (byte[] xy in list)
            {
                if (Compare(xy, xyCell))
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool TryFindCellInListAndRemove(this List<byte[]> list, byte[] xyTaking)
        {
            for (byte xyNumber = 0; xyNumber < list.Count; xyNumber++)
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