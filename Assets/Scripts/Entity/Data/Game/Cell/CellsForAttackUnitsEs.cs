using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellsForAttackUnitsEs
    {
        static Dictionary<string, Entity> _cells;


        static string Key(in byte idx, in AttackTypes attack, in PlayerTypes player) => idx.ToString() + attack + player;

        public static ref C CanAttack<C>(in byte idx, in AttackTypes attack, in PlayerTypes player) where C : struct => ref _cells[Key(idx, attack, player)].Get<C>();
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
            for (byte idx = 0; idx < StartValues.ALL_CELLS_AMOUNT; idx++)
            {
                for (var attack = AttackTypes.Start + 1; attack < AttackTypes.End; attack++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _cells.Add(Key(idx, attack, player), gameW.NewEntity()
                       .Add(new IdxsC(new List<byte>())));
                    }
                }
            }
        }

        public static bool CanAttack(in byte idx_from, in byte idx_to, in PlayerTypes player, out AttackTypes attack)
        {
            for (attack = AttackTypes.Start + 1; attack < AttackTypes.End; attack++)
            {
                if (CanAttack<IdxsC>(idx_from, attack, player).Contains(idx_to)) return true;
            }
            return false;
        }
    }
}