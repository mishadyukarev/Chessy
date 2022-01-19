using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEnvironmentEs
    {
        static Dictionary<EnvironmentTypes, Entity[]> _envEnts;

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
                _envEnts.Add(env, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _envEnts[env][idx] = gameW.NewEntity()
                        .Add(new HaveEnvironmentC())
                        .Add(new AmountC());
                }
            }
        }

        public static void SetNew(in EnvironmentTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            byte randAmountRes = 0;


            var forMin = 3;

            if (env == EnvironmentTypes.Fertilizer || env == EnvironmentTypes.AdultForest)
            {
                randAmountRes = (byte)UnityEngine.Random.Range(EnvironmentValues.MaxAmount(env) / forMin, EnvironmentValues.MaxAmount(env) + 1);
            }
            else if (env == EnvironmentTypes.Hill)
            {
                randAmountRes = (byte)(EnvironmentValues.MaxAmount(env) / forMin);
            }

            Environment<AmountC>(env, idx).Amount = randAmountRes;


            EntWhereEnviroments.HaveEnv<HaveEnvC>(env, idx).Have = true;
            Environment<HaveEnvironmentC>(env, idx).Have = true;
        }
        public static void Remove(in EnvironmentTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            if (Environment<HaveEnvironmentC>(env, idx).Have)
            {
                if (env == EnvironmentTypes.AdultForest)
                {
                    CellTrailEs.ResetAll(idx);
                    CellFireEs.Fire<HaveEffectC>(idx).Disable();
                }

                Environment<AmountC>(env, idx).Amount = 0;

                EntWhereEnviroments.HaveEnv<HaveEnvC>(env, idx).Have = false;
                Environment<HaveEnvironmentC>(env, idx).Have = false;
            }
        }
    }
}