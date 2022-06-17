using Chessy.Game.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void TryBuyFromMarketBuildingM(in MarketBuyTypes marketBuyT, in Player sender)
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
                if (needRes[resT] > _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources -= needRes[resT];
                }
                switch (marketBuyT)
                {
                    case MarketBuyTypes.FoodToWood:
                        _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD;
                        break;

                    case MarketBuyTypes.WoodToFood:
                        _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToFood:
                        _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToWood:
                        _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD;
                        break;
                }

                _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                _eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}