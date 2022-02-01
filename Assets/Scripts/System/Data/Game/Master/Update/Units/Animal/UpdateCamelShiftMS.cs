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

                var idx_1 = CellEsWorker.GetIdxCellByDirect(idx_0, (DirectTypes)randDir);

                if (CellEs(idx_1).ParentE.IsActiveSelf.IsActive
                    && !EnvironmentEs(idx_1).Mountain.HaveEnvironment
                    && !UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                {
                    UnitEs(idx_0).MainE.Shift(idx_1, Es);
                }
            }
        }
    }
}