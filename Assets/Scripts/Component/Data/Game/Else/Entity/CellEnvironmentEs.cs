using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEnvironmentEs
    {
        static Dictionary<EnvTypes, Entity[]> _envEnts;
        static EnvironmentValues _values;

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

        public CellEnvironmentEs(in EcsWorld gameW)
        {
            _envEnts = new Dictionary<EnvTypes, Entity[]>();

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _envEnts.Add(env, new Entity[CellValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                {

                    _envEnts[env][idx] = gameW.NewEntity()
                        .Add(new EnvCellEC(idx, env))
                        .Add(new HaveEnvironmentC())
                        .Add(new AmountC());
                }
            }

            _values = new EnvironmentValues();
        }

        public static void SetNew(in EnvTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            byte randAmountRes = 0;


            var forMin = 3;

            if (env == EnvTypes.Fertilizer || env == EnvTypes.AdultForest)
            {
                randAmountRes = (byte)UnityEngine.Random.Range(_values.MaxAmount(env) / forMin, _values.MaxAmount(env) + 1);
            }
            else if (env == EnvTypes.Hill)
            {
                randAmountRes = (byte)(_values.MaxAmount(env) / forMin);
            }

            Environment<AmountC>(env, idx).Amount = randAmountRes;


            EntWhereEnviroments.HaveEnv<HaveEnvC>(env, idx).Have = true;
            Environment<HaveEnvironmentC>(env, idx).Have = true;
        }
        public static void Remove(in EnvTypes env, in byte _idx)
        {
            if (env == default) throw new Exception();

            if (Environment<HaveEnvironmentC>(env, _idx).Have)
            {
                if (env == EnvTypes.AdultForest)
                {
                    CellTrailEs.Trail<TrailCellEC>(_idx).ResetAll();
                    CellFireEs.Fire<HaveEffectC>(_idx).Disable();
                }

                Environment<AmountC>(env, _idx).Amount = 0;

                EntWhereEnviroments.HaveEnv<HaveEnvC>(env, _idx).Have = false;
                Environment<HaveEnvironmentC>(env, _idx).Have = false;
            }
        }

        public static byte Max(in EnvTypes env) => _values.MaxAmount(env);
    }
}