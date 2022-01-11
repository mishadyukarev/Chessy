using System;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityCellFirePool;
using static Game.Game.EntityCellTrailPool;

namespace Game.Game
{
    public readonly struct EnvCellEC : IEnvCell
    {
        readonly byte _idx;
        readonly EnvTypes _env;

        readonly EnvironmentValues _values;


        public byte Max() => _values.MaxAmount(_env);
        public bool Have() => Environment<ResourcesC>(_env, _idx).Resources > 0;
        public bool HaveMax() => Environment<ResourcesC>(_env, _idx).Resources >= Max();


        internal EnvCellEC(in byte idx, in EnvTypes env)
        {
            _idx = idx;
            _env = env;
            _values = new EnvironmentValues();
        }
        public void SetNew()
        {
            if (_env == default) throw new Exception();

            byte randAmountRes = 0;


            var forMin = 3;

            if (_env == EnvTypes.Fertilizer || _env == EnvTypes.AdultForest)
            {
                randAmountRes = (byte)UnityEngine.Random.Range(_values.MaxAmount(_env) / forMin, _values.MaxAmount(_env) + 1);
            }
            else if (_env == EnvTypes.Hill)
            {
                randAmountRes = (byte)(_values.MaxAmount(_env) / forMin);
            }

            Environment<ResourcesC>(_env, _idx).Resources = randAmountRes;


            WhereEnvC.Set(_env, _idx, true);
            Environment<HaveEnvironmentC>(_env, _idx).Have = true;
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

                Environment<ResourcesC>(_env, _idx).Resources = 0;

                WhereEnvC.Set(_env, _idx, false);
                Environment<HaveEnvironmentC>(_env, _idx).Have = false;
            }
        }
        public void SetMax()
        {
            Environment<ResourcesC>(_env, _idx).Resources = Max();
        }
        public void Add(in int adding = 1)
        {
            if (adding == 0) throw new Exception();
            if (adding < 0) throw new Exception();

            Environment<ResourcesC>(_env, _idx).Resources += adding;

            if (Environment<ResourcesC>(_env, _idx).Resources > Max()) Environment<ResourcesC>(_env, _idx).Resources = Max();
        }
        public void AddMax()
        {
            Environment<ResourcesC>(_env, _idx).Resources += Max();
        }
        public void Take(in int taking = 1)
        {
            if (taking == 0) throw new Exception();
            if (taking < 0) throw new Exception();

            Environment<ResourcesC>(_env, _idx).Resources -= taking;

            if (Environment<ResourcesC>(_env, _idx).Resources < 0) Environment<ResourcesC>(_env, _idx).Resources = 0;
        }

        public void Sync(in int amount)
        {
            Environment<ResourcesC>(_env, _idx).Resources = amount;
        }

    }
}