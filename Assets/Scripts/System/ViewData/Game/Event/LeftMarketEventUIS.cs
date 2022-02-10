namespace Game.Game
{
    public sealed class LeftMarketEventUIS : SystemViewAbstract
    {
        internal LeftMarketEventUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
            VEs.UIEs.LeftEs.MarketEs.FoodToWood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.FoodToWood); });
            VEs.UIEs.LeftEs.MarketEs.WoodToFood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.WoodToFood); });
            VEs.UIEs.LeftEs.MarketEs.GoldToFood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToFood); });
            VEs.UIEs.LeftEs.MarketEs.GoldToWood.ButtonUIC.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToWood); });
        }

        void BuyResource(in MarketBuyTypes marketBuyT)
        {
            if (Es.WhoseMoveE.IsMyMove)
            {
                Es.RpcE.BuyResource(marketBuyT);
            }
            
        }
    }
}