using System;

namespace Game.Game
{
    struct ConditionMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var cond = EntitiesMaster.ConditionUnit<ConditionUnitC>().Condition;
            var idx_0 = EntitiesMaster.ConditionUnit<IdxC>().Idx;

            ref var cond_0 = ref CellUnitEs.Else(idx_0).ConditionC;


            switch (cond)
            {
                case ConditionUnitTypes.None:
                    cond_0.Reset();
                    break;

                case ConditionUnitTypes.Protected:
                    if (cond_0.Is(ConditionUnitTypes.Protected))
                    {
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (CellUnitEs.Step(idx_0).AmountC.Have)
                    {
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        CellUnitEs.Step(idx_0).AmountC.Take();
                        cond_0.Condition = cond;
                    }

                    else
                    {
                        Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (cond_0.Is(ConditionUnitTypes.Relaxed))
                    {
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (CellUnitEs.Step(idx_0).AmountC.Have)
                    {
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Condition = cond;
                        CellUnitEs.Step(idx_0).AmountC.Take();
                    }

                    else
                    {
                        Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}