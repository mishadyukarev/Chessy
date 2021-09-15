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
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
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
    internal sealed class SpawnGameSys : IEcsInitSystem
    {
        private EcsWorld _currentGameWorld = default;

        private EcsFilter<DonerDataUIComponent> _donerFilter = default;
        private EcsFilter<InventorResourcesComponent> _inventorResFilter = default;
        private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerComponent, OwnerBotComponent> _cellBuildFilter = default;


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


            for (byte x = 0; x < CELL_COUNT_X; x++)
                for (byte y = 0; y < CELL_COUNT_Y; y++)
                {
                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            cell_GOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cell_GOs[x, y], x, y);
                        }
                        if (x % 2 != 0)
                        {
                            cell_GOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cell_GOs[x, y], x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            cell_GOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cell_GOs[x, y], x, y);
                        }
                        if (x % 2 == 0)
                        {
                            cell_GOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cell_GOs[x, y], x, y);
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

                    cell_GOs[x, y].transform.SetParent(supportParentForCells.transform);

                    cell_GOs[x, y].transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);


                    _currentGameWorld.NewEntity()
                        .Replace(new XyCellComponent(new byte[] { x, y }))

                        .Replace(new CellViewComponent(cell_GOs[x, y]))

                        .Replace(new CellEnvironDataCom(new Dictionary<EnvironmentTypes, bool>()))
                        .Replace(new CellEnvironViewCom(cell_GOs[x, y]))
                        .Replace(new CellFireDataComponent())
                        .Replace(new CellFireViewComponent(cell_GOs[x, y]))
                        .Replace(new CellBlocksViewComponent(cell_GOs[x, y]))
                        .Replace(new CellBarsViewComponent(cell_GOs[x, y]))
                        .Replace(new CellSupViewComponent(cell_GOs[x, y]));


                    _currentGameWorld.NewEntity()
                         .Replace(new CellBuildDataComponent())
                         .Replace(new CellBuildViewComponent(cell_GOs[x, y]))
                         .Replace(new OwnerComponent())
                         .Replace(new OwnerBotComponent());


                    _currentGameWorld.NewEntity()
                         .Replace(new CellUnitDataComponent(new Dictionary<bool, bool>()))
                         .Replace(new CellUnitMainViewComp(cell_GOs[x, y]))
                         .Replace(new CellUnitExtraViewComp(cell_GOs[x, y]))
                         .Replace(new OwnerComponent())
                         .Replace(new OwnerBotComponent());
                }



            ///Else
            ///
            var backGroundGO = GameObject.Instantiate(ResourcesComponent.PrefabConfig.BackGroundCollider2D,
                Main.Instance.transform.position + new Vector3(7, 5.5f, 2), Main.Instance.transform.rotation);


            var audioSourceParentGO = new GameObject("AudioSource");
            audioSourceParentGO.transform.SetParent(generalZoneGO.transform);


            var infoEnt = _currentGameWorld.NewEntity()
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

            var canvasEnt = _currentGameWorld.NewEntity()
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


            if (PhotonNetwork.IsMasterClient)
            {
                _currentGameWorld.NewEntity()
                   .Replace(new InfoMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForGettingUnitMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForSettingUnitMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForAttackMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForShiftMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForDonerMasCom())
                    .Replace(new NeedActiveSomethingMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForBuildingMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForSeedingMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ConditionMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForCircularAttackMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForCreatingUnitMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForDestroyMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForFireMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForReadyMasCom())
                    .Replace(new NeedActiveSomethingMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForUpgradeMasCom());

                _currentGameWorld.NewEntity()
                    .Replace(new ForGiveTakeToolWeaponComp());


                canvasEnt.Get<DonerDataUIComponent>().SetDoned(false, true);

                int random;

                foreach (byte curIdxCell in _cellEnvFilter)
                {
                    var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);

                    ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(curIdxCell);

                    if (_cellViewFilter.Get1(curIdxCell).IsActiveParent)
                    {
                        if (curXyCell[1] >= 4 && curXyCell[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironmentValues.START_MOUNTAIN_PERCENT)
                                curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.Mountain);
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_FOREST_PERCENT)
                                {
                                    curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_HILL_PERCENT)
                                    curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.Hill);
                            }
                        }
                        else
                        {

                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= EnvironmentValues.START_FOREST_PERCENT)
                            {
                                curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= EnvironmentValues.START_FERTILIZER_PERCENT)
                                {
                                    curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.Fertilizer);
                                }
                            }
                        }
                    }
                }

                CameraComponent.ResetRotation();
                CameraComponent.SetPosition(Main.Instance.transform.position + CameraComponent.PosForCamera);

                ref var unitInventorCom = ref _inventorUnitsFilter.Get1(0);
                ref var inventorResCom = ref _inventorResFilter.Get1(0);


                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
                unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


                inventorResCom.SetAmountResources(ResourceTypes.Food, true, EconomyValues.AMOUNT_FOOD_MASTER);
                inventorResCom.SetAmountResources(ResourceTypes.Wood, true, EconomyValues.AMOUNT_WOOD_MASTER);
                inventorResCom.SetAmountResources(ResourceTypes.Ore, true, EconomyValues.AMOUNT_ORE_MASTER);
                inventorResCom.SetAmountResources(ResourceTypes.Iron, true, EconomyValues.AMOUNT_IRON_MASTER);
                inventorResCom.SetAmountResources(ResourceTypes.Gold, true, EconomyValues.AMOUNT_GOLD_MASTER);

                inventorResCom.SetAmountResources(ResourceTypes.Food, false, EconomyValues.AMOUNT_FOOD_OTHER);
                inventorResCom.SetAmountResources(ResourceTypes.Wood, false, EconomyValues.AMOUNT_WOOD_OTHER);
                inventorResCom.SetAmountResources(ResourceTypes.Ore, false, EconomyValues.AMOUNT_ORE_OTHER);
                inventorResCom.SetAmountResources(ResourceTypes.Iron, false, EconomyValues.AMOUNT_IRON_OTHER);
                inventorResCom.SetAmountResources(ResourceTypes.Gold, false, EconomyValues.AMOUNT_GOLD_OTHER);

            }

            else
            {
                _currentGameWorld.NewEntity()
                    .Replace(new FromInfoComponent());

                CameraComponent.SetRotation(new Quaternion(0, 0, 180, 0));
                CameraComponent.SetPosition(Main.Instance.transform.position + CameraComponent.PosForCamera + new Vector3(0, 0.5f, 0));
            }


            if (PhotonNetwork.OfflineMode)
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
        }

    }
}
