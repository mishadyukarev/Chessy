using System;
using System.Collections.Generic;
using static Scripts.Game.StartEconomyValues;

namespace Scripts.Game
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
        internal int SetAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int value) => _amountUpgradesDict[playerType][buildingType] = value;

        internal void AddAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int adding = 1) => SetAmountUpgrades(playerType, buildingType, AmountUpgs(playerType, buildingType) + adding);
        internal void TakeAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int taking = 1) => SetAmountUpgrades(playerType, buildingType, AmountUpgs(playerType, buildingType) - taking);


        internal int GetExtractOneBuild(PlayerTypes playerType, BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    return BENEFIT_FOOD_FARM + BENEFIT_FOOD_FARM * _amountUpgradesDict[playerType][buildingType];

                case BuildingTypes.Woodcutter:
                    return BENEFIT_WOOD_WOODCUTTER + BENEFIT_WOOD_WOODCUTTER * _amountUpgradesDict[playerType][buildingType];

                case BuildingTypes.Mine:
                    return BENEFIT_ORE_MINE + BENEFIT_ORE_MINE * _amountUpgradesDict[playerType][buildingType];

                default:
                    throw new Exception();
            }
        }
    }
}
