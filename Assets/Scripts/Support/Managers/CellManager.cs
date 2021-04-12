using System;
using System.Collections.Generic;
using System.Linq;


public class CellManager
{
    private NameValueManager _nameManager;

    public CellManager(NameValueManager nameManager)
    {
        _nameManager = nameManager;
    }


    internal int[] CopyXY(int[] inArray)
    {
        int[] array = new int[_nameManager.XY_FOR_ARRAY];
        Array.Copy(inArray, array, array.Length);
        return array;
    }

    internal void CopyXYinTo(in int[] InXYCell, int[] ToXYCell)
    {
        ToXYCell[_nameManager.X] = InXYCell[_nameManager.X];
        ToXYCell[_nameManager.Y] = InXYCell[_nameManager.Y];
    }


    internal void CleanXY(int[] xy)
    {
        xy[_nameManager.X] = default;
        xy[_nameManager.Y] = default;
    }


    internal bool CompareXY(in int[] xyLeft, in int[] xyRight)
    {
        if (xyLeft[_nameManager.X] == xyRight[_nameManager.X] && xyLeft[_nameManager.Y] == xyRight[_nameManager.Y])
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
            var array = new int[_nameManager.XY_FOR_ARRAY];

            var inArray = inList[i];

            array[_nameManager.X] = inArray[_nameManager.X];
            array[_nameManager.Y] = inArray[_nameManager.Y];

            toList.Add(array);
        }

        return toList;
    }

    internal void CopyListXYinTo(List<int[]> inList, List<int[]> toList)
    {
        toList.Clear();

        for (int i = 0; i < inList.Count; i++)
        {
            var array = new int[_nameManager.XY_FOR_ARRAY];

            var inArray = inList[i];

            array[_nameManager.X] = inArray[_nameManager.X];
            array[_nameManager.Y] = inArray[_nameManager.Y];

            toList.Add(array);
        }
    }

    internal bool TryFindCellInList(int[] xyCell, in List<int[]> list)
    {
        return (from xy in list where CompareXY(xy, xyCell) select xy).Count() != 0;
    }
}
