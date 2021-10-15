using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct InventorUnitsComponent
    {
        private Dictionary<PlayerTypes, Dictionary<UnitTypes, int>> _unitsInventorDict;

        internal InventorUnitsComponent(bool needNew) : this()
        {
            if (needNew)
            {
                _unitsInventorDict = new Dictionary<PlayerTypes, Dictionary<UnitTypes, int>>();

                _unitsInventorDict.Add(PlayerTypes.First, new Dictionary<UnitTypes, int>());
                _unitsInventorDict.Add(PlayerTypes.Second, new Dictionary<UnitTypes, int>());


                for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    var dict1 = new Dictionary<PlayerTypes, int>();

                    _unitsInventorDict[PlayerTypes.First].Add(unitType, default);
                    _unitsInventorDict[PlayerTypes.Second].Add(unitType, default);
                }
            }
        }

        internal int AmountUnitsInInv(PlayerTypes playerType, UnitTypes unitType) => _unitsInventorDict[playerType][unitType];
        internal void SetAmountUnitsInInvent(PlayerTypes playerType, UnitTypes unitType, int value) => _unitsInventorDict[playerType][unitType] = value;
        internal void SetAmountUnitsInInvAll(UnitTypes unitType, int value)
        {
            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                _unitsInventorDict[playerType][unitType] = value;
            }
        }

        internal void AddUnitsInInventor(PlayerTypes playerType, UnitTypes unitType, int adding = 1) => SetAmountUnitsInInvent(playerType, unitType, AmountUnitsInInv(playerType, unitType) + adding);
        internal void TakeUnitsInInv(PlayerTypes playerType, UnitTypes unitType, int taking = 1) => SetAmountUnitsInInvent(playerType, unitType, AmountUnitsInInv(playerType, unitType) - taking);

        internal bool HaveUnitInInv(PlayerTypes playerType, UnitTypes unitType) => AmountUnitsInInv(playerType, unitType) > 0;
    }
}
