namespace Game.Game
{
    public sealed class SelectedIdxC : IdxC
    {
        public bool IsSelectedCell => Idx != 0;

        public void Reset() => Idx = 0;
    }
}