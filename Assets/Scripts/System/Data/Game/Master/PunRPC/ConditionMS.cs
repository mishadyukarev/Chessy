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
                    UnitEs(idx_0).MainE.ResetCondition();
                    break;

                case ConditionUnitTypes.Protected:
                    if (UnitEs(idx_0).MainE.ConditionTC.Is(ConditionUnitTypes.Protected))
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitEs(idx_0).MainE.ResetCondition();
                    }

                    else if (UnitStatEs(idx_0).StepE.HaveSteps)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitStatEs(idx_0).StepE.Take(cond);
                        UnitEs(idx_0).MainE.SetCondition(cond);
                    }

                    else
                    {
                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (UnitEs(idx_0).MainE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitEs(idx_0).MainE.ResetCondition();
                    }

                    else if (UnitStatEs(idx_0).StepE.HaveSteps)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        UnitEs(idx_0).MainE.SetCondition(cond);
                        UnitStatEs(idx_0).StepE.Take(cond);
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