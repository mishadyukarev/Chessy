﻿using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TrySeedYoungForestOnCellWithPawnM(in AbilityTypes abilityT, in Player sender, in byte cell_0)
        {
            if (_buildingCs[cell_0].HaveBuilding && _buildingCs[cell_0].BuildingT != BuildingTypes.Camp)
            {
                RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
            }

            else
            {
                if (!_e.AdultForestC(cell_0).HaveAnyResources)
                {
                    if (!_e.YoungForestC(cell_0).HaveAnyResources)
                    {
                        RpcSs.SoundToGeneral(sender, abilityT);

                        _e.YoungForestC(cell_0).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;

                        _e.AmountPlantedYoungForests++;

                        if (_aboutGameC.LessonT == LessonTypes.SeedingPawn)
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