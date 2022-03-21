using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class LeftCityUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal LeftCityUIS(in EntitiesViewUI entsUI, in Chessy.Game.Entity.Model.EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var whoseMove = E.WhoseMove.Player;


            //UIE.LeftEs.CityE(BuildingTypes.Camp).Parent.SetActive(E.IsSelectedCity);


            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++)
            {
                if (buildingT == BuildingTypes.Market || buildingT == BuildingTypes.Smelter)
                {
                    UIE.LeftEs.CityE(buildingT).CostGOC.SetActive(!E.PlayerInfoE(whoseMove).HaveBuilding(buildingT));
                }
            }

            UIE.LeftEs.CityE(BuildingTypes.House).CostTextC.TextUI.text = ((int)(100 * E.PlayerInfoE(whoseMove).WoodForBuyHouse)).ToString();
            UIE.LeftEs.CityE(BuildingTypes.Market).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_MARKET)).ToString();
            UIE.LeftEs.CityE(BuildingTypes.Smelter).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_SMELTER)).ToString();
        }
    }
}