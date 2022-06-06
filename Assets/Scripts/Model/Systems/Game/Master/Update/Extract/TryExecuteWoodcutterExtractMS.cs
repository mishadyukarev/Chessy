using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class TryExecuteWoodcutterExtractMS : SystemModel
    {
        internal TryExecuteWoodcutterExtractMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryExecute()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.WoodcutterExtractC(cellIdx0).HaveAnyResources)
                {
                    var extract = eMG.WoodcutterExtractC(cellIdx0).Resources;

                    eMG.ResourcesC(eMG.BuildingPlayerTC(cellIdx0).PlayerT, ResourceTypes.Wood).Resources += extract;
                    sMG.MasterSs.TryTakeAdultForestResourcesS.TryTake(extract, cellIdx0);

                    if (!eMG.AdultForestC(cellIdx0).HaveAnyResources)
                    {
                        eMG.BuildingTC(cellIdx0).BuildingT = BuildingTypes.None;

                        if (eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdx0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }
            }
        }
    }
}