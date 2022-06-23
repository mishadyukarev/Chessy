using Chessy.Model.Model.Component;
using System.Collections.Generic;

namespace Chessy.Model.Model.Entity
{
    public struct ShiftUnitE
    {
        public readonly IdxsCellsC ForShift;
        public readonly NeedStepsC NeedStepsC;

        internal ShiftUnitE(in float[] needSteps, in HashSet<byte> cells)
        {
            NeedStepsC = new NeedStepsC(needSteps);
            ForShift = new IdxsCellsC(cells);
        }

    }
}