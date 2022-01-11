//using System;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public struct MoveInCondC : IUnitConditionCellE
//    {
//        private Dictionary<ConditionUnitTypes, int> _moveInCond;

//        public Dictionary<ConditionUnitTypes, int> MovesInCond
//        {
//            get
//            {
//                var dict = new Dictionary<ConditionUnitTypes, int>();
//                foreach (var item in _moveInCond)
//                {
//                    dict.Add(item.Key, item.Value);
//                }
//                return dict;
//            }
//        }
//        public bool HaveForBuldCamp => _moveInCond[ConditionUnitTypes.Protected] >= 4;
//        public int AmountMoves(ConditionUnitTypes cond) => _moveInCond[cond];
//        public bool HaveMoves(ConditionUnitTypes cond) => _moveInCond[cond] > 0;


//        public MoveInCondC(bool needNew)
//        {
//            if (needNew)
//            {
//                _moveInCond = new Dictionary<ConditionUnitTypes, int>();
//                for (var cond = (ConditionUnitTypes)0; cond < (ConditionUnitTypes)typeof(ConditionUnitTypes).GetEnumNames().Length; cond++)
//                {
//                    _moveInCond.Add(cond, 0);
//                }
//            }
//            else throw new Exception();
//        }


//        public void AddMove(ConditionUnitTypes cond) => _moveInCond[cond] += 1;
//        public void ResetAll()
//        {
//            foreach (var item in MovesInCond)
//                _moveInCond[item.Key] = 0;
//        }
//    }
//}