using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct UpgradesBuildingsComponent
    {
        private Dictionary<BuildingTypes, Dictionary<bool, int>> _amountUpgradesDict;

        internal UpgradesBuildingsComponent(Dictionary<BuildingTypes, Dictionary<bool, int>> dict)
        {
            _amountUpgradesDict = dict;

            for (BuildingTypes buildingType = 0; buildingType < (BuildingTypes)Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
            {
                var dict1 = new Dictionary<bool, int>();

                dict1.Add(true, default);
                dict1.Add(false, default);

                dict.Add(buildingType, dict1);
            }
        }

        internal int GetAmountUpgrades(BuildingTypes buildingType, bool key) => _amountUpgradesDict[buildingType][key];
        internal int SetAmountUpgrades(BuildingTypes buildingType, bool key, int value) => _amountUpgradesDict[buildingType][key] = value;

        internal void AddAmountUpgrades(BuildingTypes buildingType, bool key, int adding = 1) => SetAmountUpgrades(buildingType, key, GetAmountUpgrades(buildingType, key) + adding);
        internal void TakeAmountUpgrades(BuildingTypes buildingType, bool key, int taking = 1) => SetAmountUpgrades(buildingType, key, GetAmountUpgrades(buildingType, key) - taking);
    }
}
