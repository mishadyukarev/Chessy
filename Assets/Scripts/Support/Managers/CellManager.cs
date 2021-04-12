using System;
using System.Collections.Generic;
using System.Linq;


public class CellManager
{
    private StartValuesConfig _startValues;

    public CellManager(StartValuesConfig startValues)
    {
        _startValues = startValues;
    }


    internal int[] CopyXY(int[] inArray)
    {
        int[] array = new int[_startValues.XYforArray];
        Array.Copy(inArray, array, array.Length);
        return array;
    }

    internal void CopyXYinTo(in int[] InXYCell, int[] ToXYCell)
    {
        ToXYCell[_startValues.X] = InXYCell[_startValues.X];
        ToXYCell[_startValues.Y] = InXYCell[_startValues.Y];
    }


    internal void CleanXY(int[] xy)
    {
        xy[_startValues.X] = default;
        xy[_startValues.Y] = default;
    }


    internal bool CompareXY(in int[] xyLeft, in int[] xyRight)
    {
        if (xyLeft[_startValues.X] == xyRight[_startValues.X] && xyLeft[_startValues.Y] == xyRight[_startValues.Y])
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
            var array = new int[_startValues.XYforArray];

            var inArray = inList[i];

            array[_startValues.X] = inArray[_startValues.X];
            array[_startValues.Y] = inArray[_startValues.Y];

            toList.Add(array);
        }

        return toList;
    }

    internal void CopyListXYinTo(List<int[]> inList, List<int[]> toList)
    {
        toList.Clear();

        for (int i = 0; i < inList.Count; i++)
        {
            var array = new int[_startValues.XYforArray];

            var inArray = inList[i];

            array[_startValues.X] = inArray[_startValues.X];
            array[_startValues.Y] = inArray[_startValues.Y];

            toList.Add(array);
        }
    }

    internal bool TryFindCellInList(int[] xyCell, in List<int[]> list)
    {
        return (from xy in list where CompareXY(xy, xyCell) select xy).Count() != 0;
    }
}
