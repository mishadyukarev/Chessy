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

                if (CellEs.Parent(idx_1).IsActiveSelf.IsActive 
                    && !CellEnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_1).Resources.Have 
                    && !CellUnitEs.Else(idx_1).UnitC.Have)
                {
                    CellUnitEs.Shift(idx_0, idx_1, false);
                }
            }
        }
    }
}