namespace Chessy.Game
{
    public sealed class LeftMarketEventUIS : SystemUIAbstract
    {
        internal LeftMarketEventUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftEs.MarketEs.FoodToWood.AddListener(delegate { BuyResource(MarketBuyTypes.FoodToWood); });
            UIEs.LeftEs.MarketEs.WoodToFood.AddListener(delegate { BuyResource(MarketBuyTypes.WoodToFood); });
            UIEs.LeftEs.MarketEs.GoldToFood.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToFood); });
            UIEs.LeftEs.MarketEs.GoldToWood.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToWood); });
        }

        void BuyResource(in MarketBuyTypes marketBuyT)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.RpcPoolEs.BuyResource(marketBuyT);
            }
            
        }
    }
}