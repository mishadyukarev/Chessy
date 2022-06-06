using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class TryShiftWolfMS : SystemModel
    {
        internal TryShiftWolfMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryShift()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.UnitT(cellIdx0) == UnitTypes.Wolf)
                {
                    var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                    var idx_1 = eMG.AroundCellsE(cellIdx0).IdxCell((DirectTypes)randDir);

                    if (!eMG.IsBorder(idx_1) && !eMG.MountainC(idx_1).HaveAnyResources
                        && !eMG.UnitTC(idx_1).HaveUnit)
                    {
                        sMG.UnitSs.CopyUnitFromToS.Copy(cellIdx0, idx_1);

                        sMG.UnitSs.ClearUnit(cellIdx0);
                    }
                }
            }
        }
    }
}