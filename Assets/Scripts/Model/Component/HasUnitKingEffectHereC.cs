namespace Chessy.Model
{
    public struct HasUnitKingEffectHereC
    {
        readonly bool[] _have;

        public ref bool Has(in PlayerTypes playerT) => ref _have[(byte)playerT];

        internal HasUnitKingEffectHereC(in bool[] have) => _have = have;

        internal void Set(in PlayerTypes playerT, in bool have) => _have[(byte)playerT] = have;
    }
}