using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellEnvDataC
    {
        private Dictionary<EnvTypes, bool> _haveEnvir;

        public Dictionary<EnvTypes, bool> Envronments
        {
            get
            {
                var envrs = new Dictionary<EnvTypes, bool>();

                for (var envType = Support.MinEnvironType; envType < Support.MaxEnvironType; envType++)
                {
                    if (_haveEnvir[envType]) envrs.Add(envType, true);
                    else envrs.Add(envType, false);
                }

                return envrs;
            }
        }

        public CellEnvDataC(Dictionary<EnvTypes, bool> haveCellEnvironments)
        {
            _haveEnvir = haveCellEnvironments;

            for (var envirType = Support.MinEnvironType; envirType < Support.MaxEnvironType; envirType++)
            {
                _haveEnvir.Add(envirType, default);
            }
        }

        public bool Have(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            return _haveEnvir[envType];
        }
        public bool Have(EnvTypes[] envTypes)
        {
            if(envTypes == default)
            foreach (var envType in envTypes) if (Have(envType)) return true;
            return false;
        }

        public void Set(EnvTypes envType, bool haveEnv = true)
        {
            if (envType == default) throw new Exception();
            if (Have(envType)) throw new Exception();

            _haveEnvir[envType] = haveEnv;
        }
        public void Reset(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            if (!Have(envType)) throw new Exception();

            _haveEnvir[envType] = false;
        }
    }
}
