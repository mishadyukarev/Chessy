﻿using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EnvResC : IEnvCell
    {
        Dictionary<EnvTypes, int> _amountResours;

        public Dictionary<EnvTypes, int> Resources
        {
            get
            {
                var dict = new Dictionary<EnvTypes, int>();
                foreach (var item in _amountResours)
                {
                    dict.Add(item.Key, item.Value);
                }
                return dict;
            }
        }

        public int AmountRes(EnvTypes env)
        {
            if (env == default) throw new Exception();
            return _amountResours[env];
        }
        public byte MaxAmountRes(EnvTypes env)
        {
            if (env == default) throw new Exception();
            return EnvironValues.MaxAmount(env);
        }
        public bool HaveRes(EnvTypes env) => AmountRes(env) > 0;
        public bool HaveMaxRes(EnvTypes env) => AmountRes(env) >= MaxAmountRes(env);


        public EnvResC(bool needNew)
        {
            if (needNew)
            {
                _amountResours = new Dictionary<EnvTypes, int>();

                for (var env = EnvTypes.First; env < EnvTypes.End; env++)
                {
                    _amountResours.Add(env, default);
                }
            }
            else throw new Exception();
        }


        public void SetRes(EnvTypes env, int value)
        {
            if (env == default) throw new Exception();
            _amountResours[env] = value;
        }
        public void SetMaxAmountRes(EnvTypes env) => SetRes(env, MaxAmountRes(env));
        public void AddAmountRes(EnvTypes env, int adding = 1) => SetRes(env, AmountRes(env) + adding);
        public void AddMaxAmountRes(EnvTypes env) => SetRes(env, AmountRes(env) + MaxAmountRes(env));
        public void TakeAmountRes(EnvTypes env, int taking = 1) => SetRes(env, AmountRes(env) - taking);
        public void SetNew(EnvTypes env)
        {
            byte randAmountRes = 0;
            switch (env)
            {
                case EnvTypes.None:
                    throw new Exception();

                case EnvTypes.Fertilizer:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvironValues.MaxAmount(env) / 3, EnvironValues.MaxAmount(env) / 2 + 1);
                    break;

                case EnvTypes.YoungForest:
                    break;

                case EnvTypes.AdultForest:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvironValues.MaxAmount(env) / 3, EnvironValues.MaxAmount(env) / 2 + 1);
                    break;

                case EnvTypes.Hill:
                    randAmountRes = 1;
                    break;

                case EnvTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }
            SetRes(env, randAmountRes);
        }

        public void Sync(EnvTypes env, int amount)
        {
            _amountResours[env] = amount;
        }
    }
}