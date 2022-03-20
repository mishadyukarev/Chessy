using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CamelShiftUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal CamelShiftUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.Wolf))
                {
                    var randDir = Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                    var idx_1 = E.CellEs(idx_0).AroundCellE((DirectTypes)randDir).IdxC.Idx;

                    if (E.CellEs(idx_1).IsActiveParentSelf && !E.MountainC(idx_1).HaveAnyResources
                        && !E.UnitTC(idx_1).HaveUnit)
                    {
                        E.UnitEs(idx_1).Set(E.UnitEs(idx_0));
                        E.UnitTC(idx_0).Unit = UnitTypes.None;
                    }
                }
            }
        }
    }
}