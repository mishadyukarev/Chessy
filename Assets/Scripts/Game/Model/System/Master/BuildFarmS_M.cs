using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class BuildFarmS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;
        readonly BuildS _buildS;

        internal BuildFarmS_M(in CellEs cellEs, in BuildS buildS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
            _buildS = buildS;
        }

        internal void Build(in Player sender)
        {
            var whoseMove = e.WhoseMove.Player;

            if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.SET_FARM)
            {
                if (!_cellEs.BuildEs.MainE.BuildingTC.HaveBuilding || _cellEs.BuildEs.MainE.BuildingTC.Is(BuildingTypes.Camp))
                {
                    if (!_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
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

                            _cellEs.EnvironmentEs.YoungForestC.Resources = 0;


                            _buildS.Build(BuildingTypes.Farm, LevelTypes.First, whoseMove, BuildingValues.MAX_HP);

                            _cellEs.UnitStatsE.StepC.Steps -= StepValues.SET_FARM;
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