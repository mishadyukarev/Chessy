using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class StartSpawnGame : StartSpawn
{


    internal StartSpawnGame(GameObjectPool gameObjectPool, ResourcesLoadGame resourcesLoadGame, Builder builder) : base(resourcesLoadGame)
    {
        gameObjectPool.ParentScriptsGO = builder.CreateGameObject("Scripts");

        gameObjectPool.AudioSourceGO = builder.CreateGameObject("AudioSource", new Type[] { typeof(AudioSource) });
        gameObjectPool.AudioSourceGO.GetComponent<AudioSource>().clip = resourcesLoadGame.SoundConfig.AudioClip;

        _camera.gameObject.transform.position = InstanceGame.transform.position + new Vector3(7, 5.5f, -1);
        if (!InstanceGame.IsMasterClient) _camera.transform.Rotate(0, 0, 180);

        GameObject.Instantiate(resourcesLoadGame.PrefabConfig.BackGroundCollider2D,
            InstanceGame.transform.position + new Vector3(0, 0, 1), InstanceGame.transform.rotation);

        SpawnCells(gameObjectPool, resourcesLoadGame, InstanceGame.StartValuesGameConfig);
        SpawnUI(gameObjectPool, resourcesLoadGame, InstanceGame.StartValuesGameConfig);
    }

    private void SpawnUI(GameObjectPool gameObjectPool, ResourcesLoadGame resourcesLoadGameManager, StartValuesGameConfig startValuesGameConfig)
    {

        GameObject.Instantiate(resourcesLoadGameManager.Canvas);


        #region Economy Zone

        gameObjectPool.GoldAmmountText = GameObject.Find("GoldAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.FoodAmmountText = GameObject.Find("FoodAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.WoodAmmountText = GameObject.Find("WoodAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.OreAmmountText = GameObject.Find("OreAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.MetalAmmountText = GameObject.Find("MetalAmount").GetComponent<TextMeshProUGUI>();

        #endregion


        #region UI


        gameObjectPool.RightUpUnitImage = GameObject.Find("RightUpUnitImage").GetComponent<Image>();
        gameObjectPool.RightMiddleUnitImage = GameObject.Find("RightMiddleUnitImage").GetComponent<Image>();

        gameObjectPool.LeftEconomyImage = GameObject.Find("LeftEconomy").GetComponent<Image>();

        gameObjectPool.Button0 = GameObject.Find("Button0").GetComponent<Button>();
        gameObjectPool.Button1 = GameObject.Find("Button1").GetComponent<Button>();

        gameObjectPool.ButtonLeave = GameObject.Find("ButtonLeave").GetComponent<Button>();
        gameObjectPool.BuyPawnButton = GameObject.Find("BuyPawnButton").GetComponent<Button>();
        gameObjectPool.ImproveCityButton = GameObject.Find("ImproveCityButton").GetComponent<Button>();

        #endregion


        #region Ability zone

        gameObjectPool.HpCurrentUnitText = GameObject.Find("HpCurrentUnitText").GetComponent<TextMeshProUGUI>();
        gameObjectPool.DamageCurrentUnitText = GameObject.Find("DamageCurrentUnitText").GetComponent<TextMeshProUGUI>();
        gameObjectPool.ProtectionCurrentUnitText = GameObject.Find("ProtectionCurrentUnitText").GetComponent<TextMeshProUGUI>();
        gameObjectPool.StepsCurrentUnitText = GameObject.Find("StepsCurrentUnitText").GetComponent<TextMeshProUGUI>();


        gameObjectPool.BuildingAbilityButton0 = GameObject.Find("BuildingAbilityButton0").GetComponent<Button>();

        gameObjectPool.BuildingAbilityButton1 = GameObject.Find("BuildingAbilityButton1").GetComponent<Button>();
        gameObjectPool.BuildingAbilityButton2 = GameObject.Find("BuildingAbilityButton2").GetComponent<Button>();
        gameObjectPool.BuildingAbilityButton3 = GameObject.Find("BuildingAbilityButton3").GetComponent<Button>();

        gameObjectPool.BuildingAbilityButton4 = GameObject.Find("BuildingAbilityButton4").GetComponent<Button>();


        gameObjectPool.StandartAbilityButton1 = GameObject.Find("StandartAbilityButton1").GetComponent<Button>();
        gameObjectPool.StandartAbilityButton2 = GameObject.Find("StandartAbilityButton2").GetComponent<Button>();

        gameObjectPool.UniqueAbilityButton1 = GameObject.Find("UniqueAbilityButton1").GetComponent<Button>();
        gameObjectPool.UniqueAbilityButton2 = GameObject.Find("UniqueAbilityButton2").GetComponent<Button>();
        gameObjectPool.UniqueAbilityButton3 = GameObject.Find("UniqueAbilityButton3").GetComponent<Button>();


        gameObjectPool.AbilitiesImage = GameObject.Find("RightDownUnitImage").GetComponent<Image>();

        #endregion


        gameObjectPool.ParentReadyZone = GameObject.Find("ReadyZone").GetComponent<RectTransform>();
        gameObjectPool.ReadyButton = GameObject.Find("ReadyButton").GetComponent<Button>();


        gameObjectPool.ParentTheEndGameZone = GameObject.Find("TheEndGameZone").GetComponent<RectTransform>();
        gameObjectPool.TheEndGameText = GameObject.Find("TheEndGameText").GetComponent<TextMeshProUGUI>();
        gameObjectPool.ParentTheEndGameZone.gameObject.SetActive(false);


        gameObjectPool.DoneButton = GameObject.Find("ButtonDone").GetComponent<Button>();
        gameObjectPool.DonerRawImage = GameObject.Find("DonerRawImage").GetComponent<RawImage>();



        if (InstanceGame.StartValuesGameConfig.IS_TEST)
        {
            gameObjectPool.ParentReadyZone.gameObject.SetActive(false);
        }
    }

    public void SpawnCells(GameObjectPool gameObjectPool, ResourcesLoadGame resourcesLoadManager, StartValuesGameConfig startValues)
    {
        var cellGO = resourcesLoadManager.PrefabConfig.CellGO;
        var whiteCellSR = resourcesLoadManager.SpritesConfig.WhiteSprite;
        var blackCellSR = resourcesLoadManager.SpritesConfig.BlackSprite;

        gameObjectPool.CellsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        gameObjectPool.FoodsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.MountainsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.TreesGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.HillsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        gameObjectPool.SelectorVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.SpawnVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.WayUnitVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.EnemyVisionsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.ZoneVisionGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        gameObjectPool.UnitPawnsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.UnitKingsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        gameObjectPool.CampsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.FarmsGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.WoodcuttersGO = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];


        GameObject supportParent = new GameObject("Cells");

        for (int x = 0; x < startValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < startValues.CELL_COUNT_Y; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        gameObjectPool.CellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(gameObjectPool.CellsGO[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        gameObjectPool.CellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(gameObjectPool.CellsGO[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        gameObjectPool.CellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(gameObjectPool.CellsGO[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        gameObjectPool.CellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(gameObjectPool.CellsGO[x, y], x, y);
                    }
                }

                gameObjectPool.CellsGO[x, y].transform.SetParent(supportParent.transform);

                gameObjectPool.FoodsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("FoodP").gameObject;
                gameObjectPool.MountainsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("MountainP").gameObject;
                gameObjectPool.TreesGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("TreeP").gameObject;
                gameObjectPool.HillsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("HillP").gameObject;

                gameObjectPool.SelectorVisionsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("SelectorP").gameObject;
                gameObjectPool.SpawnVisionsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("AssignmentP").gameObject;
                gameObjectPool.WayUnitVisionsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("WayOfUnitVisionP").gameObject;
                gameObjectPool.EnemyVisionsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("EnemyVisionP").gameObject;
                gameObjectPool.ZoneVisionGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("ZoneVisionP").gameObject;

                gameObjectPool.UnitPawnsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("UnitPawnP").gameObject;
                gameObjectPool.UnitKingsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("UnitKingP").gameObject;

                gameObjectPool.CampsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("CampP").gameObject;
                gameObjectPool.FarmsGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("FarmP").gameObject;
                gameObjectPool.WoodcuttersGO[x, y] = gameObjectPool.CellsGO[x, y].transform.Find("WoodcutterP").gameObject;
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
