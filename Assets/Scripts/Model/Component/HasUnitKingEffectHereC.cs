namespace Chessy.Model
{
    public struct HasUnitKingEffectHereC
    {
        internal readonly bool[] HaveArray;

        public ref bool Has(in PlayerTypes playerT) => ref HaveArray[(byte)playerT];

        internal HasUnitKingEffectHereC(in bool[] have) => HaveArray = have;

        internal void Set(in PlayerTypes playerT, in bool have) => HaveArray[(byte)playerT] = have;
    }
}