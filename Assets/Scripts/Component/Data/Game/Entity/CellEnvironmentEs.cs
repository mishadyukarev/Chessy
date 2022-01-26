using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEnvironmentEs
    {
        static Dictionary<EnvironmentTypes, CellEnvironmentE[]> _env;

        public static CellEnvironmentE Environment(in EnvironmentTypes env, in byte idx) => _env[env][idx];

        public static HashSet<EnvironmentTypes> KeysEnvironment
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
                    _env[env][idx] = new CellEnvironmentE(gameW);
                }
            }
        }

        public static void SetNew(in EnvironmentTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            Environment(env, idx).Resources.Amount = CellEnvironmentValues.RandomResources(env);

            EntWhereEnviroments.HaveEnv(env, idx).Have = true;
        }
        public static void Remove(in EnvironmentTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            if (env == EnvironmentTypes.AdultForest)
            {
                CellTrailEs.ResetAll(idx);
                CellFireEs.Fire(idx).Fire.Disable();
            }

            Environment(env, idx).Resources.Reset();

            EntWhereEnviroments.HaveEnv(env, idx).Have = false;
        }
    }
}