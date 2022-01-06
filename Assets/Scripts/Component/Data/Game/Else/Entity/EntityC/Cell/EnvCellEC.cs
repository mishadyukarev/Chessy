using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct EnvCellEC : IEnvCell
    {
        readonly byte _idx;

        internal EnvCellEC(in byte idx) => _idx = idx;

        public void SetNew(EnvTypes env)
        {
            if (env == default) throw new Exception();

            Environment<EnvResC>(_idx).SetNew(env);

            WhereEnvC.Set(env, _idx, true);
            Environment<EnvironmentC>(_idx).Set(env, true);
        }
        public void Remove(EnvTypes env)
        {
            if (env == default) throw new Exception();

            if (Environment<EnvironmentC>(_idx).Have(env))
            {
                if (env == EnvTypes.AdultForest)
                {
                    Trail<TrailCellEC>(_idx).ResetAll();
                    Fire<HaveEffectC>(_idx).Disable();
                }

                Environment<EnvResC>(_idx).Reset(env);

                WhereEnvC.Set(env, _idx, false);
                Environment<EnvironmentC>(_idx).Set(env, false);
            }
        }
        public void Sync(in EnvTypes env, in bool have, in int resource)
        {
            Environment<EnvironmentC>(_idx).Set(env, have);
            Environment<EnvResC>(_idx).Set(env, resource);
        }
    }
}