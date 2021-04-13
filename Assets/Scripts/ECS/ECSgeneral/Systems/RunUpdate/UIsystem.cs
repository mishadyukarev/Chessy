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
    private Button _buttonAbility1;
    private Button _buttonAbility2;
    private Button _buttonAbility3;
    private Button _buttonAbility4;
    private Button _buyPawnButton;
    private Button _improveCityButton;
    private Button _buttonLeave;
    private Button _requestDoneButton;


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
        _rightDownUnitImage = startSpawnManager.RightDownUnitImage;
        _leftEconomyImage = startSpawnManager.LeftEconomyImage;

        _rightMiddleUnitImage.gameObject.SetActive(false);

        #endregion


        #region Buttons

        _button0 = startSpawnManager.Button0;
        _button0.onClick.AddListener(delegate { Button0(true, false); });

        _button1 = startSpawnManager.Button1;
        _button1.onClick.AddListener(delegate { Button1(true, false); });
        _buttonComponentRef.Unref().Button1Delegate = Button1;


        _buttonComponentRef.Unref().DonerDelegate = Doner;
        _requestDoneButton = startSpawnManager.RequestDoneButton;
        _requestDoneButton.onClick.AddListener(delegate { Doner(true, false); });


        _buttonAbility1 = startSpawnManager.ButtonAbility1;
        _buttonAbility1.onClick.AddListener(delegate { BittonAbility1(); });

        _buttonAbility2 = startSpawnManager.ButtonAbility2;
        _buttonAbility2.onClick.AddListener(delegate { BittonAbility2(); });

        _buttonAbility3 = startSpawnManager.ButtonAbility3;
        _buttonAbility3.onClick.AddListener(delegate { BittonAbility3(); });

        _buttonAbility4 = startSpawnManager.ButtonAbility4;
        _buttonAbility4.onClick.AddListener(delegate { BittonAbility4(); });

        _buttonLeave = startSpawnManager.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

        _buyPawnButton = startSpawnManager.BuyPawnButton;
        _buyPawnButton.onClick.AddListener(delegate { BuyPawn(); });

        _improveCityButton = startSpawnManager.ImproveCityButton;
        _improveCityButton.onClick.AddListener(delegate { ImproveCity(); });

        #endregion
    }


    public void Run()
    {
        _goldAmountText.text = _economyComponentRef.Unref().Gold.ToString();
        if (_economyUnitsComponentRef.Unref().IsSettedKing) _button0.gameObject.SetActive(false);


        var xySelectedCell = _selectorComponetRef.Unref().XYselectedCell;

        if (CellUnitComponent(xySelectedCell).HaveUnit)
        {
            if (CellUnitComponent(xySelectedCell).IsMine)
            {
                switch (CellUnitComponent(xySelectedCell).UnitType)
                {
                    case UnitTypes.None:
                        ActiveRightDown(false);
                        break;

                    case UnitTypes.King:
                        break;

                    case UnitTypes.Pawn:
                        ActiveRightDown(true);

                        if (CellUnitComponent(xySelectedCell).IsProtected) _buttonAbility1.image.color = Color.yellow;
                        else _buttonAbility1.image.color = Color.white;

                        if (CellUnitComponent(xySelectedCell).IsRelaxed) _buttonAbility2.image.color = Color.green;
                        else _buttonAbility2.image.color = Color.white;

                        if (_economyBuildingsComponentRef.Unref().IsSettedCity) _buttonAbility4.gameObject.SetActive(false);
                        else _buttonAbility4.gameObject.SetActive(true);
                        break;

                    default:
                        break;
                }
            }
        }

        else
        {
            ActiveRightDown(false);
        }


        if (CellBuildingComponent(xySelectedCell).HaveBuilding)
        {
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
            }
        }

        else
        {
            ActiveLeftEconomy(false);
        }
    }

    private void ActiveRightDown(bool isActive)
    {
        _buttonAbility1.gameObject.SetActive(isActive);
        _buttonAbility2.gameObject.SetActive(isActive);
        _buttonAbility3.gameObject.SetActive(isActive);
        _buttonAbility4.gameObject.SetActive(isActive);

        _rightDownUnitImage.gameObject.SetActive(isActive);
    }

    private void ActiveLeftEconomy(bool isActive)
    {
        _leftEconomyImage.gameObject.SetActive(isActive);
        _buyPawnButton.gameObject.SetActive(isActive);
        _improveCityButton.gameObject.SetActive(isActive);
    }


    #region Button Methods

    private void Button0(bool isRequest, bool isActive)
    {
        _photonPunRPC.GetUnit(UnitTypes.King);
    }

    private void Button1(bool isRequest, bool isActive)
    {
        if (isRequest) _photonPunRPC.GetUnit(UnitTypes.Pawn);
        else if (isActive) _selectedUnitComponentRef.Unref().SetSelectedUnit(UnitTypes.Pawn);
    }

    private void BittonAbility1()
    {
        _photonPunRPC.ProtectUnit(_selectorComponentRef.Unref().XYselectedCell);
    }
    private void BittonAbility2()
    {
        _photonPunRPC.RelaxUnit(_selectorComponentRef.Unref().XYselectedCell);
    }
    private void BittonAbility3()
    {

    }
    private void BittonAbility4()
    {
        _photonPunRPC.Build(_selectorComponentRef.Unref().XYselectedCell, BuildingTypes.City);
    }

    private void BuyPawn()
    {
        _photonPunRPC.BuyUnit(UnitTypes.Pawn);
    }
    private void ImproveCity()
    {

    }

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
