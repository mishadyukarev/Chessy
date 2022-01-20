using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntWhereEnviroments
    {
        static Dictionary<string, Entity> _envs;


        static string Key(in EnvironmentTypes env, in byte idx) => env.ToString() + idx;

        public static ref HaveEnvC HaveEnv(in EnvironmentTypes env, in byte idx) => ref _envs[Key(env, idx)].Get<HaveEnvC>();
        public static ref HaveEnvC HaveEnv(in string key)  => ref _envs[key].Get<HaveEnvC>();

        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _envs) hash.Add(item.Key);
                return hash;
            }
        }

        public EntWhereEnviroments(in EcsWorld gameW)
        {
            _envs = new Dictionary<string, Entity>();

            for (var env = EnvironmentTypes.First; env < EnvironmentTypes.End; env++)
            {
                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _envs.Add(Key(env, idx), gameW.NewEntity()
                        .Add(new HaveEnvC()));
                }
            }
        }
    }
}