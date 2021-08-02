using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers
{
    internal struct InfoBuidlingsDataContainer
    {
        private static EcsEntity _cityInfoEnt;
        private static EcsEntity _farmsInfoEnt;
        private static EcsEntity _woodcuttersInfoEnt;
        private static EcsEntity _minesInfoEnt;

        internal InfoBuidlingsDataContainer(EcsWorld gameWorld)
        {
            _cityInfoEnt = gameWorld.NewEntity()
                .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()));

            _farmsInfoEnt = gameWorld.NewEntity()
                .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
                .Replace(new AmountUpgradesDictComponent(new Dictionary<bool, int>()));

            _woodcuttersInfoEnt = gameWorld.NewEntity()
                .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
                .Replace(new AmountUpgradesDictComponent(new Dictionary<bool, int>()));

            _minesInfoEnt = gameWorld.NewEntity()
                .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
                .Replace(new AmountUpgradesDictComponent(new Dictionary<bool, int>()));
        }


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
                    return _farmsInfoEnt.Get<AmountUpgradesDictComponent>().AmountUpgradesDict[key];

                case BuildingTypes.Woodcutter:
                    return _woodcuttersInfoEnt.Get<AmountUpgradesDictComponent>().AmountUpgradesDict[key];

                case BuildingTypes.Mine:
                    return _minesInfoEnt.Get<AmountUpgradesDictComponent>().AmountUpgradesDict[key];

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
                    return _farmsInfoEnt.Get<AmountUpgradesDictComponent>().AmountUpgradesDict[key] = value;

                case BuildingTypes.Woodcutter:
                    return _woodcuttersInfoEnt.Get<AmountUpgradesDictComponent>().AmountUpgradesDict[key] = value;

                case BuildingTypes.Mine:
                    return _minesInfoEnt.Get<AmountUpgradesDictComponent>().AmountUpgradesDict[key] = value;

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
                    return _cityInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key];

                case BuildingTypes.Farm:
                    return _farmsInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key];

                case BuildingTypes.Woodcutter:
                    return _woodcuttersInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key];

                case BuildingTypes.Mine:
                    return _minesInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key];

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
                    _cityInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key] = list.Copy();
                    break;

                case BuildingTypes.Farm:
                    _farmsInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key] = list.Copy();
                    break;

                case BuildingTypes.Woodcutter:
                    _woodcuttersInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key] = list.Copy();
                    break;

                case BuildingTypes.Mine:
                    _minesInfoEnt.Get<BuildingsInGameDictComponent>().BuildingsInGameDict[key] = list.Copy();
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
