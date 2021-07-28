using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
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

        if (SelectorWorker.IsSelectedCell && CellBuildingsDataWorker.GetBuildingType(XySelectedCell) != BuildingTypes.City)
        {
            EnvirZoneLeftUIWorker.SetActiveZone(true);
        }
        else
        {
            EnvirZoneLeftUIWorker.SetActiveZone(false);
        }

        EnvirZoneLeftUIWorker.SetTextInfoCell(EnvirTextInfoTypes.Fertilizer, "Fertilizer: " + CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.Fertilizer, XySelectedCell));
        EnvirZoneLeftUIWorker.SetTextInfoCell(EnvirTextInfoTypes.Wood, "Wood: " + CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.AdultForest, XySelectedCell));
        EnvirZoneLeftUIWorker.SetTextInfoCell(EnvirTextInfoTypes.Ore, "Ore: " + CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.Hill, XySelectedCell));



        if (EnvirZoneLeftUIWorker.IsActivatedEnvrInfo)
        {
            for (int x = 0; x < CellWorker.Xamount; x++)
                for (int y = 0; y < CellWorker.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };


                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                    {
                        CellSupVisBarsWorker.ActiveVision(true, SupportStaticTypes.Fertilizer, xy);
                        CellSupVisBarsWorker.SetScale(SupportStaticTypes.Fertilizer, new Vector3((float)CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.Fertilizer, xy) / (float)(CellEnvirDataWorker.MaxAmountResources(EnvironmentTypes.Fertilizer) + CellEnvirDataWorker.MaxAmountResources(EnvironmentTypes.Fertilizer)), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    }

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        CellSupVisBarsWorker.ActiveVision(true, SupportStaticTypes.Wood, xy);
                        CellSupVisBarsWorker.SetScale(SupportStaticTypes.Wood, new Vector3((float)CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.AdultForest, xy) / (float)CellEnvirDataWorker.MaxAmountResources(EnvironmentTypes.AdultForest), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Wood, xy);
                    }

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                    {
                        CellSupVisBarsWorker.ActiveVision(true, SupportStaticTypes.Ore, xy);
                        CellSupVisBarsWorker.SetScale(SupportStaticTypes.Ore, new Vector3((float)CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.Hill, xy) / (float)CellEnvirDataWorker.MaxAmountResources(EnvironmentTypes.Hill), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Ore, xy);
                    }
                }
        }
        else
        {
            for (int x = 0; x < CellWorker.Xamount; x++)
                for (int y = 0; y < CellWorker.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };

                    CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Wood, xy);
                    CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Ore, xy);
                }
        }
    }
}
