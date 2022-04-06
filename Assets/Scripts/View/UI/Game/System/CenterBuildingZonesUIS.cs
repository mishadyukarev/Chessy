using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class CenterBuildingZonesUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;

        internal CenterBuildingZonesUIS(in EntitiesViewUIGame entsUI, in Chessy.Game.Model.Entity.EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            eUI.CenterEs.MarketE.Zone.SetActive(e.SelectedE.BuildingsC.Is(BuildingTypes.Market));
            eUI.CenterEs.SmelterE.Zone.SetActive(e.SelectedE.BuildingsC.Is(BuildingTypes.Smelter));


            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.FoodToWood).TextUI.text = (EconomyValues.FOR_BUY_FROM_MARKET_FOOD_TO_WOOD * 100).ToString();
            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.WoodToFood).TextUI.text = (EconomyValues.FOR_BUY_FROM_MARKET_WOOD_TO_FOOD * 100).ToString();
            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToFood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_FOOD.ToString();
            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToWood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_WOOD.ToString();

            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.FoodToWood).TextUI.text = (EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD * 100).ToString();
            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.WoodToFood).TextUI.text = (EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD * 100).ToString();
            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToFood).TextUI.text = EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD.ToString();
            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToWood).TextUI.text = EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD.ToString();


            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Wood).TextUI.text = (EconomyValues.WOOD_NEED_FOR_MELTING * 100).ToString();
            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Ore).TextUI.text = (EconomyValues.ORE_NEED_FOR_MELTING * 100).ToString();

            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Iron).TextUI.text = EconomyValues.IRON_AFTER_MELTING.ToString();
            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Gold).TextUI.text = EconomyValues.GOLD_AFTER_MELTING.ToString();
        }
    }
}