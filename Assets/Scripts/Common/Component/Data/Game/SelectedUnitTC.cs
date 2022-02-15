namespace Game.Game
{
    public sealed class SelectedUnitTC : UnitTC
    {
        public bool IsSelectedUnit => Unit != UnitTypes.None && Unit != UnitTypes.End;
    }
}