using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InventorResourcesE
    {
        static Dictionary<string, Entity> _resources;

        static string Key(in ResourceTypes res, in PlayerTypes player) => res.ToString() + player;

        public static ref AmountC Resource(in ResourceTypes res, in PlayerTypes player) => ref _resources[Key(res, player)].Get<AmountC>();
        public static ref AmountC Resource(in string key) => ref _resources[key].Get<AmountC>();

        public static HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _resources) keys.Add(item.Key);
                return keys;
            }
        }

        public InventorResourcesE(in EcsWorld gameW)
        {
            _resources = new Dictionary<string, Entity>();

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                {
                    _resources.Add(Key(res, player), gameW.NewEntity()
                        .Add(new AmountC(EconomyValues.AmountResources(res))));
                }
            }
        }


        public static bool CanCreateBuild(BuildingTypes build, PlayerTypes player,  out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, player).Amount - EconomyValues.AmountResForBuild(build, res);
                needRes.Add(res, EconomyValues.AmountResForBuild(build, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyBuild(in PlayerTypes player, in BuildingTypes build)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Amount -= EconomyValues.AmountResForBuild(build, resType);
        }

        public static bool CanCreateUnit(in PlayerTypes player, in UnitTypes unit, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
            {
                var amountResForBuy = EconomyValues.AmountResForBuy(unit, resType);

                var difAmountRes = Resource(resType, player).Amount - amountResForBuy;
                needRes.Add(resType, amountResForBuy);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyCreateUnit(in PlayerTypes player, in UnitTypes unit)
        {
            for (ResourceTypes resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Amount -= EconomyValues.AmountResForBuy(unit, resType);
        }

        public static bool CanMeltOre(PlayerTypes player, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var can = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                needRes[res] = EconomyValues.AmountResForMelting(res);

                if (Resource(res, player).Amount - EconomyValues.AmountResForMelting(res) < 0) can = false;
            }

            return can;
        }
        public static void BuyMeltOre(PlayerTypes player)
        {
            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
                Resource(res, player).Take(EconomyValues.AmountResForMelting(res));

            Resource(ResourceTypes.Iron, player) += 4;
            Resource(ResourceTypes.Gold, player)++;
        }

        public static bool CanBuy(PlayerTypes player, ResourceTypes res, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
            {
                var difAmountRes = Resource(resType, player).Amount - EconomyValues.AmountResForBuyRes(resType);
                needRes.Add(resType, EconomyValues.AmountResForBuyRes(resType));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyRes(PlayerTypes player, ResourceTypes res)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
            {
                Resource(resType, player).Amount -= EconomyValues.AmountResForBuyRes(resType);
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

            Resource(res, player) += amount;
        }



        public static bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, playerType).Amount - EconomyValues.AmountResForUpgradeUnit(unitType, res);
                needRes.Add(res, EconomyValues.AmountResForUpgradeUnit(unitType, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, playerType).Amount -= EconomyValues.AmountResForUpgradeUnit(unitType, resType);
        }



        public static bool CanBuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes lev, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, player).Amount - EconomyValues.AmountResForBuyTW(tw, lev, res);
                needRes.Add(res, EconomyValues.AmountResForBuyTW(tw, lev, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes level)
        {
            for (var resType = ResourceTypes.First; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Amount -= EconomyValues.AmountResForBuyTW(tw, level, resType);
        }
    }
}

