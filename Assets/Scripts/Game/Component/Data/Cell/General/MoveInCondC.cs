using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct MoveInCondC
    {
        private Dictionary<CondUnitTypes, int> _moveInCond;

        public Dictionary<CondUnitTypes, int> MovesInCond
        {
            get
            {
                var dict = new Dictionary<CondUnitTypes, int>();
                foreach (var item in _moveInCond)
                {
                    dict.Add(item.Key, item.Value);
                }
                return dict;
            }
        }
        public bool HaveForBuldCamp => _moveInCond[CondUnitTypes.Protected] >= 4;
        public int AmountMoves(CondUnitTypes cond) => _moveInCond[cond];
        public bool HaveMoves(CondUnitTypes cond) => _moveInCond[cond] > 0;


        public MoveInCondC(bool needNew)
        {
            if (needNew)
            {
                _moveInCond = new Dictionary<CondUnitTypes, int>();
                for (var cond = (CondUnitTypes)0; cond < (CondUnitTypes)typeof(CondUnitTypes).GetEnumNames().Length; cond++)
                {
                    _moveInCond.Add(cond, 0);
                }
            }
            else throw new Exception();
        }


        public void AddMove(CondUnitTypes cond) => _moveInCond[cond] += 1;
        public void ResetAll()
        {
            foreach (var item in MovesInCond) 
                _moveInCond[item.Key] = 0;
        }
    }
}