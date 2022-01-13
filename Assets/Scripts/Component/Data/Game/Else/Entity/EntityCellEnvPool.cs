using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityCellEnvPool
    {
        readonly static Dictionary<EnvTypes, Entity[]> _envEnts;

        public static ref T Environment<T>(in EnvTypes env, in byte idx) where T : struct, IEnvCell => ref _envEnts[env][idx].Get<T>();

        public static HashSet<EnvTypes> Enviroments
        {
            get
            {
                var hash = new HashSet<EnvTypes>();
                foreach (var item in _envEnts) hash.Add(item.Key);
                return hash;
            }
        }

        static EntityCellEnvPool()
        {
            _envEnts = new Dictionary<EnvTypes, Entity[]>();

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _envEnts.Add(env, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
        }
        public EntityCellEnvPool(in EcsWorld gameW)
        {
            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                for (var env = EnvTypes.First; env < EnvTypes.End; env++)
                {
                    _envEnts[env][idx] = gameW.NewEntity()
                        .Add(new EnvCellEC(idx, env))
                        .Add(new HaveEnvironmentC())
                        .Add(new AmountResourcesC());
                }
            }
        }
    }
}