using System;
using System.Collections.Generic;

namespace Chessy.Model
{
    public readonly struct DataFromViewC
    {
        readonly Action[] _sound0;
        readonly Action[] _sound1;
        readonly bool[] _isBorder;
        readonly int[] _idCells;
        readonly Dictionary<byte, Action[]> _animationsCells;

        internal Action SoundAction(in ClipTypes clipT) => _sound0[(byte)clipT];
        internal Action SoundAction(in AbilityTypes abilityT) => _sound1[(byte)abilityT];
        internal bool IsBorder(in byte cellIdx) => _isBorder[cellIdx];
        internal int IdCell(in byte cellIdx) => _idCells[cellIdx];
        internal Action AnimationCell(in byte cellIdx, in AnimationCellTypes animationCellT) => _animationsCells[cellIdx][(byte)animationCellT];

        public DataFromViewC(in (Action[], Action[], bool[], int[], Dictionary<byte, Action[]>) data)
        {
            _sound0 = data.Item1;
            _sound1 = data.Item2;
            _isBorder = data.Item3;
            _idCells = data.Item4;
            _animationsCells = data.Item5;
        }
    }
}