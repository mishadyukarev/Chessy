using System;

namespace Game.Game
{
    public struct LevelTC
    {
        public LevelTypes Level;
        public bool Is(params LevelTypes[] levels)
        {
            if (levels == default) throw new Exception();

            foreach (var level in levels) if (level == Level) return true;
            return false;
        }

        public LevelTC(in LevelTypes level) => Level = level;

        public bool TryUpgrade()
        {
            if (Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) return false;

            Level = LevelTypes.Second;
            return true;
        }
    }
}