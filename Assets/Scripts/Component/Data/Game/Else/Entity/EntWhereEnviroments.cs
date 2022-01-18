using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntWhereEnviroments
    {
        static Dictionary<string, Entity> _envs;


        static string Key(in EnvironmentTypes env, in byte idx) => env.ToString() + idx;

        public static ref C HaveEnv<C>(in EnvironmentTypes env, in byte idx) where C : struct => ref _envs[Key(env, idx)].Get<C>();
        public static ref C HaveEnv<C>(in string key) where C : struct => ref _envs[key].Get<C>();

        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _envs) hash.Add(item.Key);
                return hash;
            }
        }


        static EntWhereEnviroments()
        {
            _envs = new Dictionary<string, Entity>();

            for (var env = EnvironmentTypes.First; env < EnvironmentTypes.End; env++)
            {
                for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _envs.Add(Key(env, idx), default);
                }
            }
        }
        public EntWhereEnviroments(in EcsWorld gameW)
        {
            foreach (var key in Keys)
            {
                _envs[key] = gameW.NewEntity()
                    .Add(new HaveEnvC());
            }
        }
    }
}