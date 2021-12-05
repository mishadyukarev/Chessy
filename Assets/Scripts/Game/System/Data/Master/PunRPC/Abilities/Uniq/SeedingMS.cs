using Leopotam.Ecs;
using Game.Common;
using System;
using static Game.Game.EntityPool;

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

            ref var stepUnit_0 = ref Unit<StepUnitWC>(idx_0);

            ref var build_0 = ref Build<BuildC>(idx_0);
            ref var env_0 = ref Environment<EnvC>(idx_0);
            ref var envCell_0 = ref Environment<EnvCellC>(idx_0);


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
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!env_0.Have(EnvTypes.Fertilizer))
                            {
                                if (!env_0.Have(EnvTypes.AdultForest))

                                    if (!env_0.Have(EnvTypes.YoungForest))
                                    {
                                        RpcSys.SoundToGeneral(sender, uniq_cur);

                                        envCell_0.SetNew(EnvTypes.YoungForest);

                                        stepUnit_0.Take(uniq_cur);
                                    }
                                    else
                                    {
                                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                else
                                {
                                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            }

                        }
                    }

                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
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
