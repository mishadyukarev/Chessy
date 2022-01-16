using System;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityCellFirePool;
using static Game.Game.CellTrailEs;

namespace Game.Game
{
    public readonly struct EnvCellEC : IEnvCell
    {
        readonly byte _idx;
        readonly EnvTypes _env;

        readonly EnvironmentValues _values;


        public byte Max() => _values.MaxAmount(_env);
        public bool Have() => Environment<AmountResourcesC>(_env, _idx).Resources > 0;
        public bool HaveMax() => Environment<AmountResourcesC>(_env, _idx).Resources >= Max();


        internal EnvCellEC(in byte idx, in EnvTypes env)
        {
            _idx = idx;
            _env = env;
            _values = new EnvironmentValues();
        }
        
        public void Remove()
        {
            if (_env == default) throw new Exception();

            if (Environment<HaveEnvironmentC>(_env, _idx).Have)
            {
                if (_env == EnvTypes.AdultForest)
                {
                    Trail<TrailCellEC>(_idx).ResetAll();
                    Fire<HaveEffectC>(_idx).Disable();
                }

                Environment<AmountResourcesC>(_env, _idx).Resources = 0;

                EntWhereEnviroments.HaveEnv<HaveEnvC>(_env, _idx).Have = false;
                Environment<HaveEnvironmentC>(_env, _idx).Have = false;
            }
        }
        public void SetMax()
        {
            Environment<AmountResourcesC>(_env, _idx).Resources = Max();
        }
        public void Add(in int adding = 1)
        {
            if (adding == 0) throw new Exception();
            if (adding < 0) throw new Exception();

            Environment<AmountResourcesC>(_env, _idx).Resources += adding;

            if (Environment<AmountResourcesC>(_env, _idx).Resources > Max()) Environment<AmountResourcesC>(_env, _idx).Resources = Max();
        }
        public void AddMax()
        {
            Environment<AmountResourcesC>(_env, _idx).Resources += Max();
        }
        public void Take(in int taking = 1)
        {
            if (taking == 0) throw new Exception();
            if (taking < 0) throw new Exception();

            Environment<AmountResourcesC>(_env, _idx).Resources -= taking;

            if (Environment<AmountResourcesC>(_env, _idx).Resources < 0) Environment<AmountResourcesC>(_env, _idx).Resources = 0;
        }

        public void Sync(in int amount)
        {
            Environment<AmountResourcesC>(_env, _idx).Resources = amount;
        }

    }
}