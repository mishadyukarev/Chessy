﻿using Leopotam.Ecs;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class SeedingMS : IEcsRunSystem
    {
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<BuildC> _buildF = default;
        private EcsFilter<EnvC> _envF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EnvDoingMC.Get(out var env);
            IdxDoingMC.Get(out var idx_0);

            ref var stepUnit_0 = ref _statUnitF.Get1(idx_0);

            ref var build_0 = ref _buildF.Get1(idx_0);
            ref var env_0 = ref _envF.Get1(idx_0);


            switch (env)
            {
                case EnvTypes.None:
                    throw new Exception();

                case EnvTypes.Fertilizer:
                    throw new Exception();

                case EnvTypes.YoungForest:
                    if (stepUnit_0.HaveMinSteps)
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
                                        RpcSys.SoundToGeneral(sender, ClipTypes.Seeding);

                                        env_0.Set(EnvTypes.YoungForest);
                                        WhereEnvC.Add(EnvTypes.YoungForest, idx_0);

                                        stepUnit_0.TakeSteps();
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