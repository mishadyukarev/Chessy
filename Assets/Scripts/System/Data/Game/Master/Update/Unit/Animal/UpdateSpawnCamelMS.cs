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
                    if (!CellUnitEs.Unit(idx_0).Have && !CellEnvironmentEs.Resources(EnvironmentTypes.Mountain, idx_0).Have)
                    {
                        ref var unitC_0 = ref CellUnitEs.Unit(idx_0);


                        bool haveNearUnit = false;

                        foreach (var idx_1 in CellSpaceSupport.GetIdxAround(idx_0))
                        {
                            if (CellUnitEs.Unit(idx_1).Have)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            CellUnitEs.SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ToolWeaponTypes.None, LevelTypes.None), idx_0);
                            return;
                        }
                    }
                }
            }
        }
    }
}