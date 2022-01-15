using static Game.Game.CellE;
using static Game.Game.CellUnitE;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentE;
using Game.Common;

namespace Game.Game
{
    struct BuildMineMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = EntityMPool.Build<BuildingC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;


            ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
            ref var build_0 = ref Build<BuildingC>(idx_0);
            ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);


            ref var stepUnitCell_0 = ref Unit<UnitCellEC>(idx_0);


            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            if (build == BuildTypes.Mine)
            {
                if (stepUnitCell_0.Have(build))
                {
                    if (!build_0.Have || build_0.Is(BuildTypes.Camp))
                    {
                        if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_0).Have
                            && Environment<AmountResourcesC>(EnvTypes.Hill, idx_0).Have)
                        {
                            //if (InvResC.CanCreateBuild(whoseMove, build, out var needRes))
                            //{
                            //    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Building);

                            //    InvResC.BuyBuild(whoseMove, build);


                            //    buildCell_0.SetNew(build, whoseMove);

                            //    stepUnitCell_0.TakeForBuild();
                            //}

                            //else EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
                        }

                        else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                    else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}