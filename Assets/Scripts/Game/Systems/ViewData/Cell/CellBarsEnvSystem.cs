using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class CellBarsEnvSystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;

        public void Run()
        {
            ref var selBuildDatC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvDatC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);

            foreach (var curIdxCell in _cellBuildFilter)
            {
                ref var curEnvCom = ref _cellEnvFilter.Get1(curIdxCell);
                ref var curCellBarsViewCom = ref _cellBarsFilter.Get1(curIdxCell);

                if (EnvirZoneDataUIC.IsActivatedInfo)
                {
                    if (curEnvCom.Have(EnvTypes.Fertilizer))
                    {
                        curCellBarsViewCom.EnableSR(CellBarTypes.Food);

                        curCellBarsViewCom.SetScale(CellBarTypes.Food, new Vector3(curEnvCom.AmountRes(EnvTypes.Fertilizer) / (float)(curEnvCom.MaxAmountRes(EnvTypes.Fertilizer) + curEnvCom.MaxAmountRes(EnvTypes.Fertilizer)), 0.15f, 1));
                    }
                    else
                    {
                        curCellBarsViewCom.DisableSR(CellBarTypes.Food);
                    }

                    if (curEnvCom.Have(EnvTypes.AdultForest))
                    {
                        curCellBarsViewCom.EnableSR(CellBarTypes.Wood);
                        curCellBarsViewCom.SetScale(CellBarTypes.Wood, new Vector3(curEnvCom.AmountRes(EnvTypes.AdultForest) / (float)curEnvCom.MaxAmountRes(EnvTypes.AdultForest), 0.15f, 1));
                    }
                    else
                    {
                        curCellBarsViewCom.DisableSR(CellBarTypes.Wood);
                    }

                    if (curEnvCom.Have(EnvTypes.Hill))
                    {
                        curCellBarsViewCom.EnableSR(CellBarTypes.Ore);
                        curCellBarsViewCom.SetScale(CellBarTypes.Ore, new Vector3(curEnvCom.AmountRes(EnvTypes.Hill) / (float)curEnvCom.MaxAmountRes(EnvTypes.Hill), 0.15f, 1));
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