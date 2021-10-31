using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class EnvironmentUISystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;

        public void Run()
        {
            ref var selBuildDatC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvDatC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);


            if (SelectorC.IsSelCell && !selBuildDatC.Is(BuildTypes.City))
            {
                EnvirZoneViewUICom.SetActiveParent(true);
            }
            else
            {
                EnvirZoneViewUICom.SetActiveParent(false);
            }


            EnvirZoneViewUICom.SetTextResour(ResourceTypes.Food, selEnvDatC.AmountRes(EnvirTypes.Fertilizer).ToString());
            EnvirZoneViewUICom.SetTextResour(ResourceTypes.Wood, selEnvDatC.AmountRes(EnvirTypes.AdultForest).ToString());
            EnvirZoneViewUICom.SetTextResour(ResourceTypes.Ore, selEnvDatC.AmountRes(EnvirTypes.Hill).ToString());




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