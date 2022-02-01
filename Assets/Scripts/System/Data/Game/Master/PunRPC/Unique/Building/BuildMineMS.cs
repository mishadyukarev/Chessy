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

            var idx_0 = Es.MasterEs.BuildingMineME.WhereBuildMine.Idx;


            var build_0 = BuildEs(idx_0).BuildingE.BuildTC;

            var ability = Es.MasterEs.AbilityC.Ability;


            var whoseMove = Es.WhoseMove.WhoseMove.Player;


            if (UnitStatEs(idx_0).StepE.Have(ability))
            {
                if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                {
                    if (EnvironmentEs(idx_0).Hill.HaveEnvironment
                        && EnvironmentEs(idx_0).Hill.HaveEnvironment)
                    {
                        if (Es.InventorResourcesEs.CanCreateBuild(BuildingTypes.Mine, whoseMove, out var needRes))
                        {
                            Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                            Es.InventorResourcesEs.BuyBuild(whoseMove, BuildingTypes.Mine);


                            BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Mine, whoseMove, BuildEs(idx_0), Es.WhereBuildingEs);
                            Es.WhereBuildingEs.HaveBuild(BuildingTypes.Mine, whoseMove, idx_0).HaveBuilding.Have = true;

                            UnitStatEs(idx_0).StepE.Take(ability);
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