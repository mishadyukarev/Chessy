using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EnvironmentC : IEnvCell
    {
        Dictionary<EnvTypes, bool> _envs;

        public Dictionary<EnvTypes, bool> Envronments
        {
            get
            {
                var envrs = new Dictionary<EnvTypes, bool>();
                foreach (var item in _envs) envrs.Add(item.Key, item.Value);
                return envrs;
            }
        }
        public bool Have(params EnvTypes[] envs)
        {
            if (envs == default) throw new Exception();

            foreach (var env in envs) if (_envs[env]) return true;
            return false;
        }



        public EnvironmentC(in Dictionary<EnvTypes, bool> haveEnvs)
        {
            _envs = haveEnvs;

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _envs.Add(env, default);
            }
        }

        public void Set(in EnvTypes env, in bool have)
        {
            _envs[env] = have;
        }
    }
}
