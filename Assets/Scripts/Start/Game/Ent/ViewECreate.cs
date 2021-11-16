﻿using Chessy.Common;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class ViewECreate : IEcsInitSystem
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
            var cell = PrefabResComC.CellGO;
            var white = SpritesResComC.Sprite(SpriteGameTypes.WhiteCell);
            var black = SpritesResComC.Sprite(SpriteGameTypes.BlackCell);

            var cell_GOs = new GameObject[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            var suppParCells = new GameObject("Cells");
            suppParCells.transform.SetParent(genZone.transform);

            byte idx_0 = 0;

            for (byte x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (byte y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var curParCell = cell_GOs[x, y];

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            curParCell = CreateGameObject(cell, black, x, y, MainGoVC.Main_GO);
                            SetActive(curParCell, x, y);
                        }
                        if (x % 2 != 0)
                        {
                            curParCell = CreateGameObject(cell, white, x, y, MainGoVC.Main_GO);
                            SetActive(curParCell, x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            curParCell = CreateGameObject(cell, black, x, y, MainGoVC.Main_GO);
                            SetActive(curParCell, x, y);
                        }
                        if (x % 2 == 0)
                        {
                            curParCell = CreateGameObject(cell, white, x, y, MainGoVC.Main_GO);
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




                    ++idx_0;
                }



            var backGroundGO = GameObject.Instantiate(PrefabResComC.BackGroundCollider2D,
                MainGoVC.Main_GO.transform.position + new Vector3(7, 5.5f, 2), MainGoVC.Main_GO.transform.rotation);

            new ClipResourcesVC(true);
            new BackgroundVC(backGroundGO, PhotonNetwork.IsMasterClient);
            new GenerZoneVC(genZone);
            new CameraVC(Camera.main, new Vector3(7.4f, 4.8f, -2));
            

            GenerZoneVC.Attach(backGroundGO.transform);
        }

        public static void Dispose()
        {
            UnityEngine.Object.Destroy(RpcViewC.RpcView_GO);
            WhereBuildsC.Start();
        }
    }
}
