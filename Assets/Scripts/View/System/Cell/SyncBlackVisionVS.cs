using Chessy.Model;
using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.Component;
using System;
using System.Linq;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncBlackVisionVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivated = new bool[IndexCellsValues.CELLS];
        readonly SpriteRenderer[] _noneVisionSRC;

        internal SyncBlackVisionVS(in SpriteRenderer[] noneVisionsSRC, in EntitiesModel eMG) : base(eMG)
        {
            _noneVisionSRC = noneVisionsSRC;
        }

        internal sealed override void Sync()
        {
            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (_cellCs[currentCellIdx_0].IsBorder) continue;

                _needActive[currentCellIdx_0] = false;
            }

            var lessonT = _aboutGameC.LessonType;

            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (_cellCs[currentCellIdx_0].IsBorder) continue;


                var curUnitC = _unitCs[currentCellIdx_0];
                var curUnitT = curUnitC.UnitType;

                

                if (lessonT.HaveLesson())
                {
                    switch (lessonT)
                    {
                        case LessonTypes.ClickWindInfo:
                            {
                                //if (_e.UnitT(currentCellIdx) != UnitTypes.Snowy && _e.CenterCloudCellIdx != currentCellIdx && !_e.AroundCellsE(_e.CenterCloudCellIdx).CellsAround.Contains(currentCellIdx))
                                //{
                                //    _isActive[currentCellIdx] = true;
                                //}
                            }
                            break;

                        case LessonTypes.YouNeedDestroyKing:
                            {
                                if (curUnitT != UnitTypes.King)
                                {
                                    _needActive[currentCellIdx_0] = true;
                                }
                            }
                            break;

                        default:

                            break;
                    }


                    if (lessonT < LessonTypes.MenuInfo)
                    {
                        if (curUnitT == UnitTypes.Snowy)
                        {
                            _needActive[currentCellIdx_0] = true;
                        }

                        if (lessonT < LessonTypes.ClickAtYourPawn)
                        {
                            if (curUnitT == UnitTypes.Pawn)
                            {
                                if (_aboutGameC.CurrentPlayerIType == _e.UnitPlayerT(currentCellIdx_0))
                                {
                                    _needActive[currentCellIdx_0] = true;
                                }
                            }

                            if (lessonT > LessonTypes.YouNeedDestroyKing)
                            {
                                if (!_e.IsStartedCellC(currentCellIdx_0).IsStartedCell(_aboutGameC.CurrentPlayerIType))
                                {
                                    _needActive[currentCellIdx_0] = true;
                                }
                            }
                        }

                        if (lessonT <= LessonTypes.ComeToYourKing)
                        {
                            if (curUnitT == UnitTypes.King)
                            {
                                _needActive[currentCellIdx_0] = true;
                            }
                        }
                    }


                }



                if (_e.CellClickT == CellClickTypes.UniqueAbility)
                {
                    switch (_aboutGameC.AbilityType)
                    {
                        case AbilityTypes.FireArcher:
                            if (!_e.AdultForestC(currentCellIdx_0).HaveAnyResources) _needActive[currentCellIdx_0] = true;
                            break;

                        case AbilityTypes.StunElfemale:
                            if (!_e.AdultForestC(currentCellIdx_0).HaveAnyResources) _needActive[currentCellIdx_0] = true;
                            break;

                        case AbilityTypes.ChangeDirectionWind:

                            //if()

                            //foreach (var item in _e.)
                            //{

                            //}

                            //if(_e.)

                            //if (!_e.IsBorder(currentCellIdx))
                            //{


                            //    //if(!_e.HaveCloud(currentCellIdx) && !_e.IsCenterCloud(currentCellIdx))
                            //    //{
                            //    //    _isActive[currentCellIdx] = true;
                            //    //}
                            //}




                            //if (!e.AdultForestC(idx_0).HaveAnyResources) _isActive = true;
                            break;
                    }
                }

                else if (_e.CellClickT == CellClickTypes.GiveTakeTW)
                {
                    if (curUnitT == UnitTypes.Pawn && _e.UnitPlayerT(currentCellIdx_0).Is(_aboutGameC.CurrentPlayerIType))
                    {

                    }
                    else
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                else if (_e.CellClickT == CellClickTypes.SetUnit)
                {
                    if (!_e.IsStartedCellC(currentCellIdx_0).IsStartedCell(_aboutGameC.CurrentPlayerIType))
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                if (_e.MistakeT == MistakeTypes.NeedOtherPlaceFarm)
                {
                    if (_e.AdultForestC(currentCellIdx_0).HaveAnyResources || _e.MountainC(currentCellIdx_0).HaveAnyResources || _e.HillC(currentCellIdx_0).HaveAnyResources
                        || _e.HaveBuildingOnCell(currentCellIdx_0))
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                else if (_e.MistakeT == MistakeTypes.NeedOtherPlaceSeed)
                {
                    if (_e.AdultForestC(currentCellIdx_0).HaveAnyResources || _e.MountainC(currentCellIdx_0).HaveAnyResources || _e.HillC(currentCellIdx_0).HaveAnyResources
                        || _e.YoungForestC(currentCellIdx_0).HaveAnyResources || _e.HaveBuildingOnCell(currentCellIdx_0))
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }


                else if (_e.MistakeT == MistakeTypes.NeedOtherPlaceGrowAdultForest)
                {
                    if (!_e.YoungForestC(currentCellIdx_0).HaveAnyResources)
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                var needActive = _needActive[currentCellIdx_0];
                ref var wasActivated = ref _wasActivated[currentCellIdx_0];

                if(needActive != wasActivated) _noneVisionSRC[currentCellIdx_0].gameObject.SetActive(needActive);

                wasActivated = needActive;
            }
        }
    }
}