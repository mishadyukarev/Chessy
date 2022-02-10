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
            if (Resource(ResourceTypes.Wood, player).AmountResource >= 10 
                && Resource(ResourceTypes.Ore, player).AmountResource >= 10)
            {
                Resource(ResourceTypes.Wood, player).Take(10);
                Resource(ResourceTypes.Ore, player).Take(10);

                if (UnityEngine.Random.Range(0f, 1f) <= 0.7f)
                {
                    if (UnityEngine.Random.Range(0f, 1f) <= 0.2f)
                    {
                        Resource(ResourceTypes.Gold, player).Add();
                    }
                    else
                    {
                        Resource(ResourceTypes.Iron, player).Add();
                    }
                }
            }


        }

        public bool CanBuyBuilding_Master(in BuildingTypes build, in PlayerTypes player, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
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

        public bool CanBuyResourceFromMarket_Master(in MarketBuyTypes marketBuyT, in PlayerTypes player, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();

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

            foreach (var item in needRes) if (item.Value > Resource(item.Key, player).AmountResource) return false;
            return true;
        }
        public void TryBuyResourcesFromMarket_Master(in MarketBuyTypes marketBuyT, in Player sender, in Entities ents)
        {
            var whoseMove = ents.WhoseMoveE.WhoseMove.Player;

            if (CanBuyResourceFromMarket_Master(marketBuyT, whoseMove, out var needRes))
            {
                switch (marketBuyT)
                {
                    case MarketBuyTypes.FoodToWood:
                        Resource(ResourceTypes.Food, whoseMove).Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Wood, whoseMove).Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    case MarketBuyTypes.WoodToFood:
                        Resource(ResourceTypes.Wood, whoseMove).Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Food, whoseMove).Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    case MarketBuyTypes.GoldToFood:
                        Resource(ResourceTypes.Gold, whoseMove).Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Food, whoseMove).Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    case MarketBuyTypes.GoldToWood:
                        Resource(ResourceTypes.Gold, whoseMove).Take(ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT));
                        Resource(ResourceTypes.Wood, whoseMove).Add(ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT));
                        break;

                    default: throw new Exception();
                }
            }
            else
            {
                ents.RpcE.MistakeEconomyToGeneral(sender, needRes);
            }
        }

        public bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, playerType).Resources.Amount - ResourcesInInventorValues.AmountResForUpgradeUnit(unitType, res);
                needRes.Add(res, ResourcesInInventorValues.AmountResForUpgradeUnit(unitType, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, playerType).Take(ResourcesInInventorValues.AmountResForUpgradeUnit(unitType, resType));
        }

        public bool CanBuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes lev, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, player).Resources.Amount - ResourcesInInventorValues.AmountResForBuyTW(tw, lev, res);
                needRes.Add(res, ResourcesInInventorValues.AmountResForBuyTW(tw, lev, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes level)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Take(ResourcesInInventorValues.AmountResForBuyTW(tw, level, resType));
        }
    }
}

