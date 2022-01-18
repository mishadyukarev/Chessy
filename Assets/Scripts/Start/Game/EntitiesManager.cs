using ECS;
using Game.Common;
using Photon.Pun;
using System.Collections.Generic;
using static Game.Game.CellBuildE;
using static Game.Game.EntityCellCloudPool;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityCellRiverPool;
using static Game.Game.CellUnitEs;
using static Game.Game.CellEs;
using UnityEngine;

namespace Game.Game
{
    public struct EntitiesManager
    {
        public EntitiesManager(in EcsWorld gameW)
        {
            #region View

            CanvasC.SetCurZone(SceneTypes.Game);

            new EntityVPool(gameW, out var actions, out var sounds0, out var sounds1);




            var parCells = new GameObject("Cells");
            EntityVPool.GeneralZone<GeneralZoneVEC>().Attach(parCells.transform);

            byte idx_cur = 0;

            var cells = new GameObject[CellValues.ALL_CELLS_AMOUNT];

            for (byte x = 0; x < CellValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellValues.Y_AMOUNT; y++)
                {
                    var sprite = y % 2 == 0 && x % 2 != 0 || y % 2 != 0 && x % 2 == 0
                        ? SpritesResC.Sprite(SpriteTypes.WhiteCell)
                        : SpritesResC.Sprite(SpriteTypes.BlackCell);


                    var cell = GameObject.Instantiate(PrefabResC.CellGO, MainGoVC.Pos + new Vector3(x, y, MainGoVC.Pos.z), MainGoVC.Rot);
                    cell.name = "CellMain";
                    cell.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

                    if (y == 0 || y == 10 && x >= 0 && x < 15 ||
                            y >= 1 && y < 10 && x >= 0 && x <= 2 || x >= 13 && x < 15 ||

                            y == 1 && x == 3 || y == 1 && x == 12 ||
                            y == 9 && x == 3 || y == 9 && x == 12)
                    {
                        cell.SetActive(false);
                    }

                    cell.transform.SetParent(parCells.transform);

                    cells[idx_cur] = cell;

                    ++idx_cur;
                }

            
            new CellVEs(gameW, cells);
            new UnitCellVEs(gameW, cells);
            new CellFireVEs(gameW, cells);
            new CellEnvVEs(gameW, cells);
            new CellTrailVEs(gameW, cells);
            new CellCloudVEs(gameW, cells);
            new CellBuildingVEs(gameW, cells);
            new CellRiverVEs(gameW, cells);
            new SupportCellVEs(gameW, cells);
            new CellBlocksVEs(gameW, cells);
            new CellBarsVEs(gameW, cells);
            new StunCellVEs(gameW, cells);


            ///Left
            var leftZone = CanvasC.FindUnderCurZone("LeftZone").transform;
            new EntityLeftCityUIPool(gameW, leftZone);
            new EntityLeftEnvUIPool(gameW, leftZone);

            ///Center
            var centerZone = CanvasC.FindUnderCurZone("CenterZone").transform;
            new EntityCenterUIPool(gameW, centerZone);
            new CenterHeroUIE(gameW, centerZone);
            new CenterFriendUIE(gameW, centerZone);
            new CenterUpgradeUIE(gameW, centerZone);
            new CenterHintUIE(gameW, centerZone);
            new CenterSelectorUIE(gameW, centerZone);
            new CenterKingUIE(gameW, centerZone);

            ///Up
            new EconomyUpUIE(gameW);

            ///Down
            var downZone = CanvasC.FindUnderCurZone("DownZone").transform;
            new DownToolWeaponUIEs(gameW, downZone);
            new UIEntDownDoner(gameW, downZone);
            new UIEntDownUpgrade(gameW, downZone);
            var takeUnitZone = downZone.Find("TakeUnitZone");
            new PawnArcherDownUIE(gameW, takeUnitZone);
            new UIEntDownScout(gameW, takeUnitZone);
            new UIEntDownHero(gameW, takeUnitZone);

            ///Right
            var rightZone = CanvasC.FindUnderCurZone("RightZone").transform;
            new UIEntRight(gameW, rightZone.gameObject);
            new UIEntRightStats(gameW, rightZone.gameObject);
            new UIEntRightUnique(gameW, rightZone);
            new UIEntBuild(gameW, rightZone);
            new UIEntExtraTW(gameW, rightZone);
            new UIEntRightEffects(gameW, rightZone);
            var conditionZone = rightZone.Find("ConditionZone");
            new UIEntRightProtect(gameW, conditionZone);
            new UIEntRelax(gameW, conditionZone);

            #endregion


            var isActiveCells = new bool[CellValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveCells[idx] = CellVEs.Cell<GameObjectVC>(idx).IsActiveSelf;
                idCells[idx] = CellVEs.Cell<GameObjectVC>(idx).InstanceID;
            }

            var namesMethods = RpcS.NamesMethods;


            #region Data

            new EntitySound(gameW, sounds0, sounds1);
            new EntityPool(gameW, EntityVPool.Background<GameObjectVC>().Name, actions, namesMethods);

            new SelectedUnitE(gameW);

            new StatUnitsUpgradesE(gameW);

            new EntWhereEnviroments(gameW);
            new EntWhereUnits(gameW);
            new WhereBuildsE(gameW);

            new EntInventorUnits(gameW);
            new InventorResourcesE(gameW);
            new InventorToolWeaponE(gameW);

            new CellsForSetUnitsEs(gameW);
            new CellsForShiftUnitsEs(gameW);
            new CellsForAttackUnitsEs(gameW);
            new CellsForArsonArcherEs(gameW);

            new GetterUnitsE(gameW);

            new CellUnitEs(gameW);
            new CellUnitWaterEs(gameW);
            new CellUnitHpEs(gameW);
            new CellUnitStepEs(gameW);
            new CellUnitStunEs(gameW);
            new CellUnitAbilityUniqueEs(gameW);
            new CellUnitTWE(gameW);
            new CellTrailEs(gameW);
            new CellBuildE(gameW);
            new CellEnvironmentEs(gameW);
            new CellFireEs(gameW);
            new EntityCellCloudPool(gameW);
            new EntityCellRiverPool(gameW);
            new CellEs(gameW, isActiveCells, idCells);
            new CellParentE(gameW);

            new WhoseMoveE(gameW);
            new EntMistakeC(gameW);
            new EntHint(gameW);

            new CurrentDirectWindE(gameW);
            new CenterCloudEnt(gameW);
            new DirectsWindForElfemaleE(gameW);

            new AvailableCenterUpgradeEs(gameW);
            new AvailableCenterHeroEs(gameW);

            new SelectedToolWeaponE(gameW);


            new EntityMPool(gameW);

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

                    if (Cell<IsActiveC>(idx_0).IsActive)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= envValues.StartPercentForSpawn(EnvironmentTypes.Mountain))
                            {
                                SetNew(EnvironmentTypes.Mountain, idx_0);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                                {
                                    SetNew(EnvironmentTypes.AdultForest, idx_0);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercentForSpawn(EnvironmentTypes.Hill))
                                {
                                    SetNew(EnvironmentTypes.Hill, idx_0);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= envValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                            {
                                SetNew(EnvironmentTypes.AdultForest, idx_0);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= envValues.StartPercentForSpawn(EnvironmentTypes.Fertilizer))
                                {
                                    SetNew(EnvironmentTypes.Fertilizer, idx_0);
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            cloud_0.Have = true;
                            CenterCloudEnt.CenterCloud<IdxC>().Idx = idx_0;

                            CellSpaceC.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = IdxCell(item.Value);
                                //WindC.Set(item.Key, idx_1);
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

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                InventorResourcesE.Resource<AmountC>(ResTypes.Food, PlayerTypes.Second).Amount = 999999;

                foreach (byte idx_0 in Idxs)
                {
                    var curXyCell = Cell<XyC>(idx_0).Xy;
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var unit_0 = ref Unit<UnitTC>(idx_0);
                    ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
                    ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);

                    ref var hp_0 = ref CellUnitHpEs.Hp<AmountC>(idx_0);
                    ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);
                    ref var waterUnit_0 = ref CellUnitWaterEs.Water<AmountC>(idx_0);


                    ref var tw_0 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_0);
                    ref var twLevel_0 = ref CellUnitTWE.UnitTW<LevelTC>(idx_0);
                    ref var protShiel_0 = ref CellUnitTWE.UnitTW<ProtectionC>(idx_0);

                    ref var build_0 = ref Build<BuildingTC>(idx_0);
                    ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

                    if (x == 7 && y == 8)
                    {
                        Remove(EnvironmentTypes.Mountain, idx_0);
                        Remove(EnvironmentTypes.AdultForest, idx_0);

                        Unit<UnitCellEC>(idx_0).SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second));

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

                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            CellUnitTWE.UnitTW<UnitTWCellEC>(idx_0).SetNew(ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            CellUnitTWE.UnitTW<UnitTWCellEC>(idx_0).SetNew(ToolWeaponTypes.Shield, LevelTypes.First);
                        }

                        Unit<UnitCellEC>(idx_0).SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second));
                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }
                }
            }

        }
    }
}