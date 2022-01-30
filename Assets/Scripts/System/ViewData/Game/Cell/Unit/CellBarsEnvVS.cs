using UnityEngine;
using static Game.Game.CellBarsVEs;

namespace Game.Game
{
    sealed class CellBarsEnvVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellBarsEnvVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in Es.CellEs.Idxs)
            {
                if (Es.InfoEnvironment.IsActiveC.IsActive)
                {
                    if (Es.CellEs.EnvironmentEs.Fertilizer(idx_0).HaveEnvironment)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Enable();

                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).LocalScale
                            = new Vector3(Es.CellEs.EnvironmentEs.Fertilizer(idx_0).AmountResources / (float)CellEnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Disable();
                    }

                    if (Es.CellEs.EnvironmentEs.AdultForest(idx_0).HaveEnvironment)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).LocalScale =
                            new Vector3(Es.CellEs.EnvironmentEs.AdultForest(idx_0).AmountResources
                            / (float)CellEnvironmentValues.MaxResources(EnvironmentTypes.AdultForest), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Disable();
                    }

                    if (Es.CellEs.EnvironmentEs.Hill(idx_0).HaveEnvironment)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).LocalScale
                            = new Vector3(Es.CellEs.EnvironmentEs.Hill(idx_0).AmountResources
                            / (float)CellEnvironmentValues.MaxResources(EnvironmentTypes.Hill), 0.15f, 1);
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