using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.UI.Game.General;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
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
    internal sealed class InitGameGeneralSystem : IEcsInitSystem
    {
        private EcsWorld _currentGameWorld = default;

        public void Init()
        {
            ToggleZoneComponent.ReplaceZone(Main.SceneType);


            ///Cells
            ///
            var cellGO = ResourcesComponent.PrefabConfig.CellGO;
            var whiteCellSR = ResourcesComponent.SpritesConfig.WhiteSprite;
            var blackCellSR = ResourcesComponent.SpritesConfig.BlackSprite;

            var cell_GOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var supportParentForCells = new GameObject("Cells");
            ToggleZoneComponent.Attach(supportParentForCells.transform);


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


                    var dictStartCell = new Dictionary<bool, bool>();
                    dictStartCell.Add(true, default);
                    dictStartCell.Add(false, default);

                    if (y < 3 && x > 2 && x < 12)
                    {
                        dictStartCell[true] = true;
                    }
                    else if (y > 7 && x > 2 && x < 12)
                    {
                        dictStartCell[false] = true;
                    }


                    _currentGameWorld.NewEntity()
                        .Replace(new XyCellComponent(new byte[] { x, y }))

                        .Replace(new CellDataComponent(dictStartCell))
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
                         .Replace(new CellUnitViewComponent(cell_GOs[x, y]))
                         .Replace(new UnitTypeComponent())
                         .Replace(new OwnerComponent())
                         .Replace(new OwnerBotComponent());

                    if (PhotonNetwork.OfflineMode)
                    {
                        // Bot
                    }
                }



            var generalZoneGO = new GameObject("GeneralZone");
            ToggleZoneComponent.Attach(generalZoneGO.transform);



            ///Else
            ///
            var backGroundGO = GameObject.Instantiate(ResourcesComponent.PrefabConfig.BackGroundCollider2D,
                Main.Instance.transform.position + new Vector3(7, 5.5f, 2), Main.Instance.transform.rotation);


            var listMaster = new List<byte[]>();
            var listOther = new List<byte[]>();

            for (byte x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (byte y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    if (y < 3 && x > 2 && x < 12)
                    {
                        listMaster.Add(new byte[] { x, y });
                    }
                    else if (y > 7 && x > 2 && x < 12)
                    {
                        listOther.Add(new byte[] { x, y });
                    }
                }

            var dict = new Dictionary<bool, List<byte[]>>();
            dict.Add(true, listMaster);
            dict.Add(false, listOther);


            var audioSourceParentGO = new GameObject("AudioSource");
            ToggleZoneComponent.Attach(audioSourceParentGO.transform);


            var infoEnt = _currentGameWorld.NewEntity()
                .Replace(new InputComponent())
                .Replace(new SelectorComponent(CellClickTypes.Start))
                .Replace(new IdxAvailableCellsComponent(new Dictionary<AvailableCellTypes, List<byte>>()))
                .Replace(new GeneralZoneViewComponent(generalZoneGO))
                .Replace(new BackgroundComponent(backGroundGO))
                .Replace(new ForFillAvailCellsCom())

                .Replace(new XyStartCellsComponent(dict))
                .Replace(new UnitsInGameInfoComponent(new Dictionary<UnitTypes, Dictionary<bool, List<byte>>>()))
                .Replace(new UnitsInConditionInGameCom(new Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<byte>>>>()))
                .Replace(new BuildsInGameComponent(new Dictionary<BuildingTypes, Dictionary<bool, List<byte>>>()))

                .Replace(new UpgradesBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, int>>()))
                .Replace(new InventorUnitsComponent(new Dictionary<UnitTypes, Dictionary<bool, int>>()))
                .Replace(new InventorResourcesComponent(new Dictionary<ResourceTypes, Dictionary<bool, int>>()))
                .Replace(new FromInfoComponent())
                .Replace(new MistakeUEComponent(new Dictionary<ResourceTypes, UnityEvent>()))
                .Replace(new SoundViewComponent(audioSourceParentGO));


            infoEnt.Get<GeneralZoneViewComponent>().Attach(backGroundGO.transform);



            ///Canvas
            ///

            CanvasComponent.ReplaceZone(Main.SceneType);

            var upZone_GO = CanvasComponent.FindUnderParent("UpZone");
            var centerZone_GO = CanvasComponent.FindUnderParent("CenterZone");
            var downZone_GO = CanvasComponent.FindUnderParent("DownZone");
            var leftZone_GO = CanvasComponent.FindUnderParent("LeftZone");
            var rightZone_GO = CanvasComponent.FindUnderParent("RightZone");

            _currentGameWorld.NewEntity()
                ///Up
                //.Replace(new EconomyDataUICom(new Dictionary<ResourceTypes, Dictionary<bool, int>>()))
                .Replace(new EconomyViewUICom(upZone_GO))
                .Replace(new LeaveViewUIComponent(CanvasComponent.FindUnderParent<Button>("ButtonLeave")))

                ///Center
                .Replace(new EndGameDataUIComponent())
                .Replace(new EndGameViewUIComponent(centerZone_GO))
                .Replace(new ReadyViewUICom(centerZone_GO.transform.Find("ReadyZone").gameObject))
                .Replace(new ReadyDataUICom(new Dictionary<bool, bool>()))
                .Replace(new MotionsViewUIComponent(centerZone_GO))
                .Replace(new MotionsDataUIComponent())
                .Replace(new MistakeViewUICom(centerZone_GO))
                .Replace(new MistakeDataUICom(new Dictionary<ResourceTypes, bool>()))

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
        }
    }
}
