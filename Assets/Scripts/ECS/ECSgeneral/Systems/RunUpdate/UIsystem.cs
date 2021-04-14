using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UISystem : CellReductionSystem, IEcsRunSystem
{
    private PhotonManagerScene _photonManagerScene;
    private PhotonPunRPC _photonPunRPC;


    private TextMeshProUGUI _goldAmountText;

    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;
    private Image _rightDownUnitImage;
    private Image _leftEconomyImage;

    private Button _button0;
    private Button _button1;
    private Button _buyPawnButton;
    private Button _improveCityButton;
    private Button _buttonLeave;
    private Button _requestDoneButton;



    private Button _buildingAbilityButton0;
    private Button _buildingAbilityButton1;
    private Button _buildingAbilityButton2;
    private Button _buildingAbilityButton3;
    private Button _buildingAbilityButton4;

    private Button _standartAbilityButton1;
    private Button _standartAbilityButton2;

    private Button _uniqueAbilityButton1;
    private Button _uniqueAbilityButton2;
    private Button _uniqueAbilityButton3;
    private Button _uniqueAbilityButton4;



    private EcsComponentRef<ButtonComponent> _buttonComponentRef;
    private EcsComponentRef<SelectedUnitComponent> _selectedUnitComponentRef;
    private EcsComponentRef<SelectorComponent> _selectorComponentRef;
    private EcsComponentRef<SelectorComponent> _selectorComponetRef = default;

    private EcsComponentRef<EconomyComponent> _economyComponentRef = default;
    private EcsComponentRef<EconomyComponent.BuildingsComponent> _economyBuildingsComponentRef;
    private EcsComponentRef<EconomyComponent.UnitsComponent> _economyUnitsComponentRef;



    internal UISystem(ECSmanager eCSmanager, SupportManager supportManager,PhotonManager photonManager, StartSpawnManager startSpawnManager) : base(eCSmanager, supportManager)
    {
        _photonManagerScene = photonManager.PhotonManagerScene;
        _photonPunRPC = photonManager.PhotonPunRPC;


        #region ComponetRefs

        _economyComponentRef = eCSmanager.EntitiesGeneralManager.EconomyComponentRef;
        _economyBuildingsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyBuildingsComponentRef;
        _economyUnitsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyUnitsComponentRef;

        _selectorComponetRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
        _buttonComponentRef = eCSmanager.EntitiesGeneralManager.ButtonComponentRef;
        _selectedUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectedUnitComponentRef;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;

        #endregion


        #region Texts

        _goldAmountText = startSpawnManager.GoldAmmountText;

        #endregion


        #region Images

        _rightUpUnitImage = startSpawnManager.RightUpUnitImage;
        _rightMiddleUnitImage = startSpawnManager.RightMiddleUnitImage;
        _rightDownUnitImage = startSpawnManager.AbilitiesImage;
        _leftEconomyImage = startSpawnManager.LeftEconomyImage;

        _rightMiddleUnitImage.gameObject.SetActive(false);

        #endregion


        _button0 = startSpawnManager.Button0;
        _button0.onClick.AddListener(delegate { GetUnit(UnitTypes.King); });

        _button1 = startSpawnManager.Button1;
        _button1.onClick.AddListener(delegate { GetUnit(UnitTypes.Pawn); });


        _buttonComponentRef.Unref().DonerDelegate = Doner;
        _requestDoneButton = startSpawnManager.RequestDoneButton;
        _requestDoneButton.onClick.AddListener(delegate { Doner(true, false); });


        #region Abilities

        _buildingAbilityButton0 = startSpawnManager.BuildingAbilityButton0;
        _buildingAbilityButton0.onClick.AddListener(delegate { Build(BuildingTypes.City); });

        _buildingAbilityButton1 = startSpawnManager.BuildingAbilityButton1;
        _buildingAbilityButton2 = startSpawnManager.BuildingAbilityButton2;
        _buildingAbilityButton3 = startSpawnManager.BuildingAbilityButton3;
        _buildingAbilityButton4 = startSpawnManager.BuildingAbilityButton4;

        _standartAbilityButton1 = startSpawnManager.StandartAbilityButton1;
        _standartAbilityButton1.onClick.AddListener(delegate { StandartAbilityButton1(); });
        _standartAbilityButton2 = startSpawnManager.StandartAbilityButton2;
        _standartAbilityButton2.onClick.AddListener(delegate { StandartAbilityButton2(); });

        _uniqueAbilityButton1 = startSpawnManager.UniqueAbilityButton1;
        _uniqueAbilityButton2 = startSpawnManager.UniqueAbilityButton2;
        _uniqueAbilityButton3 = startSpawnManager.UniqueAbilityButton3;
        _uniqueAbilityButton4 = startSpawnManager.UniqueAbilityButton4;


        #endregion


        _buttonLeave = startSpawnManager.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

        _buyPawnButton = startSpawnManager.BuyPawnButton;
        _buyPawnButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });

        _improveCityButton = startSpawnManager.ImproveCityButton;
        _improveCityButton.onClick.AddListener(delegate { ImproveCity(); });

    }


    public void Run()
    {
        _goldAmountText.text = _economyComponentRef.Unref().Gold.ToString();
        if (_economyUnitsComponentRef.Unref().IsSettedKing) _button0.gameObject.SetActive(false);


        var xySelectedCell = _selectorComponetRef.Unref().XYselectedCell;


        if (CellUnitComponent(xySelectedCell).IsMine)
        {
            switch (CellUnitComponent(xySelectedCell).UnitType)
            {
                case UnitTypes.None:

                    ActiveAbilities(false);

                    break;

                case UnitTypes.King:

                    ActiveAbilities(true);


                    if (_economyBuildingsComponentRef.Unref().IsSettedCity) _buildingAbilityButton0.gameObject.SetActive(false);
                    else _buildingAbilityButton0.gameObject.SetActive(true);

                    if (CellUnitComponent(xySelectedCell).IsProtected) _standartAbilityButton1.image.color = Color.yellow;
                    else _standartAbilityButton1.image.color = Color.white;

                    if (CellUnitComponent(xySelectedCell).IsRelaxed) _standartAbilityButton2.image.color = Color.green;
                    else _standartAbilityButton2.image.color = Color.white;

                    break;

                case UnitTypes.Pawn:

                    ActiveAbilities(true);



                    if (_economyBuildingsComponentRef.Unref().IsSettedCity) _buildingAbilityButton0.gameObject.SetActive(false);
                    else _buildingAbilityButton0.gameObject.SetActive(true);

                    if (CellUnitComponent(xySelectedCell).IsProtected) _standartAbilityButton1.image.color = Color.yellow;
                    else _standartAbilityButton1.image.color = Color.white;

                    if (CellUnitComponent(xySelectedCell).IsRelaxed) _standartAbilityButton2.image.color = Color.green;
                    else _standartAbilityButton2.image.color = Color.white;

                    break;

                default:
                    break;


                    void ActiveAbilities(bool isActive)
                    {
                        _buildingAbilityButton0.gameObject.SetActive(isActive);

                        _buildingAbilityButton1.gameObject.SetActive(isActive);
                        _buildingAbilityButton2.gameObject.SetActive(isActive);
                        _buildingAbilityButton3.gameObject.SetActive(isActive);
                        _buildingAbilityButton4.gameObject.SetActive(isActive);

                        _standartAbilityButton1.gameObject.SetActive(isActive);
                        _standartAbilityButton2.gameObject.SetActive(isActive);

                        _uniqueAbilityButton1.gameObject.SetActive(isActive);
                        _uniqueAbilityButton2.gameObject.SetActive(isActive);
                        _uniqueAbilityButton3.gameObject.SetActive(isActive);
                        _uniqueAbilityButton4.gameObject.SetActive(isActive);

                        _rightDownUnitImage.gameObject.SetActive(isActive);
                    }
            }
        }


        switch (CellBuildingComponent(xySelectedCell).BuildingType)
        {
            case BuildingTypes.None:
                ActiveLeftEconomy(false);
                break;

            case BuildingTypes.City:
                ActiveLeftEconomy(true);
                break;

            default:
                break;

                void ActiveLeftEconomy(bool isActive)
                {
                    _leftEconomyImage.gameObject.SetActive(isActive);
                    _buyPawnButton.gameObject.SetActive(isActive);
                    _improveCityButton.gameObject.SetActive(isActive);
                }
        }
    }




    #region Button Methods


    private void GetUnit(UnitTypes unitType) => _photonPunRPC.GetUnit(unitType);


    #region Abilities

    private void Build(BuildingTypes buildingType) => _photonPunRPC.Build(_selectorComponentRef.Unref().XYselectedCell, buildingType);

    private void StandartAbilityButton1() => _photonPunRPC.ProtectUnit(_selectorComponentRef.Unref().XYselectedCell);
    private void StandartAbilityButton2() => _photonPunRPC.RelaxUnit(_selectorComponentRef.Unref().XYselectedCell);

    #endregion


    #region Economy

    private void BuyUnit(UnitTypes unitType) => _photonPunRPC.BuyUnit(unitType);
    private void ImproveCity() { }

    #endregion


    private void Leave() => _photonManagerScene.Leave();

    private void Doner(bool isRequest, bool isActive)
    {
        if (isRequest) _photonPunRPC.Done(!_buttonComponentRef.Unref().IsDone);
        else
        {
            _buttonComponentRef.Unref().IsDone = isActive;

            if (_buttonComponentRef.Unref().IsDone) _requestDoneButton.image.color = Color.red;
            else _requestDoneButton.image.color = Color.white;
        }
    }

    #endregion
}
