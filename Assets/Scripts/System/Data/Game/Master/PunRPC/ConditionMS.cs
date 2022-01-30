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

            ref var cond_0 = ref Es.CellEs.UnitEs.Main(idx_0).ConditionC;


            switch (cond)
            {
                case ConditionUnitTypes.None:
                    cond_0.Reset();
                    break;

                case ConditionUnitTypes.Protected:
                    if (cond_0.Is(ConditionUnitTypes.Protected))
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Have)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();
                        cond_0.Condition = cond;
                    }

                    else
                    {
                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (cond_0.Is(ConditionUnitTypes.Relaxed))
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Reset();
                    }

                    else if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Have)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        cond_0.Condition = cond;
                        Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();
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