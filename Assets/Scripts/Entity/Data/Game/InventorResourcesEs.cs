using ECS;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InventorResourcesEs
    {
        readonly Dictionary<string, ResourcesInInventorE> _resources;

        string Key(in ResourceTypes res, in PlayerTypes player) => res.ToString() + player;

        public ResourcesInInventorE Resource(in ResourceTypes res, in PlayerTypes player) => _resources[Key(res, player)];
        public ResourcesInInventorE Resource(in string key) => _resources[key];

        public HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _resources) keys.Add(item.Key);
                return keys;
            }
        }

        internal InventorResourcesEs(in EcsWorld gameW)
        {
            _resources = new Dictionary<string, ResourcesInInventorE>();

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                {
                    _resources.Add(Key(res, player), new ResourcesInInventorE(res, player, gameW));
                }
            }
        }

        public void Melt_Master(in PlayerTypes player)
        {
            if (Resource(ResourceTypes.Wood, player).ResourceC.Resources >= 10 
                && Resource(ResourceTypes.Ore, player).ResourceC.Resources >= 10)
            {
                Resource(ResourceTypes.Wood, player).ResourceC.Take(10);
                Resource(ResourceTypes.Ore, player).ResourceC.Take(10);

                if (UnityEngine.Random.Range(0f, 1f) <= 0.7f)
                {
                    if (UnityEngine.Random.Range(0f, 1f) <= 0.2f)
                    {
                        Resource(ResourceTypes.Gold, player).ResourceC.Add(1);
                    }
                    else
                    {
                        Resource(ResourceTypes.Iron, player).ResourceC.Add(1);
                    }
                }
            }


        }

        public bool CanBuyBuilding_Master(in BuildingTypes build, in PlayerTypes player, out Dictionary<ResourceTypes, float> needRes)
        {
            needRes = new Dictionary<ResourceTypes, float>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                needRes.Add(res, Resource(res, player).Need(build));
                if (canCreatBuild) canCreatBuild = Resource(res, player).CanBuy(build);
            }
            return canCreatBuild;
        }
        public void BuyBuilding_Master(in BuildingTypes build, in PlayerTypes player)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Buy(build);
        }

        public bool CanBuyResourceFromMarket_Master(in MarketBuyTypes marketBuyT, in PlayerTypes player, out Dictionary<ResourceTypes, float> needRes)
        {
            needRes = new Dictionary<ResourceTypes, float>();

            needRes.Add(ResourceTypes.Food, 0);
            needRes.Add(ResourceTypes.Wood, 0);
            needRes.Add(ResourceTypes.Ore, 0);
            needRes.Add(ResourceTypes.Iron, 0);
            needRes.Add(ResourceTypes.Gold, 0);

            switch (marketBuyT)
            {
                case MarketBuyTypes.FoodToWood:
                    needRes[ResourceTypes.Food] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
                    break;

                case MarketBuyTypes.WoodToFood:
                    needRes[ResourceTypes.Wood] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
                    break;

                case MarketBuyTypes.GoldToFood:
                    needRes[ResourceTypes.Gold] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
                    break;

                case MarketBuyTypes.GoldToWood:
                    needRes[ResourceTypes.Gold] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
                    break;

                default: throw new Exception();
            }

            foreach (var item in needRes) if (item.Value > Resource(item.Key, player).ResourceC.Resources) return false;
            return true;
        }
        public void TryBuyResourcesFromMarket_Master(in MarketBuyTypes marketBuyT, in Player sender, in Entities ents)
        {
            var whoseMove = ents.WhoseMovePlayerTC.Player;

            if (CanBuyResourceFromMarket_Master(marketBuyT, whoseMove, out var needRes))
            {
                switch (marketBuyT)
                {
                    case MarketBuyTypes.FoodToWood:
                        Resource(ResourceTypes.Food, whoseMove).ResourceC.Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Wood, whoseMove).ResourceC.Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    case MarketBuyTypes.WoodToFood:
                        Resource(ResourceTypes.Wood, whoseMove).ResourceC.Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Food, whoseMove).ResourceC.Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    case MarketBuyTypes.GoldToFood:
                        Resource(ResourceTypes.Gold, whoseMove).ResourceC.Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Food, whoseMove).ResourceC.Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    case MarketBuyTypes.GoldToWood:
                        Resource(ResourceTypes.Gold, whoseMove).ResourceC.Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Wood, whoseMove).ResourceC.Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    default: throw new Exception();
                }
            }
            else
            {
                ents.RpcE.MistakeEconomyToGeneral(sender, needRes);
            }
        }

        public bool CanBuyTW(ToolWeaponTypes tw, LevelTypes lev, PlayerTypes player,  out Dictionary<ResourceTypes, float> needRes)
        {
            needRes = new Dictionary<ResourceTypes, float>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, player).ResourceC.Resources - ResourcesInInventorValues.ForBuyToolWeapon(tw, lev, res);
                needRes.Add(res, ResourcesInInventorValues.ForBuyToolWeapon(tw, lev, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyTW(ToolWeaponTypes tw, LevelTypes level, PlayerTypes player)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, player).ResourceC.Take(ResourcesInInventorValues.ForBuyToolWeapon(tw, level, resType));
        }
    }
}

