using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EnvResC : IEnvCell
    {
        Dictionary<EnvTypes, int> _resources;

        public Dictionary<EnvTypes, int> Resources
        {
            get
            {
                var dict = new Dictionary<EnvTypes, int>();
                foreach (var item in _resources)
                {
                    dict.Add(item.Key, item.Value);
                }
                return dict;
            }
        }

        public int Amount(in EnvTypes env)
        {
            if (!_resources.ContainsKey(env)) throw new Exception();
            return _resources[env];
        }
        public byte Max(in EnvTypes env)
        {
            if (!_resources.ContainsKey(env)) throw new Exception();
            return EnvValuesC.MaxAmount(env);
        }
        public bool Have(in EnvTypes env) => Amount(env) > 0;
        public bool HaveMax(in EnvTypes env) => Amount(env) >= Max(env);



        public EnvResC(bool b)
        {
            _resources = new Dictionary<EnvTypes, int>();

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _resources.Add(env, default);
            }
        }


        internal void SetNew(in EnvTypes env)
        {
            byte randAmountRes = 0;


            var forMin = 3;

            if (env == EnvTypes.Fertilizer || env == EnvTypes.AdultForest)
            {
                randAmountRes = (byte)UnityEngine.Random.Range(EnvValuesC.MaxAmount(env) / forMin, EnvValuesC.MaxAmount(env) + 1);
            }
            else if (env == EnvTypes.Hill)
            {
                randAmountRes = (byte)(EnvValuesC.MaxAmount(env) / forMin);
            }

            _resources[env] = randAmountRes;
        }
        internal void Reset(in EnvTypes env)
        {
            if (env == default) throw new Exception();
            _resources[env] = 0;
        }
        internal void Set(in EnvTypes env, in int res)
        {
            _resources[env] = res;
        }


        public void SetMax(in EnvTypes env)
        {
            if (!_resources.ContainsKey(env)) throw new Exception();

            _resources[env] = EnvValuesC.MaxAmount(env);
        }
        public void Add(in EnvTypes env, in int adding = 1)
        {
            if (!_resources.ContainsKey(env)) throw new Exception();
            if (adding == 0) throw new Exception();
            if (adding < 0) throw new Exception();

            _resources[env] += adding;

            if (_resources[env] > Max(env)) _resources[env] = Max(env);
        }
        public void AddMax(in EnvTypes env)
        {
            if (!_resources.ContainsKey(env)) throw new Exception();

            _resources[env] += Max(env);
        }
        public void Take(in EnvTypes env, in int taking = 1)
        {
            if (!_resources.ContainsKey(env)) throw new Exception();
            if (taking == 0) throw new Exception();
            if (taking < 0) throw new Exception();

            _resources[env] -= taking;

            if (_resources[env] < 0) _resources[env] = 0;
        }

        public void Sync(in EnvTypes env, in int amount)
        {
            _resources[env] = amount;
        }
    }
}