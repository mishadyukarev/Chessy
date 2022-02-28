using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public static class CellBaseOperat
    {
        const byte XY_FOR_ARRAY = 2;
        const byte X = 0;
        const byte Y = 1;

        public static bool Compare(this byte[] xyLeft, in byte[] xyRight)
        {
            if (xyLeft[X] == xyRight[X]
                && xyLeft[Y] == xyRight[Y])
            {
                return true;
            }
            else return false;
        }

        public static List<byte[]> Copy(this List<byte[]> inList)
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
        public static List<byte> Copy(this List<byte> inList)
        {
            if (inList == default) throw new Exception();

            var toList = new List<byte>();

            for (var i = 0; i < inList.Count; i++)
            {
                toList.Add(inList[i]);
            }

            return toList;
        }

        public static void CopyListXyInTo(this List<byte[]> inList, List<byte[]> toList)
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
        public static bool TryFindCell(this List<byte[]> list, byte[] xyCell)
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

        public static bool TryFindCellInListAndRemove(this List<byte[]> list, byte[] xyTaking)
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