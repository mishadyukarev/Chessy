using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct WhereEnviromentEs
    {
        readonly Dictionary<string, HaveEnvironmentOnCellE> _envs;


        string Key(in EnvironmentTypes env, in byte idx) => env.ToString() + idx;

        public HaveEnvironmentOnCellE Info(in EnvironmentTypes env, in byte idx) => _envs[Key(env, idx)];
        public HaveEnvironmentOnCellE Info(in string key) => _envs[key];

        public HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _envs) hash.Add(item.Key);
                return hash;
            }
        }

        public WhereEnviromentEs(in EcsWorld gameW)
        {
            _envs = new Dictionary<string, HaveEnvironmentOnCellE>();

            for (var env = EnvironmentTypes.First; env < EnvironmentTypes.End; env++)
            {
                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _envs.Add(Key(env, idx), new HaveEnvironmentOnCellE(gameW));
                }
            }
        }
    }
}