using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class TrySetCamelMS : SystemModel
    {
        internal TrySetCamelMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TrySet()
        {
            byte cellIdx0 = 0;

            var haveCamel = false;

            for (cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.UnitTC(cellIdx0).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                cellIdx0 = (byte)UnityEngine.Random.Range(0, StartValues.CELLS);

                if (!eMG.IsBorder(cellIdx0))
                {
                    if (!eMG.UnitTC(cellIdx0).HaveUnit && !eMG.MountainC(cellIdx0).HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in eMG.AroundCellsE(cellIdx0).CellsAround)
                        {
                            if (eMG.UnitTC(cell_1).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            sMG.UnitSs.SetNewOnCellS.Set(UnitTypes.Wolf, PlayerTypes.None, cellIdx0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}