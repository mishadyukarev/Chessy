namespace Game.Game
{
    public struct LevelTC : IUnitCellE, ITWCellE, ISelectedUnitE
    {
        public LevelTypes Level;
        public bool Is(LevelTypes level) => Level == level;


        internal void Set(LevelTC levelC) => Level = levelC.Level;
        internal void Reset() => Level = LevelTypes.None;
    }
}