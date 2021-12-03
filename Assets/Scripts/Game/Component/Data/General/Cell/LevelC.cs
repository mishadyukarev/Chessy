using System;

namespace Game.Game
{
    public struct LevelC : IUnitCell, ITWCellE
    {
        public LevelTypes Level { get; internal set; }
        public bool Is(LevelTypes level) => Level == level;


        internal void Set(LevelC levelC) => Level = levelC.Level;
        internal void Reset() => Level = LevelTypes.None;
    }
}