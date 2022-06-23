using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        const int FARMS_FOR_SKIP_LESSON = 3;

        internal void TryBuildFarmOnCellWithSimplePawnM(in byte cell_0, in Player sender)
        {
            var whoseMove = _e.WhoseMovePlayerT;

            if (_e.StepUnitC(cell_0).Steps >= StepValues.SET_FARM)
            {
                if (!_e.HaveBuildingOnCell(cell_0) || _e.BuildingOnCellT(cell_0).Is(BuildingTypes.Camp))
                {
                    if (!_e.AdultForestC(cell_0).HaveAnyResources)
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

                            if (needRes[resT] > _e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                        }

                        if (canBuild)
                        {
                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                            {
                                _e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                            }

                            ExecuteSoundActionToGeneral(sender, ClipTypes.Building);
                            _e.YoungForestC(cell_0).Resources = 0;
                            _e.Build(BuildingTypes.Farm, LevelTypes.First, whoseMove, BuildingValues.MAX_HP, cell_0);
                            _e.StepUnitC(cell_0).Steps -= StepValues.SET_FARM;


                            //if (_eMG.LessonT == LessonTypes.BuildingFarmHere)
                            //{
                            //    if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                            //    {
                            //        _eMG.LessonTC.SetNextLesson();
                            //    }
                            //}
                            if (_e.LessonT == LessonTypes.Build3Farms)
                            {
                                if (_e.PlayerInfoE(whoseMove).AmountFarmsInGame >= FARMS_FOR_SKIP_LESSON)
                                {
                                    _e.LessonT.SetNextLesson();
                                }
                            }
                        }

                        else
                        {
                            MistakeEconomyToGeneral(sender, needRes);
                        }
                    }

                    else
                    {
                        SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }

                else
                {
                    SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                }
            }

            else
            {
                SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}