using Photon.Realtime;
using System.Collections.Generic;

internal class CellFinderWay
{
    internal List<int[]> TryGetXYAround(int[] xyStartCell)
    {
        var xyAvailableCells = new List<int[]>();
        var xyResultCell = new int[MainGame.InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

        for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
        {
            var xyDirectCell = GetXYDirect((DirectTypes)i);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            xyAvailableCells.Add(MainGame.InstanceGame.SupportGameManager.CellManager.CopyXY(xyResultCell));
        }

        return xyAvailableCells;
    }


    internal int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
    {
        var xyResultCell = new int[MainGame.InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

        var xyDirectCell = GetXYDirect(directType);

        xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
        xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

        return xyResultCell;
    }

    private int[] GetXYDirect(DirectTypes direct)
    {
        var xyDirectCell = new int[MainGame.InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

        switch (direct)
        {
            case DirectTypes.Right:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = 0;
                break;

            case DirectTypes.Left:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = 0;
                break;

            case DirectTypes.Up:
                xyDirectCell[0] = 0;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.Down:
                xyDirectCell[0] = 0;
                xyDirectCell[1] = -1;
                break;

            case DirectTypes.RightUp:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.LeftUp:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.RightDown:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = -1;
                break;

            case DirectTypes.LeftDown:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = -1;
                break;

            default:
                break;
        }

        return xyDirectCell;
    }
}