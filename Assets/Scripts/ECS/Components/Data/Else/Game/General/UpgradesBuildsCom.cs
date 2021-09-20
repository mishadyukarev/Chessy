using Assets.Scripts.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct UpgradesBuildsCom
    {
        private Dictionary<PlayerTypes, Dictionary<BuildingTypes, int>> _amountUpgradesDict;

        internal UpgradesBuildsCom(bool needNew) : this()
        {
            if (needNew)
            {
                _amountUpgradesDict = new Dictionary<PlayerTypes, Dictionary<BuildingTypes, int>>();

                _amountUpgradesDict.Add(PlayerTypes.First, new Dictionary<BuildingTypes, int>());
                _amountUpgradesDict.Add(PlayerTypes.Second, new Dictionary<BuildingTypes, int>());

                for (BuildingTypes buildingType = 0; buildingType < (BuildingTypes)Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
                {
                    _amountUpgradesDict[PlayerTypes.First].Add(buildingType, default);
                    _amountUpgradesDict[PlayerTypes.Second].Add(buildingType, default);
                }
            }
        }

        internal int AmountUpgs(PlayerTypes playerType, BuildingTypes buildingType) => _amountUpgradesDict[playerType][buildingType];
        internal int SetAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType,  int value) => _amountUpgradesDict[playerType][buildingType] = value;

        internal void AddAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int adding = 1) => SetAmountUpgrades(playerType, buildingType, AmountUpgs(playerType, buildingType) + adding);
        internal void TakeAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int taking = 1) => SetAmountUpgrades(playerType, buildingType, AmountUpgs(playerType, buildingType) - taking);
    }
}
