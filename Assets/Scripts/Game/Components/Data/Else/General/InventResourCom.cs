using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct InventResourCom
    {
        private Dictionary<PlayerTypes, Dictionary<ResourceTypes, int>> _amountResources;

        internal InventResourCom(bool needNew) : this()
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

        internal int AmountRes(PlayerTypes playerType, ResourceTypes resourceTypes) => _amountResources[playerType][resourceTypes];
        internal void SetAmountRes(PlayerTypes playerType, ResourceTypes resourceType, int value) => _amountResources[playerType][resourceType] = value;
        internal void SetAmountResAll(ResourceTypes resourceType, int value)
        {
            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                _amountResources[playerType][resourceType] = value;
            }
        }

        internal void AddAmountRes(PlayerTypes playerType, ResourceTypes resourceType, int adding = 1) => SetAmountRes(playerType, resourceType, AmountRes(playerType, resourceType) + adding);
        internal void TakeAmountRes(PlayerTypes playerType, ResourceTypes resourceType, int taking = 1) => SetAmountRes(playerType, resourceType, AmountRes(playerType, resourceType) - taking);






        internal bool CanCreateBuild(PlayerTypes playerType, BuildingTypes buildingType, out Dictionary<ResourceTypes, int> needRes)
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
        internal void BuyBuild(PlayerTypes playerType, BuildingTypes buildingType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                SetAmountRes(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuild(buildingType, resType));
        }

        internal bool CanCreateUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
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
        internal void BuyCreateUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                SetAmountRes(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuy(unitType, resType));
        }

        internal bool CanMeltOre(PlayerTypes playerType, out Dictionary<ResourceTypes, int> needRes)
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
        internal void BuyMeltOre(PlayerTypes playerType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                SetAmountRes(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForMelting(resType));

            AddAmountRes(playerType, ResourceTypes.Iron, 4);
            AddAmountRes(playerType, ResourceTypes.Gold, 1);
        }

        internal bool CanUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType, out Dictionary<ResourceTypes, int> needRes)
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
        internal void BuyUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                SetAmountRes(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForUpgrade(buildingType, resType));
        }



        internal bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
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
        internal void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                SetAmountRes(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForUpgradeUnit(unitType, resType));
        }



        internal bool CanBuyTW(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, out Dictionary<ResourceTypes, int> needRes)
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
        internal void BuyTW(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType)
        {
            for (ResourceTypes resType = Support.MinResType; resType < Support.MaxResType; resType++)
                SetAmountRes(playerType, resType, AmountRes(playerType, resType) - EconomyValues.AmountResForBuyTW(toolWeaponType, levelTWType, resType));
        }
    }
}

