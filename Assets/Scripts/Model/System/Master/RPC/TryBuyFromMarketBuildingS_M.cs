using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TryBuyFromMarketBuildingM(in MarketBuyTypes marketBuyT, in Player sender)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

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
                if (needRes[resT] > ResourcesInInventoryC(whoDoing).ResourcesRef(resT))
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    ResourcesInInventoryC(whoDoing).Subtract(resT, needRes[resT]);
                }
                switch (marketBuyT)
                {
                    case MarketBuyTypes.FoodToWood:
                        ResourcesInInventoryC(whoDoing).Add(ResourceTypes.Wood, EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD);
                        break;

                    case MarketBuyTypes.WoodToFood:
                        ResourcesInInventoryC(whoDoing).Add(ResourceTypes.Food, EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD);
                        break;

                    case MarketBuyTypes.GoldToFood:
                        ResourcesInInventoryC(whoDoing).Add(ResourceTypes.Food, EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD);
                        break;

                    case MarketBuyTypes.GoldToWood:
                        ResourcesInInventoryC(whoDoing).Add(ResourceTypes.Wood, EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD);
                        break;
                }

                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                RpcSs.SimpleMistakeToGeneral(sender, needRes);
            }
        }
    }
}