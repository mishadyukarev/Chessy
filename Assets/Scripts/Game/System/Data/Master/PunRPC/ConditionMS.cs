using Leopotam.Ecs;
using Game.Common;
using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class ConditionMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            CondDoingMC.Get(out var cond);
            IdxDoingMC.Get(out var idx_0);

            ref var step_0 = ref Unit<StepUnitWC>(idx_0);
            ref var cond_0 = ref Unit<ConditionC>(idx_0);


            switch (cond)
            {
                case CondUnitTypes.None:
                    cond_0.Reset();
                    break;

                case CondUnitTypes.Protected:
                    if (cond_0.Is(CondUnitTypes.Protected))
                    {
                        RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (step_0.HaveMin)
                    {
                        RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        step_0.TakeMin();
                        cond_0.Set(cond);
                    }

                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case CondUnitTypes.Relaxed:
                    if (cond_0.Is(CondUnitTypes.Relaxed))
                    {
                        RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (step_0.HaveMin)
                    {
                        RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Set(cond);
                        step_0.TakeMin();
                    }

                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}