//using ECS;
//using System;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public sealed class CellUnitEffectsEs : EntityAbstract
//    {

//        public static ref C HaveEffect<C>(in UnitStatTypes stat, in byte idx) where C : struct, IUnitStatCellE

//        public static HashSet<UnitStatTypes> Keys
//        {
//            get
//            {
//                var hash = new HashSet<UnitStatTypes>();
//                foreach (var item in _ents) hash.Add(item.Key);
//                return hash;
//            }
//        }

//        public CellUnitEffectsEs(in EcsWorld gameW)
//        {
//            _ents = new Dictionary<UnitStatTypes, Entity[]>();
//            for (var unitStat = UnitStatTypes.First; unitStat < UnitStatTypes.End; unitStat++)
//            {
//                _ents.Add(unitStat, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);

//                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
//                {
//                    _ents[unitStat][idx] = gameW.NewEntity()
//                        .Add(new HaveEffectC());
//                }
//            }
//        }
//    }

//    public interface IUnitStatCellE { }
//}