using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SetUnitCellsC
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


        static SetUnitCellsC()
        {
            _cells = new Dictionary<string, Entity>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _cells.Add(Key(player, idx), default);
                }
            }
        }
        public SetUnitCellsC(in EcsWorld gameW)
        {
            foreach (var key in Keys)
            {
                _cells[key] = gameW.NewEntity()
                    .Add(new CanSetUnitC());
            }
        }
    }
}
