using UnityEngine;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellVPool;

namespace Game.Game
{
    sealed class CellBarsEnvS : IEcsRunSystem
    {
        public void Run()
        {
            //ref var selBuildDatC = ref _cellBuildFilter.Get1(SelCell.IdxSelCell);

            //ref var env_sel = ref _cellEnvFilter.Get1(SelCell.IdxSelCell);


            foreach (var curIdxCell in Idxs)
            {
                ref var env_0 = ref Environment<EnvironmentC>(curIdxCell);
                ref var envRes_0 = ref Environment<EnvResC>(curIdxCell);
                ref var barsView_0 = ref ElseCellVE<BarsVC>(curIdxCell);

                if (EnvInfoC.IsActivatedInfo)
                {
                    if (env_0.Have(EnvTypes.Fertilizer))
                    {
                        barsView_0.EnableSR(CellBarTypes.Food);

                        barsView_0.SetScale(CellBarTypes.Food, new Vector3(envRes_0.Amount(EnvTypes.Fertilizer) / (float)(envRes_0.Max(EnvTypes.Fertilizer) + envRes_0.Max(EnvTypes.Fertilizer)), 0.15f, 1));
                    }
                    else
                    {
                        barsView_0.DisableSR(CellBarTypes.Food);
                    }

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        barsView_0.EnableSR(CellBarTypes.Wood);
                        barsView_0.SetScale(CellBarTypes.Wood, new Vector3(envRes_0.Amount(EnvTypes.AdultForest) / (float)envRes_0.Max(EnvTypes.AdultForest), 0.15f, 1));
                    }
                    else
                    {
                        barsView_0.DisableSR(CellBarTypes.Wood);
                    }

                    if (env_0.Have(EnvTypes.Hill))
                    {
                        barsView_0.EnableSR(CellBarTypes.Ore);
                        barsView_0.SetScale(CellBarTypes.Ore, new Vector3(envRes_0.Amount(EnvTypes.Hill) / (float)envRes_0.Max(EnvTypes.Hill), 0.15f, 1));
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