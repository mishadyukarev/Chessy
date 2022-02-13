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
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.InfoEnvironmentE.IsActiveC.IsActive)
                {
                    if (EnvironmentEs(idx_0).Fertilizer.HaveEnvironment)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Enable();

                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).LocalScale
                            = new Vector3(EnvironmentEs(idx_0).Fertilizer.Resources / (float)CellEnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Disable();
                    }

                    if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).LocalScale =
                            new Vector3(EnvironmentEs(idx_0).AdultForest.Resources
                            / (float)CellEnvironmentValues.MaxResources(EnvironmentTypes.AdultForest), 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Disable();
                    }

                    if (EnvironmentEs(idx_0).Hill.HaveEnvironment)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).LocalScale
                            = new Vector3(EnvironmentEs(idx_0).Hill.Resources
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