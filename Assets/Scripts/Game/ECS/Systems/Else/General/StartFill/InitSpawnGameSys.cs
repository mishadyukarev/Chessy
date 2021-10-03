using Assets.Scripts.ECS.Components.Data.Else.Game.Master;
using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Scripts.Game.CellValues;

namespace Scripts.Game
{
    public sealed class InitSpawnGameSys : IEcsInitSystem
    {
        private EcsWorld _curGameWorld = default;

        private EcsFilter<InventResourCom> _inventorResFilter = default;
        private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;
        private EcsFilter<FriendZoneDataUICom> _friendZoneUIFilt = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilt = default;
        private EcsFilter<CellUnitDataCom, CellUnitExtraViewComp, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;


        public void Init()
        {
            ToggleZoneComponent.ReplaceZone(SceneTypes.Game);




            SoundComComp.SavedVolume = SoundComComp.Volume;


            var generalZoneGO = new GameObject("GeneralZone");
            ToggleZoneComponent.Attach(generalZoneGO.transform);


            ///Cells
            ///
            var cellGO = ResourcesComponent.PrefabConfig.CellGO;
            var whiteCellSR = ResourcesComponent.SpritesConfig.WhiteCell_Sprite;
            var blackCellSR = ResourcesComponent.SpritesConfig.BlackCell_Sprite;

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
                        if (xx >= 0 && yy == 0 || xx >= 0 && yy == 10 ||
                            xx == 1 && yy >= 0 || xx == 13 && yy >= 0 ||
                        xx == 0 && yy >= 0 || xx == 14 && yy >= 0 ||
                        xx == 1 && yy == 1 || xx == 2 && yy == 1 || xx == 12 && yy == 1 || xx == 13 && yy == 1 ||
                        xx == 1 && yy == 9 || xx == 2 && yy == 9 || xx == 12 && yy == 9 || xx == 13 && yy == 9)
                            go.SetActive(false);
                    }

                    curCell_GO.transform.SetParent(supportParentForCells.transform);


                    var cellView_GO = curCell_GO.transform.Find("Cell").gameObject;

                    _curGameWorld.NewEntity()
                        .Replace(new XyCellComponent(new byte[] { x, y }))

                        .Replace(new CellViewComponent(cellView_GO))

                        .Replace(new CellEnvironDataCom(new Dictionary<EnvirTypes, bool>()))
                        .Replace(new CellEnvironViewCom(curCell_GO))
                        .Replace(new CellFireDataComponent())
                        .Replace(new CellFireViewComponent(curCell_GO))
                        .Replace(new CellBlocksViewComponent(curCell_GO))
                        .Replace(new CellBarsViewComponent(curCell_GO))
                        .Replace(new CellSupViewComponent(curCell_GO));


                    _curGameWorld.NewEntity()
                         .Replace(new CellBuildDataComponent())
                         .Replace(new CellBuildViewComponent(curCell_GO))
                         .Replace(new OwnerCom());


                    _curGameWorld.NewEntity()
                         .Replace(new CellUnitDataCom(true))
                         .Replace(new CellUnitMainViewComp(curCell_GO))
                         .Replace(new CellUnitExtraViewComp(curCell_GO))
                         .Replace(new OwnerCom());

                    ++curIdx;
                }



            ///Else
            ///
            var backGroundGO = GameObject.Instantiate(ResourcesComponent.PrefabConfig.BackGroundCollider2D,
                SpawnInitComSys.Main_GO.transform.position + new Vector3(7, 5.5f, 2), SpawnInitComSys.Main_GO.transform.rotation);


            var audioSourceParentGO = new GameObject("AudioSource");
            audioSourceParentGO.transform.SetParent(generalZoneGO.transform);


            var infoEnt = _curGameWorld.NewEntity()
                .Replace(new InputComponent())
                .Replace(new SelectorCom(ToolWeaponTypes.Pick))
                .Replace(new GenerZoneViewCom(generalZoneGO))
                .Replace(new BackgroundComponent(backGroundGO, PhotonNetwork.IsMasterClient))

                .Replace(new CellsForSetUnitComp(true))
                .Replace(new CellsForShiftCom(true))
                .Replace(new CellsArsonArcherComp(true))
                .Replace(new CellsForAttackCom(true))
                .Replace(new CellsGiveTWComp(true))
                .Replace(new WhoseMoveCom(PlayerTypes.First))

                .Replace(new UpgradesBuildsCom(true))

                .Replace(new InventorUnitsComponent(true))
                .Replace(new InventResourCom(true))
                .Replace(new InventorTWCom(true))

                .Replace(new FromInfoComponent())
                .Replace(new SoundEffectsComp(audioSourceParentGO));


            infoEnt.Get<GenerZoneViewCom>().Attach(backGroundGO.transform);



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
                .Replace(new EconomyViewUICom(upZone_GO))
                .Replace(new LeaveViewUIComponent(CanvasCom.FindUnderParent<Button>("ButtonLeave")))

                ///Center
                .Replace(new EndGameDataUIComponent())
                .Replace(new EndGameViewUIComponent(centerZone_GO))
                .Replace(new ReadyViewUICom(centerZone_GO.transform.Find("ReadyZone").gameObject))
                .Replace(new ReadyDataUICom(new Dictionary<bool, bool>()))
                .Replace(new MotionsViewUIComponent(centerZone_GO))
                .Replace(new MotionsDataUIComponent())
                .Replace(new MistakeViewUICom(centerZone_GO))
                .Replace(new MistakeDataUICom(new Dictionary<ResourceTypes, bool>()))
                .Replace(new KingZoneViewUIComp(centerZone_GO))
                .Replace(new SelectorTypeViewUIComp(centerZone_GO))
                .Replace(new FriendZoneViewUICom(centerZone_GO.transform))
                .Replace(new FriendZoneDataUICom())

                ///Down
                .Replace(new GetterUnitsDataUICom(new Dictionary<UnitTypes, bool>()))
                .Replace(new GetterUnitsViewUICom(downZone_GO))
                .Replace(new DonerUICom(downZone_GO))
                .Replace(new GiveTakeZoneViewUIComp(downZone_GO))

                ///Left
                .Replace(new BuildLeftZoneViewUICom(leftZone_GO))
                .Replace(new EnvirZoneDataUICom())
                .Replace(new EnvirZoneViewUICom(leftZone_GO))

                ///Right
                .Replace(new StatZoneViewUICom(rightZone_GO))
                .Replace(new CondUnitUICom(rightZone_GO.transform.Find("ConditionZone")))
                .Replace(new UniqueAbiltUICom(rightZone_GO.transform.Find("UniqueAbilitiesZone")))
                .Replace(new BuildAbilitUICom(rightZone_GO.transform.Find("BuildingZone")));


            ref var invResCom = ref _inventorResFilter.Get1(0);

            _curGameWorld.NewEntity()
               .Replace(new InfoCom())
               .Replace(new ForSettingUnitMasCom())
               .Replace(new ForAttackMasCom())
               .Replace(new ForShiftMasCom())
               .Replace(new ForBuildingMasCom())
               .Replace(new ForSeedingMasCom())
               .Replace(new ConditionMasCom())
               .Replace(new ForCircularAttackMasCom())
               .Replace(new ForCreatingUnitMasCom())
               .Replace(new ForDestroyMasCom())
               .Replace(new ForFireMasCom())
               .Replace(new ForUpgradeMasCom())
               .Replace(new ForGiveTakeToolWeaponComp())
               .Replace(new UpdatedMasCom());


            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);

                    ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);

                    if (_cellViewFilt.Get1(curIdxCell).IsActiveParent)
                    {
                        if (curXyCell[1] >= 4 && curXyCell[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironmentValues.START_MOUNTAIN_PERCENT)
                                curEnvDatCom.SetNewEnvir(EnvirTypes.Mountain);
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_FOREST_PERCENT)
                                {
                                    curEnvDatCom.SetNewEnvir(EnvirTypes.AdultForest);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_HILL_PERCENT)
                                    curEnvDatCom.SetNewEnvir(EnvirTypes.Hill);
                            }
                        }
                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironmentValues.START_FOREST_PERCENT)
                            {
                                curEnvDatCom.SetNewEnvir(EnvirTypes.AdultForest);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_FERTILIZER_PERCENT)
                                {
                                    curEnvDatCom.SetNewEnvir(EnvirTypes.Fertilizer);
                                }
                            }
                        }
                    }

                    _cellViewFilt.Get1(curIdxCell).SetRotForClient(PhotonNetwork.IsMasterClient);
                }

                ref var unitInvCom = ref _inventorUnitsFilter.Get1(0);


                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.First, UnitTypes.King, EconomyValues.AMOUNT_KING_MASTER);
                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.First, UnitTypes.Pawn, EconomyValues.AMOUNT_PAWN_MASTER);
                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.First, UnitTypes.Rook, EconomyValues.AMOUNT_ROOK_MASTER);
                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.First, UnitTypes.Bishop, EconomyValues.AMOUNT_BISHOP_MASTER);

                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.Second, UnitTypes.King, EconomyValues.AMOUNT_KING_OTHER);
                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.Second, UnitTypes.Pawn, EconomyValues.AMOUNT_PAWN_OTHER);
                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.Second, UnitTypes.Rook, EconomyValues.AMOUNT_ROOK_OTHER);
                unitInvCom.SetAmountUnitsInInvent(PlayerTypes.Second, UnitTypes.Bishop, EconomyValues.AMOUNT_BISHOP_OTHER);


                invResCom.SetAmountResources(PlayerTypes.First, ResourceTypes.Food, EconomyValues.AMOUNT_FOOD_MASTER);
                invResCom.SetAmountResources(PlayerTypes.First, ResourceTypes.Wood, EconomyValues.AMOUNT_WOOD_MASTER);
                invResCom.SetAmountResources(PlayerTypes.First, ResourceTypes.Ore, EconomyValues.AMOUNT_ORE_MASTER);
                invResCom.SetAmountResources(PlayerTypes.First, ResourceTypes.Iron, EconomyValues.AMOUNT_IRON_MASTER);
                invResCom.SetAmountResources(PlayerTypes.First, ResourceTypes.Gold, EconomyValues.AMOUNT_GOLD_MASTER);

                invResCom.SetAmountResources(PlayerTypes.Second, ResourceTypes.Food, EconomyValues.AMOUNT_FOOD_OTHER);
                invResCom.SetAmountResources(PlayerTypes.Second, ResourceTypes.Wood, EconomyValues.AMOUNT_WOOD_OTHER);
                invResCom.SetAmountResources(PlayerTypes.Second, ResourceTypes.Ore, EconomyValues.AMOUNT_ORE_OTHER);
                invResCom.SetAmountResources(PlayerTypes.Second, ResourceTypes.Iron, EconomyValues.AMOUNT_IRON_OTHER);
                invResCom.SetAmountResources(PlayerTypes.Second, ResourceTypes.Gold, EconomyValues.AMOUNT_GOLD_OTHER);
            }

            else
            {
                _curGameWorld.NewEntity()
                    .Replace(new FromInfoComponent());
            }


            if (GameModesCom.IsOnlineMode)
            {
                var isMaster = PhotonNetwork.IsMasterClient;
                CameraComComp.SetPosRotClient(isMaster, SpawnInitComSys.Main_GO.transform.position);

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    _cellViewFilt.Get1(curIdxCell).SetRotForClient(isMaster);
                }

                WhoseMoveCom.WhoseMoveOnline = PlayerTypes.First;
            }

            else
            {
                CameraComComp.SetPosRotClient(true, SpawnInitComSys.Main_GO.transform.position);

                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    foreach (byte curIdxCell in _xyCellFilter)
                    {
                        var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);
                        var x = curXyCell[0];
                        var y = curXyCell[1];

                        ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);
                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                        ref var curOwnUnitCom = ref _cellUnitFilter.Get3(curIdxCell);
                        ref var curBuildDatCom = ref _cellBuildFilter.Get1(curIdxCell);

                        if (x == 7 && y == 6)
                        {
                            curEnvDatCom.ResetEnvironment(EnvirTypes.Mountain);
                            curEnvDatCom.ResetEnvironment(EnvirTypes.AdultForest);

                            curUnitDatCom.UnitType = UnitTypes.King;
                            curUnitDatCom.AmountHealth = 1;
                            curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                            curOwnUnitCom.PlayerType = PlayerTypes.Second;
                        }

                        else if (x == 8 && y == 6)
                        {
                            curEnvDatCom.ResetEnvironment(EnvirTypes.Mountain);
                            curEnvDatCom.ResetEnvironment(EnvirTypes.AdultForest);

                            curBuildDatCom.BuildType = BuildingTypes.City;
                            curOwnUnitCom.PlayerType = PlayerTypes.Second;
                        }

                        else if (x == 6 && y == 6 || x == 9 && y == 6 || x <= 9 && x >= 6 && y == 5 || x <= 9 && x >= 6 && y == 7)
                        {
                            curEnvDatCom.ResetEnvironment(EnvirTypes.Mountain);

                            curUnitDatCom.UnitType = UnitTypes.Pawn;

                            int rand = Random.Range(0, 100);

                            if (rand >= 50) curUnitDatCom.ExtraTWPawnType = ToolWeaponTypes.Sword;

                            curUnitDatCom.AmountHealth = 100;
                            curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                            curOwnUnitCom.PlayerType = PlayerTypes.Second;
                        }
                    }
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    _friendZoneUIFilt.Get1(0).IsActiveFriendZone = true;
                }


                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    invResCom.SetAmountResources(PlayerTypes.Second, ResourceTypes.Food, 999999);
                }
            }
        }
    }
}
