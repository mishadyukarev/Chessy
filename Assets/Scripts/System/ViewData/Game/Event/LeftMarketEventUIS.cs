namespace Game.Game
{
    public sealed class LeftMarketEventUIS : SystemUIAbstract
    {
        internal LeftMarketEventUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftEs.MarketEs.FoodToWood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.FoodToWood); });
            UIEs.LeftEs.MarketEs.WoodToFood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.WoodToFood); });
            UIEs.LeftEs.MarketEs.GoldToFood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToFood); });
            UIEs.LeftEs.MarketEs.GoldToWood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToWood); });
        }

        void BuyResource(in MarketBuyTypes marketBuyT)
        {
            if (E.IsMyMove)
            {
                E.RpcE.BuyResource(marketBuyT);
            }
            
        }
    }
}