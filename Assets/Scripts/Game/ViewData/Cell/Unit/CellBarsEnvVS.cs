using UnityEngine;

namespace Chessy.Game
{
    sealed class CellBarsEnvVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellBarsEnvVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.EnvIsActive)
                {
                    if (E.EnvironmentEs(idx_0).FertilizeC.HaveAnyResources)
                    {
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Food).Enable();

                        VEs.CellEs(idx_0).Bar(CellBarTypes.Food).LocalScale
                            = new Vector3(E.EnvironmentEs(idx_0).FertilizeC.Resources / (float)Environment_Values.ENVIRONMENT_MAX, 0.15f, 1);
                    }
                    else
                    {
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Food).Disable();
                    }

                    if (E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Wood).Enable();
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Wood).LocalScale =
                            new Vector3(E.AdultForestC(idx_0).Resources
                            / (float)Environment_Values.ENVIRONMENT_MAX, 0.15f, 1);
                    }
                    else
                    {
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Wood).Disable();
                    }

                    if (E.EnvironmentEs(idx_0).HillC.HaveAnyResources)
                    {
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Ore).Enable();
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Ore).LocalScale
                            = new Vector3(E.EnvironmentEs(idx_0).HillC.Resources
                            / (float)Environment_Values.ENVIRONMENT_MAX, 0.15f, 1);
                    }
                    else
                    {
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Ore).Disable();
                    }
                }
                else
                {
                    VEs.CellEs(idx_0).Bar(CellBarTypes.Food).Disable();
                    VEs.CellEs(idx_0).Bar(CellBarTypes.Wood).Disable();
                    VEs.CellEs(idx_0).Bar(CellBarTypes.Ore).Disable();
                }
            }

        }
    }
}