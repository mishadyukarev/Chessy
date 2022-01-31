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
                byte idx_0 = (byte)Random.Range(0, CellEs.Idxs.Count);

                if (CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
                {
                    if (!UnitEs.Main(idx_0).UnitTC.Have && !EnvironmentEs.Mountain( idx_0).HaveEnvironment)
                    {
                        bool haveNearUnit = false;

                        foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                        {
                            if (UnitEs.Main(idx_1).UnitTC.Have)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            UnitEs.Main(idx_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}