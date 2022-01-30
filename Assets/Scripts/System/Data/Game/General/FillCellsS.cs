using Game.Common;
using Photon.Pun;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class FillCellsS : SystemCellAbstract
    {
        public FillCellsS(in Entities ents) : base(ents)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in Es.CellEs.Idxs)
                {
                    var xy_0 = Es.CellEs.CellE(idx_0).XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (Es.CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
                    {
                        if (y >= 4 && y <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Mountain))
                            {
                                EnvironmentEs.Mountain(idx_0).SetNew(Es.WhereEnviromentEs);
                                Es.WhereEnviromentEs.Info(EnvironmentTypes.Mountain, idx_0).HaveEnv.Have = true;
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                                {
                                    EnvironmentEs.AdultForest(idx_0).SetNew(Es.WhereEnviromentEs);
                                    Es.WhereEnviromentEs.Info(EnvironmentTypes.AdultForest, idx_0).HaveEnv.Have = true;
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Hill))
                                {
                                    EnvironmentEs.Hill(idx_0).SetNew(Es.WhereEnviromentEs);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                            {
                                EnvironmentEs.AdultForest(idx_0).SetNew(Es.WhereEnviromentEs);
                                Es.WhereEnviromentEs.Info(EnvironmentTypes.AdultForest, idx_0).HaveEnv.Have = true;
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Fertilizer))
                                {
                                    EnvironmentEs.Fertilizer(idx_0).SetNew();
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            Es.WindE.CenterCloud.Idx = idx_0;

                            Es.CellEs.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = Es.CellEs.GetIdxCell(item.Value);
                                //WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref Es.CellEs.RiverEs.River(idx_0).RiverTC;


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            Es.CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            Es.CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            Es.CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up);
                        }


                        foreach (var dir in Es.CellEs.RiverEs.Keys)
                        {
                            if (Es.CellEs.RiverEs.HaveRive(dir, idx_0).HaveRiver.Have)
                            {
                                var xy_next = Es.CellEs.GetXyCellByDirect(Es.CellEs.CellE(idx_0).XyC.Xy, dir);
                                var idx_next = Es.CellEs.GetIdxCell(xy_next);

                                Es.CellEs.RiverEs.River(idx_next).RiverTC.River = RiverTypes.End;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = Es.CellEs.GetXyCellByDirect(Es.CellEs.CellE(idx_0).XyC.Xy, dir);
                            var idx_next = Es.CellEs.GetIdxCell(xy_next);

                            Es.CellEs.RiverEs.River(idx_next).RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                Es.InventorResourcesEs.Resource(ResourceTypes.Food, PlayerTypes.Second).Resources.Amount = 999999;

                foreach (byte idx_0 in Es.CellEs.Idxs)
                {
                    var xy_0 = Es.CellEs.CellE(idx_0).XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                    ref var levUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
                    ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;

                    ref var hp_0 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health;
                    ref var condUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).ConditionC;
                    ref var waterUnit_0 = ref Es.CellEs.UnitEs.StatEs.Water(idx_0).Water;


                    ref var tw_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeapon;
                    ref var twLevel_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).LevelTW;
                    ref var protShiel_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).Protection;

                    ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;
                    ref var ownBuild_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;

                    if (x == 7 && y == 8)
                    {
                        Es.CellEs.EnvironmentEs.Mountain(idx_0).Destroy(Es.WhereEnviromentEs);
                        Es.CellEs.EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);

                        UnitEs.SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second), Es, idx_0);

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }

                    else if (x == 8 && y == 8)
                    {
                        Es.CellEs.EnvironmentEs.Mountain(idx_0).Destroy(Es.WhereEnviromentEs);
                        Es.CellEs.EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);

                        Es.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.City, PlayerTypes.Second);
                        Es.WhereBuildingEs.HaveBuild(BuildingTypes.City, PlayerTypes.Second, idx_0).HaveBuilding.Have = true;
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Es.CellEs.EnvironmentEs.Mountain(idx_0).Destroy(Es.WhereEnviromentEs);

                        UnitEs.SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second), Es, idx_0);


                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            Es.CellEs.UnitEs.ToolWeapon(idx_0).SetNew(ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            Es.CellEs.UnitEs.ToolWeapon(idx_0).SetNew(ToolWeaponTypes.Shield, LevelTypes.First);
                        }

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }
                }
            }
        }
    }
}