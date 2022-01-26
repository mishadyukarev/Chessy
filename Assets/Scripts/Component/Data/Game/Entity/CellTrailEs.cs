using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellTrailEs
    {
        static Dictionary<DirectTypes, CellTrailE[]> _trails;
        static Dictionary<PlayerTypes, CellTrailPlayerE[]> _players;

        public static CellTrailE Trail(in DirectTypes dir, in byte idx) => _trails[dir][idx];
        public static CellTrailPlayerE IsVisible(in PlayerTypes player, in byte idx) => _players[player][idx];

        public static HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _trails) keys.Add(item.Key);
                return keys;
            }
        }
        public static bool HaveAnyTrail(in byte idx)
        {
            foreach (var item in Keys) if (Trail(item, idx).Health.Have) return true;
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
                foreach (var item in _trails) _trails[item.Key][idx] = new CellTrailE(gameW);
                foreach (var item in _players) _players[item.Key][idx] = new CellTrailPlayerE(gameW);
            }
        }

        public static bool TrySetNewTrail(in byte idx, in DirectTypes dir, in bool haveAdultForest)
        {
            if (haveAdultForest) Trail(dir, idx).Health.Amount = 7;
            return haveAdultForest;
        }
        public static void SetAllTrail(in byte idx)
        {
            foreach (var item in Keys)
            {
                Trail(item, idx).Health.Amount = 7;
            }
        }
        public static void TakeHealth(in byte idx, in DirectTypes dir)
        {
            Trail(dir, idx).Health.Amount -= 1;
        }
        public static void ResetAll(in byte idx)
        {
            foreach (var item in Keys)
            {
                Trail(item, idx).Health.Amount = 0;
            }
        }
    }
}