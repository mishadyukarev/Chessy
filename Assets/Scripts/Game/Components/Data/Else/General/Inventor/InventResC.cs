﻿using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public readonly struct InventResC
    {
        private static Dictionary<PlayerTypes, Dictionary<ResTypes, int>> _amountResources;

        public InventResC(bool needNew) : this()
        {
            if (needNew)
            {
                _amountResources = new Dictionary<PlayerTypes, Dictionary<ResTypes, int>>();

                _amountResources[PlayerTypes.First] = new Dictionary<ResTypes, int>();
                _amountResources[PlayerTypes.Second] = new Dictionary<ResTypes, int>();

                for (ResTypes resourceType = (ResTypes)1; resourceType < (ResTypes)Enum.GetNames(typeof(ResTypes)).Length; resourceType++)
                {
                    _amountResources[PlayerTypes.First].Add(resourceType, default);
                    _amountResources[PlayerTypes.Second].Add(resourceType, default);
                }
            }
        }

        public static int AmountRes(PlayerTypes playerType, ResTypes resourceTypes) => _amountResources[playerType][resourceTypes];
        public static void Set(PlayerTypes player, ResTypes res, int value) => _amountResources[player][res] = value;
        public static void SetAmountResAll(ResTypes resourceType, int value)
        {
            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                _amountResources[playerType][resourceType] = value;
            }
        }
        public static bool HaveRes(PlayerTypes player, ResTypes res) => AmountRes(player, res) > 0;
        public static bool IsMinusRes(PlayerTypes player, ResTypes res) => AmountRes(player, res) < 0;
        public static void ResetRes(PlayerTypes player, ResTypes res) => Set(player, res, 0); 

        public static void AddAmountRes(PlayerTypes playerType, ResTypes resourceType, int adding = 1) => Set(playerType, resourceType, AmountRes(playerType, resourceType) + adding);
        public static void TakeAmountRes(PlayerTypes playerType, ResTypes resourceType, int taking = 1) => Set(playerType, resourceType, AmountRes(playerType, resourceType) - taking);






        public static bool CanCreateBuild(PlayerTypes playerType, BuildTypes buildingType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForBuild(buildingType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyBuild(PlayerTypes playerType, BuildTypes buildingType)
        {
            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuild(buildingType, resType));
        }

        public static bool CanCreateUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForBuy(unitType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyCreateUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuy(unitType, resType));
        }

        public static bool CanMeltOre(PlayerTypes playerType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForMelting(resType);
                needRes.Add(resType, difAmountRes);

                if(canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyMeltOre(PlayerTypes playerType)
        {
            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForMelting(resType));

            AddAmountRes(playerType, ResTypes.Iron, 4);
            AddAmountRes(playerType, ResTypes.Gold, 1);
        }

        public static bool CanUpgradeBuildings(PlayerTypes playerType, BuildTypes buildingType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForUpgrade(buildingType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyUpgradeBuildings(PlayerTypes playerType, BuildTypes buildingType)
        {
            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForUpgrade(buildingType, resType));
        }



        public static bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForUpgradeUnit(unitType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForUpgradeUnit(unitType, resType));
        }



        public static bool CanBuyTW(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, out Dictionary<ResTypes, int> needRes)
        {
            needRes = new Dictionary<ResTypes, int>();
            var canCreatBuild = true;

            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForBuyTW(toolWeaponType, levelTWType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyTW(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType)
        {
            for (ResTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuyTW(toolWeaponType, levelTWType, resType));
        }
    }
}

