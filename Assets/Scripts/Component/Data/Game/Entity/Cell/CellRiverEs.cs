using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellRiverEs
    {
        readonly Dictionary<DirectTypes, CellRiverDirectE> _directs;
        public CellRiverDirectE HaveRive(in DirectTypes dir) => _directs[dir];

        public readonly CellRiverE River;
        
        public HashSet<DirectTypes> Keys
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
            River = new CellRiverE(gameW);

            _directs = new Dictionary<DirectTypes, CellRiverDirectE>();
            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                _directs[dir] = new CellRiverDirectE(gameW);
            }
        }

        public void SetStart(params DirectTypes[] dirs)
        {
            if (dirs == default) throw new Exception();

            River.RiverTC.River = RiverTypes.Start;
            foreach (var item in dirs) HaveRive(item).HaveRiver.Have = true;
        }
    }
}