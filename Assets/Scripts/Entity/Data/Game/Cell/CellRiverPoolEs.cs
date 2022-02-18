﻿using System;

namespace Game.Game
{
    public struct CellRiverPoolEs
    {
        readonly bool[] _haveRive;
        public ref bool HaveRive(in DirectTypes dir) => ref _haveRive[(byte)dir - 1];

        public CellRiverE RiverE;

        public CellRiverPoolEs(in bool def) : this()
        {
            _haveRive = new bool[(byte)DirectTypes.End - 1];
        }

        public void SetStart(params DirectTypes[] dirs)
        {
            if (dirs == default) throw new Exception();

            RiverE.RiverTC.River = RiverTypes.Start;
            foreach (var item in dirs) HaveRive(item) = true;
        }
    }
}