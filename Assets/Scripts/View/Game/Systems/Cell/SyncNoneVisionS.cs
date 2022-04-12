using System;
using System.Linq;

namespace Chessy.Game.System.View
{
    public struct SyncNoneVisionS
    {
        bool _isActive;

        public void Sync(in byte idx_0, in SpriteRendererVC srC, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            _isActive = false;


            if (e.CellClickTC.CellClickT == CellClickTypes.UniqueAbility)
            {
                switch (e.SelectedE.AbilityTC.Ability)
                {
                    case AbilityTypes.FireArcher:
                        if (!e.AdultForestC(idx_0).HaveAnyResources) _isActive = true;
                        break;

                    case AbilityTypes.StunElfemale:
                        if (!e.AdultForestC(idx_0).HaveAnyResources) _isActive = true;
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        if (e.IsActiveParentSelf(idx_0))
                        {
                            if (!e.AroundCellsE(idx_0).CellsAround.Contains(e.WeatherE.CloudC.Center))
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
                if (e.UnitTC(idx_0).UnitT == UnitTypes.Pawn && e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.PlayerT))
                {
                    
                }
                else
                {
                    _isActive = true;
                }
            }

            else if (e.CellClickTC.CellClickT == CellClickTypes.SetUnit)
            {
                if (!e.IsStartedCellC(idx_0).IsStartedCell(e.CurPlayerITC.PlayerT))
                {
                    _isActive = true;
                }
            }

            if (e.MistakeT == MistakeTypes.NeedOtherPlaceFarm)
            {
                if (e.AdultForestC(idx_0).HaveAnyResources || e.MountainC(idx_0).HaveAnyResources || e.HillC(idx_0).HaveAnyResources
                    || e.BuildingTC(idx_0).HaveBuilding)
                {
                    _isActive = true;
                }
            }

            else if (e.MistakeT == MistakeTypes.NeedOtherPlaceSeed)
            {
                if (e.AdultForestC(idx_0).HaveAnyResources || e.MountainC(idx_0).HaveAnyResources || e.HillC(idx_0).HaveAnyResources
                    || e.YoungForestC(idx_0).HaveAnyResources || e.BuildingTC(idx_0).HaveBuilding)
                {
                    _isActive = true;
                }
            }


            else if (e.MistakeT == MistakeTypes.NeedOtherPlaceGrowAdultForest)
            {
                if (!e.YoungForestC(idx_0).HaveAnyResources)
                {
                    _isActive = true;
                }
            }


            srC.GO.SetActive(_isActive);
        }
    }
}