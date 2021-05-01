using System;
using System.Collections.Generic;
using System.Linq;
using static MainGame;

public class CellManager
{
    internal int[] CopyXY(int[] inArray)
    {
        int[] array = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];
        Array.Copy(inArray, array, array.Length);
        return array;
    }

    internal void CopyXYinTo(in int[] InXYCell, int[] ToXYCell)
    {
        ToXYCell[InstanceGame.StartValuesGameConfig.X] = InXYCell[InstanceGame.StartValuesGameConfig.X];
        ToXYCell[InstanceGame.StartValuesGameConfig.Y] = InXYCell[InstanceGame.StartValuesGameConfig.Y];
    }


    internal void CleanXY(int[] xy)
    {
        xy[InstanceGame.StartValuesGameConfig.X] = default;
        xy[InstanceGame.StartValuesGameConfig.Y] = default;
    }


    internal bool CompareXY(in int[] xyLeft, in int[] xyRight)
    {
        if (xyLeft[InstanceGame.StartValuesGameConfig.X] == xyRight[InstanceGame.StartValuesGameConfig.X] && xyLeft[InstanceGame.StartValuesGameConfig.Y] == xyRight[InstanceGame.StartValuesGameConfig.Y])
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
            var array = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

            var inArray = inList[i];

            array[InstanceGame.StartValuesGameConfig.X] = inArray[InstanceGame.StartValuesGameConfig.X];
            array[InstanceGame.StartValuesGameConfig.Y] = inArray[InstanceGame.StartValuesGameConfig.Y];

            toList.Add(array);
        }

        return toList;
    }

    internal void CopyListXYinTo(List<int[]> inList, List<int[]> toList)
    {
        toList.Clear();

        for (int i = 0; i < inList.Count; i++)
        {
            var array = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

            var inArray = inList[i];

            array[InstanceGame.StartValuesGameConfig.X] = inArray[InstanceGame.StartValuesGameConfig.X];
            array[InstanceGame.StartValuesGameConfig.Y] = inArray[InstanceGame.StartValuesGameConfig.Y];

            toList.Add(array);
        }
    }

    internal bool TryFindCellInList(int[] xyCell, in List<int[]> list)
    {
        return (from xy in list where CompareXY(xy, xyCell) select xy).Count() != 0;
    }
}
