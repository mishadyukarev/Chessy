namespace Chessy.Game.Model.Entity
{
    public struct CanSetUnitHereC
    {
        readonly bool[] _visibles;
        public ref bool ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player];

        internal CanSetUnitHereC(in bool[] canSet) => _visibles = canSet;
    }
}