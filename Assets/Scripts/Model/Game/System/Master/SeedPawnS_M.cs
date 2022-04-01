using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class SeedPawnS_M : SystemModelGameAbs
    {
        internal SeedPawnS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Seed(in AbilityTypes abilityT, in Player sender, in byte cell_0)
        {
            if (eMG.UnitStepC(cell_0).Steps >= StepValues.SEED_PAWN)
            {
                if (eMG.BuildingTC(cell_0).HaveBuilding && !eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                }

                else
                {
                    if (!eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (!eMG.YoungForestC(cell_0).HaveAnyResources)
                        {
                            eMG.RpcPoolEs.SoundToGeneral(sender, abilityT);

                            eMG.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                            eMG.UnitStepC(cell_0).Steps -= StepValues.SEED_PAWN;

                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                            {
                                if (eMG.LessonTC.Is(LessonTypes.SeedingPawn))
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }

                        }

                        else
                        {
                            eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                        }
                    }

                    else
                    {
                        eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}