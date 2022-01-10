using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class SeedingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EnvDoingMC.Get(out var env);
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            ref var build_0 = ref Build<BuildC>(idx_0);
            ref var env_0 = ref Environment<HaveEnvironmentC>(idx_0);
            ref var envCell_0 = ref Environment<EnvCellEC>(idx_0);


            switch (env)
            {
                case EnvTypes.None:
                    throw new Exception();

                case EnvTypes.Fertilizer:
                    throw new Exception();

                case EnvTypes.YoungForest:
                    if (stepUnit_0.Have(uniq_cur))
                    {
                        if (build_0.Have && !build_0.Is(BuildTypes.Camp))
                        {
                            EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!env_0.Have(EnvTypes.Fertilizer))
                            {
                                if (!env_0.Have(EnvTypes.AdultForest))

                                    if (!env_0.Have(EnvTypes.YoungForest))
                                    {
                                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, uniq_cur);

                                        envCell_0.SetNew(EnvTypes.YoungForest);

                                        stepUnit_0.Take(uniq_cur);
                                    }
                                    else
                                    {
                                        EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                else
                                {
                                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            }

                        }
                    }

                    else
                    {
                        EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;

                case EnvTypes.AdultForest:
                    throw new Exception();

                case EnvTypes.Hill:
                    throw new Exception();

                case EnvTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}
