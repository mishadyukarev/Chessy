using System;

namespace Game.Game
{
    public readonly struct CellUnitShiftE
    {
        readonly Action<byte, byte> _shift;

        public CellUnitShiftE(in Action<byte, byte> action) : this()
        {
            _shift = action;
        }

        public void Shift(in byte idx_from, in byte idx_to) => _shift(idx_from, idx_to);
    }
}