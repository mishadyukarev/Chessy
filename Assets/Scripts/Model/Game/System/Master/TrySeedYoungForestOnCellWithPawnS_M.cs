using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        const int NEED_PLANTED_YOUNG_FOREST_FOR_SKIP_LESSON = 5;

        internal void TrySeedYoungForestOnCellWithPawnM(in AbilityTypes abilityT, in Player sender, in byte cell_0)
        {
            if (_eMG.StepUnitC(cell_0).Steps >= StepValues.SEED_PAWN)
            {
                if (_eMG.BuildingTC(cell_0).HaveBuilding && !_eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                }

                else
                {
                    if (!_eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (!_eMG.YoungForestC(cell_0).HaveAnyResources)
                        {
                            _eMG.RpcPoolEs.SoundToGeneral(sender, abilityT);

                            _eMG.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                            _eMG.StepUnitC(cell_0).Steps -= StepValues.SEED_PAWN;

                            //if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                            //{
                            //    if (_eMG.LessonTC.Is(LessonTypes.SeedingPawn))
                            //    {
                            //        _eMG.LessonTC.SetNextLesson();
                            //    }
                            //}

                            _eMG.AmountPlantedYoungForests++;

                            if (_eMG.LessonT == LessonTypes.SeedingPawn)
                            {
                                if (_eMG.AmountPlantedYoungForests >= NEED_PLANTED_YOUNG_FOREST_FOR_SKIP_LESSON)
                                {
                                    _eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }

                        else
                        {
                            _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                        }
                    }

                    else
                    {
                        _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }
            }

            else
            {
                _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}