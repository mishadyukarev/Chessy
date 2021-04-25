using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class StartSpawnGameManager : StartSpawnManager
{
    private GameObject _parentScriptsGO;
    private AudioSource _audioSource;
    private GameObject[,] _cellsGO;

    internal GameObject ParentScriptsGO => _parentScriptsGO;
    internal AudioSource AudioSource => _audioSource;


    #region Cell

    internal GameObject[,] CellsGO => _cellsGO;

    internal GameObject[,] UnitPawnsGO;
    internal GameObject[,] UnitKingsGO;
    internal SpriteRenderer[,] UnitPawnsGOsr;
    internal SpriteRenderer[,] UnitKingsGOsr;

    internal GameObject[,] FoodsGO;
    internal GameObject[,] MountainsGO;
    internal GameObject[,] TreesGO;
    internal GameObject[,] HillsGO;

    internal GameObject[,] SelectorVisionsGO;
    internal GameObject[,] SpawnVisionsGO;
    internal GameObject[,] WayUnitVisionsGO;
    internal GameObject[,] EnemyVisionsGO;

    internal GameObject[,] CampsGO;

    #endregion


    #region Canvas


    internal TextMeshProUGUI GoldAmmountText;
    internal Image RightUpUnitImage;
    internal Image RightMiddleUnitImage;
    internal Image AbilitiesImage;
    internal Image LeftEconomyImage;



    internal Button Button0;
    internal Button Button1;

    internal Button BuyPawnButton;
    internal Button ImproveCityButton;

    internal Button ButtonLeave;
    internal Button DoneButton;


    #endregion


    #region Ability Zone

    internal TextMeshProUGUI HpCurrentUnitText;
    internal TextMeshProUGUI DamageCurrentUnitText;
    internal TextMeshProUGUI ProtectionCurrentUnitText;
    internal TextMeshProUGUI StepsCurrentUnitText;

    internal Button BuildingAbilityButton0;

    internal Button BuildingAbilityButton1;
    internal Button BuildingAbilityButton2;
    internal Button BuildingAbilityButton3;

    internal Button UniqueAbilityButton1;
    internal Button UniqueAbilityButton2;
    internal Button UniqueAbilityButton3;

    internal Button StandartAbilityButton1;
    internal Button StandartAbilityButton2;

    #endregion


    #region Ready Zone

    internal RectTransform ParentReadyZone;

    internal Button ReadyButton;

    #endregion


    internal StartSpawnGameManager(SupportGameManager supportManager, out Transform parentTransformScrips) : base(supportManager.ResourcesLoadGameManager)
    {
        _audioSource = supportManager.BuilderManager.CreateGameObject
            ("AudioSource", new Type[] { typeof(AudioSource) }).GetComponent<AudioSource>();
        _audioSource.clip = supportManager.ResourcesLoadGameManager.AudioClip;

        _parentScriptsGO = supportManager.BuilderManager.CreateGameObject("Scripts");
        parentTransformScrips = _parentScriptsGO.transform;

        _camera.gameObject.transform.position = InstanceGame.transform.position + new Vector3(7, 5.5f, -1);
        if (!InstanceGame.IsMasterClient) _camera.transform.Rotate(0, 0, 180);

        GameObject.Instantiate(supportManager.ResourcesLoadGameManager.BackGroundCollider2D,
            InstanceGame.transform.position + new Vector3(0, 0, 1), InstanceGame.transform.rotation);

        SpawnCells(supportManager.ResourcesLoadGameManager, supportManager.StartValuesGameConfig);       
        SpawnUI(supportManager.ResourcesLoadGameManager);
    }

    private void SpawnUI(ResourcesLoadGameManager resourcesLoadManager)
    {
        GameObject.Instantiate(resourcesLoadManager.Canvas);


        #region UI

        GoldAmmountText = GameObject.Find("GoldAmmount").GetComponent<TextMeshProUGUI>();
        RightUpUnitImage = GameObject.Find("RightUpUnitImage").GetComponent<Image>();
        RightMiddleUnitImage = GameObject.Find("RightMiddleUnitImage").GetComponent<Image>();

        LeftEconomyImage = GameObject.Find("LeftEconomy").GetComponent<Image>();

        Button0 = GameObject.Find("Button0").GetComponent<Button>();
        Button1 = GameObject.Find("Button1").GetComponent<Button>();
        DoneButton = GameObject.Find("ButtonDone").GetComponent<Button>();

        ButtonLeave = GameObject.Find("ButtonLeave").GetComponent<Button>();
        BuyPawnButton = GameObject.Find("BuyPawnButton").GetComponent<Button>();
        ImproveCityButton = GameObject.Find("ImproveCityButton").GetComponent<Button>();

        #endregion


        #region Ability zone

        HpCurrentUnitText = GameObject.Find("HpCurrentUnitText").GetComponent<TextMeshProUGUI>();
        DamageCurrentUnitText = GameObject.Find("DamageCurrentUnitText").GetComponent<TextMeshProUGUI>();
        ProtectionCurrentUnitText = GameObject.Find("ProtectionCurrentUnitText").GetComponent<TextMeshProUGUI>();
        StepsCurrentUnitText = GameObject.Find("StepsCurrentUnitText").GetComponent<TextMeshProUGUI>();

        BuildingAbilityButton0 = GameObject.Find("BuildingAbilityButton0").GetComponent<Button>();

        BuildingAbilityButton1 = GameObject.Find("BuildingAbilityButton1").GetComponent<Button>();
        BuildingAbilityButton2 = GameObject.Find("BuildingAbilityButton2").GetComponent<Button>();
        BuildingAbilityButton3 = GameObject.Find("BuildingAbilityButton3").GetComponent<Button>();

        StandartAbilityButton1 = GameObject.Find("StandartAbilityButton1").GetComponent<Button>();
        StandartAbilityButton2 = GameObject.Find("StandartAbilityButton2").GetComponent<Button>();

        UniqueAbilityButton1 = GameObject.Find("UniqueAbilityButton1").GetComponent<Button>();
        UniqueAbilityButton2 = GameObject.Find("UniqueAbilityButton2").GetComponent<Button>();
        UniqueAbilityButton3 = GameObject.Find("UniqueAbilityButton3").GetComponent<Button>();


        AbilitiesImage = GameObject.Find("RightDownUnitImage").GetComponent<Image>();

        #endregion


        ParentReadyZone = GameObject.Find("ReadyZone").GetComponent<RectTransform>();

        ReadyButton = GameObject.Find("ReadyButton").GetComponent<Button>();

    }

    public void SpawnCells(ResourcesLoadGameManager resourcesLoadManager, StartValuesGameConfig startValues)
    {
        var cellGO = resourcesLoadManager.CellGO;
        var whiteCellSR = resourcesLoadManager.WhiteCellSprite;
        var blackCellSR = resourcesLoadManager.BlackCellSprite;

        _cellsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        FoodsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        MountainsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        TreesGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        HillsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        SelectorVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        SpawnVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        WayUnitVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        EnemyVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        UnitPawnsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        UnitKingsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        UnitPawnsGOsr = new SpriteRenderer[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        UnitKingsGOsr = new SpriteRenderer[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        CampsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];


        GameObject supportParent = new GameObject("Cells");

        for (int x = 0; x < startValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < startValues.CELL_COUNT_Y; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                }

                _cellsGO[x, y].transform.SetParent(supportParent.transform);

                FoodsGO[x, y] = _cellsGO[x, y].transform.Find("FoodP").gameObject;
                MountainsGO[x, y] = _cellsGO[x, y].transform.Find("MountainP").gameObject;
                TreesGO[x, y] = _cellsGO[x, y].transform.Find("TreeP").gameObject;
                HillsGO[x, y] = _cellsGO[x, y].transform.Find("HillP").gameObject;

                SelectorVisionsGO[x, y] = _cellsGO[x, y].transform.Find("SelectorP").gameObject;
                SpawnVisionsGO[x, y] = _cellsGO[x, y].transform.Find("AssignmentP").gameObject;
                WayUnitVisionsGO[x, y] = _cellsGO[x, y].transform.Find("WayOfUnitVisionP").gameObject;
                EnemyVisionsGO[x, y] = _cellsGO[x, y].transform.Find("EnemyVisionP").gameObject;

                UnitPawnsGO[x, y] = _cellsGO[x, y].transform.Find("UnitPawnP").gameObject;
                UnitKingsGO[x, y] = _cellsGO[x, y].transform.Find("UnitKingP").gameObject;
                UnitPawnsGOsr[x, y] = UnitPawnsGO[x, y].GetComponent<SpriteRenderer>();
                UnitKingsGOsr[x, y] = UnitKingsGO[x, y].GetComponent<SpriteRenderer>();

                CampsGO[x, y] = _cellsGO[x, y].transform.Find("CampP").gameObject;
            }
        }
    }

    private GameObject CreatGameObject(GameObject go, Sprite sprite, int x, int y)
    {
        var mainGO = InstanceGame.gameObject;

        var goo = GameObject.Instantiate(go, mainGO.transform.position + new Vector3(x, y, mainGO.transform.position.z), mainGO.transform.rotation);

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
