namespace Chessy.Game.Model.Entity
{
    public struct CanSetUnitHereC
    {
        readonly bool[] _visibles;

        public bool ForPlayer(in PlayerTypes player) => _visibles[(byte)player];

        internal CanSetUnitHereC(in bool[] canSet) => _visibles = canSet;

        internal void Set(in PlayerTypes playerT, in bool canSet) => _visibles[(byte)playerT] = canSet;
    }
}