using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.EconomyValues;

namespace Assets.Scripts.Workers
{
    internal class InfoBuidlingsWorker : MainGeneralWorker
    {
        #region BuidingUpgrade

        internal static int AmountUpgrades(BuildingTypes buildingType, bool key)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    return EGGM.FarmsInfoEnt_AmountUpgradesCom.AmountUpgradesDict[key];

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountUpgradesCom.AmountUpgradesDict[key];

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountUpgradesCom.AmountUpgradesDict[key];

                default:
                    throw new Exception();
            }
        }
        internal static int SetAmountUpgrades(BuildingTypes buildingType, bool key, int value)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    return EGGM.FarmsInfoEnt_AmountUpgradesCom.AmountUpgradesDict[key] = value;

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountUpgradesCom.AmountUpgradesDict[key] = value;

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountUpgradesCom.AmountUpgradesDict[key] = value;

                default:
                    throw new Exception();
            }
        }

        internal static void AddAmountUpgrades(BuildingTypes buildingType, bool key, int adding = 1)
            => SetAmountUpgrades(buildingType, key, AmountUpgrades(buildingType, key) + adding);
        internal static void TakeAmountUpgrades(BuildingTypes buildingType, bool key, int taking = 1)
            => SetAmountUpgrades(buildingType, key, AmountUpgrades(buildingType, key) - taking);

        #endregion


        #region Buidings

        internal static bool IsSettedCity(bool key) => AmountBuildingsInGame(BuildingTypes.City, key) > 0;


        internal static List<int[]> GetListXyBuildingsInGame(BuildingTypes buildingType, bool key)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return EGGM.CityInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key];

                case BuildingTypes.Farm:
                    return EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key];

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key];

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key];

                default:
                    throw new Exception();
            }
        }
        internal static void SetListXyBuildingsInGame(BuildingTypes buildingType, bool key, List<int[]> list)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    EGGM.CityInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list;
                    break;

                case BuildingTypes.Farm:
                    EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list;
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list;
                    break;

                case BuildingTypes.Mine:
                    EGGM.MinesInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static int AmountBuildingsInGame(BuildingTypes buildingType, bool key) => GetListXyBuildingsInGame(buildingType, key).Count;

        internal static void AddXyBuildingsInGame(BuildingTypes buildingType, bool key, int[] xyAdding) => GetListXyBuildingsInGame(buildingType, key).Add(xyAdding);
        internal static bool TakeXyBuildingsInGame(BuildingTypes buildingType, bool key, int[] xyTaking) => GetListXyBuildingsInGame(buildingType, key).Remove(xyTaking);

        internal static int GetAmountAllBuildingsInGame(bool key)
        {
            var amountAllBuildInGame = 0;

            for (int curNumberBuilType = 1; curNumberBuilType < Enum.GetNames(typeof(BuildingTypes)).Length; curNumberBuilType++)
            {
                amountAllBuildInGame += AmountBuildingsInGame((BuildingTypes)curNumberBuilType, key);
            }

            return amountAllBuildInGame;
        }
        internal static int GetAmountAllBuildingsInGame() => GetAmountAllBuildingsInGame(true) + GetAmountAllBuildingsInGame(false);

        internal static int GetExtractionBuildingType(BuildingTypes buildingType, bool key)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    return BENEFIT_FOOD_FARM + BENEFIT_FOOD_FARM * AmountUpgrades(buildingType, key);

                case BuildingTypes.Woodcutter:
                    return BENEFIT_WOOD_WOODCUTTER + BENEFIT_WOOD_WOODCUTTER * AmountUpgrades(buildingType, key);

                case BuildingTypes.Mine:
                    return BENEFIT_ORE_MINE + BENEFIT_ORE_MINE * AmountUpgrades(buildingType, key);

                default:
                    throw new Exception();
            }
        }

        #endregion
    }
}
