using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncBarsEnvironmentVS : SystemViewCellGameAbs
    {
        readonly EntitiesViewGame vEs;

        internal SyncBarsEnvironmentVS(in EntitiesViewGame vEs, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            this.vEs = vEs;
        }

        internal sealed override void Sync()
        {
            if (e.ZoneInfoC.IsActiveEnvironment)
            {
                if (e.FertilizeC(_currentCell).HaveAnyResources)
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Food).Enable();

                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Food).Transform.localScale
                        = new Vector3(e.FertilizeC(_currentCell).Resources / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Food).Disable();
                }

                if (e.AdultForestC(_currentCell).HaveAnyResources)
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Wood).Enable();
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Wood).Transform.localScale =
                        new Vector3(e.AdultForestC(_currentCell).Resources
                        / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Wood).Disable();
                }

                if (e.HillC(_currentCell).HaveAnyResources)
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Ore).Enable();
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Ore).Transform.localScale
                        = new Vector3(e.HillC(_currentCell).Resources
                        / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Ore).Disable();
                }
            }
            else
            {
                vEs.CellEs(_currentCell).Bar(CellBarTypes.Food).Disable();
                vEs.CellEs(_currentCell).Bar(CellBarTypes.Wood).Disable();
                vEs.CellEs(_currentCell).Bar(CellBarTypes.Ore).Disable();
            }
        }
    }
}