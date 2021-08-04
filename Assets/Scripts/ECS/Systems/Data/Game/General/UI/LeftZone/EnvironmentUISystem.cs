using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI.Left;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class EnvironmentUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);

        if (selCom.IsSelectedCell && !CellBuildDataSystem.BuildTypeCom(selCom.XySelectedCell).Is(BuildingTypes.City))
        {
            EnvirZoneLeftUIViewContainer.SetActiveZone(true);
        }
        else
        {
            EnvirZoneLeftUIViewContainer.SetActiveZone(false);
        }

        EnvirZoneLeftUIViewContainer.SetTextInfoCell(EnvirTextInfoTypes.Fertilizer, "Fertilizer: " + CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Fertilizer, selCom.XySelectedCell));
        EnvirZoneLeftUIViewContainer.SetTextInfoCell(EnvirTextInfoTypes.Wood, "Wood: " + CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.AdultForest, selCom.XySelectedCell));
        EnvirZoneLeftUIViewContainer.SetTextInfoCell(EnvirTextInfoTypes.Ore, "Ore: " + CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Hill, selCom.XySelectedCell));



        if (EnvirZoneLeftUIViewContainer.IsActivatedEnvrInfo)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };


                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                    {
                        CellSupVisBarsViewSystem.ActiveVision(true, SupportStaticTypes.Fertilizer, xy);
                        CellSupVisBarsViewSystem.SetScale(SupportStaticTypes.Fertilizer, new Vector3((float)CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Fertilizer, xy) / (float)(CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Fertilizer) + CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Fertilizer)), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    }

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        CellSupVisBarsViewSystem.ActiveVision(true, SupportStaticTypes.Wood, xy);
                        CellSupVisBarsViewSystem.SetScale(SupportStaticTypes.Wood, new Vector3((float)CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.AdultForest, xy) / (float)CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.AdultForest), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Wood, xy);
                    }

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                    {
                        CellSupVisBarsViewSystem.ActiveVision(true, SupportStaticTypes.Ore, xy);
                        CellSupVisBarsViewSystem.SetScale(SupportStaticTypes.Ore, new Vector3((float)CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Hill, xy) / (float)CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Hill), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Ore, xy);
                    }
                }
        }
        else
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Wood, xy);
                    CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Ore, xy);
                }
        }
    }
}
