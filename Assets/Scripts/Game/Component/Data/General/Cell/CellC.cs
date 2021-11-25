namespace Game.Game
{
    public readonly struct CellC : ICell
    {
        public readonly bool IsActiveCell;
        public readonly int InstanceID;

        public CellC(in bool isActiveCell, in int instanceID)
        {
            IsActiveCell = isActiveCell;
            InstanceID = instanceID;
        }
    }
}