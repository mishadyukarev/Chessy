using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;

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

        private static List<int[]> GetListXyBuild(BuildingTypes buildingType, bool key)
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

        internal static bool IsSettedCity(bool key) => GetAmountBuild(BuildingTypes.City, key) > 0;

        internal static void SetXyBuildings(BuildingTypes buildingType, bool key, List<int[]> list)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    EGGM.CityInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list.Copy();
                    break;

                case BuildingTypes.Farm:
                    EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list.Copy();
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list.Copy();
                    break;

                case BuildingTypes.Mine:
                    EGGM.MinesInfoEnt_AmountBuildingsInGameCom.BuildingsInGameDict[key] = list.Copy();
                    break;

                default:
                    throw new Exception();
            }
        }


        internal static int[] GetXyBuildByIndex(BuildingTypes buildingType, bool key, int index) => (int[])GetListXyBuild(buildingType, key)[index].Clone();
        internal static int GetAmountBuild(BuildingTypes buildingType, bool key) => GetListXyBuild(buildingType, key).Count;
        internal static int GetAmountAllBuild(bool key)
        {
            var amountAllBuildInGame = 0;

            for (int curNumberBuilType = 1; curNumberBuilType < Enum.GetNames(typeof(BuildingTypes)).Length; curNumberBuilType++)
            {
                amountAllBuildInGame += GetAmountBuild((BuildingTypes)curNumberBuilType, key);
            }

            return amountAllBuildInGame;
        }
        internal static int GetAmountAllBuild() => GetAmountAllBuild(true) + GetAmountAllBuild(false);

        internal static void AddXyBuild(BuildingTypes buildingType, bool key, int[] xyAdding) => GetListXyBuild(buildingType, key).Add(xyAdding);
        internal static void RemoveXyBuild(BuildingTypes buildingType, bool key, int[] xyTaking)
        {
            if (!GetListXyBuild(buildingType, key).TryFindCellInListAndRemove(xyTaking)) throw new Exception();
        }
        internal static void RemoveXyBuild(BuildingTypes buildingType, bool key, int index) => GetListXyBuild(buildingType, key).RemoveAt(index);



        #endregion
    }
}
