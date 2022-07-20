using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Values;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class CenterBuildingZonesUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;

        internal CenterBuildingZonesUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            eUI.CenterEs.MarketE.Zone.TrySetActive(_selectedBuildingsInTownC.Is(BuildingTypes.Market));
            eUI.CenterEs.SmelterE.Zone.TrySetActive(_selectedBuildingsInTownC.Is(BuildingTypes.Smelter));


            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.FoodToWood).TextUI.text = (EconomyValues.FOR_BUY_FROM_MARKET_FOOD_TO_WOOD * Valuess.FOR_ROUND_UP_RESOURCES).ToString();
            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.WoodToFood).TextUI.text = (EconomyValues.FOR_BUY_FROM_MARKET_WOOD_TO_FOOD * Valuess.FOR_ROUND_UP_RESOURCES).ToString();
            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToFood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_FOOD.ToString();
            eUI.CenterEs.MarketE.Text1C(MarketBuyTypes.GoldToWood).TextUI.text = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_WOOD.ToString();

            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.FoodToWood).TextUI.text = (EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD * Valuess.FOR_ROUND_UP_RESOURCES).ToString();
            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.WoodToFood).TextUI.text = (EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD * Valuess.FOR_ROUND_UP_RESOURCES).ToString();
            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToFood).TextUI.text = (EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD * Valuess.FOR_ROUND_UP_RESOURCES).ToString();
            eUI.CenterEs.MarketE.Text2C(MarketBuyTypes.GoldToWood).TextUI.text = (EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD * Valuess.FOR_ROUND_UP_RESOURCES).ToString();


            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Wood).TextUI.text = (EconomyValues.WOOD_NEED_FOR_MELTING * Valuess.FOR_ROUND_UP_RESOURCES).ToString();
            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Ore).TextUI.text = (EconomyValues.ORE_NEED_FOR_MELTING * Valuess.FOR_ROUND_UP_RESOURCES).ToString();

            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Iron).TextUI.text = AmountResourcesAfterMelting.IRON_AFTER_MELTING.ToString();
            eUI.CenterEs.SmelterE.TextC(ResourceTypes.Gold).TextUI.text = AmountResourcesAfterMelting.GOLD_AFTER_MELTING.ToString();
        }
    }
}