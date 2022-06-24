namespace Chessy.Model.Model.Entity
{
    public struct CanSetUnitHereC
    {
        readonly bool[] _canSet;

        public bool ForPlayer(in PlayerTypes player) => _canSet[(byte)player];

        internal CanSetUnitHereC(in bool[] canSet) => _canSet = canSet;

        internal void Set(in PlayerTypes playerT, in bool canSet) => _canSet[(byte)playerT] = canSet;
    }
}