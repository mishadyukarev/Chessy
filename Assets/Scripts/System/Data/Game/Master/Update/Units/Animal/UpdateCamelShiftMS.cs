using UnityEngine;

namespace Game.Game
{
    sealed class UpdateCamelShiftMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateCamelShiftMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitE(idx_0).Is(UnitTypes.Camel) && Es.UnitE(idx_0).Is(LevelTypes.First) && Es.UnitE(idx_0).Is(PlayerTypes.None))
                {
                    var randDir = Random.Range((int)DirectTypes.None + 1, (int)DirectTypes.End);

                    var idx_1 = CellWorker.GetIdxCellByDirect(idx_0, (DirectTypes)randDir);

                    if (Es.CellEs(idx_1).ParentE.IsActiveSelf.IsActive
                        && !Es.EnvironmentEs(idx_1).Mountain.HaveEnvironment
                        && !Es.UnitEs(idx_1).UnitE.HaveUnit)
                    {
                        Es.UnitE(idx_0).Shift(idx_1, false, Es);
                    }
                }
            }
        }
    }
}