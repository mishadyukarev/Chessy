using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitStepsInConditionEs
    {
        static Dictionary<ConditionUnitTypes, Entity[]> _ents;

        public static ref C Steps<C>(in ConditionUnitTypes cond, in byte idx) where C : struct, ICellUnitConditionE
        {
            if (!_ents.ContainsKey(cond)) throw new Exception();
            return ref _ents[cond][idx].Get<C>();
        }

        public static HashSet<ConditionUnitTypes> KeysCondition
        {
            get
            {
                var hash = new HashSet<ConditionUnitTypes>();
                foreach (var item in _ents) hash.Add(item.Key);
                return hash;
            }
        }

        public CellUnitStepsInConditionEs(in EcsWorld gameW)
        {
            _ents = new Dictionary<ConditionUnitTypes, Entity[]>();
            for (var cond = ConditionUnitTypes.Start; cond < ConditionUnitTypes.End; cond++)
            {
                _ents.Add(cond, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);
                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _ents[cond][idx] = gameW.NewEntity()
                        .Add(new AmountC());
                }
            }
        }
    }
    public interface ICellUnitConditionE { }
}