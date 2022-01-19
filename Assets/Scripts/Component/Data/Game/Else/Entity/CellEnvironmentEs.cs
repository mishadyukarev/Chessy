using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEnvironmentEs
    {
        static Dictionary<EnvironmentTypes, Entity[]> _envEnts;

        public static ref AmountC Resources(in EnvironmentTypes env, in byte idx) => ref _envEnts[env][idx].Get<AmountC>();

        public static HashSet<EnvironmentTypes> Keys
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
                        .Add(new AmountC());
                }
            }
        }

        public static void SetNew(in EnvironmentTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            Resources(env, idx).Amount = EnvironmentValues.RandomResources(env);

            EntWhereEnviroments.HaveEnv<HaveEnvC>(env, idx).Have = true;
        }
        public static void Remove(in EnvironmentTypes env, in byte idx)
        {
            if (env == default) throw new Exception();

            if (env == EnvironmentTypes.AdultForest)
            {
                CellTrailEs.ResetAll(idx);
                CellFireEs.Fire<HaveEffectC>(idx).Disable();
            }

            Resources(env, idx).Reset();

            EntWhereEnviroments.HaveEnv<HaveEnvC>(env, idx).Have = false;
        }
    }
}