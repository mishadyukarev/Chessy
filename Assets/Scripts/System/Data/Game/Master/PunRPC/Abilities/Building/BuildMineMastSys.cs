using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class BuildMineMastSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            BuildDoingMC.Get(out var build);
            IdxDoingMC.Get(out var idx_0);


            ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
            ref var build_0 = ref Build<BuildC>(idx_0);
            ref var ownBuild_0 = ref Build<OwnerC>(idx_0);


            ref var stepUnitCell_0 = ref Unit<UnitCellEC>(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (build == BuildTypes.Mine)
            {
                if (stepUnitCell_0.Have(build))
                {
                    if (!build_0.Have || build_0.Is(BuildTypes.Camp))
                    {
                        if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_0).Have 
                            && Environment<ResourcesC>(EnvTypes.Hill, idx_0).Have)
                        {
                            if (InvResC.CanCreateBuild(whoseMove, build, out var needRes))
                            {
                                EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Building);

                                InvResC.BuyBuild(whoseMove, build);


                                buildCell_0.SetNew(build, whoseMove);

                                stepUnitCell_0.TakeForBuild();
                            }

                            else EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
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