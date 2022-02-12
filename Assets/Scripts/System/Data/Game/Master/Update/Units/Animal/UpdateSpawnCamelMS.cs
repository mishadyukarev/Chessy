using UnityEngine;

namespace Game.Game
{
    sealed class UpdateSpawnCamelMS : SystemCellAbstract, IEcsRunSystem
    {
        public UpdateSpawnCamelMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var haveCamel = false;

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitE(idx_0).Is(UnitTypes.Camel))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                byte idx_0 = (byte)Random.Range(0, CellWorker.Idxs.Count);

                if (CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                {
                    if (!UnitEs(idx_0).UnitE.HaveUnit && !EnvironmentEs(idx_0).Mountain.HaveEnvironment)
                    {
                        bool haveNearUnit = false;

                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (UnitEs(idx_1).UnitE.HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            Es.UnitEs(idx_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}