using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Game.System.Model.Master
{
    public struct SetConditionUnitS_M
    {
        public SetConditionUnitS_M(in byte idx_0, in ConditionUnitTypes condT, in Player sender, in EntitiesModel e)
        {
            switch (condT)
            {
                case ConditionUnitTypes.None:
                    e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    break;

                case ConditionUnitTypes.Protected:
                    if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitStepC(idx_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitStepC(idx_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                        e.UnitConditionTC(idx_0).Condition = condT;
                    }

                    else
                    {
                        e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitStepC(idx_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(idx_0).Condition = condT;
                        e.UnitStepC(idx_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            if (e.UnitHpC(idx_0).Health >= HpValues.MAX)
                            {
                                if (e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                                {
                                    new BuildS(BuildingTypes.Woodcutter, LevelTypes.First, e.UnitPlayerTC(idx_0).Player, BuildingValues.MAX_HP, idx_0, e);
                                }
                            }
                        }
                    }

                    else
                    {
                        e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}