using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct BuildFarmMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = EntityMPool.Build<BuildingTC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;

            ref var build_0 = ref Build<BuildingTC>(idx_0);
            ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

            var whoseMove = WhoseMoveE.WhoseMove.Player;



            if (build == BuildingTypes.Farm)
            {
                if (CellBuildE.CanBuild(idx_0, build, whoseMove, out var mistake))
                {
                    if (InventorResourcesE.CanCreateBuild(build, whoseMove,  out var needRes))
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                        Remove(EnvironmentTypes.YoungForest, idx_0);

                        if (Resources(EnvironmentTypes.Fertilizer, idx_0).Have)
                        {
                            Resources(EnvironmentTypes.Fertilizer, idx_0).Amount = CellEnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer);
                        }
                        else
                        {
                            SetNew(EnvironmentTypes.Fertilizer, idx_0);
                        }

                        InventorResourcesE.BuyBuild(whoseMove, build);

                        CellBuildE.SetNew(build, whoseMove, idx_0);

                        EntitiesPool.UnitStep.Take(idx_0, build);
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
