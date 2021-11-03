using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellTrailDataC
    {
        private readonly Dictionary<DirectTypes, int> _health;

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

        public bool TrySetNewTrain(DirectTypes dir, CellEnvDataC envC)
        {
            if (envC.Have(EnvTypes.AdultForest)) _health[dir] = 10;
            return envC.Have(EnvTypes.AdultForest);
        }
        public bool Have(DirectTypes dir) => _health[dir] > 0;
        public void TakeHealth(DirectTypes dir) => _health[dir] -= 1;
        public void ResetAll()
        {
            for (var dir = (DirectTypes)1; dir < (DirectTypes)typeof(DirectTypes).GetEnumNames().Length; dir++)
            {
                _health[dir] = 0;
            }
        }
    }
}