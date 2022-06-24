using Chessy.Model.Enum;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Photon.Pun;

namespace Chessy.Model.Model.System
{
    sealed class CellSimpleClickS : SystemModel
    {
        readonly SelectorSoundS _selectorSoundS;

        internal CellSimpleClickS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _selectorSoundS = new SelectorSoundS(sMG, eMG);
        }

        internal void Execute()
        {
            if (_e.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.ClickDefend))
            {
                return;
            }


            _e.MistakeT = MistakeTypes.None;


            _e.DataFromViewC.AnimationCell(_e.CurrentCellIdx, AnimationCellTypes.AdultForest).Invoke();
            _e.DataFromViewC.AnimationCell(_e.CurrentCellIdx, AnimationCellTypes.JumpAppearanceUnit).Invoke();





            if (_e.CellsC.IsSelectedCell)
            {
                _e.IsSelectedCity = false;



                if (_e.LessonT.HaveLesson())
                {
                    //if (eMG.LessonT == LessonTypes.ClickBuyMelterInTown) eMG.LessonTC.SetPreviousLesson();


                    //else
                    //{
                    //    if (eMG.LessonTC.Is(LessonTypes.PawnFireAdultForest)) eMG.LessonTC.SetPreviousLesson();
                    //}



                    if (_e.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_e.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Pawn && _e.UnitPlayerT(_e.CurrentCellIdx) == _e.CurPlayerIT)
                            {
                                _e.CommonInfoAboutGameC.SetNextLesson();
                            }
                        }

                        else if (_e.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_e.CurrentCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn))
                                {
                                    _e.CommonInfoAboutGameC.SetNextLesson();
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




                        //if (eMG.CurrentCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                        //    {
                        //        if (eMG.UnitTC(eMG.CurrentCellIdx).Is(UnitTypes.Pawn) && eMG.ExtraToolWeaponTC(eMG.CurrentCellIdx).Is(ToolWeaponTypes.Pick))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.ExtractHillPawnHere)) eMG.LessonTC.SetPreviousLesson();
                        //}

                        //if (eMG.CurrentCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.DrinkWaterHere))
                        //    {
                        //        if (eMG.UnitTC(eMG.CurrentCellIdx).Is(UnitTypes.Pawn))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    //if (eMG.LessonTC.Is(LessonTypes.BuildingFarmHere)) eMG.LessonTC.SetPreviousLesson();
                        //}



                        if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                        {
                            if (_e.UnitT(_e.CurrentCellIdx).HaveUnit())
                            {

                                if (_e.WhereUnitCanAttackSimpleAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx) || _e.WhereUnitCanAttackUniqueAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx))
                                {
                                    TryAttack(_e.SelectedCellIdx, _e.CurrentCellIdx);
                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                                else if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn) && _e.UnitPlayerT(_e.CurrentCellIdx).Is(_e.WhoseMovePlayerT)
                                    || !_e.UnitPlayerT(_e.CurrentCellIdx).Is(_e.WhoseMovePlayerT))
                                {
                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }

                                if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Snowy)
                                {
                                    if (_e.LessonT >= LessonTypes.ChangeDirectionWind)
                                    {
                                        _e.IsSelectedCity = false;

                                        SetNewSelectedCell();
                                        _selectorSoundS.Sound();
                                    }
                                }
                                else if (!_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.King))
                                {
                                    _e.IsSelectedCity = false;

                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                            }

                            else
                            {
                                if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_e.CurPlayerIT) && _e.WhereUnitCanShiftC(_e.SelectedCellIdx).CanShiftHere(_e.CurrentCellIdx))
                                {
                                    TryShift(_e.SelectedCellIdx, _e.CurrentCellIdx);
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
                            if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Snowy)
                            {
                                if (_e.LessonT >= LessonTypes.ChangeDirectionWind)
                                {
                                    _e.IsSelectedCity = false;

                                    SetNewSelectedCell();
                                    _selectorSoundS.Sound();
                                }
                            }
                            else if (!_e.UnitT(_e.SelectedCellIdx).Is(UnitTypes.King))
                            {
                                _e.IsSelectedCity = false;

                                SetNewSelectedCell();
                                _selectorSoundS.Sound();
                            }
                        }

                        //if (e.UnitTC(e.CurrentCellIdx).Is(UnitTypes.Pawn) || !e.UnitTC(e.CurrentCellIdx).HaveUnit())
                        //{

                        //}
                    }
                }

                else
                {
                    if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                    {
                        if (_e.WhereUnitCanAttackSimpleAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx)
                        || _e.WhereUnitCanAttackUniqueAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx))
                        {
                            TryAttack(_e.SelectedCellIdx, _e.CurrentCellIdx);
                        }

                        else if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_e.CurPlayerIT)
                            && _e.WhereUnitCanShiftC(_e.SelectedCellIdx).CanShiftHere(_e.CurrentCellIdx))
                        {
                            TryShift(_e.SelectedCellIdx, _e.CurrentCellIdx);
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


                if (_e.LessonT.HaveLesson())
                {
                    if (_e.LessonT.Is(LessonTypes.TryBuyingHouse))
                    {

                    }

                    //else if (eMG.LessonTC.Is(LessonTypes.ClickBuyMelterInTown))
                    //{
                    //    eMG.LessonTC.SetPreviousLesson();

                    //}
                    else
                    {
                        _e.IsSelectedCity = false;
                    }

                    if (_e.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_e.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Pawn && _e.UnitPlayerT(_e.CurrentCellIdx) == _e.CurPlayerIT)
                            {
                                _e.CommonInfoAboutGameC.SetNextLesson();
                            }
                        }

                        else if (_e.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_e.CurrentCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn))
                                {
                                    _e.CommonInfoAboutGameC.SetNextLesson();
                                }
                            }
                        }
                        //else if (eMG.LessonT == LessonTypes.ShiftPawnForSeedingHere)
                        //{
                        //    if (eMG.CurrentCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                        //    {
                        //        if (eMG.UnitTC(eMG.CurrentCellIdx).Is(UnitTypes.Pawn))
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


                        //if (eMG.CurrentCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        //{
                        //    if (eMG.LessonTC.Is(LessonTypes.ShiftPawnHere))
                        //    {
                        //        if (eMG.UnitTC(eMG.CurrentCellIdx).Is(UnitTypes.Pawn))
                        //        {
                        //            eMG.LessonTC.SetNextLesson();
                        //        }
                        //    }
                        //}

                        if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn) || !_e.UnitT(_e.CurrentCellIdx).HaveUnit())
                        {
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }

                        //if (eMG.UnitT(eMG.CurrentCellIdx) == UnitTypes.Snowy)
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
                    _e.IsSelectedCity = false;

                    SetNewSelectedCell();
                    _selectorSoundS.Sound();
                }
            }
        }

        void SetNewSelectedCell()
        {
            _e.CellsC.PreviousSelected = _e.SelectedCellIdx;
            _e.SelectedCellIdx = _e.CurrentCellIdx;
        }

        void TryShift(in byte idxCellFrom, in byte idxCellTo)
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryShiftUnitOntoOtherCellM), idxCellFrom, idxCellTo });
        }

        void TryAttack(in byte idxCellFrom, in byte idxCellTo)
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryAttackUnitOnCellM), idxCellFrom, idxCellTo });
        }
    }
}