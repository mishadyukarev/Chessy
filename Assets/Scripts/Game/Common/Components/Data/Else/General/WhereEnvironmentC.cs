using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public readonly struct WhereEnvironmentC
    {
        private static Dictionary<EnvirTypes, List<byte>> _whereEnviron;

        public WhereEnvironmentC(bool needNew) : this()
        {
            if (needNew)
            {
                _whereEnviron = new Dictionary<EnvirTypes, List<byte>>();

                for (var environType = Support.MinEnvironType; environType < Support.MaxEnvironType; environType++)
                {
                    _whereEnviron.Add(environType, new List<byte>());
                }
            }
        }

        public static void Add(EnvirTypes envirType, byte idx) 
        {
            if (envirType == EnvirTypes.None) throw new Exception(); 
            _whereEnviron[envirType].Add(idx);
        }

        public static void Remove(EnvirTypes envirType, byte idx)
        {
            if (envirType == EnvirTypes.None) throw new Exception();

            if (_whereEnviron[envirType].Contains(idx)) _whereEnviron[envirType].Remove(idx);
            else throw new Exception();
        }
    }
}