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
            if (!Es.WhereUnitsEs.HaveUnit(UnitTypes.Camel))
            {
                byte idx_0 = (byte)Random.Range(0, CellWorker.Idxs.Count);

                if (CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                {
                    if (!UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)) && !EnvironmentEs(idx_0).Mountain.HaveEnvironment)
                    {
                        bool haveNearUnit = false;

                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            UnitEs(idx_0).MainE.SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}