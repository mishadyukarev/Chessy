using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct UpgBuildsC
    {
        private static Dictionary<PlayerTypes, Dictionary<BuildTypes, int>> _amountUpgradesDict;

        public UpgBuildsC(bool needNew) : this()
        {
            if (needNew)
            {
                _amountUpgradesDict = new Dictionary<PlayerTypes, Dictionary<BuildTypes, int>>();

                _amountUpgradesDict.Add(PlayerTypes.First, new Dictionary<BuildTypes, int>());
                _amountUpgradesDict.Add(PlayerTypes.Second, new Dictionary<BuildTypes, int>());

                for (BuildTypes buildingType = 0; buildingType < (BuildTypes)Enum.GetNames(typeof(BuildTypes)).Length; buildingType++)
                {
                    _amountUpgradesDict[PlayerTypes.First].Add(buildingType, default);
                    _amountUpgradesDict[PlayerTypes.Second].Add(buildingType, default);
                }
            }
        }

        public static int AmountUpgs(PlayerTypes playerType, BuildTypes buildingType) => _amountUpgradesDict[playerType][buildingType];
        public static int SetAmountUpgs(PlayerTypes playerType, BuildTypes buildingType, int value) => _amountUpgradesDict[playerType][buildingType] = value;

        public static void AddAmountUpgs(PlayerTypes playerType, BuildTypes buildingType, int adding = 1) => SetAmountUpgs(playerType, buildingType, AmountUpgs(playerType, buildingType) + adding);
        public static void TakeAmountUpgs(PlayerTypes playerType, BuildTypes buildingType, int taking = 1) => SetAmountUpgs(playerType, buildingType, AmountUpgs(playerType, buildingType) - taking);

        public static int GetExtractOneBuild(PlayerTypes playerType, BuildTypes buildType) => 1 + 1 * AmountUpgs(playerType, buildType);
    }
}
