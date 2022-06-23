using Chessy.Model.Enum;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Chessy.Model.View.System;
using System;
using System.Linq;

namespace Chessy.Model.System.View
{
    sealed class SyncBlackVisionVS : SystemViewAbstract
    {
        bool[] _isActive = new bool[StartValues.CELLS];
        readonly SpriteRendererVC[] _noneVisionSRC;

        internal SyncBlackVisionVS(in SpriteRendererVC[] noneVisionsSRC, in EntitiesModel eMG) : base(eMG)
        {
            _noneVisionSRC = noneVisionsSRC;
        }

        internal sealed override void Sync()
        {
            for (byte currentCellIdx = 0; currentCellIdx < StartValues.CELLS; currentCellIdx++)
            {
                if (!_e.IsBorder(currentCellIdx))
                {
                    _isActive[currentCellIdx] = false;
                }
            }


            for (byte currentCellIdx = 0; currentCellIdx < StartValues.CELLS; currentCellIdx++)
            {
                if (!_e.IsBorder(currentCellIdx))
                {
                    if (_e.LessonT.HaveLesson())
                    {
                        switch (_e.LessonT)
                        {
                            case LessonTypes.ClickWindInfo:
                                {
                                    if (_e.UnitT(currentCellIdx) != UnitTypes.Snowy && _e.WeatherE.CellIdxCenterCloud != currentCellIdx && !_e.AroundCellsE(_e.WeatherE.CellIdxCenterCloud).CellsAround.Contains(currentCellIdx))
                                    {
                                        _isActive[currentCellIdx] = true;
                                    }
                                }
                                break;

                            case LessonTypes.YouNeedDestroyKing:
                                {
                                    if (_e.UnitT(currentCellIdx) != UnitTypes.King)
                                    {
                                        _isActive[currentCellIdx] = true;
                                    }
                                }
                                break;

                            default:

                                break;
                        }

                        if (_e.LessonT >= LessonTypes.SettingKing)
                        {
                            if (_e.UnitT(currentCellIdx) == UnitTypes.King)
                            {
                                _isActive[currentCellIdx] = true;
                            }
                        }



                        if (_e.LessonT < LessonTypes.ChangeDirectionWind)
                        {
                            if (_e.UnitT(currentCellIdx) == UnitTypes.Snowy)
                            {
                                _isActive[currentCellIdx] = true;
                            }

                            if (_e.LessonT < LessonTypes.ClickAtYourPawn)
                            {
                                if (_e.UnitT(currentCellIdx) == UnitTypes.Pawn)
                                {
                                    if (_e.CurPlayerIT == _e.UnitPlayerT(currentCellIdx))
                                    {
                                        _isActive[currentCellIdx] = true;
                                    }
                                }

                                if (_e.LessonT > LessonTypes.YouNeedDestroyKing)
                                {
                                    if (!_e.IsStartedCellC(currentCellIdx).IsStartedCell(_e.CurPlayerIT))
                                    {
                                        _isActive[currentCellIdx] = true;
                                    }
                                }
                            }
                        }


                    }



                    if (_e.CellClickT == CellClickTypes.UniqueAbility)
                    {
                        switch (_e.SelectedE.AbilityT)
                        {
                            case AbilityTypes.FireArcher:
                                if (!_e.AdultForestC(currentCellIdx).HaveAnyResources) _isActive[currentCellIdx] = true;
                                break;

                            case AbilityTypes.StunElfemale:
                                if (!_e.AdultForestC(currentCellIdx).HaveAnyResources) _isActive[currentCellIdx] = true;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                if (!_e.IsBorder(currentCellIdx))
                                {
                                    if (!_e.AroundCellsE(currentCellIdx).CellsAround.Contains(_e.WeatherE.CellIdxCenterCloud))
                                    {
                                        _isActive[currentCellIdx] = true;
                                    }
                                }




                                //if (!e.AdultForestC(idx_0).HaveAnyResources) _isActive = true;
                                break;
                        }
                    }

                    else if (_e.CellClickT == CellClickTypes.GiveTakeTW)
                    {
                        if (_e.UnitT(currentCellIdx) == UnitTypes.Pawn && _e.UnitPlayerT(currentCellIdx).Is(_e.CurPlayerIT))
                        {

                        }
                        else
                        {
                            _isActive[currentCellIdx] = true;
                        }
                    }

                    else if (_e.CellClickT == CellClickTypes.SetUnit)
                    {
                        if (!_e.IsStartedCellC(currentCellIdx).IsStartedCell(_e.CurPlayerIT))
                        {
                            _isActive[currentCellIdx] = true;
                        }
                    }

                    if (_e.MistakeT == MistakeTypes.NeedOtherPlaceFarm)
                    {
                        if (_e.AdultForestC(currentCellIdx).HaveAnyResources || _e.MountainC(currentCellIdx).HaveAnyResources || _e.HillC(currentCellIdx).HaveAnyResources
                            || _e.HaveBuildingOnCell(currentCellIdx))
                        {
                            _isActive[currentCellIdx] = true;
                        }
                    }

                    else if (_e.MistakeT == MistakeTypes.NeedOtherPlaceSeed)
                    {
                        if (_e.AdultForestC(currentCellIdx).HaveAnyResources || _e.MountainC(currentCellIdx).HaveAnyResources || _e.HillC(currentCellIdx).HaveAnyResources
                            || _e.YoungForestC(currentCellIdx).HaveAnyResources || _e.HaveBuildingOnCell(currentCellIdx))
                        {
                            _isActive[currentCellIdx] = true;
                        }
                    }


                    else if (_e.MistakeT == MistakeTypes.NeedOtherPlaceGrowAdultForest)
                    {
                        if (!_e.YoungForestC(currentCellIdx).HaveAnyResources)
                        {
                            _isActive[currentCellIdx] = true;
                        }
                    }


                    _noneVisionSRC[currentCellIdx].GO.SetActive(_isActive[currentCellIdx]);
                }
            }



        }
    }
}