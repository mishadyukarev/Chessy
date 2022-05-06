using System;

namespace Chessy.Game
{
    public readonly struct DataFromViewC
    {
        readonly Action[] _sound0;
        readonly Action[] _sound1;
        readonly bool[] _isActiveParents;
        readonly int[] _idCells;

        public Action SoundAction(in ClipTypes clipT) => _sound0[(byte)clipT];
        public Action SoundAction(in AbilityTypes abilityT) => _sound1[(byte)abilityT];
        public bool IsActiveParent(in byte cellIdx) => _isActiveParents[cellIdx];
        public int IdCell(in byte cellIdx) => _idCells[cellIdx];

        public DataFromViewC(in (Action[], Action[], bool[], int[]) data)
        {
            _sound0 = data.Item1;
            _sound1 = data.Item2;
            _isActiveParents = data.Item3;
            _idCells = data.Item4;
        }
    }
}