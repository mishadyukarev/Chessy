﻿using static Game.Game.CellEs;
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

            ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
            ref var build_0 = ref Build<BuildingTC>(idx_0);
            ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;



            if (build == BuildingTypes.Farm)
            {
                if (buildCell_0.CanBuild(build, whoseMove, out var mistake))
                {
                    if (InventorResourcesE.CanCreateBuild(build, whoseMove,  out var needRes))
                    {
                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Building);

                        Remove(EnvTypes.YoungForest, idx_0);

                        if (Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, idx_0).Have)
                        {
                            Environment<AmountC>(EnvTypes.Fertilizer, idx_0).Amount = Max(EnvTypes.Fertilizer);
                        }
                        else
                        {
                            SetNew(EnvTypes.Fertilizer, idx_0);
                        }

                        InventorResourcesE.BuyBuild(whoseMove, build);

                        CellBuildE.SetNew(build, whoseMove, idx_0);

                        CellUnitStepEs.Take(idx_0, build);
                    }
                    else
                    {
                        EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
                    }
                }

                else
                {
                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(mistake, sender);
                }
            }
        }
    }
}
