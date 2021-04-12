using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

class ButtonSystem : CellReductionSystem, IEcsInitSystem, IEcsRunSystem
{
    private PhotonManagerScene _photonManagerScene;
    private PhotonPunRPC _photonPunRPC;

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

    private EcsComponentRef<EconomyComponent> _economyComponentRef;
    private EcsComponentRef<EconomyComponent.BuildingsComponent> _economyBuildingsComponentRef;

    internal ButtonSystem(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager) : base(eCSmanager, supportManager)
    {
        _photonManagerScene = photonManager.PhotonManagerScene;
        _photonPunRPC = photonManager.PhotonPunRPC;

        _buttonComponentRef = eCSmanager.EntitiesGeneralManager.ButtonComponentRef;
        _selectedUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectedUnitComponentRef;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;

        _economyComponentRef = eCSmanager.EntitiesGeneralManager.EconomyComponentRef;
        _economyBuildingsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyBuildingsComponentRef;
    }


    public void Init()
    {
        _button1 = GameObject.Find("Button1").GetComponent<Button>();
        _button1.onClick.AddListener(delegate { Button1(true, false); });

        _buttonLeave = GameObject.Find("ButtonLeave").GetComponent<Button>();
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

        _buttonComponentRef.Unref().DonerDelegate = Doner;
        _requestDoneButton = GameObject.Find("ButtonDone").GetComponent<Button>();
        _requestDoneButton.onClick.AddListener(delegate { Doner(true, false); });



        _buttonComponentRef.Unref().Button1Delegate = Button1;



        _buttonAbility1 = GameObject.Find("ButtonAbility1").GetComponent<Button>();
        _buttonAbility1.onClick.AddListener(delegate { BittonAbility1(); });

        _buttonAbility2 = GameObject.Find("ButtonAbility2").GetComponent<Button>();
        _buttonAbility2.onClick.AddListener(delegate { BittonAbility2(); });

        _buttonAbility3 = GameObject.Find("ButtonAbility3").GetComponent<Button>();
        _buttonAbility3.onClick.AddListener(delegate { BittonAbility3(); });

        _buttonAbility4 = GameObject.Find("ButtonAbility4").GetComponent<Button>();
        _buttonAbility4.onClick.AddListener(delegate { BittonAbility4(); });



        _buyPawnButton = GameObject.Find("BuyPawnButton").GetComponent<Button>();
        _buyPawnButton.onClick.AddListener(delegate { BuyPawn(); });

        _improveCityButton = GameObject.Find("ImproveCityButton").GetComponent<Button>();
        _improveCityButton.onClick.AddListener(delegate { ImproveCity(); });
    }

    public void Run()
    {
        var xySelectedCell = _selectorComponentRef.Unref().XYselectedCell;

        if (CellUnitComponent(xySelectedCell).IsMine)
        {
            switch (CellUnitComponent(xySelectedCell).UnitType)
            {
                case UnitTypes.None:
                    SetActiveRightDown(false);

                    break;

                case UnitTypes.King:
                    break;

                case UnitTypes.Pawn:

                    SetActiveRightDown(true);

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

        else
        {
            SetActiveRightDown(false);
        }


        switch (CellBuildingComponent(xySelectedCell).BuildingType)
        {
            case BuildingTypes.None:

                _buyPawnButton.gameObject.SetActive(false);
                _improveCityButton.gameObject.SetActive(false);

                break;

            case BuildingTypes.City:

                _buyPawnButton.gameObject.SetActive(true);
                _improveCityButton.gameObject.SetActive(true);

                break;

            default:
                break;
        }
    }

    private void SetActiveRightDown(bool isActive)
    {
        _buttonAbility1.gameObject.SetActive(isActive);
        _buttonAbility2.gameObject.SetActive(isActive);
        _buttonAbility3.gameObject.SetActive(isActive);
        _buttonAbility4.gameObject.SetActive(isActive);
    }



    private void Button1(bool isRequest, bool isActive)
    {
        if (isRequest) _photonPunRPC.GetUnit(UnitTypes.Pawn);
        else if (isActive) _selectedUnitComponentRef.Unref().SetReset(UnitTypes.Pawn);
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
}
