using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Chessy.Game.CellValues;

namespace Chessy.Game
{
    public sealed class FillEntitiesSys : IEcsInitSystem
    {
        private EcsWorld _curGameWorld = default;


        private readonly EcsFilter<XyCellComponent> _xyCellFilter = default;
        private readonly EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvFilter = default;
        private readonly EcsFilter<CellViewC, CellDataC> _cellViewFilt = default;
        private readonly EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private readonly EcsFilter<CellCloudDataC> _cellWeatherFilt = default;
        private readonly EcsFilter<CellRiverDataC> _cellRiverFilt = default;

        private readonly EcsFilter<CellUnitDataC, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private readonly EcsFilter<CellUnitDataC, HpUnitC, DamageC, StepComponent> _cellUnitStatsFilt = default;
        private readonly EcsFilter<CellUnitDataC, ConditionUnitC, ToolWeaponC, WaterUnitC> _cellUnitOtherFilt = default;

        public FillEntitiesSys(EcsWorld gameWorld)
        {
            var gameSysts = new EcsSystems(gameWorld);

            #region Data

            #region General

            var runGenData = new EcsSystems(gameWorld);

            new RpcViewC(new GameObject("RpcView"));

            var rpcGameSys = new EcsSystems(gameWorld)
                .Add(RpcViewC.RpcView_GO.AddComponent<RpcSys>());


            var syncAbilities = new EcsSystems(gameWorld)
                .Add(new AbilSyncMasSys());


            var fillAvailCells = new EcsSystems(gameWorld)
                 .Add(new SoundSystem())
                .Add(new ClearAvailCellsSys())
                .Add(new FillCellsForAttackKingSys())
                .Add(new FillCellsForAttackPawnSys())
                .Add(new FillCellsForAttackRookSys())
                .Add(new FillCellsForAttackBishopSys())
                .Add(new FillCellsForSetUnitSys())
                .Add(new FillCellsForShiftSys())
                .Add(new FillCellsArsonSys());


            var eventExecuters = new EcsSystems(gameWorld)
                .Add(new CenterEventUISys())
                .Add(new LeftCityEventUISys())
                .Add(new LeftEnvEventUISys())
                .Add(new DownEventUISys())
                .Add(new RightUnitEventUISys());


            runGenData
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorS())
                .Add(syncAbilities)
                .Add(fillAvailCells)
                .Add(eventExecuters);


            new GameGenSysDataC(runGenData.Run);

            #endregion

            #region Master

            var rpcSystems = new Dictionary<RpcMasterTypes, EcsSystems>();

            rpcSystems.Add(RpcMasterTypes.Build, new EcsSystems(gameWorld)
                .Add(new BuildMineMastSys())
                .Add(new BuildFarmMastSys())
                .Add(new BuildCityMastSys()));
            rpcSystems.Add(RpcMasterTypes.DestroyBuild, new EcsSystems(gameWorld).Add(new DestroyMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Shift, new EcsSystems(gameWorld).Add(new ShiftUnitMasSys()));
            rpcSystems.Add(RpcMasterTypes.Attack, new EcsSystems(gameWorld).Add(new AttackMastSys()));
            rpcSystems.Add(RpcMasterTypes.ConditionUnit, new EcsSystems(gameWorld).Add(new ConditionMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Ready, new EcsSystems(gameWorld).Add(new ReadyMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Done, new EcsSystems(gameWorld).Add(new DonerMastSys()));
            rpcSystems.Add(RpcMasterTypes.CreateUnit, new EcsSystems(gameWorld).Add(new CreateUnitMastSys()));
            rpcSystems.Add(RpcMasterTypes.MeltOre, new EcsSystems(gameWorld).Add(new MeltOreMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.SetUnit, new EcsSystems(gameWorld).Add(new SetterUnitMastSys()));
            rpcSystems.Add(RpcMasterTypes.BuyRes, new EcsSystems(gameWorld).Add(new BuyResMastS()));
            rpcSystems.Add(RpcMasterTypes.Fire, new EcsSystems(gameWorld).Add(new FireMastSys()));
            rpcSystems.Add(RpcMasterTypes.SeedEnvironment, new EcsSystems(gameWorld).Add(new SeedingMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.CircularAttackKing, new EcsSystems(gameWorld).Add(new CircularAttackKingMastSys()));
            rpcSystems.Add(RpcMasterTypes.UpgradeUnit, new EcsSystems(gameWorld).Add(new UpgradeUnitMasSys()));
            rpcSystems.Add(RpcMasterTypes.OldToNewUnit, new EcsSystems(gameWorld).Add(new ScoutOldNewSys()));
            rpcSystems.Add(RpcMasterTypes.BonusNearUnitKing, new EcsSystems(gameWorld).Add(new BonusNearUnitKingMasSys()));
            rpcSystems.Add(RpcMasterTypes.PickUpgrade, new EcsSystems(gameWorld).Add(new PickUpgMasSys()));
            rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, new EcsSystems(gameWorld).Add(new GiveTakeTWMasSys()));
            rpcSystems.Add(RpcMasterTypes.GetHero, new EcsSystems(gameWorld).Add(new GetHeroMastS()));


            var updateMotion = new EcsSystems(gameWorld)
                .Add(new UpdatorMastSys())
                .Add(new ExtractBuildUpdMasSys())
                .Add(new FireUpdMasSys())
                .Add(new CloudUpdMasSys())
                .Add(new ThirstyUpdMasSys())
                .Add(new RelaxUpdMasSys())
                .Add(new HungryUpdMasSys());

            var truceSystems = new EcsSystems(gameWorld)
                .Add(new TruceMasterSystem());



            var systs = new Dictionary<MastDataSysTypes, Action>();
            systs.Add(MastDataSysTypes.Update, updateMotion.Run);
            systs.Add(MastDataSysTypes.Truce, truceSystems.Run);

            var rpcSystsAction = new Dictionary<RpcMasterTypes, Action>();
            foreach (var item_0 in rpcSystems) rpcSystsAction.Add(item_0.Key, item_0.Value.Run);

            new MastSysDataC(systs, rpcSystsAction);


            #endregion

            #endregion


            #region ViewData

            var syncCellViewSyss = new EcsSystems(gameWorld)
                .Add(new VisibElseSys())
                .Add(new SyncCellUnitViewSys())
                .Add(new SyncCellSelUnitViewSys())
                .Add(new SyncCellUnitSupVisSystem())
                .Add(new SyncCellBuildViewSystem())
                .Add(new SyncCellEnvirsVisSystem())
                .Add(new SyncCellEffectsVisSystem())
                .Add(new SyncSupportViewSystem())
                .Add(new CellWeatherViewSys())
                .Add(new CellRiverViewSys())
                .Add(new FliperAndRotatorUnitSystem())
                .Add(new CellBarsEnvSystem())
                .Add(new SyncCellTrailSys());


            var syncCanvasViewSyss = new EcsSystems(gameWorld)
            ///left
            .Add(new BuildZoneUISys())
               .Add(new EnvironmentUISystem())

            ///right
            .Add(new RightZoneUISys())
                .Add(new StatsUISystem())
                .Add(new ProtectUISys())
                .Add(new RelaxUISys())
                .Add(new UniqButSyncUISys())
                .Add(new FirstButtonBuildUISys())
                .Add(new SecButtonBuildUISys())
                .Add(new ThirdButtonBuildUISys())
                .Add(new ShieldUISys())
                .Add(new EffectsUISys())

            ///down
            .Add(new DonerUISystem())
                .Add(new GetterUnitsUISystem())
                .Add(new GiveTakeUISystem())
                .Add(new ScoutSyncUIS())
                .Add(new HeroSyncUIS())

            ///up
            .Add(new EconomyUpUISys())
            .Add(new WindUISys())

            ///center
            .Add(new SelectorUISys())
                .Add(new TheEndGameUISystem())
                .Add(new MotionCenterUISystem())
                .Add(new ReadyZoneUISystem())
                .Add(new MistakeUISys())
                .Add(new KingZoneUISys())
                .Add(new FriendZoneUISys())
                .Add(new ActiveHitUISys())
                .Add(new PickUpgUISys())
                .Add(new HeroesSyncUISys());


            var rotateCurPlayer = new EcsSystems(gameWorld)
                .Add(new RotateAllSys());


            var sysGenDataView = new EcsSystems(gameWorld)
                .Add(syncCellViewSyss)
                .Add(syncCanvasViewSyss);


            new GameGenSysDataViewC(sysGenDataView.Run, rotateCurPlayer.Run);

            #endregion




            gameSysts
                 .Add(this)

                .Add(runGenData)
                .Add(rpcGameSys)
                .Add(truceSystems)
                .Add(updateMotion)
                .Add(rotateCurPlayer)
                .Add(sysGenDataView);

            foreach (var system in rpcSystems.Values) gameSysts.Add(system);


            gameSysts.Init();

            GameGenSysDataViewC.RotateAll.Invoke();
        }

        public void Init()
        {
            ToggleZoneComponent.ReplaceZone(SceneTypes.Game);

            SoundComC.SavedVolume = SoundComC.Volume;


            var generalZoneGO = new GameObject("GeneralZone");
            ToggleZoneComponent.Attach(generalZoneGO.transform);


            ///Cells
            ///
            var cellGO = PrefabResComC.CellGO;
            var whiteCellSR = SpritesResComC.Sprite(SpriteGameTypes.WhiteCell);
            var blackCellSR = SpritesResComC.Sprite(SpriteGameTypes.BlackCell);

            var cell_GOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var supportParentForCells = new GameObject("Cells");

            supportParentForCells.transform.SetParent(generalZoneGO.transform);

            byte curIdx = 0;

            for (byte x = 0; x < CELL_COUNT_X; x++)
                for (byte y = 0; y < CELL_COUNT_Y; y++)
                {
                    var curParentCell_GO = cell_GOs[x, y];

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, MainGoVC.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                        if (x % 2 != 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, MainGoVC.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, MainGoVC.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                        if (x % 2 == 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, MainGoVC.Main_GO);
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
                        .Replace(new XyCellComponent(curIdx, new byte[] { x, y }))

                        .Replace(new CellDataC(cellView_GO))
                        .Replace(new CellViewC(cellView_GO))

                        .Replace(new CellEnvDataC(new Dictionary<EnvTypes, bool>()))
                        .Replace(new CellEnvResC(true))
                        .Replace(new CellEnvironViewCom(curParentCell_GO))
                        .Replace(new CellFireDataC())
                        .Replace(new CellFireViewComponent(curParentCell_GO))
                        .Replace(new CellBlocksViewComponent(curParentCell_GO))
                        .Replace(new CellBarsViewComponent(curParentCell_GO))
                        .Replace(new CellSupViewComponent(curParentCell_GO))
                        .Replace(new CellCloudDataC())
                        .Replace(new CellCloudViewC(curParentCell_GO))
                        .Replace(new CellRiverDataC(new List<byte>()))
                        .Replace(new CellRiverViewC(curParentCell_GO.transform));


                    _curGameWorld.NewEntity()
                         .Replace(new CellBuildDataC())
                         .Replace(new OwnerCom())
                         .Replace(new VisibleC(true))
                         .Replace(new CellBuildViewComponent(curParentCell_GO));


                    _curGameWorld.NewEntity()
                         .Replace(new CellUnitDataC())

                         .Replace(new LevelUnitC())
                         .Replace(new OwnerCom())

                         .Replace(new HpUnitC())
                         .Replace(new DamageC())
                         .Replace(new StepComponent())

                         .Replace(new ConditionUnitC())
                         .Replace(new MoveInCondC(true))

                         .Replace(new Uniq1C())
                         .Replace(new Uniq2C())

                         .Replace(new UnitEffectsC(true))
                         .Replace(new WaterUnitC())                       

                         .Replace(new ToolWeaponC())

                         .Replace(new VisibleC(true))
                         .Replace(new CellUnitMainViewCom(curParentCell_GO))
                         .Replace(new CellUnitExtraViewComp(curParentCell_GO));


                    _curGameWorld.NewEntity()
                        .Replace(new CellTrailDataC(new Dictionary<DirectTypes, int>()))
                        .Replace(new CellTrailViewC(curParentCell_GO.transform))
                        .Replace(new VisibleC(true));


                    ++curIdx;
                }


            ///Else
            ///
            var backGroundGO = GameObject.Instantiate(PrefabResComC.BackGroundCollider2D,
                MainGoVC.Main_GO.transform.position + new Vector3(7, 5.5f, 2), MainGoVC.Main_GO.transform.rotation);


            var audioSourceParentGO = new GameObject("AudioSource");
            audioSourceParentGO.transform.SetParent(generalZoneGO.transform);


            var infoEnt = _curGameWorld.NewEntity()
                .Replace(new GenerZoneViewC(generalZoneGO))
                .Replace(new BackgroundC(backGroundGO, PhotonNetwork.IsMasterClient))
                .Replace(new WindC(DirectTypes.Right))
                .Replace(new CameraC(Camera.main, new Vector3(7.4f, 4.8f, -2)))
                .Replace(new SoundEffectC(audioSourceParentGO))
                .Replace(new ExtractC())

                .Replace(new BuildsUpgC(true))
                .Replace(new UnitPercUpgC(true))
                .Replace(new UnitStepUpgC(new Dictionary<PlayerTypes, Dictionary<UnitTypes, int>>()))

                .Replace(new CellsForSetUnitC(true))
                .Replace(new CellsForShiftCom(true))
                .Replace(new CellsArsonArcherComp(true))
                .Replace(new CellsAttackC(true))
                .Replace(new CellsGiveTWComp(true))

                .Replace(new WhereCloudsC(true))
                .Replace(new WhereEnvC(true))
                .Replace(new WhereUnitsC(true))

                .Replace(new InvUnitsC(true))
                .Replace(new InventResC(true))
                .Replace(new InvToolWeapC(true));

            HeroInvC.Start();


            GenerZoneViewC.Attach(backGroundGO.transform);



            ///Canvas
            ///

            CanvasC.SetCurZone(SceneTypes.Game);

            var upZone_GO = CanvasC.FindUnderCurZone("UpZone");
            var centerZone_GO = CanvasC.FindUnderCurZone("CenterZone");
            var downZone_GO = CanvasC.FindUnderCurZone("DownZone");
            var leftZone_GO = CanvasC.FindUnderCurZone("LeftZone");
            var rightZone_GO = CanvasC.FindUnderCurZone("RightZone");


            var uniqAbilZone_trans = rightZone_GO.transform.Find("UniqueAbilitiesZone");


            var canvasEnt = _curGameWorld.NewEntity()
                ///Up
                .Replace(new EconomyViewUIC(upZone_GO))
                .Replace(new LeaveViewUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave")))
                .Replace(new WindUIC(upZone_GO.transform))

                ///Center
                .Replace(new EndGameDataUIC(default))
                .Replace(new EndGameViewUIC(centerZone_GO))
                .Replace(new ReadyViewUIC(centerZone_GO.transform.Find("ReadyZone").gameObject))
                .Replace(new ReadyDataUIC(new Dictionary<PlayerTypes, bool>()))
                .Replace(new MotionsViewUIC(centerZone_GO))
                .Replace(new MotionsDataUIC(default))
                .Replace(new MistakeViewUIC(centerZone_GO))
                .Replace(new MistakeDataUIC(new Dictionary<ResTypes, int>()))
                .Replace(new KingZoneViewUIC(centerZone_GO))
                .Replace(new SelectorUIC(centerZone_GO))
                .Replace(new FriendZoneViewUIC(centerZone_GO.transform))
                .Replace(new FriendZoneDataUIC(false))
                .Replace(new HintDataUIC(new Dictionary<VideoClipTypes, bool>()))
                .Replace(new HintViewUIC(centerZone_GO.transform))
                .Replace(new PickUpgZoneDataUIC(new Dictionary<PlayerTypes, bool>()))
                .Replace(new PickUpgZoneViewUIC(centerZone_GO.transform))
                .Replace(new HeroesViewUIC(centerZone_GO.transform))

                ///Down
                .Replace(new GetterUnitsDataUIC(new Dictionary<UnitTypes, bool>()))
                .Replace(new GetterUnitsViewUIC(downZone_GO))
                .Replace(new DonerUICom(downZone_GO))
                .Replace(new GiveTakeViewUIC(downZone_GO))
                .Replace(new ScoutViewUIC(downZone_GO.transform))
                .Replace(new HeroDownUIC(downZone_GO.transform))

                ///Left
                .Replace(new CutyLeftZoneViewUIC(leftZone_GO))
                .Replace(new EnvirZoneDataUIC())
                .Replace(new EnvirZoneViewUICom(leftZone_GO))

                ///Right
                .Replace(new StatZoneViewUIC(rightZone_GO))
                .Replace(new CondUnitUIC(rightZone_GO.transform.Find("ConditionZone")))

                .Replace(new UniqFirstButDataC())
                .Replace(new UniqButtonsViewC(uniqAbilZone_trans))
                .Replace(new UniqSecButDataC())
                .Replace(new UniqSecButViewC(uniqAbilZone_trans))
                .Replace(new UniqThirdButDataC())
                .Replace(new UniqThirdButViewC(uniqAbilZone_trans))

                .Replace(new BuildAbilitDataUIC(true))
                .Replace(new BuildAbilitViewUIC(rightZone_GO.transform.Find("BuildingZone")))
                .Replace(new ExtraTWZoneUIC(rightZone_GO.transform))
                .Replace(new EffectsIUC(rightZone_GO.transform));


            _curGameWorld.NewEntity()
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
               .Replace(new ForBuyResMasC())
               .Replace(new ForGiveTakeToolWeaponComp())
               .Replace(new UpdatedMasCom());



            if (!HintComC.IsOnHint)
            {
                HintViewUIC.SetActiveHintZone(false);
            }

            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    var curXyCell = _xyCellFilter.Get1(curIdxCell).XyCell;
                    var x = curXyCell[0];
                    var y = curXyCell[1];

                    ref var env_0 = ref _cellEnvFilter.Get1(curIdxCell);
                    ref var envRes_0 = ref _cellEnvFilter.Get2(curIdxCell);
                    ref var curWeatherDatCom = ref _cellWeatherFilt.Get1(curIdxCell);

                    if (_cellViewFilt.Get2(curIdxCell).IsActiveCell)
                    {
                        if (curXyCell[1] >= 4 && curXyCell[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_MOUNTAIN_PERCENT)
                            {
                                env_0.Set(EnvTypes.Mountain);
                                WhereEnvC.Add(EnvTypes.Mountain, curIdxCell);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FOREST_PERCENT)
                                {
                                    env_0.Set(EnvTypes.AdultForest);
                                    envRes_0.SetNew(EnvTypes.AdultForest);
                                    WhereEnvC.Add(EnvTypes.AdultForest, curIdxCell);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_HILL_PERCENT)
                                {
                                    env_0.Set(EnvTypes.Hill);
                                    envRes_0.SetNew(EnvTypes.Hill);
                                    WhereEnvC.Add(EnvTypes.Hill, curIdxCell);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_FOREST_PERCENT)
                            {
                                env_0.Set(EnvTypes.AdultForest);
                                envRes_0.SetNew(EnvTypes.AdultForest);
                                WhereEnvC.Add(EnvTypes.AdultForest, curIdxCell);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FERTILIZER_PERCENT)
                                {
                                    env_0.Set(EnvTypes.Fertilizer);
                                    envRes_0.SetNew(EnvTypes.Fertilizer);
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
                            corners.Add(DirectTypes.UpRight);
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
                                _cellRiverFilt.Get1(curIdxCell).AddDir(dirType);

                                var xy_next = CellSpaceSupport.GetXyCellByDirect(_xyCellFilter.Get1(curIdxCell).XyCell, dirType);
                                var idx_next = _xyCellFilter.GetIdxCell(xy_next);

                                _cellRiverFilt.Get1(idx_next).RiverType = RiverTypes.End;

                                if (dirType == DirectTypes.Up)
                                {
                                    _cellRiverFilt.Get1(idx_next).AddDir(DirectTypes.Down);
                                }
                                else if (dirType == DirectTypes.Right)
                                {
                                    _cellRiverFilt.Get1(idx_next).AddDir(DirectTypes.Left);
                                }

                                //_cellRiverFilt.Get1(idx_next).IdxsNextCells.Add(curIdxCell);

                                //_cellRiverFilt.Get1(curIdxCell).IdxsNextCells.Add(idx_next);
                            }


                            foreach (var dirType in corners)
                            {
                                var xy_next = CellSpaceSupport.GetXyCellByDirect(_xyCellFilter.Get1(curIdxCell).XyCell, dirType);
                                var idx_next = _xyCellFilter.GetIdxCell(xy_next);

                                _cellRiverFilt.Get1(idx_next).RiverType = RiverTypes.Corner;
                            }

                        }
                    }
                }

                InvUnitsC.SetStartAmountUnitAll();

                for (ResTypes resourceTypes = Support.MinResType; resourceTypes < Support.MaxResType; resourceTypes++)
                {
                    InventResC.SetAmountResAll(resourceTypes, EconomyValues.AmountResources(resourceTypes));
                }
            }


            if (PhotonNetwork.OfflineMode)
            {
                //CameraC.SetPosRotClient(PlayerTypes.First, SpawnInitComSys.Main_GO.transform.position);

                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    foreach (byte idx_0 in _xyCellFilter)
                    {
                        var curXyCell = _xyCellFilter.Get1(idx_0).XyCell;
                        var x = curXyCell[0];
                        var y = curXyCell[1];

                        ref var curEnvDatCom = ref _cellEnvFilter.Get1(idx_0);

                        ref var unit_0 = ref _cellUnitStatsFilt.Get1(idx_0);

                        ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
                        ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

                        ref var hpUnitC_0 = ref _cellUnitStatsFilt.Get2(idx_0);

                        ref var condUnit_0 = ref _cellUnitOtherFilt.Get2(idx_0);
                        ref var twUnit_0 = ref _cellUnitOtherFilt.Get3(idx_0);
                        ref var thirUnitC_0 = ref _cellUnitOtherFilt.Get4(idx_0);

                        ref var buildC_0 = ref _cellBuildFilter.Get1(idx_0);
                        ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idx_0);

                        if (x == 7 && y == 8)
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



                            unit_0.SetUnit(UnitTypes.King);
                            levUnit_0.SetLevel(LevelUnitTypes.Wood);
                            ownUnit_0.SetOwner(PlayerTypes.Second);
                            hpUnitC_0.SetMaxHp();
                            thirUnitC_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));
                            condUnit_0.SetNew(CondUnitTypes.Protected);
                            WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                        }

                        else if (x == 8 && y == 8)
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

                            buildC_0.Build = BuildTypes.City;
                            ownBuildC_0.SetOwner(PlayerTypes.Second);
                            WhereBuildsC.Add(ownBuildC_0.Owner, buildC_0.Build, idx_0);
                        }

                        else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                        {
                            if (curEnvDatCom.Have(EnvTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvTypes.Mountain);
                                WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                            }

                            unit_0.SetUnit(UnitTypes.Pawn);
                            levUnit_0.SetLevel(LevelUnitTypes.Wood);
                            

                            int rand = UnityEngine.Random.Range(0, 100);

                            if (rand >= 50)
                            {
                                twUnit_0.ToolWeapType = ToolWeaponTypes.Sword;
                                twUnit_0.LevelTWType = LevelTWTypes.Iron;
                            }
                            else
                            {
                                twUnit_0.ToolWeapType = ToolWeaponTypes.Shield;
                                twUnit_0.LevelTWType = LevelTWTypes.Wood;
                                twUnit_0.AddShieldProtect(LevelTWTypes.Wood);
                            }
                            hpUnitC_0.SetMaxHp();
                            condUnit_0.SetNew(CondUnitTypes.Protected);
                            ownUnit_0.SetOwner(PlayerTypes.Second);
                            thirUnitC_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));

                            WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
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

            }
        }

        public static void Dispose()
        {
            UnityEngine.Object.Destroy(RpcViewC.RpcView_GO);
            WhereBuildsC.Start();
        }
    }
}
