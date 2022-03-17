using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncBarsEnvironmentVS
    {
        public static void Sync(in byte idx_0, in EntitiesView vEs, in EntitiesModel e)
        {
            if (e.ZoneInfoC.EnvIsActive)
            {
                if (e.EnvironmentEs(idx_0).FertilizeC.HaveAnyResources)
                {
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Food).Enable();

                    vEs.CellEs(idx_0).Bar(CellBarTypes.Food).Transform.localScale
                        = new Vector3(e.EnvironmentEs(idx_0).FertilizeC.Resources / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Food).Disable();
                }

                if (e.AdultForestC(idx_0).HaveAnyResources)
                {
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Wood).Enable();
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Wood).Transform.localScale =
                        new Vector3(e.AdultForestC(idx_0).Resources
                        / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Wood).Disable();
                }

                if (e.EnvironmentEs(idx_0).HillC.HaveAnyResources)
                {
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Ore).Enable();
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Ore).Transform.localScale
                        = new Vector3(e.EnvironmentEs(idx_0).HillC.Resources
                        / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Ore).Disable();
                }
            }
            else
            {
                vEs.CellEs(idx_0).Bar(CellBarTypes.Food).Disable();
                vEs.CellEs(idx_0).Bar(CellBarTypes.Wood).Disable();
                vEs.CellEs(idx_0).Bar(CellBarTypes.Ore).Disable();
            }
        }
    }
}