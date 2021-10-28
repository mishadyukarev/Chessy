using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public readonly struct InventResourcesC
    {
        private static Dictionary<PlayerTypes, Dictionary<ResourceTypes, int>> _amountResources;

        public InventResourcesC(bool needNew) : this()
        {
            if (needNew)
            {
                _amountResources = new Dictionary<PlayerTypes, Dictionary<ResourceTypes, int>>();

                _amountResources[PlayerTypes.First] = new Dictionary<ResourceTypes, int>();
                _amountResources[PlayerTypes.Second] = new Dictionary<ResourceTypes, int>();

                for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
                {
                    _amountResources[PlayerTypes.First].Add(resourceType, default);
                    _amountResources[PlayerTypes.Second].Add(resourceType, default);
                }
            }
        }

        public static int AmountRes(PlayerTypes playerType, ResourceTypes resourceTypes) => _amountResources[playerType][resourceTypes];
        public static void Set(PlayerTypes playerType, ResourceTypes resourceType, int value) => _amountResources[playerType][resourceType] = value;
        public static void SetAmountResAll(ResourceTypes resourceType, int value)
        {
            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                _amountResources[playerType][resourceType] = value;
            }
        }

        public static void AddAmountRes(PlayerTypes playerType, ResourceTypes resourceType, int adding = 1) => Set(playerType, resourceType, AmountRes(playerType, resourceType) + adding);
        public static void TakeAmountRes(PlayerTypes playerType, ResourceTypes resourceType, int taking = 1) => Set(playerType, resourceType, AmountRes(playerType, resourceType) - taking);






        public static bool CanCreateBuild(PlayerTypes playerType, BuildingTypes buildingType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForBuild(buildingType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyBuild(PlayerTypes playerType, BuildingTypes buildingType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuild(buildingType, resType));
        }

        public static bool CanCreateUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForBuy(unitType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyCreateUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuy(unitType, resType));
        }

        public static bool CanMeltOre(PlayerTypes playerType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForMelting(resType);
                needRes.Add(resType, difAmountRes);

                if(canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyMeltOre(PlayerTypes playerType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForMelting(resType));

            AddAmountRes(playerType, ResourceTypes.Iron, 4);
            AddAmountRes(playerType, ResourceTypes.Gold, 1);
        }

        public static bool CanUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForUpgrade(buildingType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForUpgrade(buildingType, resType));
        }



        public static bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForUpgradeUnit(unitType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForUpgradeUnit(unitType, resType));
        }



        public static bool CanBuyTW(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                var difAmountRes = AmountRes(playerType, resType) - EconomyValues.AmountResForBuyTW(toolWeaponType, levelTWType, resType);
                needRes.Add(resType, difAmountRes);

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public static void BuyTW(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                Set(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuyTW(toolWeaponType, levelTWType, resType));
        }
    }
}

