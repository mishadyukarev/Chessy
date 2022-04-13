using Chessy.Game.Model.Entity;
using System;
using System.Linq;

namespace Chessy.Game.System.View
{
    sealed class SyncNoneVisionVS : SystemViewCellGameAbs
    {
        bool _isActive;

        readonly SpriteRendererVC _noneVisionSRC;

        internal SyncNoneVisionVS(in SpriteRendererVC noneVisionSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _noneVisionSRC = noneVisionSRC;
        }

        internal sealed override void Sync()
        {
            _isActive = false;


            if (e.CellClickTC.CellClickT == CellClickTypes.UniqueAbility)
            {
                switch (e.SelectedE.AbilityTC.Ability)
                {
                    case AbilityTypes.FireArcher:
                        if (!e.AdultForestC(_currentCell).HaveAnyResources) _isActive = true;
                        break;

                    case AbilityTypes.StunElfemale:
                        if (!e.AdultForestC(_currentCell).HaveAnyResources) _isActive = true;
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        if (e.IsActiveParentSelf(_currentCell))
                        {
                            if (!e.AroundCellsE(_currentCell).CellsAround.Contains(e.WeatherE.CloudC.Center))
                            {
                                _isActive = true;
                            }
                        }



                        
                        //if (!e.AdultForestC(idx_0).HaveAnyResources) _isActive = true;
                        break;
                }
            }

            else if (e.CellClickTC.CellClickT == CellClickTypes.GiveTakeTW)
            {
                if (e.UnitTC(_currentCell).UnitT == UnitTypes.Pawn && e.UnitPlayerTC(_currentCell).Is(e.CurPlayerITC.PlayerT))
                {
                    
                }
                else
                {
                    _isActive = true;
                }
            }

            else if (e.CellClickTC.CellClickT == CellClickTypes.SetUnit)
            {
                if (!e.IsStartedCellC(_currentCell).IsStartedCell(e.CurPlayerITC.PlayerT))
                {
                    _isActive = true;
                }
            }

            if (e.MistakeT == MistakeTypes.NeedOtherPlaceFarm)
            {
                if (e.AdultForestC(_currentCell).HaveAnyResources || e.MountainC(_currentCell).HaveAnyResources || e.HillC(_currentCell).HaveAnyResources
                    || e.BuildingTC(_currentCell).HaveBuilding)
                {
                    _isActive = true;
                }
            }

            else if (e.MistakeT == MistakeTypes.NeedOtherPlaceSeed)
            {
                if (e.AdultForestC(_currentCell).HaveAnyResources || e.MountainC(_currentCell).HaveAnyResources || e.HillC(_currentCell).HaveAnyResources
                    || e.YoungForestC(_currentCell).HaveAnyResources || e.BuildingTC(_currentCell).HaveBuilding)
                {
                    _isActive = true;
                }
            }


            else if (e.MistakeT == MistakeTypes.NeedOtherPlaceGrowAdultForest)
            {
                if (!e.YoungForestC(_currentCell).HaveAnyResources)
                {
                    _isActive = true;
                }
            }


            _noneVisionSRC.GO.SetActive(_isActive);
        }
    }
}