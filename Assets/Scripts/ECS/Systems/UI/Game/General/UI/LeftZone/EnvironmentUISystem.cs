using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class EnvironmentUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<EnvirZoneDataUICom, EnvirZoneViewUICom> _envirZoneUIFilter;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);

        if (selCom.IsSelectedCell && !CellBuildDataSystem.BuildTypeCom(selCom.XySelectedCell).Is(BuildingTypes.City))
        {
            _envirZoneUIFilter.Get2(0).SetActiveParent(true);
        }
        else
        {
            _envirZoneUIFilter.Get2(0).SetActiveParent(false);
        }

        _envirZoneUIFilter.Get2(0).SetText(ResourceTypes.Food, "Fertilizer: " + CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Fertilizer, selCom.XySelectedCell));
        _envirZoneUIFilter.Get2(0).SetText(ResourceTypes.Wood, "Wood: " + CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.AdultForest, selCom.XySelectedCell));
        _envirZoneUIFilter.Get2(0).SetText(ResourceTypes.Ore, "Ore: " + CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Hill, selCom.XySelectedCell));



        if (_envirZoneUIFilter.Get1(0).IsActivatedInfo)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };


                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                    {
                        CellSupVisBarsViewSystem.ActiveVision(true, CellBarTypes.Food, xy);
                        CellSupVisBarsViewSystem.SetScale(CellBarTypes.Food, new Vector3((float)CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Fertilizer, xy) / (float)(CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Fertilizer) + CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Fertilizer)), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Food, xy);
                    }

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        CellSupVisBarsViewSystem.ActiveVision(true, CellBarTypes.Wood, xy);
                        CellSupVisBarsViewSystem.SetScale(CellBarTypes.Wood, new Vector3((float)CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.AdultForest, xy) / (float)CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.AdultForest), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Wood, xy);
                    }

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                    {
                        CellSupVisBarsViewSystem.ActiveVision(true, CellBarTypes.Ore, xy);
                        CellSupVisBarsViewSystem.SetScale(CellBarTypes.Ore, new Vector3((float)CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Hill, xy) / (float)CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Hill), 0.15f, 1), xy);
                    }
                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Ore, xy);
                    }
                }
        }
        else
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Food, xy);
                    CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Wood, xy);
                    CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Ore, xy);
                }
        }
    }
}
