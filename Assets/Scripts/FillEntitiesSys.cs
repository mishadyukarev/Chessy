using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Scripts.Game.CellValues;

namespace Scripts.Game
{
    public sealed class FillEntitiesSys : IEcsInitSystem
    {
        private EcsWorld _curGameWorld = default;


        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvFilter = default;
        private EcsFilter<CellViewC, CellDataC> _cellViewFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellCloudsDataC> _cellWeatherFilt = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;

        private readonly EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private readonly EcsFilter<CellUnitDataCom, HpUnitC, DamageC, StepComponent> _cellUnitStatsFilt = default;
        private readonly EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, WaterUnitC> _cellUnitOtherFilt = default;
        private readonly EcsFilter<CellUnitDataCom, VisibleC, CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitViewFilt = default;

        public static EcsSystems Run { get; private set; }


        public FillEntitiesSys(EcsSystems gameSystems)
        {
            var gameWorld = gameSystems.World;


            new PhotonRpcViewC(true);

            var rpcGameSys = new EcsSystems(gameWorld)
                .Add(PhotonRpcViewC.RpcView_GO.AddComponent<RpcSys>());

            Run = new EcsSystems(gameWorld)
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


            gameSystems
                .Add(this)
                .Add(eventExecuters)
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorSystem())

                .Add(Run)

                .Add(rpcGameSys);



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

            var giveTakeSystems = new EcsSystems(gameWorld)
                .Add(new GiveTakeTWMasSys());
            rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, giveTakeSystems);


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


            gameSystems
                .Add(truceSystems)
                .Add(updateMotion);

            foreach (var system in rpcSystems.Values) gameSystems.Add(system);


            var systs = new Dictionary<MastDataSysTypes, EcsSystems>();
            systs.Add(MastDataSysTypes.Update, updateMotion);
            systs.Add(MastDataSysTypes.Truce, truceSystems);

            new MastDataSysC(systs, rpcSystems);


            gameSystems.Init();
        }


        public void Init()
        {
            ToggleZoneComponent.ReplaceZone(SceneTypes.Game);

            SoundComC.SavedVolume = SoundComC.Volume;


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

            byte curIdx = 0;

            for (byte x = 0; x < CELL_COUNT_X; x++)
                for (byte y = 0; y < CELL_COUNT_Y; y++)
                {
                    var curParentCell_GO = cell_GOs[x, y];

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, MainGOComC.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                        if (x % 2 != 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, MainGOComC.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, MainGOComC.Main_GO);
                            SetActive(curParentCell_GO, x, y);
                        }
                        if (x % 2 == 0)
                        {
                            curParentCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, MainGOComC.Main_GO);
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
                         .Replace(new DamageC())
                         .Replace(new StepComponent())

                         .Replace(new ConditionUnitC())
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
            var backGroundGO = GameObject.Instantiate(PrefabsResComCom.BackGroundCollider2D,
                MainGOComC.Main_GO.transform.position + new Vector3(7, 5.5f, 2), MainGOComC.Main_GO.transform.rotation);


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
                .Replace(new SoundEffectC(audioSourceParentGO))
                .Replace(new ExtractC())
                .Replace(new GiveTakeDataUIC(true))

                .Replace(new BuildsUpgC(true))
                .Replace(new UnitsUpgC(true))

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

                ///Down
                .Replace(new GetterUnitsDataUIC(new Dictionary<UnitTypes, bool>()))
                .Replace(new GetterUnitsViewUIC(downZone_GO))
                .Replace(new DonerUICom(downZone_GO))
                .Replace(new GiveTakeViewUIC(downZone_GO))
                .Replace(new HeroZoneUIC(downZone_GO.transform))

                ///Left
                .Replace(new CutyLeftZoneViewUIC(leftZone_GO))
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

                        ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
                        ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

                        ref var hpUnitC_0 = ref _cellUnitStatsFilt.Get2(idx_0);

                        ref var condUnitC_0 = ref _cellUnitOtherFilt.Get2(idx_0);
                        ref var twUnitC = ref _cellUnitOtherFilt.Get3(idx_0);
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
                            levUnitC_0.SetLevel(LevelUnitTypes.Wood);
                            ownUnit_0.SetOwner(PlayerTypes.Second);
                            hpUnitC_0.AmountHp = 1;
                            thirUnitC_0.SetMaxWater(UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));
                            condUnitC_0.CondUnitType = CondUnitTypes.Protected;
                            WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);
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

                            buildC_0.SetBuild(BuildTypes.City);
                            ownBuildC_0.SetOwner(PlayerTypes.Second);
                            WhereBuildsC.Add(ownBuildC_0.Owner, buildC_0.BuildType, idx_0);
                        }

                        else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                        {
                            if (curEnvDatCom.Have(EnvTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvTypes.Mountain);
                                WhereEnvC.Remove(EnvTypes.Mountain, idx_0);
                            }

                            unit_0.SetUnit(UnitTypes.Pawn);
                            levUnitC_0.SetLevel(LevelUnitTypes.Wood);
                            

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
                            ownUnit_0.SetOwner(PlayerTypes.Second);
                            thirUnitC_0.SetMaxWater(UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));

                            WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);
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
                //CameraC.SetPosRotClient(WhoseMoveC.CurPlayerI, SpawnInitComSys.Main_GO.transform.position);

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    _cellViewFilt.Get1(curIdxCell).SetRotForClient(WhoseMoveC.CurPlayerI);
                }
            }
        }

        public static void Dispose()
        {
            UnityEngine.Object.Destroy(PhotonRpcViewC.RpcView_GO);
        }
    }
}
