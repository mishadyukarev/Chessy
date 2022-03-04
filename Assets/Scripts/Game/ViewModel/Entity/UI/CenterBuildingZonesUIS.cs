namespace Chessy.Game
{
    sealed class CenterBuildingZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterBuildingZonesUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {

        }

        public void Run()
        {
            UIE.CenterEs.MarketE.Zone.SetActive(E.SelectedBuildingsC.Is(BuildingTypes.Market));
            UIE.CenterEs.SmelterE.Zone.SetActive(E.SelectedBuildingsC.Is(BuildingTypes.Smelter));


            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.FoodToWood).TextUI.text = Economy_VALUES.FOR_BUY_FROM_MARKET_FOOD_TO_WOOD.ToString();
            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.WoodToFood).TextUI.text = Economy_VALUES.FOR_BUY_FROM_MARKET_WOOD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToFood).TextUI.text = Economy_VALUES.FOR_BUY_FROM_MARKET_GOLD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToWood).TextUI.text = Economy_VALUES.FOR_BUY_FROM_MARKET_GOLD_TO_WOOD.ToString();

            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.FoodToWood).TextUI.text = Economy_VALUES.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD.ToString();
            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.WoodToFood).TextUI.text = Economy_VALUES.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToFood).TextUI.text = Economy_VALUES.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToWood).TextUI.text = Economy_VALUES.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD.ToString();


            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Wood).TextUI.text = Economy_VALUES.WOOD_NEED_FOR_MELTING.ToString();
            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Ore).TextUI.text = Economy_VALUES.WOOD_NEED_FOR_MELTING.ToString();

            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Iron).TextUI.text = Economy_VALUES.IRON_AFTER_MELTING.ToString();
            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Gold).TextUI.text = Economy_VALUES.GOLD_AFTER_MELTING.ToString();


            //if (UIE.CenterEs.SmelterE.ButtonC.Button.Cli)
            //{

            //}
            
        }
    }
}