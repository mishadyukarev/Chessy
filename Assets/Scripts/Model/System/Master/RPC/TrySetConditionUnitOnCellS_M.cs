using Chessy.Model.Enum;
using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel
    {
        internal void TrySetConditionUnitOnCellM(in ConditionUnitTypes condT, in byte cell_0, in Player sender)
        {
            if (!_e.StunUnitC(cell_0).IsStunned)
            {
                switch (condT)
                {
                    case ConditionUnitTypes.None:
                        _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);
                        break;

                    case ConditionUnitTypes.Protected:
                        if (_e.UnitConditionT(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);
                        }

                        else if (_e.StepUnitC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.StepUnitC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                            _e.SetUnitConditionT(cell_0, condT);

                            if (_e.LessonT == LessonTypes.ClickDefend) _e.LessonT.SetNextLesson();
                        }

                        else
                        {
                            SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;


                    case ConditionUnitTypes.Relaxed:
                        if (_e.UnitConditionT(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);
                        }

                        else if (_e.StepUnitC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cell_0, condT);
                            _e.StepUnitC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                            if (_e.UnitT(cell_0).Is(UnitTypes.Pawn))
                            {
                                if (!_e.HaveBuildingOnCell(cell_0))
                                {
                                    if (_e.AdultForestC(cell_0).HaveAnyResources)
                                    {
                                        if (_e.HpUnitC(cell_0).Health >= HpValues.MAX)
                                        {
                                            if (_e.PlayerInfoE(_e.UnitPlayerT(cell_0)).GodInfoE.UnitT.Is(UnitTypes.Elfemale))
                                            {
                                                _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _e.UnitPlayerT(cell_0), BuildingValues.MAX_HP, cell_0);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }

            else
            {
                ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}