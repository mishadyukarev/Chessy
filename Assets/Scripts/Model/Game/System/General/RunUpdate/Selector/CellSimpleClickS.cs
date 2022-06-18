using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed class CellSimpleClickS : SystemModel
    {
        readonly SelectorSoundS _selectorSoundS;

        internal CellSimpleClickS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _selectorSoundS = new SelectorSoundS(sMG, eMG);
        }

        internal void Execute()
        {
            if (_eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.ClickDefend))
            {
                return;
            }


            _eMG.MistakeTC.MistakeT = MistakeTypes.None;


            _eMG.DataFromViewC.AnimationCell(_eMG.CurrentCellIdx, AnimationCellTypes.AdultForest).Invoke();
            _eMG.DataFromViewC.AnimationCell(_eMG.CurrentCellIdx, AnimationCellTypes.JumpAppearanceUnit).Invoke();





            if (_eMG.CellsC.IsSelectedCell)
            {
                _eMG.IsSelectedCity = false;

                

                if (_eMG.LessonTC.HaveLesson)
                {
                    //if (eMG.LessonT == LessonTypes.ClickBuyMelterInTown) eMG.LessonTC.SetPreviousLesson();


                    //else
                    //{
                    //    if (eMG.LessonTC.Is(LessonTypes.PawnFireAdultForest)) eMG.LessonTC.SetPreviousLesson();
                    //}



                    if (_eMG.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_eMG.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_eMG.UnitT(_eMG.CurrentCellIdx) == UnitTypes.Pawn && _eMG.UnitPlayerT(_eMG.CurrentCellIdx) == _eMG.CurPlayerIT)
                            {
                                _eMG.LessonTC.SetNextLesson();
                            }
                        }

                        else if (_eMG.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_eMG.UnitTC(_eMG.CellsC.Current).Is(UnitTypes.Pawn))
                                {
                                    _eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }
                        //else if (eMG.LessonT == LessonTypes.ShiftPawnForFireForestHere)
                        //{
                        //    if (eMG.CurrentCellIdx == StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST)
                        //    {
                        //        if (eMG.UnitT(eMG.CurrentCellIdx) == UnitTypes.Pawn && eMG.MainToolWeaponTC(eMG.CurrentCellIdx).Is(ToolWeaponTypes.Axe))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}
                        



                        //if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                        //    {
                        //        if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn) && eMG.ExtraToolWeaponTC(eMG.CellsC.Current).Is(ToolWeaponTypes.Pick))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.ExtractHillPawnHere)) eMG.LessonTC.SetPreviousLesson();
                        //}

                        //if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.DrinkWaterHere))
                        //    {
                        //        if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    //if (eMG.LessonTC.Is(LessonTypes.BuildingFarmHere)) eMG.LessonTC.SetPreviousLesson();
                        //}



                        if (_eMG.UnitTC(_eMG.CellsC.Selected).HaveUnit)
                        {
                            if (_eMG.UnitTC(_eMG.CellsC.Current).HaveUnit)
                            {

                                if (_eMG.AttackSimpleCellsC(_eMG.CellsC.Selected).Contains(_eMG.CellsC.Current) || _eMG.AttackUniqueCellsC(_eMG.CellsC.Selected).Contains(_eMG.CellsC.Current))
                                {
                                    _eMG.RpcPoolEs.TryAttackUnit_ToMaster(_eMG.CellsC.Selected, _eMG.CellsC.Current);
                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                                else if (_eMG.UnitTC(_eMG.CellsC.Current).Is(UnitTypes.Pawn) && _eMG.UnitPlayerTC(_eMG.CellsC.Current).Is(_eMG.WhoseMovePlayerTC.PlayerT)
                                    || !_eMG.UnitPlayerTC(_eMG.CellsC.Current).Is(_eMG.WhoseMovePlayerTC.PlayerT))
                                {
                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }

                                if (_eMG.UnitT(_eMG.CellsC.Current) == UnitTypes.Snowy)
                                {
                                    if (_eMG.LessonT >= LessonTypes.ChangeDirectionWind)
                                    {
                                        _eMG.IsSelectedCity = false;

                                        SetNewSelectedCell();
                                        _selectorSoundS.Sound();
                                    }
                                }
                                else if (!_eMG.UnitTC(_eMG.CellsC.Current).Is(UnitTypes.King))
                                {
                                    _eMG.IsSelectedCity = false;

                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                            }

                            else
                            {
                                if (_eMG.UnitPlayerTC(_eMG.CellsC.Selected).Is(_eMG.CurPlayerITC.PlayerT) && _eMG.CellsForShift(_eMG.CellsC.Selected).Contains(_eMG.CellsC.Current))
                                {
                                    _eMG.RpcPoolEs.TryShiftUnit_ToMaster(_eMG.CellsC.Selected, _eMG.CellsC.Current);
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
                            if (_eMG.UnitT(_eMG.CellsC.Current) == UnitTypes.Snowy)
                            {
                                if (_eMG.LessonT >= LessonTypes.ChangeDirectionWind)
                                {
                                    _eMG.IsSelectedCity = false;

                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                            }
                            else if (!_eMG.UnitTC(_eMG.CellsC.Current).Is(UnitTypes.King))
                            {
                                _eMG.IsSelectedCity = false;

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
                    if (_eMG.UnitTC(_eMG.CellsC.Selected).HaveUnit)
                    {
                        if (_eMG.AttackSimpleCellsC(_eMG.CellsC.Selected).Contains(_eMG.CellsC.Current)
                        || _eMG.AttackUniqueCellsC(_eMG.CellsC.Selected).Contains(_eMG.CellsC.Current))
                        {
                            _eMG.RpcPoolEs.TryAttackUnit_ToMaster(_eMG.CellsC.Selected, _eMG.CellsC.Current);
                        }

                        else if (_eMG.UnitPlayerTC(_eMG.CellsC.Selected).Is(_eMG.CurPlayerITC.PlayerT)
                            && _eMG.CellsForShift(_eMG.CellsC.Selected).Contains(_eMG.CellsC.Current))
                        {
                            _eMG.RpcPoolEs.TryShiftUnit_ToMaster(_eMG.CellsC.Selected, _eMG.CellsC.Current);
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


                if (_eMG.LessonTC.HaveLesson)
                {
                    if (_eMG.LessonTC.Is(LessonTypes.TryBuyingHouse))
                    {

                    }

                    //else if (eMG.LessonTC.Is(LessonTypes.ClickBuyMelterInTown))
                    //{
                    //    eMG.LessonTC.SetPreviousLesson();

                    //}
                    else
                    {
                        _eMG.IsSelectedCity = false;
                    }

                    if (_eMG.LessonTC.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_eMG.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_eMG.UnitT(_eMG.CurrentCellIdx) == UnitTypes.Pawn && _eMG.UnitPlayerT(_eMG.CurrentCellIdx) == _eMG.CurPlayerIT)
                            {
                                _eMG.LessonTC.SetNextLesson();
                            }
                        }

                        else if (_eMG.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_eMG.UnitTC(_eMG.CellsC.Current).Is(UnitTypes.Pawn))
                                {
                                    _eMG.LessonTC.SetNextLesson();
                                }
                            }
                        }
                        //else if (eMG.LessonT == LessonTypes.ShiftPawnForSeedingHere)
                        //{
                        //    if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                        //    {
                        //        if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}
                        //else if (eMG.LessonT == LessonTypes.ShiftPawnForFireForestHere)
                        //{
                        //    if (eMG.CurrentCellIdx == StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST)
                        //    {
                        //        if (eMG.UnitT(eMG.CurrentCellIdx) == UnitTypes.Pawn)
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}


                        //if (eMG.CellsC.Current == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.ShiftPawnHere))
                        //    {
                        //        if (eMG.UnitTC(eMG.CellsC.Current).Is(UnitTypes.Pawn))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}

                        if (_eMG.UnitTC(_eMG.CellsC.Current).Is(UnitTypes.Pawn) || !_eMG.UnitTC(_eMG.CellsC.Current).HaveUnit)
                        {
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }

                        //if (eMG.UnitT(eMG.CellsC.Current) == UnitTypes.Snowy)
                        //{
                        //    if (eMG.LessonT >= LessonTypes.ChangeDirectionWind)
                        //    {
                        //        SetNewSelectedCell();
                        //        _selectorSoundS.Sound();
                        //    }
                        //}
                    }
                }

                else
                {
                    _eMG.IsSelectedCity = false;

                    SetNewSelectedCell();
                    _selectorSoundS.Sound();
                }
            }
        }

        void SetNewSelectedCell()
        {
            _eMG.CellsC.PreviousSelected = _eMG.CellsC.Selected;
            _eMG.CellsC.Selected = _eMG.CellsC.Current;
        }
    }
}