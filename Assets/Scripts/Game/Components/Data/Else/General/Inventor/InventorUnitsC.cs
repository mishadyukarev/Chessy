using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct InventorUnitsC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>> _unitsInventorDict;

        public InventorUnitsC(bool needNew) : this()
        {
            if (needNew)
            {
                _unitsInventorDict = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>>();

                _unitsInventorDict.Add(PlayerTypes.First, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>());
                _unitsInventorDict.Add(PlayerTypes.Second, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>());


                for (UnitTypes unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    _unitsInventorDict[PlayerTypes.First].Add(unitType, new Dictionary<LevelUnitTypes, int>());
                    _unitsInventorDict[PlayerTypes.Second].Add(unitType, new Dictionary<LevelUnitTypes, int>());


                    _unitsInventorDict[PlayerTypes.First][unitType].Add(LevelUnitTypes.Wood, default);
                    _unitsInventorDict[PlayerTypes.First][unitType].Add(LevelUnitTypes.Iron, default);

                    _unitsInventorDict[PlayerTypes.Second][unitType].Add(LevelUnitTypes.Wood, default);
                    _unitsInventorDict[PlayerTypes.Second][unitType].Add(LevelUnitTypes.Iron, default);
                }
            }
        }

        public static int AmountUnitsInInv(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType) => _unitsInventorDict[playerType][unitType][levelUnitType];
        public static int AmountUnitsInInv(PlayerTypes playerType, UnitTypes unitType) => _unitsInventorDict[playerType][unitType][LevelUnitTypes.Wood] + _unitsInventorDict[playerType][unitType][LevelUnitTypes.Iron];
        public static void Set(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType, int value) => _unitsInventorDict[playerType][unitType][levelUnitType] = value;
        public static void SetAmountUnitsInInvAll(UnitTypes unitType, LevelUnitTypes levelUnitType, int value)
        {
            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                _unitsInventorDict[playerType][unitType][levelUnitType] = value;
            }
        }

        public static void AddUnit(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType, int adding = 1) => Set(playerType, unitType, levelUnitType, AmountUnitsInInv(playerType, unitType, levelUnitType) + adding);
        public static void TakeUnitsInInv(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType, int taking = 1) => Set(playerType, unitType, levelUnitType, AmountUnitsInInv(playerType, unitType, levelUnitType) - taking);

        public static bool HaveUnitInInv(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType) => AmountUnitsInInv(playerType, unitType, levelUnitType) > 0;
    }
}
