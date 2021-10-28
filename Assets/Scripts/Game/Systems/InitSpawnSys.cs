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
    public sealed class InitSpawnSys : IEcsInitSystem
    {
        private EcsWorld _curGameWorld = default;

        private EcsFilter<InventorUnitsC> _inventorUnitsFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilt = default;
        private EcsFilter<CellUnitDataCom, CellUnitExtraViewComp, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellCloudsDataC> _cellWeatherFilt = default;


        public void Init()
        {
            ToggleZoneComponent.ReplaceZone(SceneTypes.Game);




            SoundComComp.SavedVolume = SoundComComp.Volume;


            var generalZoneGO = new GameObject("GeneralZone");
            ToggleZoneComponent.Attach(generalZoneGO.transform);


            ///Cells
            ///
            var cellGO = PrefabsResComCom.CellGO;
            var whiteCellSR = SpritesResComCom.Sprite(SpriteGameTypes.WhiteCell);
            var blackCellSR = SpritesResComCom.Sprite(SpriteGameTypes.BlackCell);

            var cell_GOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var supportParentForCells = new GameObject("Cells");

            supportParentForCells.transform.SetParent(generalZoneGO.transform);

            var curIdx = 0;

            for (byte x = 0; x < CELL_COUNT_X; x++)
                for (byte y = 0; y < CELL_COUNT_Y; y++)
                {
                    var curCell_GO = cell_GOs[x, y];

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            curCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curCell_GO, x, y);
                        }
                        if (x % 2 != 0)
                        {
                            curCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curCell_GO, x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            curCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curCell_GO, x, y);
                        }
                        if (x % 2 == 0)
                        {
                            curCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, SpawnInitComSys.Main_GO);
                            SetActive(curCell_GO, x, y);
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

                    curCell_GO.transform.SetParent(supportParentForCells.transform);


                    var cellView_GO = curCell_GO.transform.Find("Cell").gameObject;

                    _curGameWorld.NewEntity()
                        .Replace(new XyCellComponent(new byte[] { x, y }))

                        .Replace(new CellViewComponent(cellView_GO))

                        .Replace(new CellEnvironmentDataC(new Dictionary<EnvirTypes, bool>()))
                        .Replace(new CellEnvironViewCom(curCell_GO))
                        .Replace(new CellFireDataComponent())
                        .Replace(new CellFireViewComponent(curCell_GO))
                        .Replace(new CellBlocksViewComponent(curCell_GO))
                        .Replace(new CellBarsViewComponent(curCell_GO))
                        .Replace(new CellSupViewComponent(curCell_GO))
                        .Replace(new CellCloudsDataC())
                        .Replace(new CellWeatherViewCom(curCell_GO));


                    _curGameWorld.NewEntity()
                         .Replace(new CellBuildDataCom())
                         .Replace(new OwnerCom())
                         .Replace(new VisibleC(true))
                         .Replace(new CellBuildViewComponent(curCell_GO));


                    _curGameWorld.NewEntity()
                         .Replace(new CellUnitDataCom(true))
                         .Replace(new OwnerCom())
                         .Replace(new VisibleC(true))
                         .Replace(new CellUnitMainViewCom(curCell_GO))
                         .Replace(new CellUnitExtraViewComp(curCell_GO));

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
                .Replace(new BuildsInGameC(true))
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
                .Replace(new WhereEnvironmentC(true))

                .Replace(new InventorUnitsC(true))
                .Replace(new InventResourcesC(true))
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
                .Replace(new MistakeDataUIC(new Dictionary<ResourceTypes, int>()))
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
                .Replace(new RightUniqueViewUIC(rightZone_GO.transform.Find("UniqueAbilitiesZone")))
                .Replace(new BuildAbilitUIC(rightZone_GO.transform.Find("BuildingZone")))
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

                    ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);
                    ref var curWeatherDatCom = ref _cellWeatherFilt.Get1(curIdxCell);

                    if (_cellViewFilt.Get1(curIdxCell).IsActiveParent)
                    {
                        if (curXyCell[1] >= 4 && curXyCell[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_MOUNTAIN_PERCENT)
                            {
                                curEnvDatCom.SetNew(EnvirTypes.Mountain);
                                WhereEnvironmentC.Add(EnvirTypes.Mountain, curIdxCell);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FOREST_PERCENT)
                                {
                                    curEnvDatCom.SetNew(EnvirTypes.AdultForest);
                                    WhereEnvironmentC.Add(EnvirTypes.AdultForest, curIdxCell);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_HILL_PERCENT)
                                {
                                    curEnvDatCom.SetNew(EnvirTypes.Hill);
                                    WhereEnvironmentC.Add(EnvirTypes.Hill, curIdxCell);
                                }
                            }
                        }
                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironValues.START_FOREST_PERCENT)
                            {
                                curEnvDatCom.SetNew(EnvirTypes.AdultForest);
                                WhereEnvironmentC.Add(EnvirTypes.AdultForest, curIdxCell);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironValues.START_FERTILIZER_PERCENT)
                                {
                                    curEnvDatCom.SetNew(EnvirTypes.Fertilizer);
                                    WhereEnvironmentC.Add(EnvirTypes.Fertilizer, curIdxCell);
                                }
                            }
                        }
                        if (curXyCell[0] == 5 && curXyCell[1] == 5)
                        {
                            curWeatherDatCom.HaveCloud = true;
                            curWeatherDatCom.CloudWidthType = CloudWidthTypes.OneBlock;
                            WhereCloudsC.Add(curIdxCell);
                        }
                    }



                    //_cellViewFilt.Get1(curIdxCell).SetRotForClient(PhotonNetwork.IsMasterClient);
                }

                ref var unitInvCom = ref _inventorUnitsFilter.Get1(0);

                for (UnitTypes unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    InventorUnitsC.SetAmountUnitsInInvAll(unitType, LevelUnitTypes.Wood, EconomyValues.AmountUnits(unitType));
                }

                for (ResourceTypes resourceTypes = Support.MinResType; resourceTypes < Support.MaxResType; resourceTypes++)
                {
                    InventResourcesC.SetAmountResAll(resourceTypes, EconomyValues.AmountResources(resourceTypes));
                }
            }


            if (PhotonNetwork.OfflineMode)
            {
                CameraC.SetPosRotClient(PlayerTypes.First, SpawnInitComSys.Main_GO.transform.position);

                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    foreach (byte curIdxCell in _xyCellFilter)
                    {
                        var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);
                        var x = curXyCell[0];
                        var y = curXyCell[1];

                        ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);

                        ref var curUnitCom = ref _cellUnitFilter.Get1(curIdxCell);
                        ref var curOwnUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

                        ref var curBuildCom = ref _cellBuildFilter.Get1(curIdxCell);
                        ref var curOwnBuildCom = ref _cellBuildFilter.Get2(curIdxCell);

                        if (x == 7 && y == 6)
                        {
                            if (curEnvDatCom.Have(EnvirTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvirTypes.Mountain);
                                WhereEnvironmentC.Remove(EnvirTypes.Mountain, curIdxCell);
                            }
                            if (curEnvDatCom.Have(EnvirTypes.AdultForest))
                            {
                                curEnvDatCom.Reset(EnvirTypes.AdultForest);
                                WhereEnvironmentC.Remove(EnvirTypes.AdultForest, curIdxCell);
                            }



                            curUnitCom.UnitType = UnitTypes.King;
                            curUnitCom.LevelUnitType = LevelUnitTypes.Wood;
                            curUnitCom.AmountHealth = 1;
                            curUnitCom.CondUnitType = CondUnitTypes.Protected;
                            curOwnUnitCom.PlayerType = PlayerTypes.Second;
                        }

                        else if (x == 8 && y == 6)
                        {
                            if (curEnvDatCom.Have(EnvirTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvirTypes.Mountain);
                                WhereEnvironmentC.Remove(EnvirTypes.Mountain, curIdxCell);
                            }
                            if (curEnvDatCom.Have(EnvirTypes.AdultForest))
                            {
                                curEnvDatCom.Reset(EnvirTypes.AdultForest);
                                WhereEnvironmentC.Remove(EnvirTypes.AdultForest, curIdxCell);
                            }

                            curBuildCom.BuildType = BuildingTypes.City;
                            curOwnBuildCom.PlayerType = PlayerTypes.Second;
                        }

                        else if (x == 6 && y == 6 || x == 9 && y == 6 || x <= 9 && x >= 6 && y == 5 || x <= 9 && x >= 6 && y == 7)
                        {
                            if (curEnvDatCom.Have(EnvirTypes.Mountain))
                            {
                                curEnvDatCom.Reset(EnvirTypes.Mountain);
                                WhereEnvironmentC.Remove(EnvirTypes.Mountain, curIdxCell);
                            }

                            curUnitCom.UnitType = UnitTypes.Pawn;
                            curUnitCom.LevelUnitType = LevelUnitTypes.Wood;

                            int rand = UnityEngine.Random.Range(0, 100);

                            if (rand >= 50)
                            {
                                curUnitCom.TWExtraType = ToolWeaponTypes.Sword;
                                curUnitCom.LevelTWType = LevelTWTypes.Iron;
                            }
                            else
                            {
                                curUnitCom.TWExtraType = ToolWeaponTypes.Shield;
                                curUnitCom.LevelTWType = LevelTWTypes.Wood;
                                curUnitCom.AddShieldProtect(LevelTWTypes.Wood);
                            }
                            curUnitCom.AmountHealth = 100;
                            curUnitCom.CondUnitType = CondUnitTypes.Protected;
                            curOwnUnitCom.PlayerType = PlayerTypes.Second;
                        }
                    }
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    FriendZoneDataUIC.IsActiveFriendZone = true;
                }


                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    InventResourcesC.Set(PlayerTypes.Second, ResourceTypes.Food, 999999);
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
