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
                var amountMountains = 0;

                for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
                {
                    var xy_0 = Es.CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (Es.CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                Es.MountainE(idx_0).SetRandomResources();
                                amountMountains++;
                            }

                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    Es.AdultForestE(idx_0).SetRandomResources();
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                Es.AdultForestE(idx_0).SetRandomResources();
                            }
                            //else
                            //{
                            //    random = UnityEngine.Random.Range(1, 100);
                            //    if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Fertilizer))
                            //    {
                            //        EnvironmentEs(idx_0).Fertilizer.SetNewRandom();
                            //    }
                            //}
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            Es.RiverEs(idx_0).SetStart( DirectTypes.Up);
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            Es.RiverEs(idx_0).SetStart( DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            Es.RiverEs(idx_0).SetStart( DirectTypes.Up);
                        }


                        foreach (var dir in Es.CellEs(idx_0).RiverEs.Keys)
                        {
                            if (Es.RiverEs(idx_0).HaveRive(dir).HaveRiver.Have)
                            {
                                var xy_next = CellWorker.GetXyCellByDirect(Es.CellEs(idx_0).CellE.XyC.Xy, dir);
                                var idx_next = CellWorker.GetIdxCell(xy_next);

                                Es.RiverEs(idx_next).RiverE.RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellWorker.GetXyCellByDirect(Es.CellEs(idx_0).CellE.XyC.Xy, dir);
                            var idx_next = CellWorker.GetIdxCell(xy_next);

                            Es.RiverEs(idx_next).RiverE.RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                Es.InventorResourcesEs.Resource(ResourceTypes.Food, PlayerTypes.Second).ResourceC.Set(999999);

                foreach (byte idx_0 in CellWorker.Idxs)
                {
                    var xy_0 = Es.CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        Es.EnvironmentEs(idx_0).Mountain.SetZeroResources();
                        Es.EnvironmentEs(idx_0).AdultForest.Destroy(Es.TrailEs(idx_0).Trails);

                        Es.UnitE(idx_0).SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected, false), Es);
                    }

                    else if (x == 8 && y == 8)
                    {
                        Es.MountainE(idx_0).SetZeroResources();
                        Es.AdultForestE(idx_0).Destroy(Es.TrailEs(idx_0).Trails);

                        Es.BuildingE(idx_0).SetNew(BuildingTypes.City, PlayerTypes.Second);
                        //Es.WhereBuildingEs.HaveBuild(BuildingTypes.City, PlayerTypes.Second, idx_0).HaveBuilding.Have = true;
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Es.EnvironmentEs(idx_0).Mountain.SetZeroResources();

                        Es.UnitE(idx_0).SetNewPawn((LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected), Es);

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            Es.UnitEs(idx_0).ExtraToolWeaponE.SetNew(ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            Es.UnitEs(idx_0).ExtraToolWeaponE.SetNew(ToolWeaponTypes.Shield, LevelTypes.First);
                        }
                    }
                }
            }
        }
    }
}