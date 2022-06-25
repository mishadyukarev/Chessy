using Chessy.Model;
using Chessy.Model.Values;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncBarsEnvironmentVS : SystemViewCellGameAbs
    {
        readonly EntitiesView vEs;

        internal SyncBarsEnvironmentVS(in EntitiesView vEs, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            this.vEs = vEs;
        }

        internal sealed override void Sync()
        {
            if (_e.ZoneInfoC.IsActiveEnvironment)
            {
                if (_e.FertilizeC(_currentCell).HaveAnyResources)
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Food).Enable();

                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Food).Transform.localScale
                        = new Vector3(_e.FertilizeC(_currentCell).Resources / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Food).Disable();
                }

                if (_e.AdultForestC(_currentCell).HaveAnyResources)
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Wood).Enable();
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Wood).Transform.localScale =
                        new Vector3(_e.AdultForestC(_currentCell).Resources
                        / (float)EnvironmentValues.MAX_RESOURCES, 0.15f, 1);
                }
                else
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Wood).Disable();
                }

                if (_e.HillC(_currentCell).HaveAnyResources)
                {
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Ore).Enable();
                    vEs.CellEs(_currentCell).Bar(CellBarTypes.Ore).Transform.localScale
                        = new Vector3(_e.HillC(_currentCell).Resources
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