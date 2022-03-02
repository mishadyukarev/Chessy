namespace Chessy.Game
{
    public sealed class LeftMarketEventUIS : SystemUIAbstract
    {
        internal LeftMarketEventUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
            //UIE.LeftEs.MarketEs.FoodToWood.AddListener(delegate { BuyResource(MarketBuyTypes.FoodToWood); });
            //UIE.LeftEs.MarketEs.WoodToFood.AddListener(delegate { BuyResource(MarketBuyTypes.WoodToFood); });
            //UIE.LeftEs.MarketEs.GoldToFood.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToFood); });
            //UIE.LeftEs.MarketEs.GoldToWood.AddListener(delegate { BuyResource(MarketBuyTypes.GoldToWood); });
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