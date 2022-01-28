using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellEnvironmentEs
    {
        Dictionary<EnvironmentTypes, CellEnvironmentE[]> _env;

        public CellEnvironmentE Environment(in EnvironmentTypes env, in byte idx) => _env[env][idx];
        public CellEnvironmentE[] Environments(in byte idx)
        {
            var envs = new CellEnvironmentE[_env.Keys.Count];
            var i = 0;
            foreach (var envT in _env.Keys) envs[i++] = _env[envT][idx];
            return envs;
        }

        public HashSet<EnvironmentTypes> Keys
        {
            get
            {
                var hash = new HashSet<EnvironmentTypes>();
                foreach (var item in _env) hash.Add(item.Key);
                return hash;
            }
        }

        public CellEnvironmentEs(in EcsWorld gameW)
        {
            _env = new Dictionary<EnvironmentTypes, CellEnvironmentE[]>();

            for (var env = EnvironmentTypes.None + 1; env < EnvironmentTypes.End; env++)
            {
                _env.Add(env, new CellEnvironmentE[CellStartValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _env[env][idx] = new CellEnvironmentE(env, gameW);
                }
            }
        }
    }
}