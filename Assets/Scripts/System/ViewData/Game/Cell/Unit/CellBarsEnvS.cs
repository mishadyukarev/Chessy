using UnityEngine;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellVPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    struct CellBarsEnvS : IEcsRunSystem
    {
        public void Run()
        {

            foreach (var idx_0 in Idxs)
            {
                ref var barsView_0 = ref ElseCellVE<BarsVC>(idx_0);

                if (EntityPool.EnvironmentInfo<IsActivatedC>().IsActivated)
                {
                    if (Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, idx_0).Have)
                    {
                        barsView_0.EnableSR(CellBarTypes.Food);

                        barsView_0.SetScale(CellBarTypes.Food, new Vector3(Environment<AmountResourcesC>(EnvTypes.Fertilizer, idx_0).Resources / (float)(Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).Max() + Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).Max()), 0.15f, 1));
                    }
                    else
                    {
                        barsView_0.DisableSR(CellBarTypes.Food);
                    }

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        barsView_0.EnableSR(CellBarTypes.Wood);
                        barsView_0.SetScale(CellBarTypes.Wood, new Vector3(Environment<AmountResourcesC>(EnvTypes.AdultForest, idx_0).Resources / (float)Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).Max(), 0.15f, 1));
                    }
                    else
                    {
                        barsView_0.DisableSR(CellBarTypes.Wood);
                    }

                    if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_0).Have)
                    {
                        barsView_0.EnableSR(CellBarTypes.Ore);
                        barsView_0.SetScale(CellBarTypes.Ore, new Vector3(Environment<AmountResourcesC>(EnvTypes.Hill, idx_0).Resources / (float)Environment<EnvCellEC>(EnvTypes.Hill, idx_0).Max(), 0.15f, 1));
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