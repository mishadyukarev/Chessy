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


            _mistakeC.MistakeT = MistakeTypes.None;


            _dataFromViewC.AnimationCell(_cellsC.Current, AnimationCellTypes.AdultForest).Invoke();
            _dataFromViewC.AnimationCell(_unitWhereViewDataCs[_cellsC.Current].ViewIdxCell, AnimationCellTypes.JumpAppearanceUnit).Invoke();





            if (_cellsC.IsSelectedCell)
            {
                _e.IsSelectedCity = false;



                if (_aboutGameC.LessonT.HaveLesson())
                {
                    if (_aboutGameC.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_aboutGameC.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_e.UnitT(_cellsC.Current) == UnitTypes.Pawn && _unitCs[_cellsC.Current].PlayerT == _aboutGameC.CurrentPlayerIT)
                            {
                                 _s.SetNextLesson();
                            }
                        }

                        else if (_aboutGameC.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_cellsC.Current == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_e.UnitT(_cellsC.Current).Is(UnitTypes.Pawn))
                                {
                                     _s.SetNextLesson();
                                }
                            }
                        }

                        if (_e.UnitT(_cellsC.Selected).HaveUnit())
                        {
                            if (_e.UnitT(_cellsC.Current).HaveUnit())
                            {
                                if (_whereSimpleAttackCs[_cellsC.Selected].Can(_cellsC.Current) || _whereUniqueAttackCs[_cellsC.Selected].Can(_cellsC.Current))
                                {
                                    TryAttack(_cellsC.Selected, _cellsC.Current);
                                    //_selectorSoundS.Sound();
                                }
                                //else if (_e.UnitT(_cellsC.Current).Is(UnitTypes.Pawn) && _unitCs[_cellsC.Current).Is(_aboutGameC.CurrentPlayerIT)
                                //    || !_unitCs[_cellsC.Current).Is(_aboutGameC.CurrentPlayerIT))
                                //{
                                //    //_selectorSoundS.Sound();
                                //}

                                //if (_e.UnitT(_cellsC.Current) == UnitTypes.Snowy)
                                //{
                                //    if (_aboutGameC.LessonT >= LessonTypes.ChangeDirectionWind)
                                //    {
                                //        _e.IsSelectedCity = false;

                                //        _selectorSoundS.Sound();
                                //    }
                                //}
                                //else if (!_e.UnitT(_cellsC.Current).Is(UnitTypes.King))
                                //{
                                    

                                    
                                //}

                                _e.IsSelectedCity = false;
                                SetNewSelectedCell();
                                _selectorSoundS.Sound();
                            }

                            else
                            {
                                if (_unitCs[_cellsC.Selected].PlayerT.Is(_aboutGameC.CurrentPlayerIT) && _whereUnitCanShiftCs[_cellsC.Selected].CanShiftHere(_cellsC.Current))
                                {
                                    TryShift(_cellsC.Selected, _cellsC.Current);
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
                            //if (_e.UnitT(_cellsC.Current) == UnitTypes.Snowy)
                            //{
                            //    if (_aboutGameC.LessonT >= LessonTypes.ChangeDirectionWind)
                            //    {
                                   

                                   
                            //        _selectorSoundS.Sound();
                            //    }
                            //}
                            //else if (!_e.UnitT(_cellsC.Selected).Is(UnitTypes.King))
                            //{
                            //    _e.IsSelectedCity = false;
                            //}
                            _e.IsSelectedCity = false;
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }
                    }
                }

                else
                {
                    if (_e.UnitT(_cellsC.Selected).HaveUnit())
                    {
                        if (_whereSimpleAttackCs[_cellsC.Selected].Can(_cellsC.Current)
                        || _whereUniqueAttackCs[_cellsC.Selected].Can(_cellsC.Current))
                        {
                            TryAttack(_cellsC.Selected, _cellsC.Current);
                        }

                        else if (_unitCs[_cellsC.Selected].PlayerT.Is(_aboutGameC.CurrentPlayerIT)
                            && _whereUnitCanShiftCs[_cellsC.Selected].CanShiftHere(_cellsC.Current))
                        {
                            TryShift(_cellsC.Selected, _cellsC.Current);
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


                if (_aboutGameC.LessonT.HaveLesson())
                {
                    if (_aboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse))
                    {

                    }
                    else
                    {
                        _e.IsSelectedCity = false;
                    }

                    if (_aboutGameC.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_aboutGameC.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_e.UnitT(_cellsC.Current) == UnitTypes.Pawn && _unitCs[_cellsC.Current].PlayerT == _aboutGameC.CurrentPlayerIT)
                            {
                                 _s.SetNextLesson();
                            }
                        }

                        else if (_aboutGameC.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_cellsC.Current == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_e.UnitT(_cellsC.Current).Is(UnitTypes.Pawn))
                                {
                                     _s.SetNextLesson();
                                }
                            }
                        }

                        if (_e.UnitT(_cellsC.Current).Is(UnitTypes.Pawn) || !_e.UnitT(_cellsC.Current).HaveUnit())
                        {
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }
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
            _cellsC.PreviousSelected = _cellsC.Selected;
            _cellsC.Selected = _cellsC.Current;
        }

        void TryShift(in byte idxCellFrom, in byte idxCellTo)
        {
            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryShiftUnitOntoOtherCellM), idxCellFrom, idxCellTo });
        }

        void TryAttack(in byte idxCellFrom, in byte idxCellTo)
        {
            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryAttackUnitOnCellM), idxCellFrom, idxCellTo });
        }
    }
}