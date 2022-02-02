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

            var idx_0 = Es.MasterEs.BuildingFarmME.IdxC.Idx;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            var ability = Es.MasterEs.AbilityC.Ability;

            var buildC = BuildEs(idx_0).BuildingE.BuildTC;

            if (UnitStatEs(idx_0).StepE.Have(ability))
            {
                if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                {
                    if (!EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        if (Es.InventorResourcesEs.CanCreateBuild(BuildingTypes.Farm, whoseMove, out var needRes))
                        {
                            Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                            EnvironmentEs(idx_0).YoungForest.Destroy(Es.WhereEnviromentEs);

                            if (EnvironmentEs(idx_0).Fertilizer.HaveEnvironment)
                            {
                                EnvironmentEs(idx_0).Fertilizer.AddAfterBuildingFarm();
                            }
                            else
                            {
                                EnvironmentEs(idx_0).Fertilizer.SetNew();
                            }

                            Es.InventorResourcesEs.BuyBuild(whoseMove, BuildingTypes.Farm);

                            BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Farm, whoseMove, BuildEs(idx_0), Es.WhereBuildingEs);

                            UnitStatEs(idx_0).StepE.Take(ability);
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
