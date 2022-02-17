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
                if (Es.UnitTC(idx_0).Is(UnitTypes.Camel) && Es.UnitLevelTC(idx_0).Is(LevelTypes.First) && Es.UnitPlayerTC(idx_0).Is(PlayerTypes.None))
                {
                    var randDir = Random.Range((int)DirectTypes.None + 1, (int)DirectTypes.End);

                    var idx_1 = CellWorker.GetIdxCellByDirect(idx_0, (DirectTypes)randDir);

                    if (Es.CellEs(idx_1).IsActiveParentSelf
                        && !Es.EnvironmentEs(idx_1).MountainC.HaveAny
                        && !Es.UnitTC(idx_1).HaveUnit)
                    {
                        //Es.UnitE(idx_0).Shift(idx_1, false, Es);
                    }
                }
            }
        }
    }
}