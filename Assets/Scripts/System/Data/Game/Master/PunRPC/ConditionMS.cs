using System;

namespace Game.Game
{
    struct ConditionMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var cond = Entities.MasterEs.ConditionUnit<ConditionUnitC>().Condition;
            var idx_0 = Entities.MasterEs.ConditionUnit<IdxC>().Idx;

            ref var cond_0 = ref Entities.CellEs.UnitEs.Else(idx_0).ConditionC;


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

                    else if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
                    {
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();
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

                    else if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
                    {
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Condition = cond;
                        Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();
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