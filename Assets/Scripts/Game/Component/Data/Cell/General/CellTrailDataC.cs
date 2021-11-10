using System.Collections.Generic;

namespace Chessy.Game
{
    public struct CellTrailDataC
    {
        private readonly Dictionary<DirectTypes, int> _health;

        public Dictionary<DirectTypes, int> Health
        {
            get
            {
                var dict_0 = new Dictionary<DirectTypes, int>();

                foreach (var item_0 in _health)
                {
                    dict_0.Add(item_0.Key, item_0.Value);
                }

                return dict_0;
            }
        }
        public Dictionary<DirectTypes, bool> DictTrail
        {
            get
            {
                var dict = new Dictionary<DirectTypes, bool>();
                foreach (var item in _health) dict[item.Key] = _health[item.Key] > 0;
                return dict;
            }
        }
        public bool HaveAnyTrail
        {
            get
            {
                foreach (var item in DictTrail) if (item.Value) return true;
                return false;
            }
        }


        public CellTrailDataC(Dictionary<DirectTypes, int> directs)
        {
            _health = directs;

            for (var dir = (DirectTypes)1; dir < (DirectTypes)typeof(DirectTypes).GetEnumNames().Length; dir++)
            {
                _health.Add(dir, 0);
            }
        }

        public bool TrySetNewTrail(DirectTypes dir, CellEnvDataC envC)
        {
            if (envC.Have(EnvTypes.AdultForest)) _health[dir] = 7;
            return envC.Have(EnvTypes.AdultForest);
        }
        public void SetAllTrail()
        {
            foreach (var item in Health)
            {
                _health[item.Key] = 7;
            }
        }
        public bool Have(DirectTypes dir) => _health[dir] > 0;
        public void TakeHealth(DirectTypes dir) => _health[dir] -= 1;
        public void ResetAll()
        {
            foreach (var item in Health)
            {
                _health[item.Key] = 0;
            }
        }

        public void SyncTrail(DirectTypes dir, int amount)
        {
            _health[dir] = amount;
        }
    }
}