using System;

namespace Chessy.Game
{
    public struct LevelTC
    {
        public LevelTypes LevelT { get; internal set; }
        public bool Is(params LevelTypes[] levels)
        {
            if (levels == default) throw new Exception();

            foreach (var level in levels) if (level == LevelT) return true;
            return false;
        }

        internal bool TryUpgrade()
        {
            if (Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) return false;

            LevelT = LevelTypes.Second;
            return true;
        }
    }
}