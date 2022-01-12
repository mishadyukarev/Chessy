using System;
using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    struct SeedingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EnvDoingMC.Get(out var env);
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            ref var build_0 = ref Build<BuildC>(idx_0);


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
                            if (!Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, idx_0).Have)
                            {
                                if (!Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)

                                    if (!Environment<HaveEnvironmentC>(EnvTypes.YoungForest, idx_0).Have)
                                    {
                                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, uniq_cur);

                                        Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).SetNew();

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
