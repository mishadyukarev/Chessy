﻿using System;

namespace Game.Game
{
    public struct LevelC : IUnitCell
    {
        private LevelTypes _levelUnit;
        public LevelTypes Level => _levelUnit;
        public bool Is(LevelTypes level) => _levelUnit == level;


        public void SetLevel(LevelTypes level)
        {
            if (level == LevelTypes.None) throw new Exception();
            //if (_levelUnit == level) throw new Exception();

            _levelUnit = level;
        }
        public void Sync(LevelTypes level) => _levelUnit = level;   
    }
}