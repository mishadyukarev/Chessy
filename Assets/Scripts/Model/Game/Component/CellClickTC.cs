namespace Chessy.Game
{
    public struct CellClickTC
    {
        public CellClickTypes CellClickT { get; internal set; }

        public bool Is(params CellClickTypes[] clicks)
        {
            foreach (var click in clicks)
                if (click == CellClickT) return true;
            return false;
        }
    }
}