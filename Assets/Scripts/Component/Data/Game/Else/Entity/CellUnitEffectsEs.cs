using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitEffectsEs
    {
        static Dictionary<UnitStatTypes, Entity[]> _ents;


        public static ref C HaveEffect<C>(in UnitStatTypes stat, in byte idx) where C : struct, IUnitStatCellE
        {
            if (!_ents.ContainsKey(stat)) throw new Exception();
            return ref _ents[stat][idx].Get<C>();
        }

        public static HashSet<UnitStatTypes> KeysStat
        {
            get
            {
                var hash = new HashSet<UnitStatTypes>();
                foreach (var item in _ents) hash.Add(item.Key);
                return hash;
            }
        }

        public CellUnitEffectsEs(in EcsWorld gameW)
        {
            _ents = new Dictionary<UnitStatTypes, Entity[]>();
            for (var unitStat = UnitStatTypes.First; unitStat < UnitStatTypes.End; unitStat++)
            {
                _ents.Add(unitStat, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _ents[unitStat][idx] = gameW.NewEntity()
                        .Add(new HaveEffectC());
                }
            }
        }
    }

    public interface IUnitStatCellE { }
}