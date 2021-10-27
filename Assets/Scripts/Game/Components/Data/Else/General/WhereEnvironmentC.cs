using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal readonly struct WhereEnvironmentC
    {
        private static Dictionary<EnvirTypes, List<byte>> _whereEnviron;

        internal WhereEnvironmentC(bool needNew) : this()
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

        internal static void Add(EnvirTypes envirType, byte idx) 
        {
            if (envirType == EnvirTypes.None) throw new Exception(); 
            _whereEnviron[envirType].Add(idx);
        }

        internal static void Remove(EnvirTypes envirType, byte idx)
        {
            if (envirType == EnvirTypes.None) throw new Exception();

            if (_whereEnviron[envirType].Contains(idx)) _whereEnviron[envirType].Remove(idx);
            else throw new Exception();
        }
    }
}