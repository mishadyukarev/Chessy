﻿namespace Chessy.Model.Component
{
    public sealed class IsStartedCellC
    {
        readonly bool[] _isStartedCell;
        public bool IsStartedCellForPlayer(in PlayerTypes playerT) => _isStartedCell[(byte)playerT];
        public bool IsStartedCellForPlayer(in byte player_byte) => _isStartedCell[player_byte];

        internal IsStartedCellC(in bool[] isStarted) => _isStartedCell = isStarted;
    }
}