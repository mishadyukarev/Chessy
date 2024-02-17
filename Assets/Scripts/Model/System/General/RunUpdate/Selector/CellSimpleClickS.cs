using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
namespace Chessy.Model.System
{
    sealed class CellSimpleClickS : SystemModelAbstract
    {
        readonly SelectorSoundS _selectorSoundS;

        internal CellSimpleClickS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _selectorSoundS = new SelectorSoundS(sMG, eMG);
        }

        internal void Execute()
        {
            //if (_aboutGameC.LessonT.Is(LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.ClickDefend))
            //{
            //    return;
            //}


            mistakeC.MistakeT = MistakeTypes.None;


            dataFromViewC.AnimationCell(indexesCellsC.Current, AnimationCellTypes.AdultForest).Invoke();
            dataFromViewC.AnimationCell(_unitWhereViewDataCs[indexesCellsC.Current].ViewIdxCell, AnimationCellTypes.JumpAppearanceUnit).Invoke();





            if (indexesCellsC.IsSelectedCell)
            {
                aboutGameC.IsSelectedCity = false;



                if (aboutGameC.LessonT.HaveLesson())
                {
                    if (aboutGameC.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (aboutGameC.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (unitCs[indexesCellsC.Current].UnitT == UnitTypes.Pawn && unitCs[indexesCellsC.Current].PlayerT == aboutGameC.CurrentPlayerIT)
                            {
                                s.SetNextLesson();
                            }
                        }

                        else if (aboutGameC.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (indexesCellsC.Current == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (unitCs[indexesCellsC.Current].UnitT == UnitTypes.Pawn)
                                {
                                    s.SetNextLesson();
                                }
                            }
                        }

                        if (unitCs[indexesCellsC.Selected].HaveUnit)
                        {
                            if (unitCs[indexesCellsC.Current].HaveUnit)
                            {
                                if (_whereSimpleAttackCs[indexesCellsC.Selected].Can(indexesCellsC.Current) || _whereUniqueAttackCs[indexesCellsC.Selected].Can(indexesCellsC.Current))
                                {
                                    TryAttack(indexesCellsC.Selected, indexesCellsC.Current);
                                    //_selectorSoundS.Sound();
                                }
                                //else if (_unitCs[_cellsC.Current).Is(UnitTypes.Pawn) && _unitCs[_cellsC.Current).Is(_aboutGameC.CurrentPlayerIT)
                                //    || !_unitCs[_cellsC.Current).Is(_aboutGameC.CurrentPlayerIT))
                                //{
                                //    //_selectorSoundS.Sound();
                                //}

                                //if (_unitCs[_cellsC.Current) == UnitTypes.Snowy)
                                //{
                                //    if (_aboutGameC.LessonT >= LessonTypes.ChangeDirectionWind)
                                //    {
                                //        _aboutGameC.IsSelectedCity = false;

                                //        _selectorSoundS.Sound();
                                //    }
                                //}
                                //else if (!_unitCs[_cellsC.Current).Is(UnitTypes.King))
                                //{



                                //}

                                aboutGameC.IsSelectedCity = false;
                                SetNewSelectedCell();
                                _selectorSoundS.Sound();
                            }

                            else
                            {
                                if (unitCs[indexesCellsC.Selected].PlayerT.Is(aboutGameC.CurrentPlayerIT) && WhereUnitCanShiftC(indexesCellsC.Selected).CanShiftHere(indexesCellsC.Current))
                                {
                                    TryShift(indexesCellsC.Selected, indexesCellsC.Current);
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
                            //if (_unitCs[_cellsC.Current) == UnitTypes.Snowy)
                            //{
                            //    if (_aboutGameC.LessonT >= LessonTypes.ChangeDirectionWind)
                            //    {



                            //        _selectorSoundS.Sound();
                            //    }
                            //}
                            //else if (!_unitCs[_cellsC.Selected).Is(UnitTypes.King))
                            //{
                            //    _aboutGameC.IsSelectedCity = false;
                            //}
                            aboutGameC.IsSelectedCity = false;
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }
                    }
                }

                else
                {
                    if (unitCs[indexesCellsC.Selected].HaveUnit)
                    {
                        if (_whereSimpleAttackCs[indexesCellsC.Selected].Can(indexesCellsC.Current)
                        || _whereUniqueAttackCs[indexesCellsC.Selected].Can(indexesCellsC.Current))
                        {
                            TryAttack(indexesCellsC.Selected, indexesCellsC.Current);
                        }

                        else if (unitCs[indexesCellsC.Selected].PlayerT.Is(aboutGameC.CurrentPlayerIT)
                            && WhereUnitCanShiftC(indexesCellsC.Selected).CanShiftHere(indexesCellsC.Current))
                        {
                            TryShift(indexesCellsC.Selected, indexesCellsC.Current);
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


                if (aboutGameC.LessonT.HaveLesson())
                {
                    if (aboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse))
                    {

                    }
                    else
                    {
                        aboutGameC.IsSelectedCity = false;
                    }

                    if (aboutGameC.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (aboutGameC.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (unitCs[indexesCellsC.Current].UnitT == UnitTypes.Pawn && unitCs[indexesCellsC.Current].PlayerT == aboutGameC.CurrentPlayerIT)
                            {
                                s.SetNextLesson();
                            }
                        }

                        else if (aboutGameC.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (indexesCellsC.Current == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (unitCs[indexesCellsC.Current].UnitT == UnitTypes.Pawn)
                                {
                                    s.SetNextLesson();
                                }
                            }
                        }

                        if (unitCs[indexesCellsC.Current].UnitT == UnitTypes.Pawn || !unitCs[indexesCellsC.Current].HaveUnit)
                        {
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }
                    }
                }

                else
                {
                    aboutGameC.IsSelectedCity = false;

                    SetNewSelectedCell();
                    _selectorSoundS.Sound();
                }
            }
        }

        void SetNewSelectedCell()
        {
            indexesCellsC.PreviousSelected = indexesCellsC.Selected;
            indexesCellsC.Selected = indexesCellsC.Current;
        }

        void TryShift(in byte idxCellFrom, in byte idxCellTo)
        {
            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryShiftUnitOntoOtherCellM), idxCellFrom, idxCellTo });
        }

        void TryAttack(in byte idxCellFrom, in byte idxCellTo)
        {
            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryAttackUnitOnCellM), idxCellFrom, idxCellTo });
        }
    }
}