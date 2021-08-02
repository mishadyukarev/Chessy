using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.UI.Left;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class EnvironmentUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);


    public void Run()
    {

        if (SelectorWorker.IsSelectedCell && CellBuildDataContainer.GetBuildingType(XySelectedCell) != BuildingTypes.City)
        {
            EnvirZoneLeftUIViewContainer.SetActiveZone(true);
        }
        else
        {
            EnvirZoneLeftUIViewContainer.SetActiveZone(false);
        }

        EnvirZoneLeftUIViewContainer.SetTextInfoCell(EnvirTextInfoTypes.Fertilizer, "Fertilizer: " + CellEnvirDataContainer.GetAmountResources(EnvironmentTypes.Fertilizer, XySelectedCell));
        EnvirZoneLeftUIViewContainer.SetTextInfoCell(EnvirTextInfoTypes.Wood, "Wood: " + CellEnvirDataContainer.GetAmountResources(EnvironmentTypes.AdultForest, XySelectedCell));
        EnvirZoneLeftUIViewContainer.SetTextInfoCell(EnvirTextInfoTypes.Ore, "Ore: " + CellEnvirDataContainer.GetAmountResources(EnvironmentTypes.Hill, XySelectedCell));



        if (EnvirZoneLeftUIViewContainer.IsActivatedEnvrInfo)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };


                    if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                    {
                        CellSupVisBarsContainer.ActiveVision(true, SupportStaticTypes.Fertilizer, xy);
                        CellSupVisBarsContainer.SetScale(SupportStaticTypes.Fertilizer, new Vector3((float)CellEnvirDataContainer.GetAmountResources(EnvironmentTypes.Fertilizer, xy) / (float)(CellEnvirDataContainer.MaxAmountResources(EnvironmentTypes.Fertilizer) + CellEnvirDataContainer.MaxAmountResources(EnvironmentTypes.Fertilizer)), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    }

                    if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        CellSupVisBarsContainer.ActiveVision(true, SupportStaticTypes.Wood, xy);
                        CellSupVisBarsContainer.SetScale(SupportStaticTypes.Wood, new Vector3((float)CellEnvirDataContainer.GetAmountResources(EnvironmentTypes.AdultForest, xy) / (float)CellEnvirDataContainer.MaxAmountResources(EnvironmentTypes.AdultForest), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Wood, xy);
                    }

                    if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.Hill, xy))
                    {
                        CellSupVisBarsContainer.ActiveVision(true, SupportStaticTypes.Ore, xy);
                        CellSupVisBarsContainer.SetScale(SupportStaticTypes.Ore, new Vector3((float)CellEnvirDataContainer.GetAmountResources(EnvironmentTypes.Hill, xy) / (float)CellEnvirDataContainer.MaxAmountResources(EnvironmentTypes.Hill), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Ore, xy);
                    }
                }
        }
        else
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Wood, xy);
                    CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Ore, xy);
                }
        }
    }
}
