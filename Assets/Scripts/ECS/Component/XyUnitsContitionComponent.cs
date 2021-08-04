using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct XyUnitsContitionComponent
    {
        private Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>> _xyUnitsContitionDict;

        internal XyUnitsContitionComponent(Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>> dict)
        {
            _xyUnitsContitionDict = dict;

            for (ConditionUnitTypes conditionType = 0; conditionType < (ConditionUnitTypes)Enum.GetNames(typeof(ConditionUnitTypes)).Length; conditionType++)
            {
                var dict1 = new Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>();


                for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    var dict2 = new Dictionary<bool, List<int[]>>();

                    dict2.Add(true, new List<int[]>());
                    dict2.Add(false, new List<int[]>());

                    dict1.Add(unitType, dict2);
                }

                _xyUnitsContitionDict.Add(conditionType, dict1);
            }
        }


        private List<int[]> GetUnitsInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key) => _xyUnitsContitionDict[protectRelaxType][unitType][key];

        internal int[] GetXyInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index) => (int[])GetUnitsInCondition(protectRelaxType, unitType, key)[index].Clone();
        internal int GetAmountUnitsInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key) => GetUnitsInCondition(protectRelaxType, unitType, key).Count;
        internal bool TryFindUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy) => GetUnitsInCondition(protectRelaxType, unitType, key).TryFindCell(xy);
        internal void AddUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xyAdding) => GetUnitsInCondition(protectRelaxType, unitType, key).Add(xyAdding);
        internal void RemoveUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xyTaking) { if (!GetUnitsInCondition(protectRelaxType, unitType, key).TryFindCellInListAndRemove(xyTaking)) throw new Exception(); }
        internal void ReplaceCondition(ConditionUnitTypes preConditionType, ConditionUnitTypes newConditionType, UnitTypes unitType, bool isMasterKey, int[] xy)
        {
            RemoveUnitInCondition(preConditionType, unitType, isMasterKey, xy);
            AddUnitInCondition(newConditionType, unitType, isMasterKey, xy);
        }

        internal void RemoveUnitInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index) => GetUnitsInCondition(protectRelaxType, unitType, key).RemoveAt(index);

    }
}
