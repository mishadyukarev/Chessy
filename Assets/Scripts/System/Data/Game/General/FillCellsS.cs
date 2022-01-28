using Game.Common;
using Photon.Pun;
using System.Collections.Generic;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellRiverEs;

namespace Game.Game
{
    public class FillCellsS
    {
        public FillCellsS()
        {

            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in Entities.CellEs.Idxs)
                {
                    var xy_0 = Entities.CellEs.CellE(idx_0).XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (Entities.CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
                    {
                        if (y >= 4 && y <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Mountain))
                            {
                                Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_0).SetNew();
                                EntWhereEnviroments.HaveEnv(EnvironmentTypes.Mountain, idx_0).Have = true;
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                                {
                                    Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).SetNew();
                                    EntWhereEnviroments.HaveEnv(EnvironmentTypes.AdultForest, idx_0).Have = true;
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Hill))
                                {
                                    Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx_0).SetNew();
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                            {
                                Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).SetNew();
                                EntWhereEnviroments.HaveEnv(EnvironmentTypes.AdultForest, idx_0).Have = true;
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Fertilizer))
                                {
                                    Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).SetNew();
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            Entities.WindE.CenterCloud.Idx = idx_0;

                            CellSpaceSupport.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = Entities.CellEs.IdxCell(item.Value);
                                //WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref Entities.CellEs.RiverEs.River(idx_0).RiverTC;


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            Entities.CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            Entities.CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            Entities.CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up);
                        }


                        foreach (var dir in Entities.CellEs.RiverEs.Keys)
                        {
                            if (Entities.CellEs.RiverEs.HaveRive(dir, idx_0).HaveRiver.Have)
                            {
                                var xy_next = CellSpaceSupport.GetXyCellByDirect(Entities.CellEs.CellE(idx_0).XyC.Xy, dir);
                                var idx_next = Entities.CellEs.IdxCell(xy_next);

                                Entities.CellEs.RiverEs.River(idx_next).RiverTC.River = RiverTypes.End;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellSpaceSupport.GetXyCellByDirect(Entities.CellEs.CellE(idx_0).XyC.Xy, dir);
                            var idx_next = Entities.CellEs.IdxCell(xy_next);

                            Entities.CellEs.RiverEs.River(idx_next).RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                InventorResourcesE.Resource(ResourceTypes.Food, PlayerTypes.Second).Amount = 999999;

                foreach (byte idx_0 in Entities.CellEs.Idxs)
                {
                    var xy_0 = Entities.CellEs.CellE(idx_0).XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                    ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
                    ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

                    ref var hp_0 = ref Entities.CellEs.UnitEs.Hp(idx_0).AmountC;
                    ref var condUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).ConditionC;
                    ref var waterUnit_0 = ref Entities.CellEs.UnitEs.Water(idx_0).AmountC;


                    ref var tw_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeaponC;
                    ref var twLevel_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).LevelC;
                    ref var protShiel_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).Protection;

                    ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
                    ref var ownBuild_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;

                    if (x == 7 && y == 8)
                    {
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_0).Remove();
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Remove();

                        Entities.CellEs.UnitEs.SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second, default, default), idx_0);

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }

                    else if (x == 8 && y == 8)
                    {
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_0).Remove();
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Remove();

                        Entities.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.City, PlayerTypes.Second);
                        Entities.WhereBuildingEs.HaveBuild(BuildingTypes.City, PlayerTypes.Second, idx_0).HaveBuilding.Have = true;
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_0).Remove();

                        Entities.CellEs.UnitEs.SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second, default, default), idx_0);


                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            Entities.CellEs.UnitEs.SetNew(idx_0, ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            Entities.CellEs.UnitEs.SetNew(idx_0, ToolWeaponTypes.Shield, LevelTypes.First);
                        }

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }
                }
            }
        }
    }
}