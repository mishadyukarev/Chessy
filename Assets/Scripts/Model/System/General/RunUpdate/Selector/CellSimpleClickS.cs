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
            //if (_e.LessonT.Is(LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.ClickDefend))
            //{
            //    return;
            //}


            _e.MistakeT = MistakeTypes.None;


            _e.DataFromViewC.AnimationCell(_e.CurrentCellIdx, AnimationCellTypes.AdultForest).Invoke();
            _e.DataFromViewC.AnimationCell(_e.WhereViewDataUnitC(_e.CurrentCellIdx).ViewIdxCell, AnimationCellTypes.JumpAppearanceUnit).Invoke();





            if (_e.CellsC.IsSelectedCell)
            {
                _e.IsSelectedCity = false;



                if (_e.LessonT.HaveLesson())
                {
                    if (_e.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_e.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Pawn && _e.UnitPlayerT(_e.CurrentCellIdx) == _e.CurrentPlayerIT)
                            {
                                 _s.SetNextLesson();
                            }
                        }

                        else if (_e.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_e.CurrentCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn))
                                {
                                     _s.SetNextLesson();
                                }
                            }
                        }

                        if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                        {
                            if (_e.UnitT(_e.CurrentCellIdx).HaveUnit())
                            {
                                if (_e.WhereUnitCanAttackSimpleAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx) || _e.WhereUnitCanAttackUniqueAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx))
                                {
                                    TryAttack(_e.SelectedCellIdx, _e.CurrentCellIdx);
                                    //_selectorSoundS.Sound();
                                }
                                //else if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn) && _e.UnitPlayerT(_e.CurrentCellIdx).Is(_e.CurrentPlayerIT)
                                //    || !_e.UnitPlayerT(_e.CurrentCellIdx).Is(_e.CurrentPlayerIT))
                                //{
                                //    //_selectorSoundS.Sound();
                                //}

                                //if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Snowy)
                                //{
                                //    if (_e.LessonT >= LessonTypes.ChangeDirectionWind)
                                //    {
                                //        _e.IsSelectedCity = false;

                                //        _selectorSoundS.Sound();
                                //    }
                                //}
                                //else if (!_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.King))
                                //{
                                    

                                    
                                //}

                                _e.IsSelectedCity = false;
                                SetNewSelectedCell();
                                _selectorSoundS.Sound();
                            }

                            else
                            {
                                if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_e.CurrentPlayerIT) && _e.WhereUnitCanShiftC(_e.SelectedCellIdx).CanShiftHere(_e.CurrentCellIdx))
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
                            //if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Snowy)
                            //{
                            //    if (_e.LessonT >= LessonTypes.ChangeDirectionWind)
                            //    {
                                   

                                   
                            //        _selectorSoundS.Sound();
                            //    }
                            //}
                            //else if (!_e.UnitT(_e.SelectedCellIdx).Is(UnitTypes.King))
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
                    if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                    {
                        if (_e.WhereUnitCanAttackSimpleAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx)
                        || _e.WhereUnitCanAttackUniqueAttackToEnemyC(_e.SelectedCellIdx).Can(_e.CurrentCellIdx))
                        {
                            TryAttack(_e.SelectedCellIdx, _e.CurrentCellIdx);
                        }

                        else if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_e.CurrentPlayerIT)
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
                    else
                    {
                        _e.IsSelectedCity = false;
                    }

                    if (_e.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (_e.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_e.UnitT(_e.CurrentCellIdx) == UnitTypes.Pawn && _e.UnitPlayerT(_e.CurrentCellIdx) == _e.CurrentPlayerIT)
                            {
                                 _s.SetNextLesson();
                            }
                        }

                        else if (_e.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (_e.CurrentCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn))
                                {
                                     _s.SetNextLesson();
                                }
                            }
                        }

                        if (_e.UnitT(_e.CurrentCellIdx).Is(UnitTypes.Pawn) || !_e.UnitT(_e.CurrentCellIdx).HaveUnit())
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