using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellsForAttackUnitsEs
    {
        static Dictionary<string, Entity> _cells;


        static string Key(in byte idx, in AttackTypes attack) => idx.ToString() + attack;

        public static ref C CanAttack<C>(in byte idx, in AttackTypes attack) where C : struct => ref _cells[Key(idx, attack)].Get<C>();
        public static ref C CanAttack<C>(in string key) where C : struct => ref _cells[key].Get<C>();

        public static HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _cells) keys.Add(item.Key);
                return keys;
            }
        }

        public CellsForAttackUnitsEs(in EcsWorld gameW)
        {
            _cells = new Dictionary<string, Entity>();

            for (var attack = AttackTypes.Start + 1; attack < AttackTypes.End; attack++)
            {
                for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _cells.Add(Key(idx, attack), gameW.NewEntity()
                       .Add(new IdxsC(new List<byte>())));
                }
            }
        }

        public static bool CanAttack(in byte idx_from, in byte idx_to, out AttackTypes attack)
        {
            for (attack = AttackTypes.Start + 1; attack < AttackTypes.End; attack++)
            {
                if (CanAttack<IdxsC>(idx_from, attack).Contains(idx_to)) return true;
            }
            return false;
        }
    }
}