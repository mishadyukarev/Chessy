using UnityEditor;
using UnityEngine;

namespace Chessy.Model
{
    public struct HasUnitKingEffectHereC
    {
        readonly bool[] _have;

        public bool Has(in PlayerTypes playerT) => _have[(byte)playerT];

        internal HasUnitKingEffectHereC(in bool[] have) => _have = have;

        internal void Set(in PlayerTypes playerT, in bool have) => _have[(byte)playerT] = have;
    }
}