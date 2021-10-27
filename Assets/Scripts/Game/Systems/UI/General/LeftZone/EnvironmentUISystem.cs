using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class EnvironmentUISystem : IEcsRunSystem
    {
        private EcsFilter<SelectorC> _selectorFilter = default;

        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;

        private EcsFilter<EnvirZoneDataUIC, EnvirZoneViewUICom> _envirZoneUIFilter = default;

        public void Run()
        {
            ref var cellEnvZoneDataUICom = ref _envirZoneUIFilter.Get1(0);
            ref var envViewUICom = ref _envirZoneUIFilter.Get2(0);

            ref var selCellBuildDataCom = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selCellEnvDataCom = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);


            if (SelectorC.IsSelCell && !selCellBuildDataCom.Is(BuildingTypes.City))
            {
                envViewUICom.SetActiveParent(true);
            }
            else
            {
                envViewUICom.SetActiveParent(false);
            }

            //var v = selCellEnvDataCom.GetAmountResources(EnvironmentTypes.Fertilizer);


            envViewUICom.SetTextResour(ResourceTypes.Food, selCellEnvDataCom.AmountRes(EnvirTypes.Fertilizer).ToString());
            envViewUICom.SetTextResour(ResourceTypes.Wood, selCellEnvDataCom.AmountRes(EnvirTypes.AdultForest).ToString());
            envViewUICom.SetTextResour(ResourceTypes.Ore, selCellEnvDataCom.AmountRes(EnvirTypes.Hill).ToString());




            foreach (var curIdxCell in _cellBuildFilter)
            {
                ref var curEnvCom = ref _cellEnvFilter.Get1(curIdxCell);
                ref var curCellBarsViewCom = ref _cellBarsFilter.Get1(curIdxCell);

                if (EnvirZoneDataUIC.IsActivatedInfo)
                {
                    if (curEnvCom.Have(EnvirTypes.Fertilizer))
                    {
                        curCellBarsViewCom.EnableSR(CellBarTypes.Food);

                        curCellBarsViewCom.SetScale(CellBarTypes.Food, new Vector3(curEnvCom.AmountRes(EnvirTypes.Fertilizer) / (float)(curEnvCom.MaxAmountRes(EnvirTypes.Fertilizer) + curEnvCom.MaxAmountRes(EnvirTypes.Fertilizer)), 0.15f, 1));
                    }
                    else
                    {
                        curCellBarsViewCom.DisableSR(CellBarTypes.Food);
                    }

                    if (curEnvCom.Have(EnvirTypes.AdultForest))
                    {
                        curCellBarsViewCom.EnableSR(CellBarTypes.Wood);
                        curCellBarsViewCom.SetScale(CellBarTypes.Wood, new Vector3(curEnvCom.AmountRes(EnvirTypes.AdultForest) / (float)curEnvCom.MaxAmountRes(EnvirTypes.AdultForest), 0.15f, 1));
                    }
                    else
                    {
                        curCellBarsViewCom.DisableSR(CellBarTypes.Wood);
                    }

                    if (curEnvCom.Have(EnvirTypes.Hill))
                    {
                        curCellBarsViewCom.EnableSR(CellBarTypes.Ore);
                        curCellBarsViewCom.SetScale(CellBarTypes.Ore, new Vector3(curEnvCom.AmountRes(EnvirTypes.Hill) / (float)curEnvCom.MaxAmountRes(EnvirTypes.Hill), 0.15f, 1));
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
}