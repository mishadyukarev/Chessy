using UnityEngine;

namespace Game.Game
{
    sealed class UpdateCamelShiftMS : SystemCellAbstract, IEcsRunSystem
    {
        internal UpdateCamelShiftMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            if (Es.WhereUnitsEs.HaveUnit(UnitTypes.Camel))
            {
                var idx_0 = Es.WhereUnitsEs.IdxUnit(UnitTypes.Camel, LevelTypes.First, PlayerTypes.None);

                var randDir = Random.Range((int)DirectTypes.None + 1, (int)DirectTypes.End);

                var idx_1 = CellEs.GetIdxCellByDirect(idx_0, (DirectTypes)randDir);

                if (CellEs.ParentE(idx_1).IsActiveSelf.IsActive
                    && !EnvironmentEs.Mountain( idx_1).HaveEnvironment
                    && !UnitEs.Main(idx_1).HaveUnit(UnitStatEs))
                {
                    UnitEs.Main(idx_0).Shift(idx_1, Es);
                }
            }
        }
    }
}