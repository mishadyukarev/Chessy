using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CamelShiftUpdateMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal CamelShiftUpdateMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Wolf))
                {
                    var randDir = Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                    var idx_1 = eMGame.CellEs(cell_0).AroundCellE((DirectTypes)randDir).IdxC.Idx;

                    if (eMGame.CellEs(idx_1).IsActiveParentSelf && !eMGame.MountainC(idx_1).HaveAnyResources
                        && !eMGame.UnitTC(idx_1).HaveUnit)
                    {
                        eMGame.UnitEs(idx_1).Set(eMGame.UnitEs(cell_0));
                        eMGame.UnitTC(cell_0).Unit = UnitTypes.None;
                    }
                }
            }
        }
    }
}