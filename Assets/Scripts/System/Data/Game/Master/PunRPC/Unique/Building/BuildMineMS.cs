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


            var build_0 = BuildEs.BuildingE(idx_0).BuildTC;
            var ownBuild_0 = BuildEs.BuildingE(idx_0).Owner;


            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (build == BuildingTypes.Mine)
            {
                if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(build))
                {
                    if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                    {
                        if (CellEs.EnvironmentEs.Hill( idx_0).HaveEnvironment
                            && CellEs.EnvironmentEs.Hill( idx_0).HaveEnvironment)
                        {
                            if (Es.InventorResourcesEs.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                Es.InventorResourcesEs.BuyBuild(whoseMove, build);


                                BuildEs.BuildingE(idx_0).SetNew(build, whoseMove, BuildEs, Es.WhereBuildingEs);
                                Es.WhereBuildingEs.HaveBuild(build, whoseMove, idx_0).HaveBuilding.Have = true;

                                UnitEs.StatEs.Step(idx_0).Steps.Amount--;
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