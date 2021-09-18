﻿using System;
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

        internal int AmountUnitsInInv(UnitTypes unitType, bool key) => _unitsInventorDict[unitType][key];
        internal void SetAmountUnitsInInvent(UnitTypes unitType, bool key, int value) => _unitsInventorDict[unitType][key] = value;

        internal void AddUnitsInInventor(UnitTypes unitType, bool key, int adding = 1) => SetAmountUnitsInInvent(unitType, key, AmountUnitsInInv(unitType, key) + adding);
        internal void TakeUnitsInInv(UnitTypes unitType, bool key, int taking = 1) => SetAmountUnitsInInvent(unitType, key, AmountUnitsInInv(unitType, key) - taking);

        internal bool HaveUnitInInv(UnitTypes unitType, bool key) => AmountUnitsInInv(unitType, key) > 0;
    }
}
