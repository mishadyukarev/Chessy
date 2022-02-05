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
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitMainE(idx_0).Is(UnitTypes.Camel) && Es.UnitLevelE(idx_0).Is(LevelTypes.First) && Es.UnitOwnerE(idx_0).Is(PlayerTypes.None))
                {
                    var randDir = Random.Range((int)DirectTypes.None + 1, (int)DirectTypes.End);

                    var idx_1 = CellWorker.GetIdxCellByDirect(idx_0, (DirectTypes)randDir);

                    if (CellEs(idx_1).ParentE.IsActiveSelf.IsActive
                        && !EnvironmentEs(idx_1).Mountain.HaveEnvironment
                        && !UnitEs(idx_1).MainE.HaveUnit)
                    {
                        UnitEs(idx_0).MainE.Shift(idx_1, Es);
                    }
                }
            }
        }
    }
}