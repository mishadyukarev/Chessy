﻿using System;

namespace Chessy.Game
{
    public sealed class CellRiverE
    {
        public RiverTC RiverTC;

        readonly bool[] _haveRive = new bool[(byte)DirectTypes.End - 1];
        public ref bool HaveRive(in DirectTypes dir) => ref _haveRive[(byte)dir - 1];

        public void SetStart(params DirectTypes[] dirs)
        {
            if (dirs == default) throw new Exception();

            RiverTC.River = RiverTypes.Start;
            foreach (var item in dirs) HaveRive(item) = true;
        }
    }
}