using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InventorResourcesEs
    {
        readonly Dictionary<string, AmountResourcesInInventorE> _resources;

        string Key(in ResourceTypes res, in PlayerTypes player) => res.ToString() + player;

        public AmountResourcesInInventorE Resource(in ResourceTypes res, in PlayerTypes player) => _resources[Key(res, player)];
        public AmountResourcesInInventorE Resource(in string key) => _resources[key];

        public HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _resources) keys.Add(item.Key);
                return keys;
            }
        }

        public InventorResourcesEs(in EcsWorld gameW)
        {
            _resources = new Dictionary<string, AmountResourcesInInventorE>();

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                {
                    _resources.Add(Key(res, player), new AmountResourcesInInventorE(EconomyValues.AmountResources(res), gameW));
                }
            }
        }


        public bool CanCreateBuild(BuildingTypes build, PlayerTypes player, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, player).Resources.Amount - EconomyValues.AmountResForBuild(build, res);
                needRes.Add(res, EconomyValues.AmountResForBuild(build, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyBuild(in PlayerTypes player, in BuildingTypes build)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Resources.Amount -= EconomyValues.AmountResForBuild(build, resType);
        }

        public bool CanCreateUnit(in PlayerTypes player, in UnitTypes unit, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
            {
                var amountResForBuy = EconomyValues.AmountResForBuy(unit, resType);

                var difAmountRes = Resource(resType, player).Resources.Amount - amountResForBuy;
                needRes.Add(resType, amountResForBuy);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyCreateUnit(in PlayerTypes player, in UnitTypes unit)
        {
            for (ResourceTypes resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Resources.Amount -= EconomyValues.AmountResForBuy(unit, resType);
        }

        public bool CanMeltOre(PlayerTypes player, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var can = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                needRes[res] = EconomyValues.AmountResForMelting(res);

                if (Resource(res, player).Resources.Amount - EconomyValues.AmountResForMelting(res) < 0) can = false;
            }

            return can;
        }
        public void BuyMeltOre(PlayerTypes player)
        {
            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
                Resource(res, player).Resources.Take(EconomyValues.AmountResForMelting(res));

            Resource(ResourceTypes.Iron, player).Resources += 4;
            Resource(ResourceTypes.Gold, player).Resources++;
        }

        public bool CanBuy(PlayerTypes player, ResourceTypes res, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
            {
                var difAmountRes = Resource(resType, player).Resources.Amount - EconomyValues.AmountResForBuyRes(resType);
                needRes.Add(resType, EconomyValues.AmountResForBuyRes(resType));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyRes(PlayerTypes player, ResourceTypes res)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
            {
                Resource(resType, player).Resources.Amount -= EconomyValues.AmountResForBuyRes(resType);
            }

            var amount = 0;

            switch (res)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: amount = 100; break;
                case ResourceTypes.Wood: amount = 100; break;
                case ResourceTypes.Ore: throw new Exception();
                case ResourceTypes.Iron: throw new Exception();
                case ResourceTypes.Gold: throw new Exception();
                default: throw new Exception();
            }

            Resource(res, player).Resources += amount;
        }



        public bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, playerType).Resources.Amount - EconomyValues.AmountResForUpgradeUnit(unitType, res);
                needRes.Add(res, EconomyValues.AmountResForUpgradeUnit(unitType, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, playerType).Resources.Amount -= EconomyValues.AmountResForUpgradeUnit(unitType, resType);
        }



        public bool CanBuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes lev, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, player).Resources.Amount - EconomyValues.AmountResForBuyTW(tw, lev, res);
                needRes.Add(res, EconomyValues.AmountResForBuyTW(tw, lev, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes level)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Resources.Amount -= EconomyValues.AmountResForBuyTW(tw, level, resType);
        }
    }
}

