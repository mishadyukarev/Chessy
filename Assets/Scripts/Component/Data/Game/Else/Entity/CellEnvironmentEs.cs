using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEnvironmentEs
    {
        static Dictionary<EnvironmentTypes, Entity[]> _envEnts;
        static EnvironmentValues _values;

        public static ref T Environment<T>(in EnvironmentTypes env, in byte idx) where T : struct, IEnvCell => ref _envEnts[env][idx].Get<T>();

        public static HashSet<EnvironmentTypes> Enviroments
        {
            get
            {
                var hash = new HashSet<EnvironmentTypes>();
                foreach (var item in _envEnts) hash.Add(item.Key);
                return hash;
            }
        }

        public CellEnvironmentEs(in EcsWorld gameW)
        {
            _envEnts = new Dictionary<EnvironmentTypes, Entity[]>();

            for (var env = EnvironmentTypes.First; env < EnvironmentTypes.End; env++)
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

        public static void SetNew(in EnvironmentTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            byte randAmountRes = 0;


            var forMin = 3;

            if (env == EnvironmentTypes.Fertilizer || env == EnvironmentTypes.AdultForest)
            {
                randAmountRes = (byte)UnityEngine.Random.Range(_values.MaxAmount(env) / forMin, _values.MaxAmount(env) + 1);
            }
            else if (env == EnvironmentTypes.Hill)
            {
                randAmountRes = (byte)(_values.MaxAmount(env) / forMin);
            }

            Environment<AmountC>(env, idx).Amount = randAmountRes;


            EntWhereEnviroments.HaveEnv<HaveEnvC>(env, idx).Have = true;
            Environment<HaveEnvironmentC>(env, idx).Have = true;
        }
        public static void Remove(in EnvironmentTypes env, in byte _idx)
        {
            if (env == default) throw new Exception();

            if (Environment<HaveEnvironmentC>(env, _idx).Have)
            {
                if (env == EnvironmentTypes.AdultForest)
                {
                    CellTrailEs.Trail<TrailCellEC>(_idx).ResetAll();
                    CellFireEs.Fire<HaveEffectC>(_idx).Disable();
                }

                Environment<AmountC>(env, _idx).Amount = 0;

                EntWhereEnviroments.HaveEnv<HaveEnvC>(env, _idx).Have = false;
                Environment<HaveEnvironmentC>(env, _idx).Have = false;
            }
        }

        public static byte Max(in EnvironmentTypes env) => _values.MaxAmount(env);
    }
}