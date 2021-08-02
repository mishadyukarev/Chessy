using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    internal sealed class StartSpawnCellsViewSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        internal static GameObject[,] CellGOs;

        public void Init()
        {
            var cellGO = Main.Instance.ECSmanager.EntDataCommElseManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.CellGO;
            var whiteCellSR = Main.Instance.ECSmanager.EntDataCommElseManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.WhiteSprite;
            var blackCellSR = Main.Instance.ECSmanager.EntDataCommElseManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.BlackSprite;

            CellGOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var supportParentForCells = new GameObject("Cells");
            supportParentForCells.transform.SetParent(Main.Instance.ECSmanager.EntDataCommElseManager.ToggleSceneParentGOZoneEnt_ParentCom.ParentGO.transform);


            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
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
                }
        }
    }
}
