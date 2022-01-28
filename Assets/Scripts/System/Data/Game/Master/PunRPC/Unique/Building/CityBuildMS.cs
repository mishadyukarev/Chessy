using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct CityBuildMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var forBuildType = Entities.MasterEs.Build<BuildingTC>().Build;
            var idx_0 = Entities.MasterEs.Build<IdxC>().Idx;



            if (forBuildType == BuildingTypes.City)
            {
                ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;

                ref var fire_0 = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;


                var whoseMove = Entities.WhoseMove.WhoseMove.Player;


                if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(BuildingTypes.City))
                {
                    bool haveNearBorder = false;

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                    {
                        if (!Entities.CellEs.ParentE(idx_1).IsActiveSelf.IsActive)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        Entities.CellEs.BuildEs.Build(idx_0).SetNew(forBuildType, whoseMove);
                        Entities.WhereBuildingEs.HaveBuild(forBuildType, whoseMove, idx_0).HaveBuilding.Have = true;

                        Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(BuildingTypes.City));


                        fire_0.Disable();


                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Remove();
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).Remove();
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).Remove();
                    }

                    else
                    {
                        Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                    }
                }
                else
                {
                    Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
