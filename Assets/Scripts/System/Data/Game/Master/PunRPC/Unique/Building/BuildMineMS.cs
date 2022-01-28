using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct BuildMineMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = Entities.MasterEs.Build<BuildingTC>().Build;
            var idx_0 = Entities.MasterEs.Build<IdxC>().Idx;


            ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
            ref var ownBuild_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;


            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            if (build == BuildingTypes.Mine)
            {
                if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(build))
                {
                    if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                    {
                        if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx_0).Resources.Have
                            && Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx_0).Resources.Have)
                        {
                            if (InventorResourcesE.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                Entities.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                InventorResourcesE.BuyBuild(whoseMove, build);


                                Entities.CellEs.BuildEs.Build(idx_0).SetNew(build, whoseMove);
                                Entities.WhereBuildingEs.HaveBuild(build, whoseMove, idx_0).HaveBuilding.Have = true;

                                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();
                            }

                            else Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
                        }

                        else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                    else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}