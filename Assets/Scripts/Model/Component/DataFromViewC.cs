using Chessy.Model.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.Component
{
    public readonly struct DataFromViewC
    {
        readonly Action[] _sound0;
        readonly Action[] _sound1;
        readonly bool[] _isBorder;
        readonly int[] _idCells;
        readonly Vector3[] _possitionsCells;
        readonly Dictionary<byte, Action[]> _animationsCells;
        readonly Action<byte>[] _animations;

        internal Action SoundAction(in ClipTypes clipT) => _sound0[(byte)clipT];
        internal Action SoundAction(in AbilityTypes abilityT) => _sound1[(byte)abilityT];
        internal bool IsBorder(in byte cellIdx) => _isBorder[cellIdx];
        internal int IdCell(in byte cellIdx) => _idCells[cellIdx];
        internal Vector3 PossitionCell(in byte cellIdx) => _possitionsCells[cellIdx];
        internal Action AnimationCell(in byte cellIdx, in AnimationCellTypes animationCellT) => _animationsCells[cellIdx][(byte)animationCellT];
        internal Action<byte> AnimationCellDirectly(in CellAnimationDirectlyTypes cellAnimationT) => _animations[(byte)cellAnimationT];

        public DataFromViewC(in (Action[], Action[], bool[], int[], Vector3[], Dictionary<byte, Action[]>, Action<byte>[]) data)
        {
            _sound0 = data.Item1;
            _sound1 = data.Item2;
            _isBorder = data.Item3;
            _idCells = data.Item4;
            _possitionsCells = data.Item5;
            _animationsCells = data.Item6;
            _animations = data.Item7;
        }
    }
}