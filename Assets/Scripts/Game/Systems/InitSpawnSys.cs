﻿using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Scripts.Game.CellValues;

namespace Scripts.Game
{
    public sealed class InitSpawnSys : IEcsInitSystem
    {
        private EcsWorld _curGameWorld = default;


        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellViewC, CellDataC> _cellViewFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellCloudsDataC> _cellWeatherFilt = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;

        private readonly EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt;
        private readonly EcsFilter<CellUnitDataCom, HpUnitC, DamageComponent, StepComponent> _cellUnitStatsFilt;
        private readonly EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, ThirstyUnitC> _cellUnitOtherFilt;
        private readonly EcsFilter<CellUnitDataCom, VisibleC, CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitViewFilt;


        //internal static Dictionary<byte, EcsComponentRef<CellUnitDataCom>> CellUnitDataCRefs { get; private set; }

        public void Init()
        {
            //CellUnitDataCRefs = new Dictionary<byte, EcsComponentRef<CellUnitDataCom>>();
            //CellUnitDataCRef = _cellUnitBaseFilt.Get1Ref(0);

            ToggleZoneComponent.ReplaceZone(SceneTypes.Game);

            SoundComComp.SavedVolume = SoundComComp.Volume;


            var generalZoneGO = new GameObject("GeneralZone");
            ToggleZoneComponent.Attach(generalZoneGO.transform);


            ///Cells
            ///
            var cellGO = PrefabsResComCom.CellGO;
            var whiteCellSR = SpritesResComC.Sprite(SpriteGameTypes.WhiteCell);
            var blackCellSR = SpritesResComC.Sprite(SpriteGameTypes.BlackCell);

            var cell_GOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var supportParentForCells = new GameObject("Cells");

            supportParentForCells.transform.SetParent(generalZoneGO.transform);

            var curIdx = 0;

            for (byte x = 0; x < CELL_COUNT_X; x++)
                for (byte y = 0; y < CELL_COUNT_Y; y++)
                {
                    var curParentCell_GO = cell_GOs[x, y];

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                        if (x % 2 != 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                        if (x % 2 == 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                    }

                    GameObject CreateGameObject(GameObject cellGOForCreation, Sprite sprite, int xxx, int yyy, GameObject mainGame_GO)
                    {
                        var go = GameObject.Instantiate(cellGOForCreation, mainGame_GO.transform.position + new Vector3(xxx, yyy, mainGame_GO.transform.position.z), mainGame_GO.transform.rotation);
                        go.name = "Cell";
                        go.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

                        return go;
                    }

                    void SetActive(GameObject go, int xx, int yy)
                    {
                        if (yy == 0 || yy == 10 && xx >= 0 && xx < 15 ||
                            yy >= 1 && yy < 10 && xx >= 0 && xx <= 2 || xx >= 13 && xx < 15 ||

                            yy == 1 && xx == 3 || yy == 1 && xx == 12 ||
                            yy == 9 && xx == 3 || yy == 9 && xx == 12)
                        {
                            go.SetActive(false);
                        }
                    }

                    curParentCell_GO.transform.SetParent(supportParentForCells.transform);


                    var cellView_GO = curParentCell_GO.transform.Find("Cell").gameObject;
                    
                    _curGameWorld.NewEntity()
                        .Replace(new XyCellComponent(new byte[] { x, y }))

                        .Replace(new CellDataC(cellView_GO))
                        .Replace(new CellViewC(cellView_GO))

                        .Replace(new CellEnvDataC(new Dictionary<EnvTypes, bool>()))
                        .Replace(new CellEnvironViewCom(curParentCell_GO))
                        .Replace(new CellFireDataComponent())
                        .Replace(new CellFireViewComponent(curParentCell_GO))
                        .Replace(new CellBlocksViewComponent(curParentCell_GO))
                        .Replace(new CellBarsViewComponent(curParentCell_GO))
                        .Replace(new CellSupViewComponent(curParentCell_GO))
                        .Replace(new CellCloudsDataC())
                        .Replace(new CellWeatherViewCom(curParentCell_GO))
                        .Replace(new CellRiverDataC(new List<byte>()))
                        .Replace(new CellRiverViewC(curParentCell_GO.transform));


                    _curGameWorld.NewEntity()
                         .Replace(new CellBuildDataC())
                         .Replace(new OwnerCom())
                         .Replace(new VisibleC(true))
                         .Replace(new CellBuildViewComponent(curParentCell_GO));


                    _curGameWorld.NewEntity()
                         .Replace(new CellUnitDataCom())

                         .Replace(new LevelUnitC())
                         .Replace(new OwnerCom())

                         .Replace(new HpUnitC())
                         .Replace(new DamageComponent())
                         .Replace(new StepComponent())

                         .Replace(new ConditionUnitC())
                         .Replace(new ToolWeaponC())
                         .Replace(new UnitEffectsC(true))
                         .Replace(new ThirstyUnitC())

                         .Replace(new VisibleC(true))
                         .Replace(new CellUnitMainViewCom(curParentCell_GO))
                         .Replace(new CellUnitExtraViewComp(curParentCell_GO));


                    ++curIdx;
                }


            ///Else
            ///
            var backGroundGO = GameObject.Instantiate(PrefabsResComCom.BackGroundCollider2D,
                SpawnInitComSys.Main_GO.transform.position + new Vector3(7, 5.5f, 2), SpawnInitComSys.Main_GO.transform.rotation);


            var audioSourceParentGO = new GameObject("AudioSource");
            audioSourceParentGO.transform.SetParent(generalZoneGO.transform);


            var infoEnt = _curGameWorld.NewEntity()
                .Replace(new InputC())
                .Replace(new SelectorC(ToolWeaponTypes.Pick))
                .Replace(new GenerZoneViewC(generalZoneGO))
                .Replace(new BackgroundC(backGroundGO, PhotonNetwork.IsMasterClient))
                .Replace(new WhoseMoveC(PlayerTypes.First))
                .Replace(new WhereBuildsC(true))
                .Replace(new WindC(DirectTypes.Right))
                .Replace(new CameraC(Camera.main, new Vector3(7.4f, 4.8f, -2)))
                .Replace(new UpgBuildsC(true))
                .Replace(new SoundEffectC(audioSourceParentGO))

                .Replace(new CellsForSetUnitComp(true))
                .Replace(new CellsForShiftCom(true))
                .Replace(new CellsArsonArcherComp(true))
                .Replace(new CellsAttackC(true))
                .Replace(new CellsGiveTWComp(true))

                .Replace(new WhereCloudsC(true))
                .Replace(new WhereEnvC(true))
                .Replace(new WhereUnitsC(true))

                .Replace(new InventorUnitsC(true))
                .Replace(new InventResC(true))
                .Replace(new InventorTWCom(true));


            GenerZoneViewC.Attach(backGroundGO.transform);



            ///Canvas
            ///

            CanvasCom.ReplaceZone(SceneTypes.Game);

            var upZone_GO = CanvasCom.FindUnderParent("UpZone");
            var centerZone_GO = CanvasCom.FindUnderParent("CenterZone");
            var downZone_GO = CanvasCom.FindUnderParent("DownZone");
            var leftZone_GO = CanvasCom.FindUnderParent("LeftZone");
            var rightZone_GO = CanvasCom.FindUnderParent("RightZone");

            var canvasEnt = _curGameWorld.NewEntity()
                ///Up
                .Replace(new EconomyViewUIC(upZone_GO))
                .Replace(new LeaveViewUIC(CanvasCom.FindUnderParent<Button>("ButtonLeave")))
                .Replace(new WindUIC(upZone_GO.transform))

                ///Center
                .Replace(new EndGameDataUIC(default))
                .Replace(new EndGameViewUIC(centerZone_GO))
                .Replace(new ReadyViewUIC(centerZone_GO.transform.Find("ReadyZone").gameObject))
                .Replace(new ReadyDataUIC(new Dictionary<bool, bool>()))
                .Replace(new MotionsViewUIC(centerZone_GO))
                .Replace(new MotionsDataUIC(default))
                .Replace(new MistakeViewUIC(centerZone_GO))
                .Replace(new MistakeDataUIC(new Dictionary<ResTypes, int>()))
                .Replace(new KingZoneViewUIC(centerZone_GO))
                .Replace(new SelectorUIC(centerZone_GO))
                .Replace(new FriendZoneViewUIC(centerZone_GO.transform))
                .Replace(new FriendZoneDataUIC(false))
                .Replace(new HintDataUIC(1))
                .Replace(new HintViewUIC(centerZone_GO.transform))

                ///Down
                .Replace(new GetterUnitsDataUIC(new Dictionary<UnitTypes, bool>()))
                .Replace(new GetterUnitsViewUIC(downZone_GO))
                .Replace(new DonerUICom(downZone_GO))
                .Replace(new GiveTakeViewUIC(downZone_GO))
                .Replace(new HeroZoneUIC(downZone_GO.transform))

                ///Left
                .Replace(new BuildLeftZoneViewUICom(leftZone_GO))
                .Replace(new EnvirZoneDataUIC())
                .Replace(new EnvirZoneViewUICom(leftZone_GO))

                ///Right
                .Replace(new StatZoneViewUIC(rightZone_GO))
                .Replace(new CondUnitUIC(rightZone_GO.transform.Find("ConditionZone")))
                .Replace(new RightUniqueDataUIC(true))
                .Replace(new RightUniqueViewUIC(rightZone_GO.transform.Find("UniqueAbilitiesZone")))
                .Replace(new BuildAbilitDataUIC(true))
                .Replace(new BuildAbilitViewUIC(rightZone_GO.transform.Find("BuildingZone")))
                .Replace(new ExtraTWZoneUIC(rightZone_GO.transform))
                .Replace(new EffectsIUC(rightZone_GO.transform));

            _curGameWorld.NewEntity()
               .Replace(new InfoC(true))
               .Replace(new ForSettingUnitMasCom())
               .Replace(new ForAttackMasCom())
               .Replace(new ForShiftMasCom())
               .Replace(new ForBuildingMasCom())
               .Replace(new ForSeedingMasCom())
               .Replace(new ForCondMasCom())
               .Replace(new ForCircularAttackMasCom())
               .Replace(new ForCreatingUnitMasCom())
               .Replace(new ForDestroyMasCom())
               .Replace(new ForFireMasCom())
               .Replace(new ForUpgradeMasCom())
               .Replace(new ForGiveTakeToolWeaponComp())
               .Replace(new UpdatedMasCom());



            if (!HintComC.EnabledHint)
            {
                HintViewUIC.SetActiveHintZone(false);
            }




            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);
                    ref var curWeatherDatCom = ref _cellWeatherFilt.Get1(curIdxCell);

                    if (_cellViewFilt.Get2(curIdxCell).IsActiveCell)
                    {
                        if (curXyCell[1] >= 4 && curXyCell[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_MOUNTAIN_PERCENT)
                            {
                                curEnvDatCom.SetNew(EnvTypes.Mountain);
                                WhereEnvC.Add(EnvTypes.Mountain, curIdxCell);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FOREST_PERCENT)
                                {
                                    curEnvDatCom.SetNew(EnvTypes.AdultForest);
                                    WhereEnvC.Add(EnvTypes.AdultForest, curIdxCell);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_HILL_PERCENT)
                                {
                                    curEnvDatCom.SetNew(EnvTypes.Hill);
                                    WhereEnvC.Add(EnvTypes.Hill, curIdxCell);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_FOREST_PERCENT)
                            {
                                curEnvDatCom.SetNew(EnvTypes.AdultForest);
                                WhereEnvC.Add(EnvTypes.AdultForest, curIdxCell);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FERTILIZER_PERCENT)
                                {
                                    curEnvDatCom.SetNew(EnvTypes.Fertilizer);
                                    WhereEnvC.Add(EnvTypes.Fertilizer, curIdxCell);
                                }
                            }
                        }

                        if (curXyCell[0] == 5 && curXyCell[1] == 5)
                        {
                            curWeatherDatCom.HaveCloud = true;
                            curWeatherDatCom.CloudWidthType = CloudWidthTypes.OneBlock;
                            WhereCloudsC.Add(curIdxCell);
                        }


                        var riverType = RiverTypes.None;
                        var dirTypes = new List<DirectTypes>();
                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            riverType = RiverTypes.Start;
                            dirTypes.Add(DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            riverType = RiverTypes.Start;
                            dirTypes.Add(DirectTypes.Up);
                            dirTypes.Add(DirectTypes.Right);
                            corners.Add(DirectTypes.RightUp);
                            corners.Add(DirectTypes.Down);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            riverType = RiverTypes.Start;
                            dirTypes.Add(DirectTypes.Up);
                        }

                        if (riverType != default)
                        {
                            foreach (var dirType in dirTypes)
                            {
                                _cellRiverFilt.Get1(curIdxCell).RiverType = riverType;
                                _cellRiverFilt.Get1(curIdxCell).DirectTypes.Add(dirType);

                                var xy_next = CellSpaceSupport.GetXyCellByDirect(_xyCellFilter.Get1(curIdxCell).XyCell, dirType);
                                var idx_next = _xyCellFilter.GetIdxCell(xy_next);

                                _cellRiverFilt.Get1(idx_next).RiverType = RiverTypes.End;

                                if (dirType == DirectTypes.Up)
                                {
                                    _cellRiverFilt.Get1(idx_next).DirectTypes.Add(DirectTypes.Down);
                                }
                                else if (dirType == DirectTypes.Right)
                                {
                                    _cellRiverFilt.Get1(idx_next).DirectTypes.Add(DirectTypes.Left);
                                }

                                _cellRiverFilt.Get1(idx_next).IdxsNextCells.Add(curIdxCell);

                                _cellRiverFilt.Get1(curIdxCell).IdxsNextCells.Add(idx_next);
                            }


                            foreach (var dirType in corners)
                            {
                                var xy_next = CellSpaceSupport.GetXyCellByDirect(_xyCellFilter.Get1(curIdxCell).XyCell, dirType);
                                var idx_next = _xyCellFilter.GetIdxCell(xy_next);

                                _cellRiverFilt.Get1(idx_next).RiverType = RiverTypes.Corner;
                            }

                        }
                    }



                    //_cellViewFilt.Get1(curIdxCell).SetRotForClient(PhotonNetwork.IsMasterClient);
                }

                for (UnitTypes unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    InventorUnitsC.SetAmountUnitsInInvAll(unitType, LevelUnitTypes.Wood, EconomyValues.AmountUnits(unitType));
                }

                for (ResTypes resourceTypes = Support.MinResType; resourceTypes < Support.MaxResType; resourceTypes++)
                {
                    InventResC.SetAmountResAll(resourceTypes, EconomyValues.AmountResources(resourceTypes));
                }
            }


            if (PhotonNetwork.OfflineMode)
            {
                CameraC.SetPosRotClient(PlayerTypes.First, SpawnInitComSys.Main_GO.transform.position);

                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    foreach (byte idx_0 in _xyCellFilter)
                    {
                        var curXyCell = _xyCellFilter.GetXyCell(idx_0);
                        var x = curXyCell[0];
                        var y = curXyCell[1];

                        ref var curEnvDatCom = ref _cellEnvFilter.Get1(idx_0);

                        ref var unitC_0 = ref _cellUnitStatsFilt.Get1(idx_0);

                        ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
                        ref var ownUnitC_0 = ref _cellUnitMainFilt.Get3(idx_0);

                        ref var hpUnitC_0 = ref _cellUnitStatsFilt.Get2(idx_0);

                        ref var condUnitC_0 = ref _cellUnitOtherFilt.Get2(idx_0);
                        ref var twUnitC = ref _cellUnitOtherFilt.Get3(idx_0);
                        ref var thirUnitC_0 = ref _cellUnitOtherFilt.Get4(idx_0);

                        ref var buildC_0 = ref _cellBuildFilter.Get1(idx_0);
                        ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idx_0);

                        if (x == 7 && y == 6)
                        {
                            if (curEnvDatCom.Have(EnvTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvTypes.Mountain);
                                WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                            }
                            if (curEnvDatCom.Have(EnvTypes.AdultForest))
                            {
                                curEnvDatCom.Reset(EnvTypes.AdultForest);
                                WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);
                            }



                            unitC_0.SetUnit(UnitTypes.King);
                            levUnitC_0.SetLevel(LevelUnitTypes.Wood);
                            ownUnitC_0.SetOwner(PlayerTypes.Second);
                            hpUnitC_0.AmountHp = 1;
                            thirUnitC_0.SetMaxWater(unitC_0.Unit);
                            condUnitC_0.CondUnitType = CondUnitTypes.Protected;
                            WhereUnitsC.Add(ownUnitC_0.Owner, unitC_0.Unit, levUnitC_0.Level, idx_0);
                        }

                        else if (x == 8 && y == 6)
                        {
                            if (curEnvDatCom.Have(EnvTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvTypes.Mountain);
                                WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                            }
                            if (curEnvDatCom.Have(EnvTypes.AdultForest))
                            {
                                curEnvDatCom.Reset(EnvTypes.AdultForest);
                                WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);
                            }

                            buildC_0.SetBuild(BuildTypes.City);
                            ownBuildC_0.SetOwner(PlayerTypes.Second);
                            WhereBuildsC.Add(ownBuildC_0.Owner, buildC_0.BuildType, idx_0);
                        }

                        else if (x == 6 && y == 6 || x == 9 && y == 6 || x <= 9 && x >= 6 && y == 5 || x <= 9 && x >= 6 && y == 7)
                        {
                            if (curEnvDatCom.Have(EnvTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvTypes.Mountain);
                                WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                            }

                            unitC_0.SetUnit(UnitTypes.Pawn);
                            levUnitC_0.SetLevel(LevelUnitTypes.Wood);
                            thirUnitC_0.SetMaxWater(unitC_0.Unit);

                            int rand = UnityEngine.Random.Range(0, 100);

                            if (rand >= 50)
                            {
                                twUnitC.ToolWeapType = ToolWeaponTypes.Sword;
                                twUnitC.LevelTWType = LevelTWTypes.Iron;
                            }
                            else
                            {
                                twUnitC.ToolWeapType = ToolWeaponTypes.Shield;
                                twUnitC.LevelTWType = LevelTWTypes.Wood;
                                twUnitC.AddShieldProtect(LevelTWTypes.Wood);
                            }
                            hpUnitC_0.AmountHp = 100;
                            condUnitC_0.CondUnitType = CondUnitTypes.Protected;
                            ownUnitC_0.SetOwner(PlayerTypes.Second);

                            WhereUnitsC.Add(ownUnitC_0.Owner, unitC_0.Unit, levUnitC_0.Level, idx_0);
                        }
                    }
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    FriendZoneDataUIC.IsActiveFriendZone = true;
                }


                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    InventResC.Set(PlayerTypes.Second, ResTypes.Food, 999999);
                }
            }

            else
            {
                CameraC.SetPosRotClient(WhoseMoveC.CurPlayer, SpawnInitComSys.Main_GO.transform.position);

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    _cellViewFilt.Get1(curIdxCell).SetRotForClient(WhoseMoveC.CurPlayer);
                }

                //WhoseMoveCom.WhoseMoveOnline = PlayerTypes.First;


            }
        }
    }
}