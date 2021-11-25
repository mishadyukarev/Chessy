using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EnvC : IEnvCell
    {
        Dictionary<EnvTypes, bool> _envs;
        readonly byte _idx;

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



        public EnvC(Dictionary<EnvTypes, bool> haveEnvs, byte idx)
        {
            _envs = haveEnvs;
            _idx = idx;

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _envs.Add(env, default);
            }
        }



        public void SetNew(EnvTypes env)
        {
            if (env == default) throw new Exception();
            if (Have(env)) throw new Exception();

            WhereEnvC.Set(env, _idx, true);
            _envs[env] = true;
        }
        public void Remove(EnvTypes env)
        {
            if (env == default) throw new Exception();
            if (!Have(env)) throw new Exception();

            WhereEnvC.Set(env, _idx, false);
            _envs[env] = false;
        }
        public void Sync(EnvTypes env, bool have)
        {
            if (!_envs.ContainsKey(env)) throw new Exception();

            _envs[env] = have;
        }
    }
}
