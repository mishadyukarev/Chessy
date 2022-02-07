using ECS;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitConditonE : CellEntityAbstract
    {
        ref ConditionUnitTC ConditionTCRef => ref Ent.Get<ConditionUnitTC>();
        public ConditionUnitTC ConditionTC => Ent.Get<ConditionUnitTC>();

        public bool Is(in ConditionUnitTypes cond) => ConditionTC.Is(cond);

        internal CellUnitConditonE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {

        }

        public void Reset() => ConditionTCRef.Condition = ConditionUnitTypes.None;
        public void Set(in ConditionUnitTypes cond) => ConditionTCRef.Condition = cond;

        public void Condition_Master(in ConditionUnitTypes cond, in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            switch (cond)
            {
                case ConditionUnitTypes.None:
                    e.UnitEs(idx_0).ConditionE.Reset();
                    break;

                case ConditionUnitTypes.Protected:
                    if (e.UnitEs(idx_0).ConditionE.ConditionTC.Is(ConditionUnitTypes.Protected))
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitEs(idx_0).ConditionE.Reset();
                    }

                    else if (e.UnitStatEs(idx_0).StepE.HaveSteps)
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitStatEs(idx_0).StepE.Take(cond);
                        e.UnitEs(idx_0).ConditionE.Set(cond);
                    }

                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (e.UnitEs(idx_0).ConditionE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitEs(idx_0).ConditionE.Reset();
                    }

                    else if (e.UnitStatEs(idx_0).StepE.HaveSteps)
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitEs(idx_0).ConditionE.Set(cond);
                        e.UnitStatEs(idx_0).StepE.Take(cond);
                    }

                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}