using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static Main;

public sealed class SpawnAllGeneralEntities
{
    public void SpawnCells(EntitiesGeneralManager entitiesGeneralManager, ResourcesLoadManager resourcesLoadManager, NameValueManager nameValueManager)
    {
        var cellGO = resourcesLoadManager.CellGO;
        var whiteCellSR = resourcesLoadManager.WhiteCellSpriteRender;
        var blackCellSR = resourcesLoadManager.BlackCellSpriteRender;

        var cellsGO = new GameObject[nameValueManager.CELL_COUNT_X, nameValueManager.CELL_COUNT_Y];


        // Setting cells on the map
        for (int x = 0; x < nameValueManager.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < nameValueManager.CELL_COUNT_Y; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        cellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(cellsGO[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        cellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(cellsGO[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        cellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(cellsGO[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        cellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(cellsGO[x, y], x, y);
                    }
                }
            }
        }



        //var cellsGO = _cellsGO.Where(c => c != null).ToArray();
        entitiesGeneralManager.CreateCellArray(nameValueManager.CELL_COUNT_X, nameValueManager.CELL_COUNT_Y);
        GameObject supportParent = new GameObject("Cells");

        for (int x = 0; x < nameValueManager.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < nameValueManager.CELL_COUNT_Y; y++)
            {
                cellsGO[x, y].transform.SetParent(supportParent.transform);


                bool isStartMaster = false;
                bool isStartOther = false;
                if (y < 3 && x > 2 && x < 12) isStartMaster = true;
                if (y > 8 && x > 2 && x < 12) isStartOther = true;

                CellComponent cellComponent = new CellComponent(isStartMaster, isStartOther, supportParent, cellsGO[x, y]);



                var mountainGO = cellsGO[x, y].transform.Find("MountainP").gameObject;
                var treeGO = cellsGO[x, y].transform.Find("TreeP").gameObject;
                var hillGO = cellsGO[x, y].transform.Find("HillP").gameObject;

                CellComponent.EnvironmentComponent cellEnvironmentComponent
                    = new CellComponent.EnvironmentComponent(mountainGO, treeGO, hillGO);



                var selectorVisionGO = cellsGO[x, y].transform.Find("SelectorP").gameObject;
                var assignmentVisionGO = cellsGO[x, y].transform.Find("AssignmentP").gameObject;
                var wayOfUnitVisionGO = cellsGO[x, y].transform.Find("WayOfUnitVisionP").gameObject;
                var enemyVisionGO = cellsGO[x, y].transform.Find("EnemyVisionP").gameObject;

                CellComponent.SupportVisionComponent cellSupportVisionComponent
                    = new CellComponent.SupportVisionComponent(selectorVisionGO, assignmentVisionGO, wayOfUnitVisionGO, enemyVisionGO);



                CellComponent.UnitComponent cellUnitComponent = new CellComponent.UnitComponent
                    (nameValueManager, Instance.MasterClient, cellsGO[x, y].transform.Find("UnitPawn").gameObject);


                CellComponent.BuildingComponent cellBuildingComponent = new CellComponent.BuildingComponent
                    (cellsGO[x, y].transform.Find("Camp").gameObject);


                entitiesGeneralManager.CreateCellArrayEntity(x, y);

                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellEnvironmentComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellSupportVisionComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellUnitComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellBuildingComponent);
            }
        }

        for (int x = 0; x < nameValueManager.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < nameValueManager.CELL_COUNT_Y; y++)
            {
                entitiesGeneralManager.AddRefComponentsToCell(x, y);
            }
        }
    }

    private GameObject CreatGameObject(GameObject go, SpriteRenderer spriteRenderer, int x, int y)
    {
        var mainGO = Instance.gameObject;

        var goo = Object.Instantiate(go, mainGO.transform.position + new Vector3(x, y, mainGO.transform.position.z), mainGO.transform.rotation);

        var SR = goo.GetComponent<SpriteRenderer>();
        SR.sprite = spriteRenderer.sprite;

        return goo;
    }

    private void SetActive(GameObject go, int x, int y)
    {
        if (x >= 0 && y == 0 || x >= 0 && y == 11 ||
        x == 0 && y >= 0 || x == 14 && y >= 0 ||
        x == 1 && y == 1 || x == 2 && y == 1 || x == 12 && y == 1 || x == 13 && y == 1 ||
        x == 1 && y == 10 || x == 2 && y == 10 || x == 12 && y == 10 || x == 13 && y == 10)
            go.SetActive(false);
    }
}