using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellsForSetUnitsEs
    {
        static Dictionary<string, Entity> _cells;


        static string Key(in PlayerTypes player, in byte idx) => player.ToString() + idx;

        public static ref C CanSet<C>(in PlayerTypes player, in byte idx) where C : struct => ref _cells[Key(player, idx)].Get<C>();
        public static ref C CanSet<C>(in string key) where C : struct => ref _cells[key].Get<C>();

        public static HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _cells) keys.Add(item.Key);
                return keys;
            }
        }

        public CellsForSetUnitsEs(in EcsWorld gameW)
        {
            _cells = new Dictionary<string, Entity>();

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                for (byte idx = 0; idx < StartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _cells.Add(Key(player, idx), gameW.NewEntity()
                       .Add(new CanSetUnitC()));
                }
            }
        }
    }
}
