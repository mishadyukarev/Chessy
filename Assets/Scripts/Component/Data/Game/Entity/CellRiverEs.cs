using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellRiverEs
    {
        static CellRiverE[] _rivers;
        static Dictionary<DirectTypes, CellRiverDirectE[]> _directs;

        public static CellRiverE River(in byte idx) => _rivers[idx];
        public static CellRiverDirectE HaveRive(in DirectTypes dir, in byte idx) => _directs[dir][idx];

        public static HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _directs) keys.Add(item.Key);
                return keys;
            }
        }

        public CellRiverEs(in EcsWorld gameW)
        {
            _directs = new Dictionary<DirectTypes, CellRiverDirectE[]>();

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                _directs.Add(dir, new CellRiverDirectE[CellStartValues.ALL_CELLS_AMOUNT]);
                for (byte idx = 0; idx < _directs[dir].Length; idx++)
                {
                    _directs[dir][idx] = new CellRiverDirectE(gameW);
                }
            }

            _rivers = new CellRiverE[CellStartValues.ALL_CELLS_AMOUNT];
            for (byte idx = 0; idx < _rivers.Length; idx++)
            {
                _rivers[idx] = new CellRiverE(gameW);
            }
        }

        public static void SetStart(in byte idx, params DirectTypes[] dirs)
        {
            if (dirs == default) throw new Exception();

            River(idx).RiverTC.River = RiverTypes.Start;
            foreach (var item in dirs) HaveRive(item, idx).HaveRiver.Have = true;
        }
    }
}