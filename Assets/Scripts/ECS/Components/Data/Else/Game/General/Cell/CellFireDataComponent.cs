namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
{
    internal struct CellFireDataComponent
    {
        internal bool HaveFire { get; set; }
        internal bool DisableFire() => HaveFire = false;
        internal bool EnableFire() => HaveFire = true;
    }
}
