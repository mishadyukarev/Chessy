using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal struct CellBaseOperationsComponent
{
    private EntitiesGeneralManager _eGM;

    internal CellBaseOperationsComponent(EntitiesGeneralManager eGM)
    {
        _eGM = eGM;
    }

    internal int[] CopyXY(int[] inArray)
    {
        int[] array = new int[_eGM.XYForArray];
        Array.Copy(inArray, array, array.Length);
        return array;
    }

    internal void CopyXYinTo(in int[] InXYCell, int[] ToXYCell)
    {
        ToXYCell[_eGM.X] = InXYCell[_eGM.X];
        ToXYCell[_eGM.Y] = InXYCell[_eGM.Y];
    }


    internal void CleanXY(int[] xy)
    {
        xy[_eGM.X] = default;
        xy[_eGM.Y] = default;
    }


    internal bool CompareXY(in int[] xyLeft, in int[] xyRight)
    {
        if (xyLeft[_eGM.X] == xyRight[_eGM.X]
            && xyLeft[_eGM.Y] == xyRight[_eGM.Y])
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
            var array = new int[_eGM.XYForArray];

            var inArray = inList[i];

            array[_eGM.X] = inArray[_eGM.X];
            array[_eGM.Y] = inArray[_eGM.Y];

            toList.Add(array);
        }

        return toList;
    }

    internal void CopyListXYinTo(List<int[]> inList, List<int[]> toList)
    {
        toList.Clear();

        for (int i = 0; i < inList.Count; i++)
        {
            var array = new int[_eGM.XYForArray];

            var inArray = inList[i];

            array[_eGM.X] = inArray[_eGM.X];
            array[_eGM.Y] = inArray[_eGM.Y];

            toList.Add(array);
        }
    }

    internal bool TryFindCellInList(int[] xyCell, in List<int[]> list)
    {
        foreach (var xy in list)
        {
            if(CompareXY(xy, xyCell))
            {
                return true;
            }
        }
        return false;

        //return (from xy in list where  select xy).Count() != 0;
    }
}
