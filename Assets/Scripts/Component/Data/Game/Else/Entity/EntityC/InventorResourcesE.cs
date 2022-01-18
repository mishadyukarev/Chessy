using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InventorResourcesE
    {
        static Dictionary<string, Entity> _resources;

        static string Key(in ResTypes res, in PlayerTypes player) => res.ToString() + player;

        public static ref C Resource<C>(in ResTypes res, in PlayerTypes player) where C : struct => ref _resources[Key(res, player)].Get<C>();
        public static ref C Resource<C>(in string key) where C : struct => ref _resources[key].Get<C>();

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

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _resources.Add(Key(res, player), gameW.NewEntity()
                        .Add(new AmountC(EconomyValues.AmountResources(res))));
                }
            }
        }


        public static bool CanCreateBuild(BuildingTypes build, PlayerTypes player,  out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                var difAmountRes = Resource<AmountC>(res, player).Amount - EconomyValues.AmountResForBuild(build, res);
                needRes.Add(res, EconomyValues.AmountResForBuild(build, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyBuild(in PlayerTypes player, in BuildingTypes build)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
                Resource<AmountC>(resType, player).Amount -= EconomyValues.AmountResForBuild(build, resType);
        }

        public static bool CanCreateUnit(in PlayerTypes player, in UnitTypes unit, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = Resource<AmountC>(resType, player).Amount - EconomyValues.AmountResForBuy(unit, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyCreateUnit(in PlayerTypes player, in UnitTypes unit)
        {
            for (ResTypes resType = ResTypes.First; resType < ResTypes.End; resType++)
                Resource<AmountC>(resType, player).Amount -= EconomyValues.AmountResForBuy(unit, resType);
        }

        public static bool CanMeltOre(PlayerTypes player, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var can = true;

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                needRes[res] = EconomyValues.AmountResForMelting(res);

                if (Resource<AmountC>(res, player).Amount - EconomyValues.AmountResForMelting(res) < 0) can = false;
            }

            return can;
        }
        public static void BuyMeltOre(PlayerTypes player)
        {
            for (var res = ResTypes.First; res < ResTypes.End; res++)
                Resource<AmountC>(res, player).Take(EconomyValues.AmountResForMelting(res));

            Resource<AmountC>(ResTypes.Iron, player).Add(4);
            Resource<AmountC>(ResTypes.Gold, player).Add();
        }

        public static bool CanBuy(PlayerTypes player, ResTypes res, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = Resource<AmountC>(resType, player).Amount - EconomyValues.AmountResForBuyRes(resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyRes(PlayerTypes player, ResTypes res)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                Resource<AmountC>(resType, player).Amount -= EconomyValues.AmountResForBuyRes(resType);
            }

            var amount = 0;

            switch (res)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: amount = 30; break;
                case ResTypes.Wood: amount = 15; break;
                case ResTypes.Ore: throw new Exception();
                case ResTypes.Iron: throw new Exception();
                case ResTypes.Gold: throw new Exception();
                default: throw new Exception();
            }

            Resource<AmountC>(res, player).Add(amount);
        }



        public static bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = Resource<AmountC>(resType, playerType).Amount - EconomyValues.AmountResForUpgradeUnit(unitType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
                Resource<AmountC>(resType, playerType).Amount -= EconomyValues.AmountResForUpgradeUnit(unitType, resType);
        }



        public static bool CanBuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes lev, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = Resource<AmountC>(resType, player).Amount - EconomyValues.AmountResForBuyTW(tw, lev, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes level)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
                Resource<AmountC>(resType, player).Amount -= EconomyValues.AmountResForBuyTW(tw, level, resType);
        }
    }
}

