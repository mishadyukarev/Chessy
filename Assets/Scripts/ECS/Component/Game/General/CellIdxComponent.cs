namespace Assets.Scripts.ECS.Component.Game.General
{
    internal struct CellIdxComponent
    {
        internal int IdxCell { get; private set; }

        internal CellIdxComponent(int idxCell) => IdxCell = idxCell;
    }
}
