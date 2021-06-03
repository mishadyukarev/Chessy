using System;
using UnityEngine;
using static Main;

internal sealed class ObjectPoolGame : ObjectPool
{
    internal GameObject BackGroundCollider2D;
    internal AudioSource AudioSource;
    internal AudioSource AttackAudioSource;

    #region Cell

    internal GameObject SupportParentForCells;

    internal GameObject[,] CellsGO;

    internal GameObject[,] CellUnitKingGOs;
    internal GameObject[,] CellUnitPawnGOs;
    internal GameObject[,] CellUnitRookGOs;
    internal GameObject[,] CellUnitBishopGOs;

    internal SpriteRenderer[,] CellUnitPawnSRs;
    internal SpriteRenderer[,] CellUnitKingSRs;
    internal SpriteRenderer[,] CellUnitRookSRs;
    internal SpriteRenderer[,] CellUnitBishopSRs;

    internal GameObject[,] CellEnvironmentFoodGOs;
    internal GameObject[,] CellEnvironmentForestGOs;
    internal GameObject[,] CellEnvironmentYoungForestGOs;
    internal GameObject[,] CellEnvironmentHillGOs;
    internal GameObject[,] CellEnvironmentMountainGOs;

    internal GameObject[,] CellSupportVisionSelectorGOs;
    internal GameObject[,] CellSupportVisionSpawnGOs;
    internal GameObject[,] CellSupportVisionWayUnitGOs;
    internal GameObject[,] CellSupportVisionEnemyGOs;
    internal GameObject[,] CellSupportVisionUniqueAttackGOs;
    internal GameObject[,] CellSupportVisionZoneGOs;
    internal GameObject[,] CellSupportVisionFertilizerGOs;
    internal GameObject[,] CellSupportVisionForestGOs;
    internal GameObject[,] CellSupportVisionOreGOs;

    internal GameObject[,] CellBuildingCityGOs;
    internal GameObject[,] CellBuildingFarmGOs;
    internal GameObject[,] CellBuildingWoodcutterGOs;
    internal GameObject[,] CellBuildingMineGOs;

    internal GameObject[,] CellEffectFireGOs;

    #endregion


    internal override void Spawn(ResourcesLoad resourcesLoad,Builder builder)
    {
        base.Spawn(resourcesLoad, builder);


        var audioSourceGO = builder.CreateGameObject("AudioSource", new Type[] { typeof(AudioSource) });
        AudioSource = audioSourceGO.GetComponent<AudioSource>();
        AudioSource.clip = resourcesLoad.SoundConfig.MistakeAudioClip;

        AttackAudioSource = builder.CreateGameObject("AttackAudioSource", new Type[] { typeof(AudioSource) }).GetComponent<AudioSource>();
        AttackAudioSource.clip = resourcesLoad.SoundConfig.AttackAudioClip;

        BackGroundCollider2D = GameObject.Instantiate(resourcesLoad.PrefabConfig.BackGroundCollider2D,
            Instance.transform.position + new Vector3(7, 5.5f, 2), Instance.transform.transform.rotation);

        SpawnCells(resourcesLoad, Instance.StartValuesGameConfig, Instance.gameObject);
    }

    public override void Dispose()
    {
        base.Dispose();

        if (BackGroundCollider2D != default) GameObject.Destroy(BackGroundCollider2D);
        if (AudioSource != default) GameObject.Destroy(AudioSource.gameObject);
        if (AttackAudioSource != default) GameObject.Destroy(AttackAudioSource.gameObject);

        if (SupportParentForCells != default) GameObject.Destroy(SupportParentForCells);
    }

    internal void SetActiveAll(bool isActive)
    {
        BackGroundCollider2D.SetActive(isActive);
        AudioSource.gameObject.SetActive(isActive);
        AttackAudioSource.gameObject.SetActive(isActive);

        SupportParentForCells.SetActive(isActive);

    }

    private void SpawnCells(ResourcesLoad resourcesLoad, StartValuesGameConfig startValues, GameObject mainGameGO)
    {
        var cellGO = resourcesLoad.PrefabConfig.CellGO;
        var whiteCellSR = resourcesLoad.SpritesConfig.WhiteSprite;
        var blackCellSR = resourcesLoad.SpritesConfig.BlackSprite;


        CellsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];


        CellEnvironmentFoodGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellEnvironmentMountainGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellEnvironmentForestGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellEnvironmentYoungForestGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellEnvironmentHillGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];


        CellSupportVisionSelectorGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionSpawnGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionWayUnitGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionEnemyGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionUniqueAttackGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionZoneGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionFertilizerGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionForestGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellSupportVisionOreGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];


        CellUnitKingGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellUnitPawnGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellUnitRookGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellUnitBishopGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        CellUnitKingSRs = new SpriteRenderer[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellUnitPawnSRs = new SpriteRenderer[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellUnitRookSRs = new SpriteRenderer[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellUnitBishopSRs = new SpriteRenderer[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];


        CellBuildingCityGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellBuildingFarmGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellBuildingWoodcutterGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        CellBuildingMineGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        CellEffectFireGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];



        SupportParentForCells = new GameObject("Cells");

        for (int x = 0; x < startValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < startValues.CELL_COUNT_Y; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        CellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y, mainGameGO);
                        SetActive(CellsGO[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        CellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y, mainGameGO);
                        SetActive(CellsGO[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        CellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y, mainGameGO);
                        SetActive(CellsGO[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        CellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y, mainGameGO);
                        SetActive(CellsGO[x, y], x, y);
                    }
                }



                CellsGO[x, y].transform.SetParent(SupportParentForCells.transform);



                GameObject parentGO = CellsGO[x, y].transform.Find("Environments").gameObject;

                CellEnvironmentFoodGOs[x, y] = parentGO.transform.Find("Food").gameObject;
                CellEnvironmentMountainGOs[x, y] = parentGO.transform.Find("Mountain").gameObject;
                CellEnvironmentForestGOs[x, y] = parentGO.transform.Find("Tree").gameObject;
                CellEnvironmentYoungForestGOs[x, y] = parentGO.transform.Find("YoungTree").gameObject;
                CellEnvironmentHillGOs[x, y] = parentGO.transform.Find("Hill").gameObject;



                parentGO = CellsGO[x, y].transform.Find("SupportVisions").gameObject;

                CellSupportVisionSelectorGOs[x, y] = parentGO.transform.Find("Selector").gameObject;
                CellSupportVisionSpawnGOs[x, y] = parentGO.transform.Find("Assignment").gameObject;
                CellSupportVisionWayUnitGOs[x, y] = parentGO.transform.Find("WayOfUnit").gameObject;
                CellSupportVisionEnemyGOs[x, y] = parentGO.transform.Find("Enemy").gameObject;
                CellSupportVisionUniqueAttackGOs[x, y] = parentGO.transform.Find("UniqueAttack").gameObject;
                CellSupportVisionZoneGOs[x, y] = parentGO.transform.Find("Zone").gameObject;
                CellSupportVisionFertilizerGOs[x, y] = parentGO.transform.Find("Fertilizer").gameObject;
                CellSupportVisionForestGOs[x, y] = parentGO.transform.Find("Forest").gameObject;
                CellSupportVisionOreGOs[x, y] = parentGO.transform.Find("Ore").gameObject;



                parentGO = CellsGO[x, y].transform.Find("Units").gameObject;

                CellUnitKingGOs[x, y] = parentGO.transform.Find("King").gameObject;
                CellUnitPawnGOs[x, y] = parentGO.transform.Find("Pawn").gameObject;
                CellUnitRookGOs[x, y] = parentGO.transform.Find("Rook").gameObject;
                CellUnitBishopGOs[x, y] = parentGO.transform.Find("Bishop").gameObject;

                CellUnitKingSRs[x, y] = CellUnitKingGOs[x, y].GetComponent<SpriteRenderer>();
                CellUnitPawnSRs[x, y] = CellUnitPawnGOs[x, y].GetComponent<SpriteRenderer>();
                CellUnitRookSRs[x, y] = CellUnitRookGOs[x, y].GetComponent<SpriteRenderer>();
                CellUnitBishopSRs[x, y] = CellUnitBishopGOs[x, y].GetComponent<SpriteRenderer>();


                parentGO = CellsGO[x, y].transform.Find("Buildings").gameObject;

                CellBuildingCityGOs[x, y] = parentGO.transform.Find("City").gameObject;
                CellBuildingFarmGOs[x, y] = parentGO.transform.Find("Farm").gameObject;
                CellBuildingWoodcutterGOs[x, y] = parentGO.transform.Find("Woodcutter").gameObject;
                CellBuildingMineGOs[x, y] = parentGO.transform.Find("Mine").gameObject;


                parentGO = CellsGO[x, y].transform.Find("Effects").gameObject;

                CellEffectFireGOs[x, y] = parentGO.transform.Find("Fire").gameObject;
            }
        }
    }

    private GameObject CreatGameObject(GameObject go, Sprite sprite, int x, int y, GameObject mainGameGO)
    {
        var goo = GameObject.Instantiate(go, mainGameGO.transform.position + new Vector3(x, y, mainGameGO.transform.position.z), mainGameGO.transform.rotation);

        var SR = goo.GetComponent<SpriteRenderer>();
        SR.sprite = sprite;

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