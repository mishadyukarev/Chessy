using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Game.Model.System.Master
{
    sealed class TrySetConditionUnitOnCellS_M : SystemModel
    {
        internal TrySetConditionUnitOnCellS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void TrySet(in ConditionUnitTypes condT, in byte cell_0, in Player sender)
        {
            if (!eMG.StunUnitC(cell_0).IsStunned)
            {
                switch (condT)
                {
                    case ConditionUnitTypes.None:
                        eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                        break;

                    case ConditionUnitTypes.Protected:
                        if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                        }

                        else if (eMG.StepUnitC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                            eMG.UnitConditionTC(cell_0).Condition = condT;
                        }

                        else
                        {
                            eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;


                    case ConditionUnitTypes.Relaxed:
                        if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                        }

                        else if (eMG.StepUnitC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            eMG.UnitConditionTC(cell_0).Condition = condT;
                            eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                            {
                                if (!eMG.BuildingTC(cell_0).HaveBuilding)
                                {
                                    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                                    {
                                        if (eMG.HpUnitC(cell_0).Health >= HpValues.MAX)
                                        {
                                            if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale))
                                            {
                                                sMG.BuildingSs.BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMG.UnitPlayerTC(cell_0).PlayerT, BuildingValues.MAX_HP, cell_0);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }

            else
            {
                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}