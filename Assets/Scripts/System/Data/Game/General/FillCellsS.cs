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

                foreach (byte idx_0 in CellEs.Idxs)
                {
                    var xy_0 = CellEs.CellE(idx_0).XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (CellEs.ParentE(idx_0).IsActiveSelf.IsActive)
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

                            CellEs.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = CellEs.GetIdxCell(item.Value);
                                //WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref CellEs.RiverEs.River(idx_0).RiverTC;


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            CellEs.RiverEs.SetStart(idx_0, DirectTypes.Up);
                        }


                        foreach (var dir in CellEs.RiverEs.Keys)
                        {
                            if (CellEs.RiverEs.HaveRive(dir, idx_0).HaveRiver.Have)
                            {
                                var xy_next = CellEs.GetXyCellByDirect(CellEs.CellE(idx_0).XyC.Xy, dir);
                                var idx_next = CellEs.GetIdxCell(xy_next);

                                CellEs.RiverEs.River(idx_next).RiverTC.River = RiverTypes.End;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellEs.GetXyCellByDirect(CellEs.CellE(idx_0).XyC.Xy, dir);
                            var idx_next = CellEs.GetIdxCell(xy_next);

                            CellEs.RiverEs.River(idx_next).RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                Es.InventorResourcesEs.Resource(ResourceTypes.Food, PlayerTypes.Second).Resources.Amount = 999999;

                foreach (byte idx_0 in CellEs.Idxs)
                {
                    var xy_0 = CellEs.CellE(idx_0).XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        CellEs.EnvironmentEs.Mountain(idx_0).Destroy(Es.WhereEnviromentEs);
                        CellEs.EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);

                        UnitEs.Main(idx_0).SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected, false), Es);
                    }

                    else if (x == 8 && y == 8)
                    {
                        CellEs.EnvironmentEs.Mountain(idx_0).Destroy(Es.WhereEnviromentEs);
                        CellEs.EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);

                        BuildEs.BuildingE(idx_0).SetNew(BuildingTypes.City, PlayerTypes.Second, BuildEs, Es.WhereBuildingEs);
                        Es.WhereBuildingEs.HaveBuild(BuildingTypes.City, PlayerTypes.Second, idx_0).HaveBuilding.Have = true;
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        CellEs.EnvironmentEs.Mountain(idx_0).Destroy(Es.WhereEnviromentEs);

                        UnitEs.Main(idx_0).SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected, false), Es);

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            UnitEs.ToolWeapon(idx_0).SetNew(ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            UnitEs.ToolWeapon(idx_0).SetNew(ToolWeaponTypes.Shield, LevelTypes.First);
                        }
                    }
                }
            }
        }
    }
}