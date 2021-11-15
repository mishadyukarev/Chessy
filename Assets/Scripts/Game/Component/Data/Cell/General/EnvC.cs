using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct EnvC
    {
        private Dictionary<EnvTypes, bool> _haveEnvir;

        public Dictionary<EnvTypes, bool> Envronments
        {
            get
            {
                var envrs = new Dictionary<EnvTypes, bool>();
                foreach (var item in _haveEnvir) envrs.Add(item.Key, item.Value);
                return envrs;
            }
        }

        public EnvC(Dictionary<EnvTypes, bool> haveCellEnvironments)
        {
            _haveEnvir = haveCellEnvironments;

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _haveEnvir.Add(env, default);
            }
        }

        public bool Have(params EnvTypes[] envTypes)
        {
            if (envTypes == default) throw new Exception();

            foreach (var env in envTypes) if (_haveEnvir[env]) return true;
            return false;
        }

        public void Set(EnvTypes envType, bool haveEnv = true)
        {
            if (envType == default) throw new Exception();
            if (Have(envType)) throw new Exception();

            _haveEnvir[envType] = haveEnv;
        }
        public void Remove(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            if (!Have(envType)) throw new Exception();

            _haveEnvir[envType] = false;
        }
        public void Sync(EnvTypes envType, bool haveEnv = true)
        {
            _haveEnvir[envType] = haveEnv;
        }
    }
}
