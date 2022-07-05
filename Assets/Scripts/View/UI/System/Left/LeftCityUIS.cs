using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class LeftCityUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;

        internal LeftCityUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            var whoseMove = _e.WhoseMovePlayerT;


            //UIE.LeftEs.CityE(BuildingTypes.Camp).Parent.SetActive(E.IsSelectedCity);

            var isActiveSmelter = false;
            var needActiveMarket = false;
            var needActiveFuture = false;
            var needActivePremum = false;

            if (_e.LessonT.HaveLesson())
            {
                if (_e.LessonT >= LessonTypes.NeedBuildSmelterAndMeltOre)
                {
                    isActiveSmelter = true;
                }
                if (_e.LessonT >= LessonTypes.ClickBuyMarketInTown)
                {
                    needActiveMarket = true;
                }
            }
            else
            {
                isActiveSmelter = true;
                needActiveMarket = true;
                needActiveFuture = true;
                needActivePremum = true;
            }

            eUI.LeftEs.CityE(BuildingTypes.Smelter).ZoneGOC.SetActive(isActiveSmelter);
            eUI.LeftEs.CityE(BuildingTypes.Market).ZoneGOC.SetActive(needActiveMarket);
            eUI.LeftEs.FutureGOC.SetActive(needActiveFuture);
            eUI.LeftEs.PremiumButtonC.SetActive(needActivePremum);


            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++)
            {
                if (buildingT == BuildingTypes.Market || buildingT == BuildingTypes.Smelter)
                {
                    eUI.LeftEs.CityE(buildingT).CostGOC.SetActive(!_e.PlayerInfoE(whoseMove).BuildingsInTownInfoC.HaveBuilding(buildingT));
                }
            }

            eUI.LeftEs.CityE(BuildingTypes.House).CostTextC.TextUI.text = ((int)(100 * _e.PlayerInfoE(whoseMove).PlayerInfoC.WoodForBuyHouse)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Market).CostTextC.TextUI.text = ((int)(100 * CostsForBuyBuildingInTownValues.NEED_WOOD_FOR_BUILDING_MARKET)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Smelter).CostTextC.TextUI.text = ((int)(100 * CostsForBuyBuildingInTownValues.NEED_WOOD_FOR_BUILDING_SMELTER)).ToString();
        }
    }
}