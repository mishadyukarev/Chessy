using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class LeftCityUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;

        internal LeftCityUIS(in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            var whoseMove = e.WhoseMovePlayerTC.PlayerT;


            //UIE.LeftEs.CityE(BuildingTypes.Camp).Parent.SetActive(E.IsSelectedCity);

            var isActiveSmelter = false;
            var needActiveMarket = false;
            var needActiveFuture = false;
            var needActivePremum = false;

            if (e.LessonTC.HaveLesson)
            {
                if (e.LessonT >= LessonTypes.ClickBuyMelterInTown)
                {
                    isActiveSmelter = true;
                }
                if (e.LessonT >= LessonTypes.ClickBuyMarketInTown)
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
                    eUI.LeftEs.CityE(buildingT).CostGOC.SetActive(!e.PlayerInfoE(whoseMove).BuildingsInfoC.HaveBuilding(buildingT));
                }
            }

            eUI.LeftEs.CityE(BuildingTypes.House).CostTextC.TextUI.text = ((int)(100 * e.PlayerInfoE(whoseMove).WoodForBuyHouse)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Market).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_MARKET)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Smelter).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_SMELTER)).ToString();
        }
    }
}