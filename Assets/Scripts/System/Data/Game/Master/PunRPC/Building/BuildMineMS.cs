using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct BuildMineMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = EntitiesMaster.Build<BuildingTC>().Build;
            var idx_0 = EntitiesMaster.Build<IdxC>().Idx;


            ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;
            ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;


            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            if (build == BuildingTypes.Mine)
            {
                if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(build))
                {
                    if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                    {
                        if (Environment(EnvironmentTypes.Hill, idx_0).Resources.Have
                            && Environment(EnvironmentTypes.Hill, idx_0).Resources.Have)
                        {
                            if (InventorResourcesE.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                Entities.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                InventorResourcesE.BuyBuild(whoseMove, build);


                                CellBuildEs.SetNew(build, whoseMove, idx_0);

                                CellUnitEs.Step(idx_0).AmountC.Take();
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