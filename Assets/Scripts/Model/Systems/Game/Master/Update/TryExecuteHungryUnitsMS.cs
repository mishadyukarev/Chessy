using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class TryExecuteHungryUnitsMS : SystemModel
    {
        internal TryExecuteHungryUnitsMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryExecute()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.UnitT(cellIdx0) == UnitTypes.Pawn)
                {
                    if (!eMG.LessonTC.HaveLesson || eMG.LessonT >= LessonTypes.Build3Farms)
                    {
                        eMG.ResourcesC(eMG.UnitPlayerTC(cellIdx0).PlayerT, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;
                    }
                }
            }
        }
    }
}