using UnityEngine;

namespace Game.Game
{
    public struct UpdateSpawnCamelMS : IEcsRunSystem
    {
        public void Run()
        {
            if (!WhereUnitsE.HaveUnit(UnitTypes.Camel))
            {
                byte idx_0 = (byte)Random.Range(0, Entities.CellEs.Idxs.Count);

                if (Entities.CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
                {
                    if (!Entities.CellEs.UnitEs.Else(idx_0).UnitC.Have && !Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_0).Resources.Have)
                    {
                        ref var unitC_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;


                        bool haveNearUnit = false;

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (Entities.CellEs.UnitEs.Else(idx_1).UnitC.Have)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            Entities.CellEs.UnitEs.SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ToolWeaponTypes.None, LevelTypes.None), idx_0);
                            return;
                        }
                    }
                }
            }
        }
    }
}