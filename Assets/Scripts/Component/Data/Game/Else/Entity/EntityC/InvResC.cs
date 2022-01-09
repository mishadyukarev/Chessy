using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InvResC
    {
        readonly static Dictionary<string, int> _resources;
        readonly static EconomyValues _values;

        static string Key(ResTypes res, PlayerTypes player) => res.ToString() + player;
        static bool ContainsKey(string key) => _resources.ContainsKey(key);

        public static Dictionary<string, int> Resources
        {
            get
            {
                var dict = new Dictionary<string, int>();
                foreach (var item in _resources) dict.Add(item.Key, item.Value);
                return dict;
            }
        }

        public static int AmountRes(ResTypes res, PlayerTypes player)
        {
            var key = Key(res, player);

            if (!ContainsKey(key)) throw new Exception();

            return _resources[key];
        }
        public static bool Have(ResTypes res, PlayerTypes player) => AmountRes(res, player) > 0;
        public static bool IsMinusRes(ResTypes res, PlayerTypes player) => AmountRes(res, player) < 0;
        public static int StandartAdding(ResTypes res) => _values.Adding(res);



        static InvResC()
        {
            _resources = new Dictionary<string, int>();

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _resources.Add(Key(res, player), default);
                }
            }

            _values = new EconomyValues();
        }
        public InvResC(bool isStartGame)
        {
            if (isStartGame)
            {
                for (var res = ResTypes.First; res < ResTypes.End; res++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _resources[Key(res, player)] = _values.AmountResources(res);
                    }
                }
            }
            else throw new Exception();
        }


        public static void Set(ResTypes res, PlayerTypes player, int value)
        {
            var key = Key(res, player);
            if (!ContainsKey(key)) throw new Exception();

            _resources[key] = value;
        }
        public static void Sync(string key, int value)
        {
            if (!ContainsKey(key)) throw new Exception();

            _resources[key] = value;
        }
        public static void Reset(ResTypes res, PlayerTypes player) => Set(res, player, 0);
        public static void Add(ResTypes res, PlayerTypes player, int adding = 1)
        {
            var key = Key(res, player);
            if (!ContainsKey(key)) throw new Exception();

            _resources[key] += adding;
        }
        public static void AddStandartValues()
        {
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                Add(res, PlayerTypes.First, _values.Adding(res));
            }
        }
        public static void Take(ResTypes res, PlayerTypes player, int taking = 1)
        {
            var key = Key(res, player);
            if (!ContainsKey(key)) throw new Exception();

            _resources[key] -= taking;
        }




        public static bool CanCreateBuild(PlayerTypes player, BuildTypes build, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                var difAmountRes = AmountRes(res, player) - _values.AmountResForBuild(build, res);
                needRes.Add(res, _values.AmountResForBuild(build, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyBuild(PlayerTypes player, BuildTypes build)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
                Set(resType, player, AmountRes(resType, player) - _values.AmountResForBuild(build, resType));
        }

        public static bool CanCreateUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForBuy(unitType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyCreateUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResTypes resType = ResTypes.First; resType < ResTypes.End; resType++)
                Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForBuy(unitType, resType));
        }

        public static bool CanMeltOre(PlayerTypes player, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var can = true;

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                needRes[res] = _values.AmountResForMelting(res);

                if (AmountRes(res, player) - _values.AmountResForMelting(res) < 0) can = false;
            }

            return can;
        }
        public static void BuyMeltOre(PlayerTypes player)
        {
            for (var res = ResTypes.First; res < ResTypes.End; res++)
                Set(res, player, AmountRes(res, player) - _values.AmountResForMelting(res));

            Add(ResTypes.Iron, player, 4);
            Add(ResTypes.Gold, player, 1);
        }

        public static bool CanBuy(PlayerTypes playerType, ResTypes res, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForBuyRes(resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyRes(PlayerTypes playerType, ResTypes res)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForBuyRes(resType));
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

            Add(res, playerType, amount);
        }



        public static bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForUpgradeUnit(unitType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
                Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForUpgradeUnit(unitType, resType));
        }



        public static bool CanBuyTW(PlayerTypes playerType, TWTypes toolWeaponType, LevelTypes levelTWType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
            {
                var difAmountRes = AmountRes(resType, playerType) - _values.AmountResForBuyTW(toolWeaponType, levelTWType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyTW(PlayerTypes playerType, TWTypes toolWeaponType, LevelTypes levelTWType)
        {
            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
                Set(resType, playerType, AmountRes(resType, playerType) - _values.AmountResForBuyTW(toolWeaponType, levelTWType, resType));
        }
    }
}

