using System;
using System.Collections.Generic;
using static Scripts.Game.EconomyValues;

namespace Scripts.Game
{
    public struct UpgBuildsC
    {
        private static Dictionary<PlayerTypes, Dictionary<BuildingTypes, int>> _amountUpgradesDict;

        public UpgBuildsC(bool needNew) : this()
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

        public static int AmountUpgs(PlayerTypes playerType, BuildingTypes buildingType) => _amountUpgradesDict[playerType][buildingType];
        public static int SetAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int value) => _amountUpgradesDict[playerType][buildingType] = value;

        public static void AddAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int adding = 1) => SetAmountUpgrades(playerType, buildingType, AmountUpgs(playerType, buildingType) + adding);
        public static void TakeAmountUpgrades(PlayerTypes playerType, BuildingTypes buildingType, int taking = 1) => SetAmountUpgrades(playerType, buildingType, AmountUpgs(playerType, buildingType) - taking);


        public static int GetExtractOneBuild(PlayerTypes playerType, BuildingTypes buildingType)
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
