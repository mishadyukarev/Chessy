using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class LeftCityUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal LeftCityUIS(in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var whoseMove = eMGame.WhoseMove.Player;


            //UIE.LeftEs.CityE(BuildingTypes.Camp).Parent.SetActive(E.IsSelectedCity);


            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++)
            {
                if (buildingT == BuildingTypes.Market || buildingT == BuildingTypes.Smelter)
                {
                    eUI.LeftEs.CityE(buildingT).CostGOC.SetActive(!eMGame.PlayerInfoE(whoseMove).HaveBuilding(buildingT));
                }
            }

            eUI.LeftEs.CityE(BuildingTypes.House).CostTextC.TextUI.text = ((int)(100 * eMGame.PlayerInfoE(whoseMove).WoodForBuyHouse)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Market).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_MARKET)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Smelter).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_SMELTER)).ToString();
        }
    }
}