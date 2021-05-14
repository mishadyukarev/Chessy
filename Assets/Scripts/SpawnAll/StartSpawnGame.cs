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


        #region In Game Zone

        #region Left

        gameObjectPool.LeftImage = GameObject.Find("LeftImage").GetComponent<Image>();
        gameObjectPool.LeftImproveCityButton = GameObject.Find("ImproveCityButton").GetComponent<Button>();
        gameObjectPool.LeftMeltButton = GameObject.Find("MeltOreButton").GetComponent<Button>();

        gameObjectPool.GameLeftBuyPawnButton = GameObject.Find("BuyPawnButton").GetComponent<Button>();
        gameObjectPool.GameLeftBuyRookButton = GameObject.Find("BuyRookButton").GetComponent<Button>();
        gameObjectPool.GameLeftBuyBishopButton = GameObject.Find("BuyBishopButton").GetComponent<Button>();

        #endregion


        #region Right

        gameObjectPool.RightUpImage = GameObject.Find("RightUpUnitImage").GetComponent<Image>();
        gameObjectPool.RightMiddleImage = GameObject.Find("RightMiddleUnitImage").GetComponent<Image>();


        #region Down

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


        gameObjectPool.RightDownImage = GameObject.Find("RightDownUnitImage").GetComponent<Image>();

        #endregion

        #endregion


        #region Down

        gameObjectPool.GameDownTakeUnit0Button = GameObject.Find("TakeUnit0Button").GetComponent<Button>();
        gameObjectPool.GameDownTakeUnit1Button = GameObject.Find("TakeUnit1Button").GetComponent<Button>();
        gameObjectPool.GameDownTakeUnit2Button = GameObject.Find("TakeUnit2Button").GetComponent<Button>();
        gameObjectPool.GameDownTakeUnit3Button = GameObject.Find("TakeUnit3Button").GetComponent<Button>();

        gameObjectPool.DoneButton = GameObject.Find("ButtonDone").GetComponent<Button>();
        gameObjectPool.DonerRawImage = GameObject.Find("DonerRawImage").GetComponent<RawImage>();

        #endregion


        #region Up

        gameObjectPool.GoldAmmountText = GameObject.Find("GoldAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.FoodAmmountText = GameObject.Find("FoodAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.WoodAmmountText = GameObject.Find("WoodAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.OreAmmountText = GameObject.Find("OreAmount").GetComponent<TextMeshProUGUI>();
        gameObjectPool.MetalAmmountText = GameObject.Find("MetalAmount").GetComponent<TextMeshProUGUI>();

        gameObjectPool.ButtonLeave = GameObject.Find("ButtonLeave").GetComponent<Button>();

        #endregion

        #endregion


        #region Ready Zone

        gameObjectPool.ParentReadyZone = GameObject.Find("ReadyZone").GetComponent<RectTransform>();
        gameObjectPool.ReadyButton = GameObject.Find("ReadyButton").GetComponent<Button>();

        #endregion


        #region End Game Zone

        gameObjectPool.ParentTheEndGameZone = GameObject.Find("TheEndGameZone").GetComponent<RectTransform>();
        gameObjectPool.TheEndGameText = GameObject.Find("TheEndGameText").GetComponent<TextMeshProUGUI>();
        gameObjectPool.ParentTheEndGameZone.gameObject.SetActive(false);

        #endregion



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

        gameObjectPool.CellEnvironmentFoodGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellEnvironmentMountainGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellEnvironmentTreeGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellEnvironmentHillGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        gameObjectPool.CellSupportVisionSelectorGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellSupportVisionSpawnGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellSupportVisionWayUnitGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellSupportVisionEnemyGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellSupportVisionUniqueAttackGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellSupportVisionZoneGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        gameObjectPool.CellUnitPawnGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellUnitKingGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellUnitRookGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellUnitBishopGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];

        gameObjectPool.CellBuildingCityGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellBuildingFarmGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellBuildingWoodcutterGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];
        gameObjectPool.CellBuildingMineGOs = new GameObject[startValues.CELL_COUNT_X, startValues.CELL_COUNT_Y];


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



                GameObject parentGO = gameObjectPool.CellsGO[x, y].transform.Find("Environments").gameObject;

                gameObjectPool.CellEnvironmentFoodGOs[x, y] = parentGO.transform.Find("Food").gameObject;
                gameObjectPool.CellEnvironmentMountainGOs[x, y] = parentGO.transform.Find("Mountain").gameObject;
                gameObjectPool.CellEnvironmentTreeGOs[x, y] = parentGO.transform.Find("Tree").gameObject;
                gameObjectPool.CellEnvironmentHillGOs[x, y] = parentGO.transform.Find("Hill").gameObject;



                parentGO = gameObjectPool.CellsGO[x, y].transform.Find("SupportVisions").gameObject;

                gameObjectPool.CellSupportVisionSelectorGOs[x, y] = parentGO.transform.Find("Selector").gameObject;
                gameObjectPool.CellSupportVisionSpawnGOs[x, y] = parentGO.transform.Find("Assignment").gameObject;
                gameObjectPool.CellSupportVisionWayUnitGOs[x, y] = parentGO.transform.Find("WayOfUnit").gameObject;
                gameObjectPool.CellSupportVisionEnemyGOs[x, y] = parentGO.transform.Find("Enemy").gameObject;
                gameObjectPool.CellSupportVisionUniqueAttackGOs[x, y] = parentGO.transform.Find("UniqueAttack").gameObject;
                gameObjectPool.CellSupportVisionZoneGOs[x, y] = parentGO.transform.Find("Zone").gameObject;



                parentGO = gameObjectPool.CellsGO[x, y].transform.Find("Units").gameObject;

                gameObjectPool.CellUnitPawnGOs[x, y] = parentGO.transform.Find("Pawn").gameObject;
                gameObjectPool.CellUnitKingGOs[x, y] = parentGO.transform.Find("King").gameObject;
                gameObjectPool.CellUnitRookGOs[x, y] = parentGO.transform.Find("Rook").gameObject;
                gameObjectPool.CellUnitBishopGOs[x, y] = parentGO.transform.Find("Bishop").gameObject;


                parentGO = gameObjectPool.CellsGO[x, y].transform.Find("Buildings").gameObject;

                gameObjectPool.CellBuildingCityGOs[x, y] = parentGO.transform.Find("City").gameObject;
                gameObjectPool.CellBuildingFarmGOs[x, y] = parentGO.transform.Find("Farm").gameObject;
                gameObjectPool.CellBuildingWoodcutterGOs[x, y] = parentGO.transform.Find("Woodcutter").gameObject;
                gameObjectPool.CellBuildingMineGOs[x, y] = parentGO.transform.Find("Mine").gameObject;
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
