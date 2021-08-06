using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct InventorUnitsComponent
    {
        private Dictionary<UnitTypes, Dictionary<bool, int>> _unitsInventorDict;

        internal InventorUnitsComponent(Dictionary<UnitTypes, Dictionary<bool, int>> dict)
        {
            _unitsInventorDict = dict;


            for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                var dict1 = new Dictionary<bool, int>();

                dict1.Add(true, default);
                dict1.Add(false, default);

                _unitsInventorDict.Add(unitType, dict1);
            }
        }

        internal int AmountUnitsInInventor(UnitTypes unitType, bool key) => _unitsInventorDict[unitType][key];
        internal void SetAmountUnitsInInventor(UnitTypes unitType, bool key, int value) => _unitsInventorDict[unitType][key] = value;

        internal void AddUnitsInInventor(UnitTypes unitType, bool key, int adding = 1) => SetAmountUnitsInInventor(unitType, key, AmountUnitsInInventor(unitType, key) + adding);
        internal void TakeUnitsInInventor(UnitTypes unitType, bool key, int taking = 1) => SetAmountUnitsInInventor(unitType, key, AmountUnitsInInventor(unitType, key) - taking);

        internal bool HaveUnitInInventor(UnitTypes unitType, bool key) => AmountUnitsInInventor(unitType, key) > 0;
    }
}
