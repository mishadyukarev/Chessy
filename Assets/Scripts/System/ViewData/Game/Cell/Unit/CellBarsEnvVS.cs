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
                    if (Resources(EnvironmentTypes.Fertilizer, idx_0).Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Enable();

                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).LocalScale
                            = new Vector3(Resources(EnvironmentTypes.Fertilizer, idx_0).Amount / (float)EnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Disable();
                    }

                    if (Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).LocalScale =
                            new Vector3(Resources(EnvironmentTypes.AdultForest, idx_0).Amount 
                            / (float)EnvironmentValues.MaxResources(EnvironmentTypes.AdultForest), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Disable();
                    }

                    if (Resources(EnvironmentTypes.Hill, idx_0).Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).LocalScale
                            = new Vector3(Resources(EnvironmentTypes.Hill, idx_0).Amount 
                            / (float)EnvironmentValues.MaxResources(EnvironmentTypes.Hill), 0.15f, 1);
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