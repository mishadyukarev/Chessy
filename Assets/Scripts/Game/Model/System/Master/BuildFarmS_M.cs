using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    public sealed class BuildFarmS_M : SystemModelGameAbs
    {
        readonly BuildS _buildS;

        public BuildFarmS_M(in BuildS buildS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _buildS = buildS;
        }

        public void Build(in byte cell_0, in Player sender)
        {
            var whoseMove = eMGame.WhoseMove.Player;

            if (eMGame.UnitStepC(cell_0).Steps >= StepValues.SET_FARM)
            {
                if (!eMGame.BuildingTC(cell_0).HaveBuilding || eMGame.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    if (!eMGame.AdultForestC(cell_0).HaveAnyResources)
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

                            if (needRes[resT] > eMGame.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                        }

                        if (canBuild)
                        {
                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                            {
                                eMGame.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                            }

                            eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);

                            eMGame.YoungForestC(cell_0).Resources = 0;


                            _buildS.Build(BuildingTypes.Farm, LevelTypes.First, whoseMove, BuildingValues.MAX_HP, cell_0);

                            eMGame.UnitStepC(cell_0).Steps -= StepValues.SET_FARM;
                        }

                        else
                        {
                            eMGame.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                        }
                    }

                    else
                    {
                        eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }

                else
                {
                    eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                }
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}