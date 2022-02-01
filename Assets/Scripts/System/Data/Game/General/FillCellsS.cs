using Game.Common;
using Photon.Pun;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class FillCellsS : SystemAbstract
    {
        public FillCellsS(in Entities ents) : base(ents)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in CellEsWorker.Idxs)
                {
                    var xy_0 = CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                    {
                        if (y >= 4 && y <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Mountain))
                            {
                                EnvironmentEs(idx_0).Mountain.SetNew(Es.WhereEnviromentEs);
                                Es.WhereEnviromentEs.Info(EnvironmentTypes.Mountain, idx_0).HaveEnv.Have = true;
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                                {
                                    EnvironmentEs(idx_0).AdultForest.SetNew(Es.WhereEnviromentEs);
                                    Es.WhereEnviromentEs.Info(EnvironmentTypes.AdultForest, idx_0).HaveEnv.Have = true;
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Hill))
                                {
                                    EnvironmentEs(idx_0).Hill.SetNew(Es.WhereEnviromentEs);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                            {
                                EnvironmentEs(idx_0).AdultForest.SetNew(Es.WhereEnviromentEs);
                                Es.WhereEnviromentEs.Info(EnvironmentTypes.AdultForest, idx_0).HaveEnv.Have = true;
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Fertilizer))
                                {
                                    EnvironmentEs(idx_0).Fertilizer.SetNew();
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            Es.WindE.CenterCloud.Idx = idx_0;

                            CellEsWorker.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = CellEsWorker.GetIdxCell(item.Value);
                                //WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref RiverEs(idx_0).River.RiverTC;


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            RiverEs(idx_0).SetStart( DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            RiverEs(idx_0).SetStart( DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            RiverEs(idx_0).SetStart( DirectTypes.Up);
                        }


                        foreach (var dir in CellEs(idx_0).RiverEs.Keys)
                        {
                            if (RiverEs(idx_0).HaveRive(dir).HaveRiver.Have)
                            {
                                var xy_next = CellEsWorker.GetXyCellByDirect(CellEs(idx_0).CellE.XyC.Xy, dir);
                                var idx_next = CellEsWorker.GetIdxCell(xy_next);

                                RiverEs(idx_next).River.RiverTC.River = RiverTypes.End;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellEsWorker.GetXyCellByDirect(CellEs(idx_0).CellE.XyC.Xy, dir);
                            var idx_next = CellEsWorker.GetIdxCell(xy_next);

                            RiverEs(idx_next).River.RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                Es.InventorResourcesEs.Resource(ResourceTypes.Food, PlayerTypes.Second).Resources.Amount = 999999;

                foreach (byte idx_0 in CellEsWorker.Idxs)
                {
                    var xy_0 = CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        EnvironmentEs(idx_0).Mountain.Destroy(Es.WhereEnviromentEs);
                        EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails, Es.WhereEnviromentEs);

                        UnitEs(idx_0).MainE.SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected, false), Es);
                    }

                    else if (x == 8 && y == 8)
                    {
                        EnvironmentEs(idx_0).Mountain.Destroy(Es.WhereEnviromentEs);
                        EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails, Es.WhereEnviromentEs);

                        BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.City, PlayerTypes.Second, BuildEs(idx_0), Es.WhereBuildingEs);
                        Es.WhereBuildingEs.HaveBuild(BuildingTypes.City, PlayerTypes.Second, idx_0).HaveBuilding.Have = true;
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        EnvironmentEs(idx_0).Mountain.Destroy(Es.WhereEnviromentEs);

                        UnitEs(idx_0).MainE.SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected, false), Es);

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            UnitEs(idx_0).ToolWeaponE.SetNew(ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            UnitEs(idx_0).ToolWeaponE.SetNew(ToolWeaponTypes.Shield, LevelTypes.First);
                        }
                    }
                }
            }
        }
    }
}