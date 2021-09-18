namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
{
    internal struct CellFireDataComponent
    {
        internal bool HaveFire { get; set; }
        internal bool DisFire() => HaveFire = default;
        internal bool EnabFire() => HaveFire = true;
    }
}
