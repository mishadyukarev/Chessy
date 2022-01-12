﻿using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct WhereEnvC
    {
        static Dictionary<string, bool> _envs;
        static CellValues _cellValues;


        private static string Key(in EnvTypes env, in byte idx) => env.ToString() + idx;
        private static bool ContainsKey(string key) => _envs.ContainsKey(key);

        public static Dictionary<string, bool> Envs
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _envs) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static byte Amount(EnvTypes env)
        {
            byte amount = 0;
            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (_envs[Key(env, idx)]) ++amount;
            }
            return amount;
        }


        static WhereEnvC()
        {
            _envs = new Dictionary<string, bool>();
            _cellValues = new CellValues();

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _envs.Add(Key(env, idx), default);
                }
            }
        }
        public WhereEnvC(bool needReset) : this()
        {
            if (needReset) foreach (var item in Envs) _envs[item.Key] = false;
            else throw new Exception();
        }



        public static void Set(EnvTypes env, byte idx, bool have)
        {
            var key = Key(env, idx);

            if (!ContainsKey(key)) throw new Exception();
            if (_envs[key] == have) throw new Exception();

            _envs[key] = have;
        }
        public static void Sync(string key, bool have)
        {
            if (!ContainsKey(key)) throw new Exception();

            _envs[key] = have;
        }
    }
}