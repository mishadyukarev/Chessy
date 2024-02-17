using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
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
                if (CellC(currentCellIdx_0).IsBorder) continue;

                _needActive[currentCellIdx_0] = false;
            }

            var lessonT = aboutGameC.LessonType;

            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (CellC(currentCellIdx_0).IsBorder) continue;


                var curUnitC = unitCs[currentCellIdx_0];
                var curUnitT = curUnitC.UnitType;



                if (lessonT.HaveLesson())
                {
                    switch (lessonT)
                    {
                        case LessonTypes.ClickWindInfo:
                            {
                                //if (_unitCs[currentCellIdx) != UnitTypes.Snowy && _e.CenterCloudCellIdx != currentCellIdx && !_e.AroundCellsE(_e.CenterCloudCellIdx).CellsAround.Contains(currentCellIdx))
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
                                if (aboutGameC.CurrentPlayerIType == unitCs[currentCellIdx_0].PlayerType)
                                {
                                    _needActive[currentCellIdx_0] = true;
                                }
                            }

                            if (lessonT > LessonTypes.YouNeedDestroyKing)
                            {
                                if (!isStartedCellCs[currentCellIdx_0].IsStartedCellForPlayer(aboutGameC.CurrentPlayerIType))
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



                if (aboutGameC.CellClickType == CellClickTypes.UniqueAbility)
                {
                    switch (aboutGameC.AbilityType)
                    {
                        case AbilityTypes.FireArcher:
                            if (!environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.AdultForest)) _needActive[currentCellIdx_0] = true;
                            break;

                        case AbilityTypes.StunElfemale:
                            if (!environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.AdultForest)) _needActive[currentCellIdx_0] = true;
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




                            //if (!e.AdultForestC(idx_0].HaveEnvironment(EnvironmentTypes.AdultForest)) _isActive = true;
                            break;
                    }
                }

                else if (aboutGameC.CellClickType == CellClickTypes.GiveTakeTW)
                {
                    if (curUnitT == UnitTypes.Pawn && unitCs[currentCellIdx_0].PlayerType == aboutGameC.CurrentPlayerIType)
                    {

                    }
                    else
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                else if (aboutGameC.CellClickType == CellClickTypes.SetUnit)
                {
                    if (!isStartedCellCs[currentCellIdx_0].IsStartedCellForPlayer(aboutGameC.CurrentPlayerIType))
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                if (mistakeC.MistakeT == MistakeTypes.NeedOtherPlaceFarm)
                {
                    if (environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.AdultForest) || environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.Mountain) || environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.Hill)
                        || buildingCs[currentCellIdx_0].HaveBuilding)
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                else if (mistakeC.MistakeT == MistakeTypes.NeedOtherPlaceSeed)
                {
                    if (environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.AdultForest) || environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.Mountain) || environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.Hill)
                        || environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.YoungForest) || buildingCs[currentCellIdx_0].HaveBuilding)
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }


                else if (mistakeC.MistakeT == MistakeTypes.NeedOtherPlaceGrowAdultForest)
                {
                    if (!environmentCs[currentCellIdx_0].HaveEnvironment(EnvironmentTypes.YoungForest))
                    {
                        _needActive[currentCellIdx_0] = true;
                    }
                }

                var needActive = _needActive[currentCellIdx_0];
                ref var wasActivated = ref _wasActivated[currentCellIdx_0];

                if (needActive != wasActivated) _noneVisionSRC[currentCellIdx_0].gameObject.SetActive(needActive);

                wasActivated = needActive;
            }
        }
    }
}