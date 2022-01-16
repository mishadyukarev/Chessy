﻿using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntInventorResources
    {
        readonly static Dictionary<string, Entity> _resources;
        readonly static EconomyValues _values;

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

        static EntInventorResources()
        {
            _resources = new Dictionary<string, Entity>();

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _resources.Add(Key(res, player), default);
                }
            }

            _values = new EconomyValues();
        }
        public EntInventorResources(in EcsWorld gameW)
        {
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _resources[Key(res, player)] = gameW.NewEntity()
                        .Add(new AmountC(_values.AmountResources(res)));
                }
            }
        }




        //public static bool CanCreateBuild(PlayerTypes player, BuildTypes build, out Dictionary<ResTypes, int> needRes)
        //{
        //    needRes = new Dictionary<ResTypes, int>();
        //    var canCreatBuild = true;

        //    for (var res = ResTypes.First; res < ResTypes.End; res++)
        //    {
        //        var difAmountRes = AmountRes(res, player) - _values.AmountResForBuild(build, res);
        //        needRes.Add(res, _values.AmountResForBuild(build, res));

        //        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
        //    }

        //    return canCreatBuild;
        //}
        //public static void BuyBuild(PlayerTypes player, BuildTypes build)
        //{
        //    for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
        //        Set(resType, player, AmountRes(resType, player) - _values.AmountResForBuild(build, resType));
        //}

        //public static bool CanCreateUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResTypes, int> needRes)
        //{
        //    needRes = new Dictionary<ResTypes, int>();
        //    var canCreatBuild = true;

        //    for (ResTypes resType = ResTypes.First; resType < ResTypes.End; resType++)
        //    {
        //        var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForBuy(unitType, resType);
        //        needRes.Add(resType, difAmountRes);

        //        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
        //    }

        //    return canCreatBuild;
        //}
        //public static void BuyCreateUnit(PlayerTypes playerType, UnitTypes unitType)
        //{
        //    for (ResTypes resType = ResTypes.First; resType < ResTypes.End; resType++)
        //        Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForBuy(unitType, resType));
        //}

        //public static bool CanMeltOre(PlayerTypes player, out Dictionary<ResTypes, int> needRes)
        //{
        //    needRes = new Dictionary<ResTypes, int>();
        //    var can = true;

        //    for (var res = ResTypes.First; res < ResTypes.End; res++)
        //    {
        //        needRes[res] = _values.AmountResForMelting(res);

        //        if (AmountRes(res, player) - _values.AmountResForMelting(res) < 0) can = false;
        //    }

        //    return can;
        //}
        //public static void BuyMeltOre(PlayerTypes player)
        //{
        //    for (var res = ResTypes.First; res < ResTypes.End; res++)
        //        Set(res, player, AmountRes(res, player) - _values.AmountResForMelting(res));

        //    Add(ResTypes.Iron, player, 4);
        //    Add(ResTypes.Gold, player, 1);
        //}

        //public static bool CanBuy(PlayerTypes playerType, ResTypes res, out Dictionary<ResTypes, int> needRes)
        //{
        //    needRes = new Dictionary<ResTypes, int>();
        //    var canCreatBuild = true;

        //    for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
        //    {
        //        var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForBuyRes(resType);
        //        needRes.Add(resType, difAmountRes);

        //        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
        //    }

        //    return canCreatBuild;
        //}
        //public static void BuyRes(PlayerTypes playerType, ResTypes res)
        //{
        //    for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
        //    {
        //        Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForBuyRes(resType));
        //    }

        //    var amount = 0;

        //    switch (res)
        //    {
        //        case ResTypes.None: throw new Exception();
        //        case ResTypes.Food: amount = 30; break;
        //        case ResTypes.Wood: amount = 15; break;
        //        case ResTypes.Ore: throw new Exception();
        //        case ResTypes.Iron: throw new Exception();
        //        case ResTypes.Gold: throw new Exception();
        //        default: throw new Exception();
        //    }

        //    Add(res, playerType, amount);
        //}



        //public static bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResTypes, int> needRes)
        //{
        //    needRes = new Dictionary<ResTypes, int>();
        //    var canCreatBuild = true;

        //    for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
        //    {
        //        var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForUpgradeUnit(unitType, resType);
        //        needRes.Add(resType, difAmountRes);

        //        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
        //    }

        //    return canCreatBuild;
        //}
        //public static void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        //{
        //    for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
        //        Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForUpgradeUnit(unitType, resType));
        //}



        //public static bool CanBuyTW(PlayerTypes playerType, TWTypes toolWeaponType, LevelTypes levelTWType, out Dictionary<ResTypes, int> needRes)
        //{
        //    needRes = new Dictionary<ResTypes, int>();
        //    var canCreatBuild = true;

        //    for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
        //    {
        //        var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForBuyTW(toolWeaponType, levelTWType, resType);
        //        needRes.Add(resType, difAmountRes);

        //        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
        //    }

        //    return canCreatBuild;
        //}
        //public static void BuyTW(PlayerTypes playerType, TWTypes toolWeaponType, LevelTypes levelTWType)
        //{
        //    for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
        //        Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForBuyTW(toolWeaponType, levelTWType, resType));
        //}
    }
}
