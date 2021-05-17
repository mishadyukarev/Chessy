using System;
using System.Collections.Generic;
using System.Linq;
using static MainGame;

public class CellBaseOperations
{
    private StartValuesGameConfig _startValuesGameConfig;

    private int X => _startValuesGameConfig.X;
    private int Y => _startValuesGameConfig.Y;
    private int XYForArray => _startValuesGameConfig.XY_FOR_ARRAY;

    internal CellBaseOperations(StartValuesGameConfig startValuesGameConfig)
    {
        _startValuesGameConfig = startValuesGameConfig;
    }

    internal int[] CopyXY(int[] inArray)
    {
        int[] array = new int[XYForArray];
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
            var array = new int[XYForArray];

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
            var array = new int[XYForArray];

            var inArray = inList[i];

            array[X] = inArray[X];
            array[Y] = inArray[Y];

            toList.Add(array);
        }
    }

    internal bool TryFindCellInList(int[] xyCell, in List<int[]> list)
    {
        return (from xy in list where CompareXY(xy, xyCell) select xy).Count() != 0;
    }
}
