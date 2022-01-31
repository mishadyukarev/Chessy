namespace Game.Game
{
    sealed class BuildFarmMS : SystemCellAbstract, IEcsRunSystem
    {
        public BuildFarmMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = Es.MasterEs.Build<BuildingTC>().Build;
            var idx_0 = Es.MasterEs.Build<IdxC>().Idx;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;



            if (build == BuildingTypes.Farm)
            {
                var buildC = BuildEs.BuildingE(idx_0).BuildTC;

                if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(build))
                {
                    if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                    {
                        if (!CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                        {
                            if (Es.InventorResourcesEs.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                CellEs.EnvironmentEs.YoungForest( idx_0).Destroy(Es.WhereEnviromentEs);

                                if (CellEs.EnvironmentEs.Fertilizer( idx_0).HaveEnvironment)
                                {
                                    CellEs.EnvironmentEs.Fertilizer( idx_0).SetMax();
                                }
                                else
                                {
                                    CellEs.EnvironmentEs.Fertilizer( idx_0).SetNew();
                                }

                                Es.InventorResourcesEs.BuyBuild(whoseMove, build);

                                BuildEs.BuildingE(idx_0).SetNew(build, whoseMove, BuildEs, Es.WhereBuildingEs);
                                Es.WhereBuildingEs.HaveBuild(build, whoseMove, idx_0).HaveBuilding.Have = true;

                                UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(build);
                            }
                            else
                            {
                                Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                        else
                        {
                            Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                    }
                    else
                    {
                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                }

                else
                {
                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
