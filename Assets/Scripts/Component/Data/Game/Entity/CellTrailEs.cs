using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellTrailEs
    {
        readonly Dictionary<DirectTypes, CellTrailE[]> _trails;
        readonly Dictionary<PlayerTypes, CellTrailPlayerE[]> _players;

        public CellTrailE Trail(in DirectTypes dir, in byte idx) => _trails[dir][idx];
        public CellTrailPlayerE IsVisible(in PlayerTypes player, in byte idx) => _players[player][idx];

        public CellTrailE[] Trails(in byte idx)
        {
            var trails = new CellTrailE[_trails.Keys.Count];
            var i = 0;
            foreach (var trailT in _trails.Keys) trails[i++] = _trails[trailT][idx];
            return trails;
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
        public bool HaveAnyTrail(in byte idx)
        {
            foreach (var item in Keys) if (Trail(item, idx).HaveTrail) return true;
            return false;
        }


        public CellTrailEs(in EcsWorld gameW)
        {
            _trails = new Dictionary<DirectTypes, CellTrailE[]>();
            _players = new Dictionary<PlayerTypes, CellTrailPlayerE[]>();

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new CellTrailE[CellStartValues.ALL_CELLS_AMOUNT]);
            }

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _players.Add(player, new CellTrailPlayerE[CellStartValues.ALL_CELLS_AMOUNT]);
            }


            byte idx = 0;

            for (idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                foreach (var item in _trails) _trails[item.Key][idx] = new CellTrailE(item.Key, gameW);
                foreach (var item in _players) _players[item.Key][idx] = new CellTrailPlayerE(gameW);
            }
        }

        public void ResetAll(in byte idx)
        {
            foreach (var item in Keys)
            {
                Trail(item, idx).Health.Amount = 0;
            }
        }
    }
}