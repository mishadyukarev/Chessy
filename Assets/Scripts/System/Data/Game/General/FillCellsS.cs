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
                    var xy_0 = CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (Es.CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Mountain))
                            {
                                Es.EnvMountainE(idx_0).SetRandomResources();
                                amountMountains++;
                            }

                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                                {
                                    Es.EnvAdultForestE(idx_0).SetRandomResources();
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                            {
                                Es.EnvAdultForestE(idx_0).SetRandomResources();
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
                            RiverEs(idx_0).SetStart( DirectTypes.Up);
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            RiverEs(idx_0).SetStart( DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            RiverEs(idx_0).SetStart( DirectTypes.Up);
                        }


                        foreach (var dir in CellEs(idx_0).RiverEs.Keys)
                        {
                            if (RiverEs(idx_0).HaveRive(dir).HaveRiver.Have)
                            {
                                var xy_next = CellWorker.GetXyCellByDirect(CellEs(idx_0).CellE.XyC.Xy, dir);
                                var idx_next = CellWorker.GetIdxCell(xy_next);

                                RiverEs(idx_next).RiverE.RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellWorker.GetXyCellByDirect(CellEs(idx_0).CellE.XyC.Xy, dir);
                            var idx_next = CellWorker.GetIdxCell(xy_next);

                            RiverEs(idx_next).RiverE.RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                Es.InventorResourcesEs.Resource(ResourceTypes.Food, PlayerTypes.Second).Set(999999);

                foreach (byte idx_0 in CellWorker.Idxs)
                {
                    var xy_0 = CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        EnvironmentEs(idx_0).Mountain.Destroy();
                        EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails);

                        UnitEs(idx_0).SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected, false), Es);
                    }

                    else if (x == 8 && y == 8)
                    {
                        EnvironmentEs(idx_0).Mountain.Destroy();
                        EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails);

                        BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.City, PlayerTypes.Second);
                        //Es.WhereBuildingEs.HaveBuild(BuildingTypes.City, PlayerTypes.Second, idx_0).HaveBuilding.Have = true;
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        EnvironmentEs(idx_0).Mountain.Destroy();

                        UnitEs(idx_0).SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second, ConditionUnitTypes.Protected, false), Es);

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