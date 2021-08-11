using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class EnvironmentUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;

    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;

    private EcsFilter<EnvirZoneDataUICom, EnvirZoneViewUICom> _envirZoneUIFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);

        var idxSelCell = selCom.IdxSelectedCell;

        ref var cellEnvZoneDataUICom = ref _envirZoneUIFilter.Get1(0);
        ref var cellEnvZoneViewUICom = ref _envirZoneUIFilter.Get2(0);

        ref var selCellBuildDataCom = ref _cellBuildFilter.Get1(idxSelCell);
        ref var selCellEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);


        if (selCom.IsSelectedCell && !selCellBuildDataCom.IsBuildType(BuildingTypes.City))
        {
            cellEnvZoneViewUICom.SetActiveParent(true);
        }
        else
        {
            cellEnvZoneViewUICom.SetActiveParent(false);
        }

        var v = selCellEnvDataCom.GetAmountResources(EnvironmentTypes.Fertilizer);

        cellEnvZoneViewUICom.SetText(ResourceTypes.Food, "Fertilizer: " + selCellEnvDataCom.GetAmountResources(EnvironmentTypes.Fertilizer));
        cellEnvZoneViewUICom.SetText(ResourceTypes.Wood, "Wood: " + selCellEnvDataCom.GetAmountResources(EnvironmentTypes.AdultForest));
        cellEnvZoneViewUICom.SetText(ResourceTypes.Ore, "Ore: " + selCellEnvDataCom.GetAmountResources(EnvironmentTypes.Hill));




        foreach (var curIdxCell in _cellBuildFilter)
        {
            ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(curIdxCell);
            ref var curCellBarsViewCom = ref _cellBarsFilter.Get1(curIdxCell);

            if (_envirZoneUIFilter.Get1(0).IsActivatedInfo)
            {
                if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.Fertilizer))
                {
                    curCellBarsViewCom.EnableSR(CellBarTypes.Food);

                    curCellBarsViewCom.SetScale(CellBarTypes.Food, new Vector3((float)curCellEnvDataCom.GetAmountResources(EnvironmentTypes.Fertilizer) / (float)(curCellEnvDataCom.MaxAmountResources(EnvironmentTypes.Fertilizer) + curCellEnvDataCom.MaxAmountResources(EnvironmentTypes.Fertilizer)), 0.15f, 1));
                }
                else
                {
                    curCellBarsViewCom.DisableSR(CellBarTypes.Food);
                }

                if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    curCellBarsViewCom.EnableSR(CellBarTypes.Wood);
                    curCellBarsViewCom.SetScale(CellBarTypes.Wood, new Vector3((float)curCellEnvDataCom.GetAmountResources(EnvironmentTypes.AdultForest) / (float)curCellEnvDataCom.MaxAmountResources(EnvironmentTypes.AdultForest), 0.15f, 1));
                }
                else
                {
                    curCellBarsViewCom.DisableSR(CellBarTypes.Wood);
                }

                if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.Hill))
                {
                    curCellBarsViewCom.EnableSR(CellBarTypes.Ore);
                    curCellBarsViewCom.SetScale(CellBarTypes.Ore, new Vector3((float)curCellEnvDataCom.GetAmountResources(EnvironmentTypes.Hill) / (float)curCellEnvDataCom.MaxAmountResources(EnvironmentTypes.Hill), 0.15f, 1));
                }
                else
                {
                    curCellBarsViewCom.DisableSR(CellBarTypes.Ore);
                }
            }
            else
            {
                curCellBarsViewCom.DisableSR(CellBarTypes.Food);
                curCellBarsViewCom.DisableSR(CellBarTypes.Wood);
                curCellBarsViewCom.DisableSR(CellBarTypes.Ore);
            }
        }

    }
}
