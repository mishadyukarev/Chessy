using Assets.Scripts.ECS.Entities.Game.General.Base.Containers.Cell;
using Assets.Scripts.ECS.Entities.Game.General.Base.View.Containers.Cell;
using Assets.Scripts.ECS.Entities.Game.General.Cells.View.Containers.Cell;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Game.Else.CellBuildings;
using Assets.Scripts.Workers.Game.Else.CellEnvir;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Units;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.CellEnvirDataWorker;

namespace Assets.Scripts.ECS.Entities.Game.General.Cells.View
{
    public sealed class EntGameGeneralCellViewManager
    {
        private CellBlocksViewContainerEnts _cellBlocksViewContainerEnts;
        private CellUnitsViewContainerEnts _cellUnitsViewContainerEnts;
        private CellBarsViewContainerEnts _cellBarsViewContainerEnts;
        private CellSupVisViewContainerEnts _cellSupVisViewContainerEnts;
        private CellFireViewContainerEnts _cellFireViewContainerEnts;
        private CellBuildViewContainerEnts _cellBuildViewContainerEnts;
        private CellEnvirViewContainerEnts _cellEnvirViewContainerEnts;
        private CellViewContainerEnts _cellViewContainerEnts;

        internal EntGameGeneralCellViewManager(EcsWorld gameWorld, EntCommonManager entCommonManager)
        {
            var cellGO = entCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.CellGO;
            var whiteCellSR = entCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.WhiteSprite;
            var blackCellSR = entCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.BlackSprite;

            var cellGOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

            var supportParentForCells = new GameObject("Cells");
            supportParentForCells.transform.SetParent(entCommonManager.ToggleSceneParentGOZoneEnt_ParentCom.ParentGO.transform);


            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            cellGOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cellGOs[x, y], x, y);
                        }
                        if (x % 2 != 0)
                        {
                            cellGOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cellGOs[x, y], x, y);
                        }
                    }
                    if (y % 2 != 0)
                    {
                        if (x % 2 != 0)
                        {
                            cellGOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cellGOs[x, y], x, y);
                        }
                        if (x % 2 == 0)
                        {
                            cellGOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Main.Instance.gameObject);
                            SetActive(cellGOs[x, y], x, y);
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

                    cellGOs[x, y].transform.SetParent(supportParentForCells.transform);

                    cellGOs[x, y].transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
                }


            _cellBlocksViewContainerEnts = new CellBlocksViewContainerEnts(cellGOs, gameWorld);
            new CellBlocksViewWorker(_cellBlocksViewContainerEnts);


            _cellBarsViewContainerEnts = new CellBarsViewContainerEnts(cellGOs, gameWorld);
            new CellSupVisBarsWorker(_cellBarsViewContainerEnts);


            _cellSupVisViewContainerEnts = new CellSupVisViewContainerEnts(cellGOs, gameWorld);
            new CellSupVisViewWorker(_cellSupVisViewContainerEnts);


            _cellFireViewContainerEnts = new CellFireViewContainerEnts(cellGOs, gameWorld);
            new CellFireVisWorker(_cellFireViewContainerEnts);


            _cellUnitsViewContainerEnts = new CellUnitsViewContainerEnts(cellGOs, gameWorld);
            new CellUnitsViewWorker(_cellUnitsViewContainerEnts);


            _cellBuildViewContainerEnts = new CellBuildViewContainerEnts(cellGOs, gameWorld);
            new CellBuildingsViewWorker(_cellBuildViewContainerEnts);


            _cellEnvirViewContainerEnts = new CellEnvirViewContainerEnts(cellGOs, gameWorld);
            new CellEnvirViewWorker(_cellEnvirViewContainerEnts);


            _cellViewContainerEnts = new CellViewContainerEnts(cellGOs, gameWorld);
            new CellViewWorker(_cellViewContainerEnts);


            if (PhotonNetwork.IsMasterClient)
            {
                for (int x = 0; x < CELL_COUNT_X; x++)
                    for (int y = 0; y < CELL_COUNT_Y; y++)
                    {
                        var xy = new int[] { x, y };

                        int random;

                        if (y == 4 || y == 6)
                        {
                            random = Random.Range(1, 100);
                            if (random <= START_MOUNTAIN_PERCENT)
                                SetNewEnvironment(EnvironmentTypes.Mountain, xy);
                            else
                            {
                                random = Random.Range(1, 100);
                                if (random <= START_FOREST_PERCENT)
                                {
                                    SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                                }
                            }
                        }
                        else
                        {

                            random = Random.Range(1, 100);
                            if (random <= START_FOREST_PERCENT)
                            {
                                SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                            }
                            else
                            {
                                random = Random.Range(1, 100);
                                if (random <= START_FERTILIZER_PERCENT)
                                {
                                    SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                                }
                            }


                            if (y == 5)
                            {

                                random = Random.Range(1, 100);
                                if (random <= START_HILL_PERCENT)
                                    SetNewEnvironment(EnvironmentTypes.Hill, xy);

                            }
                        }
                    }
            }


            if (PhotonNetwork.OfflineMode)
            {
                // Bot
            }
        }
    }
}
