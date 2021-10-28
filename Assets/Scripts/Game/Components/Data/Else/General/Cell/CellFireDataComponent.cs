namespace Scripts.Game
{
    public struct CellFireDataComponent
    {
        public bool HaveFire { get; set; }
        public bool DisableFire() => HaveFire = default;
        public bool EnabFire() => HaveFire = true;
    }
}
