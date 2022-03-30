using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class BuildFarmS_M : SystemModelGameAbs
    {
        internal BuildFarmS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Build(in byte cell_0, in Player sender)
        {
            var whoseMove = e.WhoseMove.Player;

            if (e.UnitStepC(cell_0).Steps >= StepValues.SET_FARM)
            {
                if (!e.BuildingTC(cell_0).HaveBuilding || e.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    if (!e.AdultForestC(cell_0).HaveAnyResources)
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

                            if (needRes[resT] > e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                        }

                        if (canBuild)
                        {
                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                            {
                                e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                            }

                            e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);

                            e.YoungForestC(cell_0).Resources = 0;

                            s.BuildS.Build(BuildingTypes.Farm, LevelTypes.First, whoseMove, BuildingValues.MAX_HP, cell_0);

                            e.UnitStepC(cell_0).Steps -= StepValues.SET_FARM;


                            if (e.LessonTC.Is(LessonTypes.BuildingFarmHere))
                            {
                                if(cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_BUILDING_FARM_LESSON)
                                {
                                    e.LessonTC.SetNextLesson();
                                }
                            }

                        }

                        else
                        {
                            e.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                        }
                    }

                    else
                    {
                        e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }

                else
                {
                    e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                }
            }

            else
            {
                e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}