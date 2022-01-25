using ECS;
using Game.Common;
using Photon.Pun;
using System.Collections.Generic;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellRiverE;
using static Game.Game.EntityCellCloudPool;

namespace Game.Game
{
    public class FillCellsS
    {
        public FillCellsS()
        {

            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in Idxs)
                {
                    var xy_0 = Cell<XyC>(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var cloud_0 = ref Cloud<HaveEffectC>(idx_0);

                    if (IsActiveC(idx_0).IsActive)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Mountain))
                            {
                                SetNew(EnvironmentTypes.Mountain, idx_0);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                                {
                                    SetNew(EnvironmentTypes.AdultForest, idx_0);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Hill))
                                {
                                    SetNew(EnvironmentTypes.Hill, idx_0);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                            {
                                SetNew(EnvironmentTypes.AdultForest, idx_0);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Fertilizer))
                                {
                                    SetNew(EnvironmentTypes.Fertilizer, idx_0);
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            cloud_0.Have = true;
                            CenterCloudEnt.CenterCloud<IdxC>().Idx = idx_0;

                            CellSpaceSupport.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = IdxCell(item.Value);
                                //WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref River(idx_0);


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            CellRiverE.SetStart(idx_0, DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            CellRiverE.SetStart(idx_0, DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            CellRiverE.SetStart(idx_0, DirectTypes.Up);
                        }


                        foreach (var dir in CellRiverE.Keys)
                        {
                            if (CellRiverE.HaveRive(dir, idx_0).Have)
                            {
                                var xy_next = CellSpaceSupport.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                                var idx_next = IdxCell(xy_next);

                                CellRiverE.River(idx_next).River = RiverTypes.End;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellSpaceSupport.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                            var idx_next = IdxCell(xy_next);

                            CellRiverE.River(idx_next).River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                InventorResourcesE.Resource(ResourceTypes.Food, PlayerTypes.Second).Amount = 999999;

                foreach (byte idx_0 in Idxs)
                {
                    var xy_0 = Cell<XyC>(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var unit_0 = ref CellUnitEntities.Else(idx_0).UnitC;
                    ref var levUnit_0 = ref CellUnitEntities.Else(idx_0).LevelC;
                    ref var ownUnit_0 = ref CellUnitEntities.Else(idx_0).OwnerC;

                    ref var hp_0 = ref CellUnitEntities.Hp(idx_0).AmountC;
                    ref var condUnit_0 = ref CellUnitEntities.Else(idx_0).ConditionC;
                    ref var waterUnit_0 = ref CellUnitEntities.Water(idx_0).AmountC;


                    ref var tw_0 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_0);
                    ref var twLevel_0 = ref CellUnitTWE.UnitTW<LevelTC>(idx_0);
                    ref var protShiel_0 = ref CellUnitTWE.UnitTW<ProtectionC>(idx_0);

                    ref var build_0 = ref Build<BuildingTC>(idx_0);
                    ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

                    if (x == 7 && y == 8)
                    {
                        Remove(EnvironmentTypes.Mountain, idx_0);
                        Remove(EnvironmentTypes.AdultForest, idx_0);

                        CellUnitEntities.SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second, default, default), idx_0);

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }

                    else if (x == 8 && y == 8)
                    {
                        Remove(EnvironmentTypes.Mountain, idx_0);
                        Remove(EnvironmentTypes.AdultForest, idx_0);


                        CellBuildE.SetNew(BuildingTypes.City, PlayerTypes.Second, idx_0);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Remove(EnvironmentTypes.Mountain, idx_0);

                        CellUnitEntities.SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second, default, default), idx_0);


                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            CellUnitTWE.SetNew(idx_0, ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            CellUnitTWE.SetNew(idx_0, ToolWeaponTypes.Shield, LevelTypes.First);
                        }

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }
                }
            }
        }
    }
}