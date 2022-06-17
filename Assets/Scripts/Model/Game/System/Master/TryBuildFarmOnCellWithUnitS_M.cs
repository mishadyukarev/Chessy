using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        const int FARMS_FOR_SKIP_LESSON = 3;

        internal void TryBuildFarmOnCellWithUnitM(in byte cell_0, in Player sender)
        {
            var whoseMove = _eMG.WhoseMovePlayerTC.PlayerT;

            if (_eMG.StepUnitC(cell_0).Steps >= StepValues.SET_FARM)
            {
                if (!_eMG.BuildingTC(cell_0).HaveBuilding || _eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    if (!_eMG.AdultForestC(cell_0).HaveAnyResources)
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

                            if (needRes[resT] > _eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                        }

                        if (canBuild)
                        {
                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                            {
                                _eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                            }

                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);
                            _eMG.YoungForestC(cell_0).Resources = 0;
                            BuildingSs.Build(BuildingTypes.Farm, LevelTypes.First, whoseMove, BuildingValues.MAX_HP, cell_0);
                            _eMG.StepUnitC(cell_0).Steps -= StepValues.SET_FARM;


                            //if (_eMG.LessonT == LessonTypes.BuildingFarmHere)
                            //{
                            //    if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                            //    {
                            //        _eMG.LessonTC.SetNextLesson();
                            //    }
                            //}
                            if (_eMG.LessonT == LessonTypes.Build3Farms)
                            {
                                if (_eMG.PlayerInfoE(whoseMove).AmountFarmsInGame >= FARMS_FOR_SKIP_LESSON)
                                {
                                    _eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }

                        else
                        {
                            _eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                        }
                    }

                    else
                    {
                        _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }

                else
                {
                    _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                }
            }

            else
            {
                _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}