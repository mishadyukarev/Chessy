using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Main;

internal class StartSpawnManager
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

    #region UI

    internal TextMeshProUGUI GoldAmmountText;
    internal Image RightUpUnitImage;
    internal Image RightMiddleUnitImage;
    internal Image RightDownUnitImage;
    internal Image LeftEconomyImage;

    #endregion


    #region Buttons

    internal Button Button0;
    internal Button Button1;

    internal Button ButtonAbility1;
    internal Button ButtonAbility2;
    internal Button ButtonAbility3;
    internal Button ButtonAbility4;

    internal Button BuyPawnButton;
    internal Button ImproveCityButton;

    internal Button ButtonLeave;
    internal Button RequestDoneButton;

    #endregion

    #endregion



    internal StartSpawnManager(SupportManager supportManager, out Transform parentTransformScrips)
    {
        _audioSource = supportManager.BuilderManager.CreateGameObject
            ("AudioSource", new Type[] { typeof(AudioSource) }).GetComponent<AudioSource>();
        _audioSource.clip = supportManager.ResourcesLoadManager.AudioClip;

        _parentScriptsGO = supportManager.BuilderManager.CreateGameObject("Scripts");
        parentTransformScrips = _parentScriptsGO.transform;

        SpawnCells(supportManager.ResourcesLoadManager, supportManager.StartValuesConfig);
        SpawnUI();


    }

    private void SpawnUI()
    {
        #region UI

        GoldAmmountText = GameObject.Find("GoldAmmount").GetComponent<TextMeshProUGUI>();
        RightUpUnitImage = GameObject.Find("RightUpUnitImage").GetComponent<Image>();
        RightMiddleUnitImage = GameObject.Find("RightMiddleUnitImage").GetComponent<Image>();
        RightDownUnitImage = GameObject.Find("RightDownUnitImage").GetComponent<Image>();
        LeftEconomyImage = GameObject.Find("LeftEconomy").GetComponent<Image>();

        #endregion


        Button0 = GameObject.Find("Button0").GetComponent<Button>();
        Button1 = GameObject.Find("Button1").GetComponent<Button>();
        RequestDoneButton = GameObject.Find("ButtonDone").GetComponent<Button>();
        ButtonAbility1 = GameObject.Find("ButtonAbility1").GetComponent<Button>();
        ButtonAbility2 = GameObject.Find("ButtonAbility2").GetComponent<Button>();
        ButtonAbility3 = GameObject.Find("ButtonAbility3").GetComponent<Button>();
        ButtonAbility4 = GameObject.Find("ButtonAbility4").GetComponent<Button>();
        ButtonLeave = GameObject.Find("ButtonLeave").GetComponent<Button>();
        BuyPawnButton = GameObject.Find("BuyPawnButton").GetComponent<Button>();
        ImproveCityButton = GameObject.Find("ImproveCityButton").GetComponent<Button>();
    }
    public void SpawnCells(ResourcesLoadManager resourcesLoadManager, StartValuesConfig startValues)
    {
        var cellGO = resourcesLoadManager.CellGO;
        var whiteCellSR = resourcesLoadManager.WhiteCellSprite;
        var blackCellSR = resourcesLoadManager.BlackCellSprite;

        _cellsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];

        MountainsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];
        TreesGO = new GameObject[startValues.CellCountX, startValues.CellCountY];
        HillsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];

        SelectorVisionsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];
        SpawnVisionsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];
        WayUnitVisionsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];
        EnemyVisionsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];

        UnitPawnsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];
        UnitKingsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];
        UnitPawnsGOsr = new SpriteRenderer[startValues.CellCountX, startValues.CellCountY];
        UnitKingsGOsr = new SpriteRenderer[startValues.CellCountX, startValues.CellCountY];

        CampsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];


        GameObject supportParent = new GameObject("Cells");
        // Setting cells on the map
        for (int x = 0; x < startValues.CellCountX; x++)
        {
            for (int y = 0; y < startValues.CellCountY; y++)
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
        var mainGO = Instance.gameObject;

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
