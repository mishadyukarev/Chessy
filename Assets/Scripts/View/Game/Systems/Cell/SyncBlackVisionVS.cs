using Chessy.Game.Model.Entity;
using System;
using System.Linq;

namespace Chessy.Game.System.View
{
    sealed class SyncBlackVisionVS : SystemViewCellGameAbs
    {
        bool _isActive;

        readonly SpriteRendererVC _noneVisionSRC;

        internal SyncBlackVisionVS(in SpriteRendererVC noneVisionSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _noneVisionSRC = noneVisionSRC;
        }

        internal sealed override void Sync()
        {
            _isActive = false;

            if (_e.LessonTC.HaveLesson)
            {
                //if (_e.CurPlayerIT == _e.UnitPlayerT(_currentCell))
                //{
                if (_e.UnitTC(_currentCell).Is(UnitTypes.King, UnitTypes.Snowy))
                {
                    _isActive = true;
                }

                if (_e.UnitT(_currentCell) == UnitTypes.Pawn)
                {
                    if (_e.CurPlayerIT != _e.UnitPlayerT(_currentCell))
                    {
                        _isActive = true;
                    }
                    if (_e.LessonT < Enum.LessonTypes.ShiftPawnHere)
                    {
                        _isActive = true;
                    }
                }
                //}

                if (_e.LessonT < Enum.LessonTypes.ShiftPawnHere)
                {
                    if (!_e.IsStartedCellC(_currentCell).IsStartedCell(_e.CurPlayerITC.PlayerT))
                    {
                        _isActive = true;
                    }
                }
            }



            if (_e.CellClickTC.CellClickT == CellClickTypes.UniqueAbility)
            {
                switch (_e.SelectedE.AbilityTC.Ability)
                {
                    case AbilityTypes.FireArcher:
                        if (!_e.AdultForestC(_currentCell).HaveAnyResources) _isActive = true;
                        break;

                    case AbilityTypes.StunElfemale:
                        if (!_e.AdultForestC(_currentCell).HaveAnyResources) _isActive = true;
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        if (!_e.IsBorder(_currentCell))
                        {
                            if (!_e.AroundCellsE(_currentCell).CellsAround.Contains(_e.WeatherE.CloudC.Center))
                            {
                                _isActive = true;
                            }
                        }



                        
                        //if (!e.AdultForestC(idx_0).HaveAnyResources) _isActive = true;
                        break;
                }
            }

            else if (_e.CellClickTC.CellClickT == CellClickTypes.GiveTakeTW)
            {
                if (_e.UnitTC(_currentCell).UnitT == UnitTypes.Pawn && _e.UnitPlayerTC(_currentCell).Is(_e.CurPlayerITC.PlayerT))
                {
                    
                }
                else
                {
                    _isActive = true;
                }
            }

            else if (_e.CellClickTC.CellClickT == CellClickTypes.SetUnit)
            {
                if (!_e.IsStartedCellC(_currentCell).IsStartedCell(_e.CurPlayerITC.PlayerT))
                {
                    _isActive = true;
                }
            }

            if (_e.MistakeT == MistakeTypes.NeedOtherPlaceFarm)
            {
                if (_e.AdultForestC(_currentCell).HaveAnyResources || _e.MountainC(_currentCell).HaveAnyResources || _e.HillC(_currentCell).HaveAnyResources
                    || _e.BuildingTC(_currentCell).HaveBuilding)
                {
                    _isActive = true;
                }
            }

            else if (_e.MistakeT == MistakeTypes.NeedOtherPlaceSeed)
            {
                if (_e.AdultForestC(_currentCell).HaveAnyResources || _e.MountainC(_currentCell).HaveAnyResources || _e.HillC(_currentCell).HaveAnyResources
                    || _e.YoungForestC(_currentCell).HaveAnyResources || _e.BuildingTC(_currentCell).HaveBuilding)
                {
                    _isActive = true;
                }
            }


            else if (_e.MistakeT == MistakeTypes.NeedOtherPlaceGrowAdultForest)
            {
                if (!_e.YoungForestC(_currentCell).HaveAnyResources)
                {
                    _isActive = true;
                }
            }


            _noneVisionSRC.GO.SetActive(_isActive);
        }
    }
}