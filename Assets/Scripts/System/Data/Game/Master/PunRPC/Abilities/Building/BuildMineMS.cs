﻿using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using Game.Common;

namespace Game.Game
{
    struct BuildMineMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = EntityMPool.Build<BuildingTC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;


            ref var build_0 = ref Build<BuildingTC>(idx_0);
            ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);


            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            if (build == BuildingTypes.Mine)
            {
                if (CellUnitStepEs.HaveForBuilding(idx_0, build))
                {
                    if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                    {
                        if (Resources(EnvironmentTypes.Hill, idx_0).Have
                            && Resources(EnvironmentTypes.Hill, idx_0).Have)
                        {
                            if (InventorResourcesE.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                InventorResourcesE.BuyBuild(whoseMove, build);


                                CellBuildE.SetNew(build, whoseMove, idx_0);

                                CellUnitStepEs.TakeForBuild(idx_0);
                            }

                            else EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                        }

                        else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                    else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}