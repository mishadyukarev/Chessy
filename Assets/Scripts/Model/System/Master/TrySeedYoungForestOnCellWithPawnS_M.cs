using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Model
{
    public sealed partial class SystemsModel : IUpdate
    {
        const int NEED_PLANTED_YOUNG_FOREST_FOR_SKIP_LESSON = 5;

        internal void TrySeedYoungForestOnCellWithPawnM(in AbilityTypes abilityT, in Player sender, in byte cell_0)
        {
            if (_e.EnergyUnitC(cell_0).Energy >= StepValues.SEED_PAWN)
            {
                if (_e.HaveBuildingOnCell(cell_0) && !_e.BuildingOnCellT(cell_0).Is(BuildingTypes.Camp))
                {
                    SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                }

                else
                {
                    if (!_e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (!_e.YoungForestC(cell_0).HaveAnyResources)
                        {
                            SoundToGeneral(sender, abilityT);

                            _e.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                            _e.EnergyUnitC(cell_0).Energy -= StepValues.SEED_PAWN;

                            //if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                            //{
                            //    if (_eMG.LessonTC.Is(LessonTypes.SeedingPawn))
                            //    {
                            //        _eMG.LessonTC.SetNextLesson();
                            //    }
                            //}

                            _e.AmountPlantedYoungForests++;

                            if (_e.LessonT == LessonTypes.SeedingPawn)
                            {
                                if (_e.AmountPlantedYoungForests >= NEED_PLANTED_YOUNG_FOREST_FOR_SKIP_LESSON)
                                {
                                    _e.CommonInfoAboutGameC.SetNextLesson();
                                }
                            }
                        }

                        else
                        {
                            SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                        }
                    }

                    else
                    {
                        SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }
            }

            else
            {
                SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}