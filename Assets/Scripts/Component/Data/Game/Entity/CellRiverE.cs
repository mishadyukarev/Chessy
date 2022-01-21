using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellRiverE
    {
        static Entity[] _rivers;
        static Dictionary<DirectTypes, Entity[]> _directs;

        public static ref RiverTC River(in byte idx) => ref _rivers[idx].Get<RiverTC>();
        public static ref HaveC HaveRive(in DirectTypes dir, in byte idx) => ref _directs[dir][idx].Get<HaveC>();

        public static HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _directs) keys.Add(item.Key);
                return keys;
            }
        }

        public CellRiverE(in EcsWorld gameW)
        {  
            _directs = new Dictionary<DirectTypes, Entity[]>();
            
            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                _directs.Add(dir, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);
                for (byte idx = 0; idx < _directs[dir].Length; idx++)
                {
                    _directs[dir][idx] = gameW.NewEntity()
                        .Add(new HaveC());
                }
            }

            _rivers = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            for (byte idx = 0; idx < _rivers.Length; idx++)
            {
                _rivers[idx] = gameW.NewEntity()
                    .Add(new RiverTC());
            }
        }

        public static void SetStart(in byte idx, params DirectTypes[] dirs)
        {
            if (dirs == default) throw new Exception();

            River(idx).River = RiverTypes.Start;
            foreach (var item in dirs) HaveRive(item, idx).Have = true;
        }
    }
}