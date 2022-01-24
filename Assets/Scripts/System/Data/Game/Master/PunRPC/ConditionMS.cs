using System;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct ConditionMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var cond = EntityMPool.ConditionUnit<ConditionUnitC>().Condition;
            var idx_0 = EntityMPool.ConditionUnit<IdxC>().Idx;

            ref var cond_0 = ref EntitiesPool.UnitElse.Condition(idx_0);


            switch (cond)
            {
                case ConditionUnitTypes.None:
                    cond_0.Reset();
                    break;

                case ConditionUnitTypes.Protected:
                    if (cond_0.Is(ConditionUnitTypes.Protected))
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (EntitiesPool.UnitStep.HaveMin(idx_0))
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        EntitiesPool.UnitStep.TakeMin(idx_0);
                        cond_0.Condition = cond;
                    }

                    else
                    {
                        EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (cond_0.Is(ConditionUnitTypes.Relaxed))
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (EntitiesPool.UnitStep.HaveMin(idx_0))
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Condition = cond;
                        EntitiesPool.UnitStep.TakeMin(idx_0);
                    }

                    else
                    {
                        EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}