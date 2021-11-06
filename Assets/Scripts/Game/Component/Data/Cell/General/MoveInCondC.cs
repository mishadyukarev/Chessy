using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct MoveInCondC
    {
        private Dictionary<CondUnitTypes, int> _moveInCond;

        public bool HaveForBuldCamp => _moveInCond[CondUnitTypes.Protected] >= 4;

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

        public int MoveInCond(CondUnitTypes cond) => _moveInCond[cond];
        public void AddMoveCond(CondUnitTypes cond) => _moveInCond[cond] += 1;
    }
}