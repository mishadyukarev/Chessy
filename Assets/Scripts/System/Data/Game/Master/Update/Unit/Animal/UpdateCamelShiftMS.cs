using UnityEngine;

namespace Game.Game
{
    public struct UpdateCamelShiftMS : IEcsRunSystem
    {
        public void Run()
        {
            if (WhereUnitsE.HaveUnit(UnitTypes.Camel))
            {
                var idx_0 = WhereUnitsE.IdxUnit(UnitTypes.Camel, LevelTypes.First, PlayerTypes.None);

                var randDir = Random.Range((int)DirectTypes.None + 1, (int)DirectTypes.End);

                var idx_1 = CellSpaceSupport.GetIdxCellByDirect(idx_0, (DirectTypes)randDir);

                if (CellEs.IsActiveC(idx_1).IsActive 
                    && !CellEnvironmentEs.Resources(EnvironmentTypes.Mountain, idx_1).Have 
                    && !CellUnitEs.Unit(idx_1).Have)
                {
                    CellUnitEs.Shift(idx_0, idx_1, false);
                }
            }
        }
    }
}