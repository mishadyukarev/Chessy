using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellTrailEs
    {
        readonly Dictionary<DirectTypes, CellTrailE> _trails;
        readonly Dictionary<PlayerTypes, CellTrailPlayerE> _players;

        public CellTrailE Trail(in DirectTypes dir) => _trails[dir];
        public CellTrailPlayerE IsVisible(in PlayerTypes player) => _players[player];

        public CellTrailE[] Trails
        {
            get
            {
                var trails = new CellTrailE[_trails.Keys.Count];
                var i = 0;
                foreach (var trailT in _trails.Keys) trails[i++] = _trails[trailT];
                return trails;

            }
        }

        public HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _trails) keys.Add(item.Key);
                return keys;
            }
        }
        public bool HaveAnyTrail
        {
            get
            {
                foreach (var item in Keys) if (Trail(item).HealthC.IsAlive) return true;
                return false;
            }
        }


        public CellTrailEs(in EcsWorld gameW)
        {
            _trails = new Dictionary<DirectTypes, CellTrailE>();
            _players = new Dictionary<PlayerTypes, CellTrailPlayerE>();

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new CellTrailE(dir, gameW));
            }
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _players.Add(player, new CellTrailPlayerE(gameW));
            }
        }

        public void DestroyAll()
        {
            foreach (var item in Keys) Trail(item).HealthC.Destroy();
        }
    }
}