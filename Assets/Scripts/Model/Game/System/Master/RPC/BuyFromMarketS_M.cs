using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class BuyFromMarketS_M : SystemModel
    {
        internal BuyFromMarketS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Buy(in MarketBuyTypes marketBuyT, in Player sender)
        {
            var needRes = new Dictionary<ResourceTypes, float>();

            needRes.Add(ResourceTypes.Food, 0);
            needRes.Add(ResourceTypes.Wood, 0);
            needRes.Add(ResourceTypes.Ore, 0);
            needRes.Add(ResourceTypes.Iron, 0);
            needRes.Add(ResourceTypes.Gold, 0);

            switch (marketBuyT)
            {
                case MarketBuyTypes.FoodToWood:
                    needRes[ResourceTypes.Food] = EconomyValues.FOR_BUY_FROM_MARKET_FOOD_TO_WOOD;
                    break;

                case MarketBuyTypes.WoodToFood:
                    needRes[ResourceTypes.Wood] = EconomyValues.FOR_BUY_FROM_MARKET_WOOD_TO_FOOD;
                    break;

                case MarketBuyTypes.GoldToFood:
                    needRes[ResourceTypes.Gold] = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_FOOD;
                    break;

                case MarketBuyTypes.GoldToWood:
                    needRes[ResourceTypes.Gold] = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_WOOD;
                    break;
            }

            var canBuy = true;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                if (needRes[resT] > eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources -= needRes[resT];
                }
                switch (marketBuyT)
                {
                    case MarketBuyTypes.FoodToWood:
                        eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD;
                        break;

                    case MarketBuyTypes.WoodToFood:
                        eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToFood:
                        eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToWood:
                        eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD;
                        break;
                }

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}