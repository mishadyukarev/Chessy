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


            _dataFromViewC.AnimationCell(IndexesCellsC.Current, AnimationCellTypes.AdultForest).Invoke();
            _dataFromViewC.AnimationCell(_unitWhereViewDataCs[IndexesCellsC.Current].ViewIdxCell, AnimationCellTypes.JumpAppearanceUnit).Invoke();





            if (IndexesCellsC.IsSelectedCell)
            {
                AboutGameC.IsSelectedCity = false;



                if (AboutGameC.LessonT.HaveLesson())
                {
                    if (AboutGameC.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (AboutGameC.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_unitCs[IndexesCellsC.Current].UnitT == UnitTypes.Pawn && _unitCs[IndexesCellsC.Current].PlayerT == AboutGameC.CurrentPlayerIT)
                            {
                                _s.SetNextLesson();
                            }
                        }

                        else if (AboutGameC.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (IndexesCellsC.Current == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_unitCs[IndexesCellsC.Current].UnitT == UnitTypes.Pawn)
                                {
                                    _s.SetNextLesson();
                                }
                            }
                        }

                        if (_unitCs[IndexesCellsC.Selected].HaveUnit)
                        {
                            if (_unitCs[IndexesCellsC.Current].HaveUnit)
                            {
                                if (_whereSimpleAttackCs[IndexesCellsC.Selected].Can(IndexesCellsC.Current) || _whereUniqueAttackCs[IndexesCellsC.Selected].Can(IndexesCellsC.Current))
                                {
                                    TryAttack(IndexesCellsC.Selected, IndexesCellsC.Current);
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

                                AboutGameC.IsSelectedCity = false;
                                SetNewSelectedCell();
                                _selectorSoundS.Sound();
                            }

                            else
                            {
                                if (_unitCs[IndexesCellsC.Selected].PlayerT.Is(AboutGameC.CurrentPlayerIT) && _whereUnitCanShiftCs[IndexesCellsC.Selected].CanShiftHere(IndexesCellsC.Current))
                                {
                                    TryShift(IndexesCellsC.Selected, IndexesCellsC.Current);
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
                            AboutGameC.IsSelectedCity = false;
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }
                    }
                }

                else
                {
                    if (_unitCs[IndexesCellsC.Selected].HaveUnit)
                    {
                        if (_whereSimpleAttackCs[IndexesCellsC.Selected].Can(IndexesCellsC.Current)
                        || _whereUniqueAttackCs[IndexesCellsC.Selected].Can(IndexesCellsC.Current))
                        {
                            TryAttack(IndexesCellsC.Selected, IndexesCellsC.Current);
                        }

                        else if (_unitCs[IndexesCellsC.Selected].PlayerT.Is(AboutGameC.CurrentPlayerIT)
                            && _whereUnitCanShiftCs[IndexesCellsC.Selected].CanShiftHere(IndexesCellsC.Current))
                        {
                            TryShift(IndexesCellsC.Selected, IndexesCellsC.Current);
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


                if (AboutGameC.LessonT.HaveLesson())
                {
                    if (AboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse))
                    {

                    }
                    else
                    {
                        AboutGameC.IsSelectedCity = false;
                    }

                    if (AboutGameC.LessonT >= LessonTypes.ClickAtYourPawn)
                    {
                        if (AboutGameC.LessonT == LessonTypes.ClickAtYourPawn)
                        {
                            if (_unitCs[IndexesCellsC.Current].UnitT == UnitTypes.Pawn && _unitCs[IndexesCellsC.Current].PlayerT == AboutGameC.CurrentPlayerIT)
                            {
                                _s.SetNextLesson();
                            }
                        }

                        else if (AboutGameC.LessonT == LessonTypes.ShiftPawnHere)
                        {
                            if (IndexesCellsC.Current == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                if (_unitCs[IndexesCellsC.Current].UnitT == UnitTypes.Pawn)
                                {
                                    _s.SetNextLesson();
                                }
                            }
                        }

                        if (_unitCs[IndexesCellsC.Current].UnitT == UnitTypes.Pawn || !_unitCs[IndexesCellsC.Current].HaveUnit)
                        {
                            SetNewSelectedCell();
                            _selectorSoundS.Sound();
                        }
                    }
                }

                else
                {
                    AboutGameC.IsSelectedCity = false;

                    SetNewSelectedCell();
                    _selectorSoundS.Sound();
                }
            }
        }

        void SetNewSelectedCell()
        {
            IndexesCellsC.PreviousSelected = IndexesCellsC.Selected;
            IndexesCellsC.Selected = IndexesCellsC.Current;
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