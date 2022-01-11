using ECS;
using Game.Common;
using Photon.Pun;
using System.Collections.Generic;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellCloudPool;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityCellRiverPool;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct EntitiesManager
    {
        public EntitiesManager(in WorldEcs gameW)
        {
            #region View

            CanvasC.SetCurZone(SceneTypes.Game);

            new EntityVPool(gameW, out var actions, out var sounds0, out var sounds1);

            var leftZone = CanvasC.FindUnderCurZone("LeftZone").transform;
            new EntityLeftCityUIPool(gameW, leftZone);
            new EntityLeftEnvUIPool(gameW, leftZone);

            var centerZone = CanvasC.FindUnderCurZone("CenterZone").transform;
            new EntityCenterUIPool(gameW, centerZone);
            new EntityCenterHeroUIPool(gameW, centerZone);
            new EntityCenterFriendUIPool(gameW, centerZone);
            new EntityCenterPickUpgUIPool(gameW, centerZone);
            new EntityCenterHintUIPool(gameW, centerZone);
            new EntityCenterSelectorUIPool(gameW, centerZone);
            new EntityCenterKingUIPool(gameW, centerZone);

            new EntityUpUIPool(gameW);
            new EntityDownUIPool(gameW);
            new EntityRightUIPool(gameW);


            new EntityCellVPool(gameW, CellValues.X_AMOUNT, CellValues.Y_AMOUNT);

            #endregion



            var isActiveCells = new bool[CellValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveCells[idx] = EntityCellVPool.Cell<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityCellVPool.Cell<CellVC>(idx).InstanceID;
            }

            var namesMethods = RpcS.NamesMethods;


            #region Data

            new EntitySoundPool(gameW, sounds0, sounds1);
            new EntityPool(gameW, EntityVPool.Background<GameObjectVC>().Name, actions, namesMethods);

            new EntityCellUnitPool(gameW);
            new EntityCellTrailPool(gameW);
            new EntityCellBuildPool(gameW);
            new EntityCellEnvPool(gameW);
            new EntityCellFirePool(gameW);
            new EntityCellCloudPool(gameW);
            new EntityCellRiverPool(gameW);
            new EntityCellPool(gameW, isActiveCells, idCells);

            #endregion



            var envValues = new EnvironmentValues();

            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in Idxs)
                {
                    var xy_0 = Cell<XyC>(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var cloud_0 = ref Cloud<HaveEffectC>(idx_0);

                    if (Cell<CellC>(idx_0).IsActiveCell)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= envValues.StartPercent(EnvTypes.Mountain))
                            {
                                Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).SetNew();
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.AdultForest))
                                {
                                    Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).SetNew();
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.Hill))
                                {
                                    Environment<EnvCellEC>(EnvTypes.Hill, idx_0).SetNew();
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= envValues.StartPercent(EnvTypes.AdultForest))
                            {
                                Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).SetNew();
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercent(EnvTypes.Fertilizer))
                                {
                                    Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).SetNew();
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            cloud_0.Have = true;
                            CloudCenterC.Idx = idx_0;

                            CellSpaceC.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = IdxCell(item.Value);
                                WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref River<RiverC>(idx_0);


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            river_0.SetStart(DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            river_0.SetStart(DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            river_0.SetStart(DirectTypes.Up);
                        }


                        foreach (var dir in river_0.Directs)
                        {
                            var xy_next = CellSpaceC.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                            var idx_next = IdxCell(xy_next);

                            River<RiverC>(idx_next).SetEnd(dir.Invert());
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellSpaceC.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                            var idx_next = IdxCell(xy_next);

                            River<RiverC>(idx_next).SetCorner();
                        }
                    }
                }
            }

            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
            {
                InvResC.Set(ResTypes.Food, PlayerTypes.Second, 999999);

                foreach (byte idx_0 in Idxs)
                {
                    var curXyCell = Cell<XyC>(idx_0).Xy;
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var unit_0 = ref Unit<UnitC>(idx_0);
                    ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                    ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

                    ref var hp_0 = ref Unit<HpC>(idx_0);
                    ref var condUnit_0 = ref Unit<ConditionC>(idx_0);
                    ref var waterUnit_0 = ref Unit<WaterC>(idx_0);


                    ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                    ref var twLevel_0 = ref UnitTW<LevelC>(idx_0);
                    ref var protShiel_0 = ref UnitTW<ProtectionC>(idx_0);

                    ref var build_0 = ref Build<BuildC>(idx_0);
                    ref var ownBuild_0 = ref Build<OwnerC>(idx_0);

                    if (x == 7 && y == 8)
                    {
                        Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).Remove();
                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).Remove();

                        Unit<UnitCellEC>(idx_0).SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second));

                        condUnit_0.Set(ConditionUnitTypes.Protected);
                    }

                    else if (x == 8 && y == 8)
                    {
                        Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).Remove();
                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).Remove();


                        Build<BuildCellEC>(idx_0).SetNew(BuildTypes.City, PlayerTypes.Second);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Environment<EnvCellEC>(EnvTypes.Mountain, idx_0).Remove();

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            UnitTW<UnitTWCellEC>(idx_0).SetNew(TWTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            UnitTW<UnitTWCellEC>(idx_0).SetNew(TWTypes.Shield, LevelTypes.First);
                        }

                        Unit<UnitCellEC>(idx_0).SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second));
                        condUnit_0.Set(ConditionUnitTypes.Protected);
                    }
                }
            }

        }
    }
}