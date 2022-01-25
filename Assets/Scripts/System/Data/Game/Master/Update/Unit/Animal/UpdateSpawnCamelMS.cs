using UnityEngine;

namespace Game.Game
{
    public struct UpdateSpawnCamelMS : IEcsRunSystem
    {
        public void Run()
        {
            if (!WhereUnitsE.HaveUnit(UnitTypes.Camel))
            {
                byte idx_0 = (byte)Random.Range(0, CellEs.Idxs.Count);

                if (CellEs.IsActiveC(idx_0).IsActive)
                {
                    if (!CellUnitEntities.Else(idx_0).UnitC.Have && !CellEnvironmentEs.Resources(EnvironmentTypes.Mountain, idx_0).Have)
                    {
                        ref var unitC_0 = ref CellUnitEntities.Else(idx_0).UnitC;


                        bool haveNearUnit = false;

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (CellUnitEntities.Else(idx_1).UnitC.Have)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            CellUnitEntities.SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ToolWeaponTypes.None, LevelTypes.None), idx_0);
                            return;
                        }
                    }
                }
            }
        }
    }
}