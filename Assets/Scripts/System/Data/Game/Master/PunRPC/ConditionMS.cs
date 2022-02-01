using System;

namespace Game.Game
{
    sealed class ConditionMS : SystemAbstract, IEcsRunSystem
    {
        public ConditionMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var cond = Es.MasterEs.ConditionUnit<ConditionUnitC>().Condition;
            var idx_0 = Es.MasterEs.ConditionUnit<IdxC>().Idx;


            switch (cond)
            {
                case ConditionUnitTypes.None:
                    UnitEs.Main(idx_0).ResetCondition();
                    break;

                case ConditionUnitTypes.Protected:
                    if (UnitEs.Main(idx_0).ConditionTC.Is(ConditionUnitTypes.Protected))
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitEs.Main(idx_0).ResetCondition();
                    }

                    else if (UnitEs.StatEs.Step(idx_0).HaveSteps)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitEs.StatEs.Step(idx_0).Steps.Amount--;
                        UnitEs.Main(idx_0).SetCondition(cond);
                    }

                    else
                    {
                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (UnitEs.Main(idx_0).ConditionTC.Is(ConditionUnitTypes.Relaxed))
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitEs.Main(idx_0).ResetCondition();
                    }

                    else if (UnitEs.StatEs.Step(idx_0).HaveSteps)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitEs.Main(idx_0).SetCondition(cond);
                        UnitEs.StatEs.Step(idx_0).Steps.Amount--;
                    }

                    else
                    {
                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}