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
                    return EGGM.FarmsInfoEnt_AmountUpgradesCom.AmountUpgrades(key);

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountUpgradesCom.AmountUpgrades(key);

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountUpgradesCom.AmountUpgrades(key);

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
                    return EGGM.FarmsInfoEnt_AmountUpgradesCom.SetAmountUpgrades(key, value);

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountUpgradesCom.SetAmountUpgrades(key, value);

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountUpgradesCom.SetAmountUpgrades(key, value);

                default:
                    throw new Exception();
            }
        }
        internal static void AddAmountUpgrades(BuildingTypes buildingType, bool key, int adding = 1)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    EGGM.FarmsInfoEnt_AmountUpgradesCom.AddAmountUpgrades(key, adding);
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.WoodcuttersInfoEnt_AmountUpgradesCom.AddAmountUpgrades(key, adding);
                    break;

                case BuildingTypes.Mine:
                    EGGM.MinesInfoEnt_AmountUpgradesCom.AddAmountUpgrades(key, adding);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void TakeAmountUpgrades(BuildingTypes buildingType, bool key, int taking = 1)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    EGGM.FarmsInfoEnt_AmountUpgradesCom.TakeAmountUpgrades(key, taking);
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.WoodcuttersInfoEnt_AmountUpgradesCom.TakeAmountUpgrades(key, taking);
                    break;

                case BuildingTypes.Mine:
                    EGGM.MinesInfoEnt_AmountUpgradesCom.TakeAmountUpgrades(key, taking);
                    break;

                default:
                    throw new Exception();
            }
        }

        #endregion


        #region Buidings

        internal static bool IsSettedCity(bool key) => EGGM.CityInfoEnt_AmountBuildingsInGameCom.AmountBuildingsInGame(key) > 0;


        internal static int AmountBuildingsInGame(BuildingTypes buildingType, bool key)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return EGGM.CityInfoEnt_AmountBuildingsInGameCom.AmountBuildingsInGame(key);

                case BuildingTypes.Farm:
                    return EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.AmountBuildingsInGame(key);

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.AmountBuildingsInGame(key);

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountBuildingsInGameCom.AmountBuildingsInGame(key);

                default:
                    throw new Exception();
            }
        }
        internal static List<int[]> GetListXyBuildingsInGame(BuildingTypes buildingType, bool key)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return EGGM.CityInfoEnt_AmountBuildingsInGameCom.GetXyBuildingsInGame(key);

                case BuildingTypes.Farm:
                    return EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.GetXyBuildingsInGame(key);

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.GetXyBuildingsInGame(key);

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountBuildingsInGameCom.GetXyBuildingsInGame(key);

                default:
                    throw new Exception();
            }
        }
        internal static void AddAmountBuildingsInGame(BuildingTypes buildingType, bool key, int[] xyAdding)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    EGGM.CityInfoEnt_AmountBuildingsInGameCom.AddAmountBuildingsInGame(key, xyAdding);
                    break;

                case BuildingTypes.Farm:
                    EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.AddAmountBuildingsInGame(key, xyAdding);
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.AddAmountBuildingsInGame(key, xyAdding);
                    break;

                case BuildingTypes.Mine:
                    EGGM.MinesInfoEnt_AmountBuildingsInGameCom.AddAmountBuildingsInGame(key, xyAdding);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void TakeAmountBuildingsInGame(BuildingTypes buildingType, bool key, int[] xyTaking)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    EGGM.CityInfoEnt_AmountBuildingsInGameCom.TakeAmountBuildingsInGame(key, xyTaking);
                    break;

                case BuildingTypes.Farm:
                    EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.TakeAmountBuildingsInGame(key, xyTaking);
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.TakeAmountBuildingsInGame(key, xyTaking);
                    break;

                case BuildingTypes.Mine:
                    EGGM.MinesInfoEnt_AmountBuildingsInGameCom.TakeAmountBuildingsInGame(key, xyTaking);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SyncBuildingsInGame(BuildingTypes buildingType, bool key, List<int[]> buildingsInGame)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    EGGM.CityInfoEnt_AmountBuildingsInGameCom.SyncBuildingsInGame(key, buildingsInGame);
                    break;

                case BuildingTypes.Farm:
                    EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.SyncBuildingsInGame(key, buildingsInGame);
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.SyncBuildingsInGame(key, buildingsInGame);
                    break;

                case BuildingTypes.Mine:
                    EGGM.MinesInfoEnt_AmountBuildingsInGameCom.SyncBuildingsInGame(key, buildingsInGame);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static int[] GetXyAnyBuildingInGame(BuildingTypes buildingType, bool key)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return EGGM.CityInfoEnt_AmountBuildingsInGameCom.GetXyAnyFirstBuildingInGame(key);

                case BuildingTypes.Farm:
                    return EGGM.FarmsInfoEnt_AmountBuildingsInGameCom.GetXyAnyFirstBuildingInGame(key);

                case BuildingTypes.Woodcutter:
                    return EGGM.WoodcuttersInfoEnt_AmountBuildingsInGameCom.GetXyAnyFirstBuildingInGame(key);

                case BuildingTypes.Mine:
                    return EGGM.MinesInfoEnt_AmountBuildingsInGameCom.GetXyAnyFirstBuildingInGame(key);

                default:
                    throw new Exception();
            }
        }

        #endregion
    }
}
