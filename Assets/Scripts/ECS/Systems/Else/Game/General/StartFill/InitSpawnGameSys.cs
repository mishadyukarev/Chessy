using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.UI.Game.General;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Center;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Down;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Components.View.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Center;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.Game.General.Systems.StartFill
{
    internal sealed class InitSpawnGameSys : IEcsInitSystem
    {
        private EcsWorld _curGameWorld = default;

        private EcsFilter<InventorResourcesComponent> _inventorResFilter = default;
        private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellBuildFilter = default;


        public void Init()
        {
            ToggleZoneComponent.ReplaceZone(Main.CurrentSceneType);


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
                    var cirCell_GO = cell_GOs[x, y];

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            cirCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cirCell_GO, x, y);
                        }
                        if (x % 2 != 0)
                        {
                            cirCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cirCell_GO, x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            cirCell_GO = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cirCell_GO, x, y);
                        }
                        if (x % 2 == 0)
                        {
                            cirCell_GO = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cirCell_GO, x, y);
                        }
                    }

                    GameObject CreateGameObject(GameObject cellGOForCreation, Sprite sprite, int xxx, int yyy, GameObject mainGameGO)
                    {
                        var go = GameObject.Instantiate(cellGOForCreation, mainGameGO.transform.position + new Vector3(xxx, yyy, mainGameGO.transform.position.z), mainGameGO.transform.rotation);
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

                    cirCell_GO.transform.SetParent(supportParentForCells.transform);


                    var cellView_GO = cirCell_GO.transform.Find("Cell").gameObject;

                    _curGameWorld.NewEntity()
                        .Replace(new XyCellComponent(new byte[] { x, y }))

                        .Replace(new CellViewComponent(cellView_GO))

                        .Replace(new CellEnvironDataCom(new Dictionary<EnvironmentTypes, bool>()))
                        .Replace(new CellEnvironViewCom(cirCell_GO))
                        .Replace(new CellFireDataComponent())
                        .Replace(new CellFireViewComponent(cirCell_GO))
                        .Replace(new CellBlocksViewComponent(cirCell_GO))
                        .Replace(new CellBarsViewComponent(cirCell_GO))
                        .Replace(new CellSupViewComponent(cirCell_GO));


                    _curGameWorld.NewEntity()
                         .Replace(new CellBuildDataComponent())
                         .Replace(new CellBuildViewComponent(cirCell_GO))
                         .Replace(new OwnerOnlineComp())
                         .Replace(new OwnerBotComponent())
                         .Replace(new OwnerFrientComp());


                    _curGameWorld.NewEntity()
                         .Replace(new CellUnitDataComponent(new Dictionary<bool, bool>()))
                         .Replace(new CellUnitMainViewComp(cirCell_GO))
                         .Replace(new CellUnitExtraViewComp(cirCell_GO))
                         .Replace(new OwnerOnlineComp())
                         .Replace(new OwnerBotComponent())
                         .Replace(new OwnerFrientComp());

                    ++curIdx;
                }



            ///Else
            ///
            var backGroundGO = GameObject.Instantiate(ResourcesComponent.PrefabConfig.BackGroundCollider2D,
                Main.Instance.transform.position + new Vector3(7, 5.5f, 2), Main.Instance.transform.rotation);


            var audioSourceParentGO = new GameObject("AudioSource");
            audioSourceParentGO.transform.SetParent(generalZoneGO.transform);


            var infoEnt = _curGameWorld.NewEntity()
                .Replace(new InputComponent())
                .Replace(new SelectorComponent(ToolWeaponTypes.Pick))
                .Replace(new GeneralZoneViewComponent(generalZoneGO))
                .Replace(new BackgroundComponent(backGroundGO))

                .Replace(new CellsForSetUnitComp(new Dictionary<bool, List<byte>>()))
                .Replace(new AvailCellsForShiftComp(new Dictionary<bool, Dictionary<byte, List<byte>>>()))
                .Replace(new CellsArsonArcherComp(true))
                .Replace(new AvailCellsForAttackComp(true))
                .Replace(new CellsGiveTWComp(true))
                .Replace(new CellsTakeTWComp(true))
                .Replace(new WhoseMoveComp())
                .Replace(new PhotonViewComp(true))

                .Replace(new UpgradesBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, int>>()))

                .Replace(new InventorUnitsComponent(new Dictionary<UnitTypes, Dictionary<bool, int>>()))
                .Replace(new InventorResourcesComponent(new Dictionary<ResourceTypes, Dictionary<bool, int>>()))
                .Replace(new InventorToolsComp(new Dictionary<bool, Dictionary<ToolTypes, byte>>()))
                .Replace(new InventorWeaponsComp(new Dictionary<bool, Dictionary<WeaponTypes, byte>>()))

                .Replace(new FromInfoComponent())
                .Replace(new SoundEffectsComp(audioSourceParentGO));


            infoEnt.Get<GeneralZoneViewComponent>().Attach(backGroundGO.transform);



            ///Canvas
            ///

            CanvasComp.ReplaceZone(Main.CurrentSceneType);

            var upZone_GO = CanvasComp.FindUnderParent("UpZone");
            var centerZone_GO = CanvasComp.FindUnderParent("CenterZone");
            var downZone_GO = CanvasComp.FindUnderParent("DownZone");
            var leftZone_GO = CanvasComp.FindUnderParent("LeftZone");
            var rightZone_GO = CanvasComp.FindUnderParent("RightZone");

            var canvasEnt = _curGameWorld.NewEntity()
                ///Up
                .Replace(new EconomyViewUICom(upZone_GO))
                .Replace(new LeaveViewUIComponent(CanvasComp.FindUnderParent<Button>("ButtonLeave")))

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

                ///Down
                .Replace(new GetterUnitsDataUICom(new Dictionary<UnitTypes, bool>()))
                .Replace(new GetterUnitsViewUICom(downZone_GO))
                .Replace(new DonerViewUIComponent(downZone_GO))
                .Replace(new DonerDataUIComponent(new Dictionary<bool, bool>()))
                .Replace(new GiveTakeZoneViewUIComp(downZone_GO))

                ///Left
                .Replace(new BuildLeftZoneViewUICom(leftZone_GO))
                .Replace(new EnvirZoneDataUICom())
                .Replace(new EnvirZoneViewUICom(leftZone_GO))

                ///Right
                .Replace(new UnitZoneViewUICom(rightZone_GO));




            var isMaster = PhotonNetwork.IsMasterClient;

            CameraComComp.SetRotForMaster(isMaster);
            CameraComComp.SetPosForMaster(isMaster);


            if (isMaster)
            {
                _curGameWorld.NewEntity()
                   .Replace(new InfoMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForGettingUnitMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForSettingUnitMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForAttackMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForShiftMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForDonerMasCom())
                    .Replace(new NeedActiveSomethingMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForBuildingMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForSeedingMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ConditionMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForCircularAttackMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForCreatingUnitMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForDestroyMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForFireMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForReadyMasCom())
                    .Replace(new NeedActiveSomethingMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForUpgradeMasCom());

                _curGameWorld.NewEntity()
                    .Replace(new ForGiveTakeToolWeaponComp());


                canvasEnt.Get<DonerDataUIComponent>().SetDoned(false, true);

                int random;

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);

                    ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);

                    if (_cellViewFilter.Get1(curIdxCell).IsActiveParent)
                    {
                        if (curXyCell[1] >= 4 && curXyCell[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironmentValues.START_MOUNTAIN_PERCENT)
                                curEnvDatCom.SetNewEnvironment(EnvironmentTypes.Mountain);
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_FOREST_PERCENT)
                                {
                                    curEnvDatCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_HILL_PERCENT)
                                    curEnvDatCom.SetNewEnvironment(EnvironmentTypes.Hill);
                            }
                        }
                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironmentValues.START_FOREST_PERCENT)
                            {
                                curEnvDatCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_FERTILIZER_PERCENT)
                                {
                                    curEnvDatCom.SetNewEnvironment(EnvironmentTypes.Fertilizer);
                                }
                            }
                        }
                    }

                    _cellViewFilter.Get1(curIdxCell).SetRotForClient(PhotonNetwork.IsMasterClient);
                }

                
                
                ref var unitInvCom = ref _inventorUnitsFilter.Get1(0);
                ref var invResCom = ref _inventorResFilter.Get1(0);


                unitInvCom.SetAmountUnitsInInvent(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
                unitInvCom.SetAmountUnitsInInvent(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

                unitInvCom.SetAmountUnitsInInvent(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
                unitInvCom.SetAmountUnitsInInvent(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

                unitInvCom.SetAmountUnitsInInvent(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
                unitInvCom.SetAmountUnitsInInvent(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

                unitInvCom.SetAmountUnitsInInvent(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
                unitInvCom.SetAmountUnitsInInvent(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


                invResCom.SetAmountResources(ResourceTypes.Food, true, EconomyValues.AMOUNT_FOOD_MASTER);
                invResCom.SetAmountResources(ResourceTypes.Wood, true, EconomyValues.AMOUNT_WOOD_MASTER);
                invResCom.SetAmountResources(ResourceTypes.Ore, true, EconomyValues.AMOUNT_ORE_MASTER);
                invResCom.SetAmountResources(ResourceTypes.Iron, true, EconomyValues.AMOUNT_IRON_MASTER);
                invResCom.SetAmountResources(ResourceTypes.Gold, true, EconomyValues.AMOUNT_GOLD_MASTER);

                invResCom.SetAmountResources(ResourceTypes.Food, false, EconomyValues.AMOUNT_FOOD_OTHER);
                invResCom.SetAmountResources(ResourceTypes.Wood, false, EconomyValues.AMOUNT_WOOD_OTHER);
                invResCom.SetAmountResources(ResourceTypes.Ore, false, EconomyValues.AMOUNT_ORE_OTHER);
                invResCom.SetAmountResources(ResourceTypes.Iron, false, EconomyValues.AMOUNT_IRON_OTHER);
                invResCom.SetAmountResources(ResourceTypes.Gold, false, EconomyValues.AMOUNT_GOLD_OTHER);

            }

            else
            {
                _curGameWorld.NewEntity()
                    .Replace(new FromInfoComponent());
            }


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModeTypeComp.IsGameModeType(GameModeTypes.TrainingOff))
                {
                    foreach (byte curIdxCell in _xyCellFilter)
                    {
                        var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);
                        var x = curXyCell[0];
                        var y = curXyCell[1];

                        ref var curCellEnvDataComp = ref _cellEnvFilter.Get1(curIdxCell);
                        ref var curCellUnitDataComp = ref _cellUnitFilter.Get1(curIdxCell);
                        ref var curBotCellUnitComp = ref _cellUnitFilter.Get3(curIdxCell);
                        ref var curCellBuildDataComp = ref _cellBuildFilter.Get1(curIdxCell);

                        if (x == 7 && y == 6)
                        {
                            curCellEnvDataComp.ResetEnvironment(EnvironmentTypes.Mountain);
                            curCellEnvDataComp.ResetEnvironment(EnvironmentTypes.AdultForest);

                            curCellUnitDataComp.UnitType = UnitTypes.King;
                            curCellUnitDataComp.AmountHealth = 1;
                            curCellUnitDataComp.ConditionUnitType = ConditionUnitTypes.Protected;
                            curBotCellUnitComp.IsBot = true;
                        }

                        else if (x == 8 && y == 6)
                        {
                            curCellEnvDataComp.ResetEnvironment(EnvironmentTypes.Mountain);
                            curCellEnvDataComp.ResetEnvironment(EnvironmentTypes.AdultForest);

                            curCellBuildDataComp.BuildingType = BuildingTypes.City;
                            _cellBuildFilter.Get3(curIdxCell).IsBot = true;
                        }

                        else if (x == 6 && y == 6 || x == 9 && y == 6 || x <= 9 && x >= 6 && y == 5 || x <= 9 && x >= 6 && y == 7)
                        {
                            curCellEnvDataComp.ResetEnvironment(EnvironmentTypes.Mountain);

                            curCellUnitDataComp.UnitType = UnitTypes.Pawn;

                            int rand = Random.Range(0, 100);

                            if (rand >= 50) curCellUnitDataComp.ExtraTWPawnType = ToolWeaponTypes.Sword;

                            curCellUnitDataComp.AmountHealth = 100;
                            curCellUnitDataComp.ConditionUnitType = ConditionUnitTypes.Protected;
                            curBotCellUnitComp.IsBot = true;



                        }
                    }
                }

                else if (GameModeTypeComp.GameModeType == GameModeTypes.FriendOff)
                {

                }
            }
        }
    }
}
