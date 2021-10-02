//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.Workers;
//using System;
//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.Component
//{
//    internal struct UnitsInConditionInGameCom
//    {
//        private Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<byte>>>> _idxUnitsInConditionDict;

//        internal UnitsInConditionInGameCom(Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<byte>>>> dict)
//        {
//            _idxUnitsInConditionDict = dict;

//            for (ConditionUnitTypes conditionType = 0; conditionType < (ConditionUnitTypes)Enum.GetNames(typeof(ConditionUnitTypes)).Length; conditionType++)
//            {
//                var dict1 = new Dictionary<UnitTypes, Dictionary<bool, List<byte>>>();


//                for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
//                {
//                    var dict2 = new Dictionary<bool, List<byte>>();

//                    dict2.Add(true, new List<byte>());
//                    dict2.Add(false, new List<byte>());

//                    dict1.Add(unitType, dict2);
//                }

//                _idxUnitsInConditionDict.Add(conditionType, dict1);
//            }
//        }


//        private List<byte> GetUnitsInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key) => _idxUnitsInConditionDict[protectRelaxType][unitType][key];

//        internal byte GetIdxInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index) => (byte)GetUnitsInCondition(protectRelaxType, unitType, key)[index];
//        internal int GetAmountUnitsInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key) => GetUnitsInCondition(protectRelaxType, unitType, key).Count;
//        internal bool TryFindUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, byte idxCell) => GetUnitsInCondition(protectRelaxType, unitType, key).Contains(idxCell);
//        internal void AddUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, byte idxCellForAdding) => GetUnitsInCondition(protectRelaxType, unitType, key).Add(idxCellForAdding);
//        internal void RemoveUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, byte idxCellForTaking) { if (!GetUnitsInCondition(protectRelaxType, unitType, key).Remove(idxCellForTaking)) throw new Exception(); }
//        internal void ReplaceCondition(ConditionUnitTypes preConditionType, ConditionUnitTypes newConditionType, UnitTypes unitType, bool isMasterKey, byte idxCell)
//        {
//            RemoveUnitInCondition(preConditionType, unitType, isMasterKey, idxCell);
//            AddUnitInCondition(newConditionType, unitType, isMasterKey, idxCell);
//        }

//        internal void RemoveUnitInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index) => GetUnitsInCondition(protectRelaxType, unitType, key).RemoveAt(index);

//    }
//}
