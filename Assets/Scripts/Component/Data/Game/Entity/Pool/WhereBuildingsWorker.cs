using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct WhereBuildingsWorker
    {
        readonly CellEs[] _cellEs;

        public bool TryGetBuilding(in BuildingTypes build, in PlayerTypes owner, out byte idx)
        {
            for (idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (_cellEs[idx].BuildEs.BuildingE.Is(build) && _cellEs[idx].BuildEs.BuildingE.Is(owner))
                {
                    return true;
                }
            }
            return false;
        }

        public WhereBuildingsWorker(in CellEs[] cellEs)
        {
            _cellEs = cellEs;
        }
    }
}