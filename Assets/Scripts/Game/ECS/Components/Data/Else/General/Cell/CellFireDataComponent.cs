namespace Scripts.Game
{
    internal struct CellFireDataComponent
    {
        internal bool HaveFire { get; set; }
        internal bool DisableFire() => HaveFire = default;
        internal bool EnabFire() => HaveFire = true;
    }
}
