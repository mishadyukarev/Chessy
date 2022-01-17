using System;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct SeedingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            var env = EntityMPool.Seed<EnvironmetC>().Environment;
            var idx_0 = EntityMPool.Seed<IdxC>().Idx;
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            ref var build_0 = ref Build<BuildingTC>(idx_0);


            switch (env)
            {
                case EnvTypes.None:
                    throw new Exception();

                case EnvTypes.Fertilizer:
                    throw new Exception();

                case EnvTypes.YoungForest:
                    if (CellUnitStepEs.Have(idx_0, uniq_cur))
                    {
                        if (build_0.Have && !build_0.Is(BuildingTypes.Camp))
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

                                        SetNew(EnvTypes.YoungForest, idx_0);

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
