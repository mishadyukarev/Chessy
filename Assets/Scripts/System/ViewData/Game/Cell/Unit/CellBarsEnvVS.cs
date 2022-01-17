using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellVEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellBarsVEs;

namespace Game.Game
{
    struct CellBarsEnvVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (EntityPool.EnvironmentInfo<IsActiveC>().IsActive)
                {
                    if (Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, idx_0).Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Enable();

                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).LocalScale
                            = new Vector3(Environment<AmountC>(EnvTypes.Fertilizer, idx_0).Amount
                            / (float)Max(EnvTypes.Fertilizer)
                            + Max(EnvTypes.Fertilizer), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Disable();
                    }

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).LocalScale =
                            new Vector3(Environment<AmountC>(EnvTypes.AdultForest, idx_0).Amount 
                            / (float)Max(EnvTypes.AdultForest), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Disable();
                    }

                    if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_0).Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).LocalScale
                            = new Vector3(Environment<AmountC>(EnvTypes.Hill, idx_0).Amount 
                            / (float)Max(EnvTypes.Hill), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).Disable();
                    }
                }
                else
                {
                    Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Disable();
                    Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Disable();
                    Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).Disable();
                }
            }

        }
    }
}