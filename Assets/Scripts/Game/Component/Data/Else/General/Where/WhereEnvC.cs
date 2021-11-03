using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public readonly struct WhereEnvC
    {
        private static Dictionary<EnvTypes, List<byte>> _whereEnviron;

        public WhereEnvC(bool needNew) : this()
        {
            if (needNew)
            {
                _whereEnviron = new Dictionary<EnvTypes, List<byte>>();

                for (var environType = Support.MinEnvironType; environType < Support.MaxEnvironType; environType++)
                {
                    _whereEnviron.Add(environType, new List<byte>());
                }
            }
        }

        public static void Add(EnvTypes envirType, byte idx) 
        {
            if (envirType == EnvTypes.None) throw new Exception(); 
            _whereEnviron[envirType].Add(idx);
        }

        public static void Remove(EnvTypes envirType, byte idx)
        {
            if (envirType == EnvTypes.None) throw new Exception();

            if (_whereEnviron[envirType].Contains(idx)) _whereEnviron[envirType].Remove(idx);
            else throw new Exception();
        }

        public static byte Amount(EnvTypes env) => (byte)_whereEnviron[env].Count;
    }
}