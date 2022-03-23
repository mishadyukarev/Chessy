using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class CenterBuildingZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterBuildingZonesUIS(in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {

        }

        public void Run()
        {
            UIE.CenterEs.MarketE.Zone.SetActive(E.SelectedE.BuildingsC.Is(BuildingTypes.Market));
            UIE.CenterEs.SmelterE.Zone.SetActive(E.SelectedE.BuildingsC.Is(BuildingTypes.Smelter));


            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.FoodToWood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_FOOD_TO_WOOD.ToString();
            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.WoodToFood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_WOOD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToFood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToWood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_WOOD.ToString();

            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.FoodToWood).TextUI.text = EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD.ToString();
            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.WoodToFood).TextUI.text = EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToFood).TextUI.text = EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD.ToString();
            UIE.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToWood).TextUI.text = EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD.ToString();


            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Wood).TextUI.text = EconomyValues.WOOD_NEED_FOR_MELTING.ToString();
            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Ore).TextUI.text = EconomyValues.ORE_NEED_FOR_MELTING.ToString();

            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Iron).TextUI.text = EconomyValues.IRON_AFTER_MELTING.ToString();
            UIE.CenterEs.SmelterE.TextC(ResourceTypes.Gold).TextUI.text = EconomyValues.GOLD_AFTER_MELTING.ToString();


            //if (UIE.CenterEs.SmelterE.ButtonC.Button.Cli)
            //{

            //}
            
        }
    }
}