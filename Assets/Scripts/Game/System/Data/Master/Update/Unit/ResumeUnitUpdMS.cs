﻿using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class ResumeUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var condUnit_0 = ref UnitEffects<ConditionC>(idx_0);

                ref var envRes_0 = ref Environment<EnvResC>(idx_0);


                if (unit_0.CanResume(out var resume, out var env))
                {
                    if (envRes_0.HaveMax(env))
                    {
                        condUnit_0.Set(CondUnitTypes.Protected);
                    }
                    else
                    {
                        envRes_0.Add(env, resume);
                    }
                }
                else if (!unit_0.CanExtract(out resume, out env, out var res))
                {
                    if (UnitStat<HpC>(idx_0).HaveMax)
                    {
                        if (unit_0.Have && UnitStat<StepC>(idx_0).HaveMin)
                        {
                            condUnit_0.Set(CondUnitTypes.Protected);
                        }
                    }
                }
            }
        }
    }
}