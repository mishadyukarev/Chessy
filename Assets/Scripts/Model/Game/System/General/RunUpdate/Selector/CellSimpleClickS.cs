using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed class CellSimpleClickS : SystemModelGameAbs
    {
        readonly SelectorSoundS _selectorSoundS;

        internal CellSimpleClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
            _selectorSoundS = new SelectorSoundS(sMC, eMC, sMG, eMG);
        }

        internal void Execute()
        {
            eMG.MistakeC.MistakeT = MistakeTypes.None;

            if (eMG.CellsC.IsSelectedCell)
            {
                eMG.IsSelectedCity = false;

                if (eMG.LessonTC.HaveLesson)
                {
                    if (eMG.LessonTC.Is(LessonTypes.ClickBuyMelterInTown)) eMG.LessonTC.SetPreviousLesson();

                    if (eMG.LessonTC.LessonT >= LessonTypes.ShiftPawnHere)
                    {
                        if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        {
                            if (eMG.LessonTC.Is(LessonTypes.ShiftPawnHere))
                            {
                                if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn))
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }
                        else
                        {
                            if (eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn)) eMG.LessonTC.SetPreviousLesson();
                        }

                        if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                        {
                            if (eMG.LessonTC.Is(LessonTypes.ShiftPawnForSeedingHere))
                            {
                                if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn))
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }
                        else
                        {
                            if (eMG.LessonTC.Is(LessonTypes.SeedingPawn)) eMG.LessonTC.SetPreviousLesson();
                        }

                        if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                        {
                            if (eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                            {
                                if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn) && eMG.UnitExtraTWTC(eMG.CellsC.Current).Is(ToolWeaponTypes.Pick))
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }
                        else
                        {
                            if (eMG.LessonTC.Is(LessonTypes.ExtractHillPawnHere)) eMG.LessonTC.SetPreviousLesson();
                        }

                        if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                        {
                            if (eMG.LessonTC.Is(LessonTypes.DrinkWaterHere))
                            {
                                if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn))
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }
                        else
                        {
                            if (eMG.LessonTC.Is(LessonTypes.BuildingFarmHere)) eMG.LessonTC.SetPreviousLesson();
                        }



                        if (eMG.UnitTC(eMG.CellsC.Selected).HaveUnit)
                        {
                            if (eMG.UnitTC(eMG.CellsC.Current).HaveUnit)
                            {

                                if (eMG.UnitEs(eMG.CellsC.Selected).SimpleAttack.Contains(eMG.CellsC.Current) || eMG.UnitEs(eMG.CellsC.Selected).UniqueAttack.Contains(eMG.CellsC.Current))
                                {
                                    eMG.RpcPoolEs.TryAttackUnit_ToMaster(eMG.CellsC.Selected, eMG.CellsC.Current);
                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                                else if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn) && eMG.UnitPlayerTC(eMG.CellsC.Current).Is(eMG.WhoseMove.PlayerT)
                                    || !eMG.UnitPlayerTC(eMG.CellsC.Current).Is(eMG.WhoseMove.PlayerT))
                                {
                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                            }

                            else
                            {
                                if (eMG.UnitPlayerTC(eMG.CellsC.Selected).Is(eMG.CurPlayerITC.PlayerT) && eMG.UnitEs(eMG.CellsC.Selected).ForShift.Contains(eMG.CellsC.Current))
                                {
                                    eMG.RpcPoolEs.TryShiftUnit_ToMaster(eMG.CellsC.Selected, eMG.CellsC.Current);
                                }
                                else
                                {
                                    _selectorSoundS.Sound();
                                }

                                SetNewSelectedCell();
                            }
                        }

                        else
                        {
                            if (!eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.King, UnitTypes.Elfemale))
                            {
                                eMG.IsSelectedCity = false;

                                SetNewSelectedCell();
                                _selectorSoundS.Sound();
                            }
                        }

                        //if (e.UnitTC(e.CellsC.Current).Is(UnitTypes.Pawn) || !e.UnitTC(e.CellsC.Current).HaveUnit)
                        //{

                        //}
                    }
                }

                else
                {
                    if (eMG.UnitTC(eMG.CellsC.Selected).HaveUnit)
                    {
                        if (eMG.UnitEs(eMG.CellsC.Selected).SimpleAttack.Contains(eMG.CellsC.Current)
                        || eMG.UnitEs(eMG.CellsC.Selected).UniqueAttack.Contains(eMG.CellsC.Current))
                        {
                            eMG.RpcPoolEs.TryAttackUnit_ToMaster(eMG.CellsC.Selected, eMG.CellsC.Current);
                        }

                        else if (eMG.UnitPlayerTC(eMG.CellsC.Selected).Is(eMG.CurPlayerITC.PlayerT)
                            && eMG.UnitEs(eMG.CellsC.Selected).ForShift.Contains(eMG.CellsC.Current))
                        {
                            eMG.RpcPoolEs.TryShiftUnit_ToMaster(eMG.CellsC.Selected, eMG.CellsC.Current);
                        }

                        else
                        {
                            _selectorSoundS.Sound();
                        }
                    }

                    else
                    {
                        _selectorSoundS.Sound();
                    }

                    SetNewSelectedCell();
                }
            }

            else
            {


                if (eMG.LessonTC.HaveLesson)
                {
                    if (eMG.LessonTC.Is(LessonTypes.BuyingHouse))
                    {

                    }

                    else if (eMG.LessonTC.Is(LessonTypes.ClickBuyMelterInTown))
                    {
                        eMG.LessonTC.SetPreviousLesson();

                    }
                    else
                    {
                        eMG.IsSelectedCity = false;
                    }

                    if (eMG.LessonTC.LessonT >= LessonTypes.ShiftPawnHere)
                    {
                        if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        {
                            if (eMG.LessonTC.Is(LessonTypes.ShiftPawnHere))
                            {
                                if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn))
                                {
                                    eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }

                        if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn) || !eMG.UnitTC(eMG.CellsC.Current).HaveUnit)
                        {
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }
                    }
                }

                else
                {
                    eMG.IsSelectedCity = false;

                    SetNewSelectedCell();
                    _selectorSoundS.Sound();
                }
            }
        }

        void SetNewSelectedCell()
        {
            eMG.CellsC.PreviousSelected = eMG.CellsC.Selected;
            eMG.CellsC.Selected = eMG.CellsC.Current;
        }
    }
}