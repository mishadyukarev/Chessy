using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Realtime;
using System;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TrySetConditionUnitOnCellM(in ConditionUnitTypes condT, in byte cell_0, in Player sender)
        {
            if (!_e.UnitEffectsC(cell_0).IsStunned)
            {
                switch (condT)
                {
                    case ConditionUnitTypes.None:
                        _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);
                        break;

                    case ConditionUnitTypes.Protected:
                        if (_e.UnitConditionT(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);
                        }

                        else if (_e.EnergyUnitC(cell_0).Energy >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.EnergyUnitC(cell_0).Energy -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                            _e.SetUnitConditionT(cell_0, condT);

                            if (_e.LessonT == LessonTypes.ClickDefend)  SetNextLesson();
                        }

                        else
                        {
                            RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;


                    case ConditionUnitTypes.Relaxed:
                        if (_e.UnitConditionT(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);
                        }

                        else if (_e.EnergyUnitC(cell_0).Energy >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cell_0, condT);
                            _e.EnergyUnitC(cell_0).Energy -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                            if (_e.UnitT(cell_0).Is(UnitTypes.Pawn))
                            {
                                if (!_e.HaveBuildingOnCell(cell_0))
                                {
                                    if (_e.AdultForestC(cell_0).HaveAnyResources)
                                    {
                                        if (_e.HpUnitC(cell_0).Health >= HpValues.MAX)
                                        {
                                            if (_e.PlayerInfoE(_e.UnitPlayerT(cell_0)).GodInfoC.UnitT.Is(UnitTypes.Elfemale))
                                            {
                                                _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _e.UnitPlayerT(cell_0), ValuesChessy.MAX_HP_ANY_BUILDING, cell_0);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }

            else
            {
                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}