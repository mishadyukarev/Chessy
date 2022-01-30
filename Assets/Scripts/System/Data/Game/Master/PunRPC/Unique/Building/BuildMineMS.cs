namespace Game.Game
{
    sealed class BuildMineMS : SystemAbstract, IEcsRunSystem
    {
        public BuildMineMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = Es.MasterEs.Build<BuildingTC>().Build;
            var idx_0 = Es.MasterEs.Build<IdxC>().Idx;


            ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;
            ref var ownBuild_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;


            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (build == BuildingTypes.Mine)
            {
                if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(build))
                {
                    if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                    {
                        if (Es.CellEs.EnvironmentEs.Hill( idx_0).HaveEnvironment
                            && Es.CellEs.EnvironmentEs.Hill( idx_0).HaveEnvironment)
                        {
                            if (Es.InventorResourcesEs.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                Es.InventorResourcesEs.BuyBuild(whoseMove, build);


                                Es.CellEs.BuildEs.Build(idx_0).SetNew(build, whoseMove);
                                Es.WhereBuildingEs.HaveBuild(build, whoseMove, idx_0).HaveBuilding.Have = true;

                                Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();
                            }

                            else Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
                        }

                        else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                    else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}