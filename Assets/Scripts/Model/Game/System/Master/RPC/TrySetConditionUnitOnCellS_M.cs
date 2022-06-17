using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void TrySetConditionUnitOnCellM(in ConditionUnitTypes condT, in byte cell_0, in Player sender)
        {
            if (!_eMG.StunUnitC(cell_0).IsStunned)
            {
                switch (condT)
                {
                    case ConditionUnitTypes.None:
                        _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                        break;

                    case ConditionUnitTypes.Protected:
                        if (_eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                        }

                        else if (_eMG.StepUnitC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                            _eMG.UnitConditionTC(cell_0).Condition = condT;

                            if (_eMG.LessonT == LessonTypes.ClickDefend) _eMG.LessonTC.SetNextLesson();
                        }

                        else
                        {
                            _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;


                    case ConditionUnitTypes.Relaxed:
                        if (_eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                        }

                        else if (_eMG.StepUnitC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            _eMG.UnitConditionTC(cell_0).Condition = condT;
                            _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                            if (_eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                            {
                                if (!_eMG.BuildingTC(cell_0).HaveBuilding)
                                {
                                    if (_eMG.AdultForestC(cell_0).HaveAnyResources)
                                    {
                                        if (_eMG.HpUnitC(cell_0).Health >= HpValues.MAX)
                                        {
                                            if (_eMG.PlayerInfoE(_eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale))
                                            {
                                                BuildingSs.Build(BuildingTypes.Woodcutter, LevelTypes.First, _eMG.UnitPlayerTC(cell_0).PlayerT, BuildingValues.MAX_HP, cell_0);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }

            else
            {
                _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}