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
                if (Es.EnvIsActive)
                {
                    if (Es.EnvironmentEs(idx_0).FertilizeC.HaveAny)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Enable();

                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).LocalScale
                            = new Vector3(Es.EnvironmentEs(idx_0).FertilizeC.Resources / (float)CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES, 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Food, idx_0).Disable();
                    }

                    if (Es.AdultForestC(idx_0).HaveAny)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).LocalScale =
                            new Vector3(Es.AdultForestC(idx_0).Resources
                            / (float)CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES, 0.15f, 1);
                    }
                    else
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Wood, idx_0).Disable();
                    }

                    if (Es.EnvironmentEs(idx_0).HillC.HaveAny)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Ore, idx_0).LocalScale
                            = new Vector3(Es.EnvironmentEs(idx_0).HillC.Resources
                            / (float)CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES, 0.15f, 1);
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