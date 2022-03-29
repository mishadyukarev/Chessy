using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Game.System.Model.Master
{
    sealed class SetConditionUnitS_M : SystemModelGameAbs
    {
        readonly SystemsModelGame _sMGame;

        internal SetConditionUnitS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMGame = sMGame;
        }

        internal void Set(in ConditionUnitTypes condT, in byte cell_0, in Player sender)
        {
            switch (condT)
            {
                case ConditionUnitTypes.None:
                    e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    break;

                case ConditionUnitTypes.Protected:
                    if (e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitStepC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitStepC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                        e.UnitConditionTC(cell_0).Condition = condT;
                    }

                    else
                    {
                        e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitStepC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(cell_0).Condition = condT;
                        e.UnitStepC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                        if (e.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        {
                            if (!e.BuildingTC(cell_0).HaveBuilding)
                            {
                                if (e.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (e.UnitHpC(cell_0).Health >= HpValues.MAX)
                                    {
                                        if (e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            _sMGame.CellSs(cell_0).BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, e.UnitPlayerTC(cell_0).Player, BuildingValues.MAX_HP);
                                        }
                                    }
                                }
                            }

                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (e.LessonTC.Is(LessonTypes.RelaxExtractPawn))
                                {
                                    e.LessonTC.SetNextLesson();
                                }
                            }
                            else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                            {
                                if (e.UnitExtraTWTC(cell_0).Is(ToolWeaponTypes.Pick))
                                {
                                    if (e.LessonTC.Is(LessonTypes.ExtractHillPawnHere))
                                    {
                                        e.LessonTC.SetNextLesson();
                                        e.IsSelectedCity = true;
                                    }
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