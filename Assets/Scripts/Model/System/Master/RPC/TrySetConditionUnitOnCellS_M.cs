using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Realtime;
using System;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TrySetConditionUnitOnCellM(in ConditionUnitTypes condT, in byte cellIdx, in Player sender)
        {
            if (!_e.UnitEffectsC(cellIdx).IsStunned)
            {
                switch (condT)
                {
                    case ConditionUnitTypes.None:
                        _e.SetUnitConditionT(cellIdx, ConditionUnitTypes.None);
                        break;

                    case ConditionUnitTypes.Protected:
                        if (_e.UnitConditionT(cellIdx).Is(ConditionUnitTypes.Protected))
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cellIdx, ConditionUnitTypes.None);
                        }
                        else
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            //_e.EnergyUnitC(cellIdx).Energy -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                            _e.SetUnitConditionT(cellIdx, condT);

                            if (_e.LessonT == LessonTypes.ClickDefend) SetNextLesson();
                        }

                        //else if (_e.EnergyUnitC(cellIdx).Energy >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        //{

                        //}

                        //else
                        //{
                        //    RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        //}
                        break;


                    case ConditionUnitTypes.Relaxed:
                        if (_e.UnitConditionT(cellIdx).Is(ConditionUnitTypes.Relaxed))
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cellIdx, ConditionUnitTypes.None);
                        }

                        //else if (_e.EnergyUnitC(cellIdx).Energy >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        //{

                        //}

                        else
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _e.SetUnitConditionT(cellIdx, condT);
                            //_e.EnergyUnitC(cellIdx).Energy -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                            if (_e.UnitT(cellIdx).Is(UnitTypes.Pawn))
                            {
                                if (!_e.HaveBuildingOnCell(cellIdx))
                                {
                                    if (_e.AdultForestC(cellIdx).HaveAnyResources)
                                    {
                                        if (_e.HpUnitC(cellIdx).Health >= HpValues.MAX)
                                        {
                                            if (_e.PlayerInfoE(_e.UnitPlayerT(cellIdx)).GodInfoC.UnitT.Is(UnitTypes.Elfemale))
                                            {
                                                _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _e.UnitPlayerT(cellIdx), ValuesChessy.MAX_HP_ANY_BUILDING, cellIdx);
                                            }
                                        }
                                    }
                                }
                            }

                            //RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                        break;

                    default:
                        throw new Exception();
                }

                _e.UnitMainC(cellIdx).HowManySecondUnitWasHereInThisCondition = 0;
            }

            else
            {
                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}