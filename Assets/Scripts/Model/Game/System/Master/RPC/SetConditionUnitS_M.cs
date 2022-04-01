﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Game.Model.System.Master
{
    sealed class SetConditionUnitS_M : SystemModelGameAbs
    {
        internal SetConditionUnitS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in ConditionUnitTypes condT, in byte cell_0, in Player sender)
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

                    else if (eMG.UnitStepC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        eMG.UnitStepC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
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

                    else if (eMG.UnitStepC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        eMG.UnitConditionTC(cell_0).Condition = condT;
                        eMG.UnitStepC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        {
                            if (!eMG.BuildingTC(cell_0).HaveBuilding)
                            {
                                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (eMG.UnitHpC(cell_0).Health >= HpValues.MAX)
                                    {
                                        if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            sMG.BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMG.UnitPlayerTC(cell_0).PlayerT, BuildingValues.MAX_HP, cell_0);
                                        }
                                    }
                                }
                            }

                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn))
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }
                            //else if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                            //{
                            //    if (e.UnitExtraTWTC(cell_0).Is(ToolWeaponTypes.Pick))
                            //    {
                            //        if (e.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                            //        {
                            //            e.LessonTC.SetNextLesson();
                            //            e.IsSelectedCity = true;
                            //        }
                            //    }
                            //}
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
    }
}