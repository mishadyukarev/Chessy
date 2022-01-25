using System;
using static Game.Game.CellUnitEntities;

namespace Game.Game
{
    struct ConditionMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var cond = EntityMPool.ConditionUnit<ConditionUnitC>().Condition;
            var idx_0 = EntityMPool.ConditionUnit<IdxC>().Idx;

            ref var cond_0 = ref CellUnitEntities.Else(idx_0).ConditionC;


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

                    else if (CellUnitEntities.Step(idx_0).AmountC.Have)
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        CellUnitEntities.Step(idx_0).AmountC.Take();
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

                    else if (CellUnitEntities.Step(idx_0).AmountC.Have)
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Condition = cond;
                        CellUnitEntities.Step(idx_0).AmountC.Take();
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