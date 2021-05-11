using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class GameObjectPool
{
    #region Variables

    private GameObject _parentScriptsGO;

    private GameObject _audioSourceGO;


    #region Cell

    private GameObject[,] _cellsGO;

    private GameObject[,] _unitPawnsGO;
    private GameObject[,] _unitKingsGO;

    private GameObject[,] _foodsGO;
    private GameObject[,] _mountainsGO;
    private GameObject[,] _treesGO;
    private GameObject[,] _hillsGO;

    private GameObject[,] _selectorVisionsGO;
    private GameObject[,] _spawnVisionsGO;
    private GameObject[,] _wayUnitVisionsGO;
    private GameObject[,] _enemyVisionsGO;
    private GameObject[,] _zoneVisionGO;

    private GameObject[,] _campsGO;
    private GameObject[,] _farmsGO;
    private GameObject[,] _woodcuttersGO;

    #endregion


    #region Canvas

    #region Economy

    private TextMeshProUGUI _goldAmmountText;
    private TextMeshProUGUI _foodAmmountText;
    private TextMeshProUGUI _woodAmmountText;
    private TextMeshProUGUI _oreAmmountText;
    private TextMeshProUGUI _metalAmmountText;

    #endregion


    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;
    private Image _abilitiesImage;
    private Image _leftEconomyImage;



    private Button _button0;
    private Button _button1;

    private Button _buyPawnButton;
    private Button _improveCityButton;

    private Button _buttonLeave;
    private Button _doneButton;


    #region Ability Zone

    private TextMeshProUGUI _hpCurrentUnitText;
    private TextMeshProUGUI _damageCurrentUnitText;
    private TextMeshProUGUI _protectionCurrentUnitText;
    private TextMeshProUGUI _stepsCurrentUnitText;


    private Button _buildingAbilityButton0;

    private Button _buildingAbilityButton1;
    private Button _buildingAbilityButton2;
    private Button _buildingAbilityButton3;

    private Button _buildingAbilityButton4;


    private Button _uniqueAbilityButton1;
    private Button _uniqueAbilityButton2;
    private Button _uniqueAbilityButton3;

    private Button _standartAbilityButton1;
    private Button _standartAbilityButton2;

    #endregion


    #region Ready Zone

    private RectTransform _parentReadyZone;

    private Button _readyButton;

    #endregion


    #region TheEndGame Zone

    private RectTransform _parentTheEndGameZone;
    private TextMeshProUGUI _theEndGameText;

    #endregion


    #region Doner Zone

    private RawImage donerRawImage;

    #endregion

    #endregion

    #endregion


    #region Properties

    public GameObject ParentScriptsGO { get => _parentScriptsGO; set => _parentScriptsGO = value; }

    public GameObject AudioSourceGO { get => _audioSourceGO; set => _audioSourceGO = value; }


    #region Cell

    public GameObject[,] CellsGO { get => _cellsGO; set => _cellsGO = value; }

    public GameObject[,] UnitPawnsGO { get => _unitPawnsGO; set => _unitPawnsGO = value; }
    public GameObject[,] UnitKingsGO { get => _unitKingsGO; set => _unitKingsGO = value; }

    public GameObject[,] FoodsGO { get => _foodsGO; set => _foodsGO = value; }
    public GameObject[,] MountainsGO { get => _mountainsGO; set => _mountainsGO = value; }
    public GameObject[,] TreesGO { get => _treesGO; set => _treesGO = value; }
    public GameObject[,] HillsGO { get => _hillsGO; set => _hillsGO = value; }

    public GameObject[,] SelectorVisionsGO { get => _selectorVisionsGO; set => _selectorVisionsGO = value; }
    public GameObject[,] SpawnVisionsGO { get => _spawnVisionsGO; set => _spawnVisionsGO = value; }
    public GameObject[,] WayUnitVisionsGO { get => _wayUnitVisionsGO; set => _wayUnitVisionsGO = value; }
    public GameObject[,] EnemyVisionsGO { get => _enemyVisionsGO; set => _enemyVisionsGO = value; }
    public GameObject[,] ZoneVisionGO { get => _zoneVisionGO; set => _zoneVisionGO = value; }

    public GameObject[,] CampsGO { get => _campsGO; set => _campsGO = value; }
    public GameObject[,] FarmsGO { get => _farmsGO; set => _farmsGO = value; }
    public GameObject[,] WoodcuttersGO { get => _woodcuttersGO; set => _woodcuttersGO = value; }

    #endregion


    #region Canvas

    public TextMeshProUGUI GoldAmmountText { get => _goldAmmountText; set => _goldAmmountText = value; }
    public TextMeshProUGUI FoodAmmountText { get => _foodAmmountText; set => _foodAmmountText = value; }
    public TextMeshProUGUI WoodAmmountText { get => _woodAmmountText; set => _woodAmmountText = value; }
    public TextMeshProUGUI OreAmmountText { get => _oreAmmountText; set => _oreAmmountText = value; }
    public TextMeshProUGUI MetalAmmountText { get => _metalAmmountText; set => _metalAmmountText = value; }
    public Image RightUpUnitImage { get => _rightUpUnitImage; set => _rightUpUnitImage = value; }
    public Image RightMiddleUnitImage { get => _rightMiddleUnitImage; set => _rightMiddleUnitImage = value; }
    public Image AbilitiesImage { get => _abilitiesImage; set => _abilitiesImage = value; }
    public Image LeftEconomyImage { get => _leftEconomyImage; set => _leftEconomyImage = value; }
    public Button Button0 { get => _button0; set => _button0 = value; }
    public Button Button1 { get => _button1; set => _button1 = value; }
    public Button BuyPawnButton { get => _buyPawnButton; set => _buyPawnButton = value; }
    public Button ImproveCityButton { get => _improveCityButton; set => _improveCityButton = value; }
    public Button ButtonLeave { get => _buttonLeave; set => _buttonLeave = value; }
    public Button DoneButton { get => _doneButton; set => _doneButton = value; }
    public TextMeshProUGUI HpCurrentUnitText { get => _hpCurrentUnitText; set => _hpCurrentUnitText = value; }
    public TextMeshProUGUI DamageCurrentUnitText { get => _damageCurrentUnitText; set => _damageCurrentUnitText = value; }
    public TextMeshProUGUI ProtectionCurrentUnitText { get => _protectionCurrentUnitText; set => _protectionCurrentUnitText = value; }
    public TextMeshProUGUI StepsCurrentUnitText { get => _stepsCurrentUnitText; set => _stepsCurrentUnitText = value; }
    public Button BuildingAbilityButton0 { get => _buildingAbilityButton0; set => _buildingAbilityButton0 = value; }
    public Button BuildingAbilityButton1 { get => _buildingAbilityButton1; set => _buildingAbilityButton1 = value; }
    public Button BuildingAbilityButton2 { get => _buildingAbilityButton2; set => _buildingAbilityButton2 = value; }
    public Button BuildingAbilityButton3 { get => _buildingAbilityButton3; set => _buildingAbilityButton3 = value; }
    public Button BuildingAbilityButton4 { get => _buildingAbilityButton4; set => _buildingAbilityButton4 = value; }
    public Button UniqueAbilityButton1 { get => _uniqueAbilityButton1; set => _uniqueAbilityButton1 = value; }
    public Button UniqueAbilityButton2 { get => _uniqueAbilityButton2; set => _uniqueAbilityButton2 = value; }
    public Button UniqueAbilityButton3 { get => _uniqueAbilityButton3; set => _uniqueAbilityButton3 = value; }
    public Button StandartAbilityButton1 { get => _standartAbilityButton1; set => _standartAbilityButton1 = value; }
    public Button StandartAbilityButton2 { get => _standartAbilityButton2; set => _standartAbilityButton2 = value; }
    public RectTransform ParentReadyZone { get => _parentReadyZone; set => _parentReadyZone = value; }
    public Button ReadyButton { get => _readyButton; set => _readyButton = value; }
    public RectTransform ParentTheEndGameZone { get => _parentTheEndGameZone; set => _parentTheEndGameZone = value; }
    public TextMeshProUGUI TheEndGameText { get => _theEndGameText; set => _theEndGameText = value; }
    internal RawImage DonerRawImage { get => donerRawImage; set => donerRawImage = value; }

    #endregion

    #endregion
}
