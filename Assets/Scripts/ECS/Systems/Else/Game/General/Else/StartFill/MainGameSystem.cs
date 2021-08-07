using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.UI.Game.General;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.Game.General.Systems.StartFill
{
    internal sealed class MainGameSystem : IEcsInitSystem
    {
        private EcsWorld _currentGameWorld;

        internal static GameObject[,] CellGOs;


        private static EcsEntity _infoEnt;
        internal static ref XyUnitsContitionComponent XyUnitsContitionCom => ref _infoEnt.Get<XyUnitsContitionComponent>();
        internal static ref XyBuildingsComponent XyBuildingsCom => ref _infoEnt.Get<XyBuildingsComponent>();
        internal static ref MistakeUEComponent MistakeCom => ref _infoEnt.Get<MistakeUEComponent>();
        internal static ref XyStartCellsComponent XyStartCellsCom => ref _infoEnt.Get<XyStartCellsComponent>();


        public void Init()
        {

            ToggleZoneComponent.ReplaceZone(Main.SceneType);
            CanvasComponent.ReplaceZone(Main.SceneType);


            var cellGO = ResourcesComponent.PrefabConfig.CellGO;
            var whiteCellSR = ResourcesComponent.SpritesConfig.WhiteSprite;
            var blackCellSR = ResourcesComponent.SpritesConfig.BlackSprite;

            CellGOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var supportParentForCells = new GameObject("Cells");
            ToggleZoneComponent.Attach(supportParentForCells.transform);


            //_cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (byte x = 0; x < CELL_COUNT_X; x++)
                for (byte y = 0; y < CELL_COUNT_Y; y++)
                {
                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            CellGOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(CellGOs[x, y], x, y);
                        }
                        if (x % 2 != 0)
                        {
                            CellGOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(CellGOs[x, y], x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            CellGOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(CellGOs[x, y], x, y);
                        }
                        if (x % 2 == 0)
                        {
                            CellGOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(CellGOs[x, y], x, y);
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

                    CellGOs[x, y].transform.SetParent(supportParentForCells.transform);

                    CellGOs[x, y].transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);



                    var cellEnt = _currentGameWorld.NewEntity()
                        .Replace(new XyCellComponent(new byte[] { x, y }))

                        .Replace(new CellEnvironDataCom(new Dictionary<EnvironmentTypes, bool>()))
                        .Replace(new CellEnvironViewCom(CellGOs[x, y]))

                        .Replace(new CellFireDataComponent())
                        .Replace(new CellFireViewComponent(CellGOs[x, y]))
                        
                        
                        
                        ;

                    var sr = MainGameSystem.CellGOs[x, y].transform.Find("ProtectRelax").GetComponent<SpriteRenderer>();
                    _cellProtectRelaxEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = MainGameSystem.CellGOs[x, y].transform.Find("MaxSteps").GetComponent<SpriteRenderer>();
                    _cellMaxStepsEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));


                    _currentGameWorld.NewEntity()
                         .Replace(new BuildingTypeComponent())
                         .Replace(new OwnerComponent())
                         .Replace(new OwnerBotComponent())
                         .Replace(new TimeStepsComponent());



                    if (PhotonNetwork.IsMasterClient)
                    {
                        int random;

                        if (y == 4 || y == 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= START_MOUNTAIN_PERCENT)
                                cellEnt.Get<CellEnvironDataCom>().SetNewEnvironment(EnvironmentTypes.Mountain);
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= START_FOREST_PERCENT)
                                {
                                    cellEnt.Get<CellEnvironDataCom>().SetNewEnvironment(EnvironmentTypes.AdultForest);
                                }
                            }
                        }
                        else
                        {

                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= START_FOREST_PERCENT)
                            {
                                cellEnt.Get<CellEnvironDataCom>().SetNewEnvironment(EnvironmentTypes.AdultForest);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= START_FERTILIZER_PERCENT)
                                {
                                    cellEnt.Get<CellEnvironDataCom>().SetNewEnvironment(EnvironmentTypes.Fertilizer);
                                }
                            }


                            if (y == 5)
                            {

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= START_HILL_PERCENT)
                                    cellEnt.Get<CellEnvironDataCom>().SetNewEnvironment(EnvironmentTypes.Hill);

                            }
                        }
                    }

                    if (PhotonNetwork.OfflineMode)
                    {
                        // Bot
                    }
                }


            var generalZoneGO = new GameObject("GeneralZone");
            ToggleZoneComponent.Attach(generalZoneGO.transform);

            var backGroundGO = GameObject.Instantiate(ResourcesComponent.PrefabConfig.BackGroundCollider2D,
                Main.Instance.transform.position + new Vector3(7, 5.5f, 2), Main.Instance.transform.rotation);


            var listMaster = new List<int[]>();
            var listOther = new List<int[]>();

            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    if (y < 3 && x > 2 && x < 12)
                    {
                        listMaster.Add(new int[] { x, y });
                    }
                    else if (y > 7 && x > 2 && x < 12)
                    {
                        listOther.Add(new int[] { x, y });
                    }
                }

            var dict = new Dictionary<bool, List<int[]>>();
            dict.Add(true, listMaster);
            dict.Add(false, listOther);


            _infoEnt = _currentGameWorld.NewEntity()
                .Replace(new InputComponent())

                .Replace(new SelectorComponent(new int[2]))

                .Replace(new AvailableCellsComponent(new Dictionary<AvailableCellTypes, List<int[]>>()))
                .Replace(new GeneralZoneViewComponent(generalZoneGO))
                .Replace(new BackgroundComponent(backGroundGO))

                .Replace(new XyStartCellsComponent(dict))
                .Replace(new XyUnitsComponent(new Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>()))
                .Replace(new XyUnitsContitionComponent(new Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>>()))
                .Replace(new XyBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, List<int[]>>>()))

                .Replace(new UpgradesBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, int>>()))
                .Replace(new InventorUnitsComponent(new Dictionary<UnitTypes, Dictionary<bool, int>>()))
                .Replace(new InventorResourcesComponent(new Dictionary<ResourceTypes, Dictionary<bool, int>>()))
                .Replace(new FromInfoComponent())
                .Replace(new MistakeUEComponent(new Dictionary<ResourceTypes, UnityEvent>()));


            _infoEnt.Get<GeneralZoneViewComponent>().Attach(backGroundGO.transform);


            var upZone_GO = CanvasComponent.FindUnderParent("UpZone");
            var centerZone_GO = CanvasComponent.FindUnderParent("CenterZone");
            var downZone_GO = CanvasComponent.FindUnderParent("DownZone");
            var leftZone_GO = CanvasComponent.FindUnderParent("LeftZone");
            var rightZone_GO = CanvasComponent.FindUnderParent("RightZone");

            var uIEnt = _currentGameWorld.NewEntity()
                ///Up
                .Replace(new EconomyDataUICom(new Dictionary<ResourceTypes, Dictionary<bool, int>>()))
                .Replace(new EconomyViewUICom(upZone_GO))
                .Replace(new LeaveViewUIComponent(CanvasComponent.FindUnderParent<Button>("ButtonLeave")))

                ///Center
                .Replace(new EndGameDataUIComponent())
                .Replace(new EndGameViewUIComponent(centerZone_GO))
                .Replace(new ReadyViewUICom(centerZone_GO.transform.Find("ReadyZone").gameObject))
                .Replace(new ReadyDataUICom(new Dictionary<bool, bool>()))
                .Replace(new MotionsViewUIComponent(centerZone_GO.transform.Find("MotionZone").transform.Find("MotionText").GetComponent<TextMeshProUGUI>()))
                .Replace(new MotionsDataUIComponent())
                .Replace(new MistakeViewUICom(centerZone_GO.transform.Find("MistakeZone").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()))

                ///Down
                .Replace(new TakerUnitsViewUICom(downZone_GO))
                .Replace(new DonerViewUIComponent(downZone_GO))
                .Replace(new DonerDataUIComponent(new Dictionary<bool, bool>()))

                ///Left
                .Replace(new BuildZoneViewUICom(leftZone_GO))
                .Replace(new EnvirZoneDataUICom())
                .Replace(new EnvirZoneViewUICom(leftZone_GO))

                ///Right
                .Replace(new UnitZoneViewUICom(rightZone_GO));



            if (PhotonNetwork.IsMasterClient)
            {
                CameraComponent.ResetRotation();
                CameraComponent.SetPosition(Main.Instance.transform.position + CameraComponent.PosForCamera);

                if (SaverComponent.StepModeType == StepModeTypes.ByQueue)
                {
                    uIEnt.Get<DonerDataUIComponent>().SetDoned(false, true);
                }

                ref var unitInventorCom = ref _infoEnt.Get<InventorUnitsComponent>();
                ref var inventorResCom = ref _infoEnt.Get<InventorResourcesComponent>();


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
                CameraComponent.SetRotation(new Quaternion(0, 0, 180, 0));
                CameraComponent.SetPosition(Main.Instance.transform.position + CameraComponent.PosForCamera + new Vector3(0, 0.5f, 0));
            }

        }
    }
}
