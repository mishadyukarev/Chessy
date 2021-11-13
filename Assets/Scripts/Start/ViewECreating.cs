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
    public sealed class ViewECreating : IEcsInitSystem
    {
        private EcsWorld _curGameW = default;

        public void Init()
        {
            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);


            SoundComC.SavedVolume = SoundComC.Volume;


            ///Cells
            ///
            var cellGO = PrefabResComC.CellGO;
            var whiteCellSR = SpritesResComC.Sprite(SpriteGameTypes.WhiteCell);
            var blackCellSR = SpritesResComC.Sprite(SpriteGameTypes.BlackCell);

            var cell_GOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var suppParCells = new GameObject("Cells");
            suppParCells.transform.SetParent(genZone.transform);

            byte cur_idx = 0;

            for (byte x = 0; x < CELL_COUNT_X; x++)
                for (byte y = 0; y < CELL_COUNT_Y; y++)
                {
                    var curParCell = cell_GOs[x, y];

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            curParCell = CreateGameObject(cellGO, blackCellSR, x, y, MainGoVC.Main_GO);
                            SetActive(curParCell, x, y);
                        }
                        if (x % 2 != 0)
                        {
                            curParCell = CreateGameObject(cellGO, whiteCellSR, x, y, MainGoVC.Main_GO);
                            SetActive(curParCell, x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            curParCell = CreateGameObject(cellGO, blackCellSR, x, y, MainGoVC.Main_GO);
                            SetActive(curParCell, x, y);
                        }
                        if (x % 2 == 0)
                        {
                            curParCell = CreateGameObject(cellGO, whiteCellSR, x, y, MainGoVC.Main_GO);
                            SetActive(curParCell, x, y);
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

                    curParCell.transform.SetParent(suppParCells.transform);


                    var cellView_GO = curParCell.transform.Find("Cell").gameObject;

                    _curGameW.NewEntity()
                        .Replace(new CellVC(cellView_GO))
                        .Replace(new EnvVC(curParCell))
                        .Replace(new FireVC(curParCell))
                        .Replace(new SupportVC(curParCell))
                        .Replace(new CloudVC(curParCell))
                        .Replace(new RiverVC(curParCell.transform));


                    _curGameW.NewEntity()
                         .Replace(new BuildVC(curParCell));


                    _curGameW.NewEntity()
                         .Replace(new UnitMainVC(curParCell))
                         .Replace(new UnitExtraVC(curParCell))
                         .Replace(new BlocksVC(curParCell))
                         .Replace(new BarsVC(curParCell))
                         .Replace(new StunVC(curParCell.transform));


                    _curGameW.NewEntity()
                        .Replace(new TrailVC(curParCell.transform));




                    ++cur_idx;
                }



            var backGroundGO = GameObject.Instantiate(PrefabResComC.BackGroundCollider2D,
                MainGoVC.Main_GO.transform.position + new Vector3(7, 5.5f, 2), MainGoVC.Main_GO.transform.rotation);


            new BackgroundVC(backGroundGO, PhotonNetwork.IsMasterClient);
            new GenerZoneVC(genZone);
            new CameraVC(Camera.main, new Vector3(7.4f, 4.8f, -2));

            GenerZoneVC.Attach(backGroundGO.transform);

            ///Canvas
            ///

            CanvasC.SetCurZone(SceneTypes.Game);

            var upZone_GO = CanvasC.FindUnderCurZone("UpZone");
            var centerZone_GO = CanvasC.FindUnderCurZone("CenterZone");
            var downZone_GO = CanvasC.FindUnderCurZone("DownZone");
            var leftZone_GO = CanvasC.FindUnderCurZone("LeftZone");
            var rightZone_go = CanvasC.FindUnderCurZone("RightZone");


            var uniqAbilZone_trans = rightZone_go.transform.Find("UniqueAbilitiesZone");


            ///Up
            new EconomyViewUIC(upZone_GO);
            new LeaveViewUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave"));
            new WindUIC(upZone_GO.transform);
            new AlphaUpUIC(upZone_GO.transform);

            ///Center
            new EndGameViewUIC(centerZone_GO);
            new ReadyViewUIC(centerZone_GO.transform.Find("ReadyZone").gameObject);
            new MotionsViewUIC(centerZone_GO);
            new MistakeViewUIC(centerZone_GO);
            new KingZoneViewUIC(centerZone_GO);
            new SelectorUIC(centerZone_GO);
            new FriendZoneViewUIC(centerZone_GO.transform);
            new HintViewUIC(centerZone_GO.transform);
            new PickUpgZoneViewUIC(centerZone_GO.transform);
            new HeroesViewUIC(centerZone_GO.transform);

            ///Down
            new GetterUnitsViewUIC(downZone_GO);
            new DonerUICom(downZone_GO);
            new GiveTakeViewUIC(downZone_GO);
            new ScoutUIC(downZone_GO.transform);
            new HeroDownUIC(downZone_GO.transform);

            ///Left
            new CutyLeftZoneViewUIC(leftZone_GO);
            new EnvirZoneViewUICom(leftZone_GO);

            ///Right
            new StatZoneViewUIC(rightZone_go);
            new UniqButtonsViewC(uniqAbilZone_trans);
            new BuildAbilitViewUIC(rightZone_go.transform.Find("BuildingZone"));
            new ExtraTWZoneUIC(rightZone_go.transform);
            new EffectsIUC(rightZone_go.transform);
            new ProtectUIC(rightZone_go.transform.Find("ConditionZone"));
            new RelaxUIC(rightZone_go.transform.Find("ConditionZone"));
        }

        public static void Dispose()
        {
            UnityEngine.Object.Destroy(RpcViewC.RpcView_GO);
            WhereBuildsC.Start();
        }
    }
}
