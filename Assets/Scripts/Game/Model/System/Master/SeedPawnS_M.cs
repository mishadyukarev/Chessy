using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class SeedPawnS_M : SystemModelGameAbs
    {
        readonly SystemsModelGame _sMGame;

        public SeedPawnS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMGame = sMGame;
        }

        public void Seed(in AbilityTypes abilityT, in Player sender, in byte cell_0)
        {
            if (e.UnitStepC(cell_0).Steps >= StepValues.SEED_PAWN)
            {
                if (e.BuildingTC(cell_0).HaveBuilding && !e.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                }

                else
                {
                    if (!e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (!e.YoungForestC(cell_0).HaveAnyResources)
                        {
                            e.RpcPoolEs.SoundToGeneral(sender, abilityT);

                            e.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                            e.UnitStepC(cell_0).Steps -= StepValues.SEED_PAWN;

                            if(cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                            {
                                if (e.LessonTC.Is(LessonTypes.SeedingPawn))
                                {
                                    e.LessonTC.SetNextLesson();
                                }
                            }

                        }

                        else
                        {
                            e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                        }
                    }

                    else
                    {
                        e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }
            }

            else
            {
                e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}