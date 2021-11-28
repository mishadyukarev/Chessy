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


        public EnvResC(bool needNew)
        {
            if (needNew)
            {
                _resources = new Dictionary<EnvTypes, int>();

                for (var env = EnvTypes.First; env < EnvTypes.End; env++)
                {
                    _resources.Add(env, default);
                }
            }
            else throw new Exception();
        }


        internal void SetNew(in EnvTypes env)
        {
            byte randAmountRes = 0;
            switch (env)
            {
                case EnvTypes.None:
                    throw new Exception();

                case EnvTypes.Fertilizer:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvValuesC.MaxAmount(env) / 3, EnvValuesC.MaxAmount(env) / 2 + 1);
                    break;

                case EnvTypes.YoungForest:
                    break;

                case EnvTypes.AdultForest:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvValuesC.MaxAmount(env) / 3, EnvValuesC.MaxAmount(env) / 2 + 1);
                    break;

                case EnvTypes.Hill:
                    randAmountRes = 1;
                    break;

                case EnvTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }

            _resources[env] = randAmountRes;
        }
        internal void Reset(in EnvTypes env)
        {
            if (env == default) throw new Exception();
            _resources[env] = 0;
        }


        public void SetMax(in EnvTypes env)
        {
            if (!_resources.ContainsKey(env)) throw new Exception();

            _resources[env] = EnvValuesC.MaxAmount(env);
        }
        public void Add(in EnvTypes env, in int adding = 1)
        {
            if(!_resources.ContainsKey(env)) throw new Exception();
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