using System.Collections.Generic;

namespace Chessy.Game.Model.Entity
{
    public struct ShiftUnitE
    {
        readonly float[] _needStepsForShift;
        public float NeedSteps(in byte idx_cell) => _needStepsForShift[idx_cell];

        public readonly IdxsCellsC ForShift;

        internal ShiftUnitE(in float[] needSteps, in HashSet<byte> cells)
        {
            _needStepsForShift = needSteps;
            ForShift = new IdxsCellsC(cells);
        }

        internal void Set(in byte idx_cell, in float steps) => _needStepsForShift[idx_cell] = steps;
    }
}