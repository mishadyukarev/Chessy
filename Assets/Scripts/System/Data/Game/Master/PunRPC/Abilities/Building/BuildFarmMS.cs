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

            var buildType = EntityMPool.Build<BuildingC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;

            ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
            ref var build_0 = ref Build<BuildingC>(idx_0);
            ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            //var whoseMove = WhoseMoveC.WhoseMove;



            //if (buildType == BuildTypes.Farm)
            //{
            //    if (buildCell_0.CanBuild(buildType, whoseMove, out var mistake, out var needRes))
            //    {
            //        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Building);

            //        Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).Remove();

            //        if (Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, idx_0).Have)
            //        {
            //            Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).AddMax();
            //        }
            //        else
            //        {
            //            Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).SetNew();
            //        }

            //        //InvResC.BuyBuild(whoseMove, buildType);


            //        buildCell_0.SetNew(buildType, whoseMove);

            //        stepUnit_0.Take(buildType);
            //    }

            //    else
            //    {
            //        if (mistake == MistakeTypes.Economy) EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
            //        else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(mistake, sender);
            //    }
            //}
        }
    }
}
