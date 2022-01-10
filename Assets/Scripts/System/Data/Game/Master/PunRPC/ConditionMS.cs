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

            ref var step_0 = ref Unit<UnitCellEC>(idx_0);
            ref var cond_0 = ref Unit<ConditionC>(idx_0);


            switch (cond)
            {
                case CondUnitTypes.None:
                    cond_0.Reset();
                    break;

                case CondUnitTypes.Protected:
                    if (cond_0.Is(CondUnitTypes.Protected))
                    {
                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (step_0.HaveMin)
                    {
                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
                        step_0.TakeMin();
                        cond_0.Set(cond);
                    }

                    else
                    {
                        EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case CondUnitTypes.Relaxed:
                    if (cond_0.Is(CondUnitTypes.Relaxed))
                    {
                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (step_0.HaveMin)
                    {
                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Set(cond);
                        step_0.TakeMin();
                    }

                    else
                    {
                        EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}