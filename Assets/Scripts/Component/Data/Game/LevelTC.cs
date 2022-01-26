namespace Game.Game
{
    public struct LevelTC : IUnitCellE, ISelectedUnitE
    {
        public LevelTypes Level;
        public bool Is(LevelTypes level) => Level == level;

        public LevelTC(in LevelTypes level) => Level = level;

        public void Reset() => Level = LevelTypes.None;
    }
}