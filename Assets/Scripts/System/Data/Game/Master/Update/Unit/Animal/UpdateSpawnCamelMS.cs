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
                byte idx_0 = (byte)Random.Range(0, Es.CellEs.Idxs.Count);

                if (CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
                {
                    if (!UnitEs.Main(idx_0).UnitC.Have && !EnvironmentEs.Mountain( idx_0).HaveEnvironment)
                    {
                        ref var unitC_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;


                        bool haveNearUnit = false;

                        foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                        {
                            if (UnitEs.Main(idx_1).UnitC.Have)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            UnitEs.SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None), Es, idx_0);
                            return;
                        }
                    }
                }
            }
        }
    }
}