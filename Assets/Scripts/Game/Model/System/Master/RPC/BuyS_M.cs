﻿using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.System.Model
{
    public sealed class BuyS_M : SystemModelGameAbs
    {
        public BuyS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Buy(in MarketBuyTypes marketBuyT, in Player sender)
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
                if (needRes[resT] > eMGame.PlayerInfoE(eMGame.WhoseMove.Player).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    eMGame.PlayerInfoE(eMGame.WhoseMove.Player).ResourcesC(resT).Resources -= needRes[resT];
                }
                switch (marketBuyT)
                {
                    case MarketBuyTypes.FoodToWood:
                        eMGame.PlayerInfoE(eMGame.WhoseMove.Player).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD;
                        break;

                    case MarketBuyTypes.WoodToFood:
                        eMGame.PlayerInfoE(eMGame.WhoseMove.Player).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToFood:
                        eMGame.PlayerInfoE(eMGame.WhoseMove.Player).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToWood:
                        eMGame.PlayerInfoE(eMGame.WhoseMove.Player).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD;
                        break;
                }

                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                eMGame.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}