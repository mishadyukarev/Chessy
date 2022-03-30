using Chessy.Game.Entity.Model;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class LeftCityUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame eUI;

        internal LeftCityUIS(in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        public void Run()
        {
            var whoseMove = e.WhoseMove.Player;


            //UIE.LeftEs.CityE(BuildingTypes.Camp).Parent.SetActive(E.IsSelectedCity);


            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++)
            {
                if (buildingT == BuildingTypes.Market || buildingT == BuildingTypes.Smelter)
                {
                    eUI.LeftEs.CityE(buildingT).CostGOC.SetActive(!e.PlayerInfoE(whoseMove).HaveBuilding(buildingT));
                }
            }

            eUI.LeftEs.CityE(BuildingTypes.House).CostTextC.TextUI.text = ((int)(100 * e.PlayerInfoE(whoseMove).WoodForBuyHouse)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Market).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_MARKET)).ToString();
            eUI.LeftEs.CityE(BuildingTypes.Smelter).CostTextC.TextUI.text = ((int)(100 * EconomyValues.NEED_WOOD_FOR_BUILDING_SMELTER)).ToString();
        }
    }
}