using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class EnvironmentUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorCom> _selectorFilter = default;

    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;

    private EcsFilter<EnvirZoneDataUICom, EnvirZoneViewUICom> _envirZoneUIFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);

        var idxSelCell = selCom.IdxSelCell;

        ref var cellEnvZoneDataUICom = ref _envirZoneUIFilter.Get1(0);
        ref var envViewUICom = ref _envirZoneUIFilter.Get2(0);

        ref var selCellBuildDataCom = ref _cellBuildFilter.Get1(idxSelCell);
        ref var selCellEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);


        if (selCom.IsSelectedCell && !selCellBuildDataCom.IsBuildType(BuildingTypes.City))
        {
            envViewUICom.SetActiveParent(true);
        }
        else
        {
            envViewUICom.SetActiveParent(false);
        }

        //var v = selCellEnvDataCom.GetAmountResources(EnvironmentTypes.Fertilizer);


        envViewUICom.SetTextEnvirInfo(LanguageComComp.GetText(GameLanguageTypes.EnvironmentInfo));

        envViewUICom.SetTextResour(ResourceTypes.Food, LanguageComComp.GetText(GameLanguageTypes.Fertilizer) + ": " + selCellEnvDataCom.GetAmountResources(EnvirTypes.Fertilizer));
        envViewUICom.SetTextResour(ResourceTypes.Wood, LanguageComComp.GetText(GameLanguageTypes.Wood) + ": " + selCellEnvDataCom.GetAmountResources(EnvirTypes.AdultForest));
        envViewUICom.SetTextResour(ResourceTypes.Ore, LanguageComComp.GetText(GameLanguageTypes.Ore) + ": " + selCellEnvDataCom.GetAmountResources(EnvirTypes.Hill));




        foreach (var curIdxCell in _cellBuildFilter)
        {
            ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(curIdxCell);
            ref var curCellBarsViewCom = ref _cellBarsFilter.Get1(curIdxCell);

            if (_envirZoneUIFilter.Get1(0).IsActivatedInfo)
            {
                if (curCellEnvDataCom.HaveEnvir(EnvirTypes.Fertilizer))
                {
                    curCellBarsViewCom.EnableSR(CellBarTypes.Food);

                    curCellBarsViewCom.SetScale(CellBarTypes.Food, new Vector3((float)curCellEnvDataCom.GetAmountResources(EnvirTypes.Fertilizer) / (float)(curCellEnvDataCom.MaxAmountResources(EnvirTypes.Fertilizer) + curCellEnvDataCom.MaxAmountResources(EnvirTypes.Fertilizer)), 0.15f, 1));
                }
                else
                {
                    curCellBarsViewCom.DisableSR(CellBarTypes.Food);
                }

                if (curCellEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                {
                    curCellBarsViewCom.EnableSR(CellBarTypes.Wood);
                    curCellBarsViewCom.SetScale(CellBarTypes.Wood, new Vector3((float)curCellEnvDataCom.GetAmountResources(EnvirTypes.AdultForest) / (float)curCellEnvDataCom.MaxAmountResources(EnvirTypes.AdultForest), 0.15f, 1));
                }
                else
                {
                    curCellBarsViewCom.DisableSR(CellBarTypes.Wood);
                }

                if (curCellEnvDataCom.HaveEnvir(EnvirTypes.Hill))
                {
                    curCellBarsViewCom.EnableSR(CellBarTypes.Ore);
                    curCellBarsViewCom.SetScale(CellBarTypes.Ore, new Vector3((float)curCellEnvDataCom.GetAmountResources(EnvirTypes.Hill) / (float)curCellEnvDataCom.MaxAmountResources(EnvirTypes.Hill), 0.15f, 1));
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
