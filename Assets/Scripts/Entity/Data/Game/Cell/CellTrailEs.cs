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

        public HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _trails) keys.Add(item.Key);
                return keys;
            }
        }


        public CellTrailEs(in EcsWorld gameW)
        {
            _trails = new Dictionary<DirectTypes, CellTrailE>();
            _players = new Dictionary<PlayerTypes, CellTrailPlayerE>();

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new CellTrailE());
            }
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _players.Add(player, new CellTrailPlayerE());
            }
        }

        public void DestroyAll()
        {
            foreach (var item in Keys) Trail(item).HealthC.Destroy();
        }
    }
}