using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TrySeedYoungForestOnCellWithPawnM(in AbilityTypes abilityT, in Player sender, in byte cell_0)
        {
            if (buildingCs[cell_0].HaveBuilding && buildingCs[cell_0].BuildingT != BuildingTypes.Camp)
            {
                RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
            }

            else
            {
                if (!environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    if (!environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.YoungForest))
                    {
                        RpcSs.SoundToGeneral(sender, abilityT);

                        environmentCs[cell_0].Set(EnvironmentTypes.YoungForest, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);

                        aboutGameC.AmountPlantedYoungForests++;

                        if (aboutGameC.LessonT == LessonTypes.SeedingPawn)
                        {
                            //if (_e.AmountPlantedYoungForests >= ValuesChessy.NEED_PLANTED_YOUNG_FOREST_FOR_SKIP_LESSON)
                            //{
                            SetNextLesson();
                            //}
                        }
                    }

                    else
                    {
                        RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                    }
                }

                else
                {
                    RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                }
            }
        }
    }
}