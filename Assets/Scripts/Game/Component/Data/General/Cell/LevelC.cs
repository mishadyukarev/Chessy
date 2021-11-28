using System;

namespace Game.Game
{
    public struct LevelC : IUnitCell, ITWCellE
    {
        LevelTypes _level;

        public LevelTypes Level => _level;
        public bool Is(LevelTypes level) => _level == level;


        public void Set(LevelTypes level)
        {
            if (level == LevelTypes.None) throw new Exception();
            //if (_levelUnit == level) throw new Exception();

            _level = level;
        }
        public void Set(LevelC levelC)
        {
            _level = levelC._level;
        }
        public void Sync(LevelTypes level) => _level = level;   
    }
}