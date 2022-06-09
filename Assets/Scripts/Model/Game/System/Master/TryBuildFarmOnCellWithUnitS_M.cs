﻿using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class TryBuildFarmOnCellWithUnitS_M : SystemModel
    {
        const int FARMS_FOR_SKIP_LESSON = 3;

        internal TryBuildFarmOnCellWithUnitS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void TryBuild(in byte cell_0, in Player sender)
        {
            var whoseMove = eMG.WhoseMovePlayerTC.PlayerT;

            if (eMG.StepUnitC(cell_0).Steps >= StepValues.SET_FARM)
            {
                if (!eMG.BuildingTC(cell_0).HaveBuilding || eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    if (!eMG.AdultForestC(cell_0).HaveAnyResources)
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

                            if (needRes[resT] > eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                        }

                        if (canBuild)
                        {
                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                            {
                                eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                            }

                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);
                            eMG.YoungForestC(cell_0).Resources = 0;
                            sMG.BuildingSs.BuildS.Build(BuildingTypes.Farm, LevelTypes.First, whoseMove, BuildingValues.MAX_HP, cell_0);
                            eMG.StepUnitC(cell_0).Steps -= StepValues.SET_FARM;


                            //if (eMG.LessonT == LessonTypes.BuildingFarmHere)
                            //{
                            //    if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                            //    {
                            //        eMG.LessonTC.SetNextLesson();
                            //    }
                            //}
                            if (eMG.LessonT == LessonTypes.Build3Farms)
                            {
                                if (eMG.PlayerInfoE(whoseMove).AmountFarmsInGame >= FARMS_FOR_SKIP_LESSON)
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }

                        else
                        {
                            eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                        }
                    }

                    else
                    {
                        eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }

                else
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                }
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}