namespace Game.Game
{
    public struct ProtectionC : ITWCellE
    {
        public int Protection { get; internal set; }



        internal void Take(int taking = 1)
        {
            Protection = taking;
        }
        internal void Set(ProtectionC shieldC) => Protection = shieldC.Protection;
        internal void Set(int shieldProt) => Protection = shieldProt;
        internal void Reset() => Protection = 0;
    }
}