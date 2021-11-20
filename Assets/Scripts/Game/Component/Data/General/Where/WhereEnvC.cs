using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct WhereEnvC
    {
        private static Dictionary<EnvTypes, List<byte>> _envInGame;

        public static Dictionary<EnvTypes, List<byte>> EnvInGame
        {
            get
            {
                var newDict_0 = new Dictionary<EnvTypes, List<byte>>();

                foreach (var item_0 in _envInGame)
                {
                    newDict_0.Add(item_0.Key, new List<byte>());

                    foreach (var item_1 in item_0.Value)
                    {
                        newDict_0[item_0.Key].Add(item_1);
                    }
                }

                return newDict_0;
            }
        }

        public WhereEnvC(bool needNew) : this()
        {
            if (needNew)
            {
                _envInGame = new Dictionary<EnvTypes, List<byte>>();

                for (var env = EnvTypes.First; env < EnvTypes.End; env++)
                {
                    _envInGame.Add(env, new List<byte>());
                }
            }
        }

        public static void Add(EnvTypes envirType, byte idx)
        {
            if (envirType == EnvTypes.None) throw new Exception();
            _envInGame[envirType].Add(idx);
        }

        public static void Remove(EnvTypes envirType, byte idx)
        {
            if (envirType == EnvTypes.None) throw new Exception();

            if (_envInGame[envirType].Contains(idx)) _envInGame[envirType].Remove(idx);
            else throw new Exception();
        }

        public static void SyncAdd(EnvTypes envirType, byte idx)
        {
            _envInGame[envirType].Add(idx);
        }
        public static void Clear(EnvTypes envirType)
        {
            _envInGame[envirType].Clear();
        }
        public static byte Amount(EnvTypes env) => (byte)_envInGame[env].Count;
    }
}