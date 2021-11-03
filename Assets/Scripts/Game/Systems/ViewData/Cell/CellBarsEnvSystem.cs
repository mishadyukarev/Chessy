using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class CellBarsEnvSystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvFilter = default;
        private EcsFilter<CellBarsViewComponent> _cellBarsFilter = default;

        public void Run()
        {
            //ref var selBuildDatC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);

            //ref var env_sel = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);
            

            foreach (var curIdxCell in _cellBuildFilter)
            {
                ref var env_0 = ref _cellEnvFilter.Get1(curIdxCell);
                ref var envRes_0 = ref _cellEnvFilter.Get2(curIdxCell);
                ref var barsView_0 = ref _cellBarsFilter.Get1(curIdxCell);

                if (EnvirZoneDataUIC.IsActivatedInfo)
                {
                    if (env_0.Have(EnvTypes.Fertilizer))
                    {
                        barsView_0.EnableSR(CellBarTypes.Food);

                        barsView_0.SetScale(CellBarTypes.Food, new Vector3(envRes_0.AmountRes(EnvTypes.Fertilizer) / (float)(envRes_0.MaxAmountRes(EnvTypes.Fertilizer) + envRes_0.MaxAmountRes(EnvTypes.Fertilizer)), 0.15f, 1));
                    }
                    else
                    {
                        barsView_0.DisableSR(CellBarTypes.Food);
                    }

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        barsView_0.EnableSR(CellBarTypes.Wood);
                        barsView_0.SetScale(CellBarTypes.Wood, new Vector3(envRes_0.AmountRes(EnvTypes.AdultForest) / (float)envRes_0.MaxAmountRes(EnvTypes.AdultForest), 0.15f, 1));
                    }
                    else
                    {
                        barsView_0.DisableSR(CellBarTypes.Wood);
                    }

                    if (env_0.Have(EnvTypes.Hill))
                    {
                        barsView_0.EnableSR(CellBarTypes.Ore);
                        barsView_0.SetScale(CellBarTypes.Ore, new Vector3(envRes_0.AmountRes(EnvTypes.Hill) / (float)envRes_0.MaxAmountRes(EnvTypes.Hill), 0.15f, 1));
                    }
                    else
                    {
                        barsView_0.DisableSR(CellBarTypes.Ore);
                    }
                }
                else
                {
                    barsView_0.DisableSR(CellBarTypes.Food);
                    barsView_0.DisableSR(CellBarTypes.Wood);
                    barsView_0.DisableSR(CellBarTypes.Ore);
                }
            }

        }
    }
}