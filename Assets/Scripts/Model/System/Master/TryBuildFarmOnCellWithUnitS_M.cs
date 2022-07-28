using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        const int FARMS_FOR_SKIP_LESSON = 3;

        internal void TryBuildFarmOnCellWithSimplePawnM(in byte cell_0, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            if (!_buildingCs[cell_0].HaveBuilding || _buildingCs[cell_0].BuildingT == BuildingTypes.Camp)
            {
                if (!_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    var needRes = new Dictionary<ResourceTypes, float>();
                    var canBuild = true;

                    for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                    {
                        if (resT == ResourceTypes.Wood)
                        {
                            needRes.Add(resT, EconomyValues.WOOD_FOR_BUILDING_FARM);
                        }
                        else
                        {
                            needRes.Add(resT, 0);
                        }

                        if (needRes[resT] > ResourcesInInventoryC(whoseMove).ResourcesRef(resT)) canBuild = false;
                    }

                    if (canBuild)
                    {
                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                        {
                            ResourcesInInventoryC(whoseMove).Subtract(resT, needRes[resT]);
                        }

                        RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Building);
                        EnvironmentC(cell_0).Set(EnvironmentTypes.YoungForest, 0);
                        Build(BuildingTypes.Farm, LevelTypes.First, whoseMove, cell_0);

                        if (AboutGameC.LessonT == LessonTypes.Build1Farms)
                        {
                            SetNextLesson();
                        }
                    }

                    else
                    {
                        RpcSs.SimpleMistakeToGeneral(sender, needRes);
                    }
                }

                else
                {
                    RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                }
            }

            else
            {
                RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
            }
        }
    }
}