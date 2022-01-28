using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct BuildFarmMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = Entities.MasterEs.Build<BuildingTC>().Build;
            var idx_0 = Entities.MasterEs.Build<IdxC>().Idx;

            ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
            ref var ownBuild_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;



            if (build == BuildingTypes.Farm)
            {
                var buildC = Entities.CellEs.BuildEs.Build(idx_0).BuildTC;

                if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(build))
                {
                    if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                    {
                        if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                        {
                            if (InventorResourcesE.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                Entities.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).Remove();

                                if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Have)
                                {
                                    Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Amount = CellEnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer);
                                }
                                else
                                {
                                    Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).SetNew();
                                }

                                InventorResourcesE.BuyBuild(whoseMove, build);

                                Entities.CellEs.BuildEs.Build(idx_0).SetNew(build, whoseMove);
                                Entities.WhereBuildingEs.HaveBuild(build, whoseMove, idx_0).HaveBuilding.Have = true;

                                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(build));
                            }
                            else
                            {
                                Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                        else
                        {
                            Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                    }
                    else
                    {
                        Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
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
