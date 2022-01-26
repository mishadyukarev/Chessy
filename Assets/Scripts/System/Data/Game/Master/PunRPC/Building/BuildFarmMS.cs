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

            var build = EntityMPool.Build<BuildingTC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;

            ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;
            ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;

            var whoseMove = Entities.WhoseMoveE.WhoseMove.Player;



            if (build == BuildingTypes.Farm)
            {
                if (CellBuildEs.CanBuild(idx_0, build, whoseMove, out var mistake))
                {
                    if (InventorResourcesE.CanCreateBuild(build, whoseMove, out var needRes))
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                        Remove(EnvironmentTypes.YoungForest, idx_0);

                        if (Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Have)
                        {
                            Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Amount = CellEnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer);
                        }
                        else
                        {
                            SetNew(EnvironmentTypes.Fertilizer, idx_0);
                        }

                        InventorResourcesE.BuyBuild(whoseMove, build);

                        CellBuildEs.SetNew(build, whoseMove, idx_0);

                        CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(build));
                    }
                    else
                    {
                        EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                    }
                }

                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(mistake, sender);
                }
            }
        }
    }
}
