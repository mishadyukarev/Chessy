using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class UISystem : CellReduction, IEcsRunSystem
{
    private PhotonManagerScene _photonManagerScene;
    private PhotonPunRPC _photonPunRPC;


    private TextMeshProUGUI _goldAmountText;

    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;
    private Image _rightDownUnitImage;
    private Image _leftEconomyImage;

    private Button _buyPawnButton;
    private Button _improveCityButton;
    private Button _buttonLeave;


    #region Ability zone

    private Button _buildingAbilityButton0;
    private Button _buildingAbilityButton1;
    private Button _buildingAbilityButton2;
    private Button _buildingAbilityButton3;



    private Button _uniqueAbilityButton1;
    private Button _uniqueAbilityButton2;
    private Button _uniqueAbilityButton3;

    #endregion

    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private EcsComponentRef<EconomyComponent> _economyComponentRef = default;
    private EcsComponentRef<EconomyComponent.BuildingsComponent> _economyBuildingsComponentRef;
    private EcsComponentRef<EconomyComponent.UnitsComponent> _economyUnitsComponentRef;



    internal UISystem(ECSmanager eCSmanager, PhotonGameManager photonManager) : base(eCSmanager)
    {
        _photonManagerScene = photonManager.PhotonManagerScene;
        _photonPunRPC = photonManager.PhotonPunRPC;


        #region ComponetRefs

        _economyComponentRef = eCSmanager.EntitiesGeneralManager.EconomyComponentRef;
        _economyBuildingsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyBuildingsComponentRef;
        _economyUnitsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyUnitsComponentRef;

        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;

        #endregion


        #region Texts

        _goldAmountText = MainGame.InstanceGame.StartSpawnGameManager.GoldAmmountText;

        #endregion


        #region Images

        _rightUpUnitImage = MainGame.InstanceGame.StartSpawnGameManager.RightUpUnitImage;
        _rightMiddleUnitImage = MainGame.InstanceGame.StartSpawnGameManager.RightMiddleUnitImage;
        _rightDownUnitImage = MainGame.InstanceGame.StartSpawnGameManager.AbilitiesImage;
        _leftEconomyImage = MainGame.InstanceGame.StartSpawnGameManager.LeftEconomyImage;

        _rightMiddleUnitImage.gameObject.SetActive(false);

        #endregion


        #region Ability zone

        _buildingAbilityButton0 = MainGame.InstanceGame.StartSpawnGameManager.BuildingAbilityButton0;
        _buildingAbilityButton0.onClick.AddListener(delegate { Build(BuildingTypes.City); });

        _buildingAbilityButton1 = MainGame.InstanceGame.StartSpawnGameManager.BuildingAbilityButton1;
        _buildingAbilityButton2 = MainGame.InstanceGame.StartSpawnGameManager.BuildingAbilityButton2;
        _buildingAbilityButton3 = MainGame.InstanceGame.StartSpawnGameManager.BuildingAbilityButton3;

        _uniqueAbilityButton1 = MainGame.InstanceGame.StartSpawnGameManager.UniqueAbilityButton1;
        _uniqueAbilityButton2 = MainGame.InstanceGame.StartSpawnGameManager.UniqueAbilityButton2;
        _uniqueAbilityButton3 = MainGame.InstanceGame.StartSpawnGameManager.UniqueAbilityButton3;


        #endregion


        _buttonLeave = MainGame.InstanceGame.StartSpawnGameManager.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

        _buyPawnButton = MainGame.InstanceGame.StartSpawnGameManager.BuyPawnButton;
        _buyPawnButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });

        _improveCityButton = MainGame.InstanceGame.StartSpawnGameManager.ImproveCityButton;
        _improveCityButton.onClick.AddListener(delegate { ImproveCity(); });



        #region Ready Zone



        #endregion

    }


    public void Run()
    {
        _goldAmountText.text = _economyComponentRef.Unref().Gold.ToString();


        var xySelectedCell = _selectorComponentRef.Unref().XYselectedCell;


        if (CellUnitComponent(xySelectedCell).IsMine)
        {
            switch (CellUnitComponent(xySelectedCell).UnitType)
            {
                case UnitTypes.None:

                    _buildingAbilityButton0.gameObject.SetActive(false);
                    ActivateBuildingAbilities(false);

                    break;

                case UnitTypes.King:

                    _buildingAbilityButton0.gameObject.SetActive(false);
                    ActivateBuildingAbilities(false);
                    ActivateUniqueAbilities(default, true);

                    break;

                case UnitTypes.Pawn:
                    ActivateUniqueAbilities(default, true);

                    if (_economyBuildingsComponentRef.Unref().IsSettedCity)
                    {
                        _buildingAbilityButton0.gameObject.SetActive(false);
                        ActivateBuildingAbilities(true);
                    }
                    else
                    {
                        _buildingAbilityButton0.gameObject.SetActive(true);
                        ActivateBuildingAbilities(false);
                    }

                    break;

                default:
                    break;
            }
        }
        else
        {
            _buildingAbilityButton0.gameObject.SetActive(false);
            ActivateBuildingAbilities(false);
            ActivateUniqueAbilities(default, false);
            _rightDownUnitImage.gameObject.SetActive(false);
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

    private void ActivateBuildingAbilities(bool isActive)
    {
        _buildingAbilityButton1.gameObject.SetActive(isActive);
        _buildingAbilityButton2.gameObject.SetActive(isActive);
        _buildingAbilityButton3.gameObject.SetActive(isActive);
    }
    private void ActivateUniqueAbilities(UnitTypes unitType, bool isActive)
    {
        //switch (unitType)
        //{
        //    case UnitTypes.None:
        //        break;

        //    case UnitTypes.King:



        //        break;

        //    case UnitTypes.Pawn:



        //        break;

        //    default:
        //        break;
        //}

        _uniqueAbilityButton1.gameObject.SetActive(isActive);
        _uniqueAbilityButton2.gameObject.SetActive(isActive);
        _uniqueAbilityButton3.gameObject.SetActive(isActive);

    }



    #region Button Methods



    #region Abilities

    private void Build(BuildingTypes buildingType) => _photonPunRPC.Build(_selectorComponentRef.Unref().XYselectedCell, buildingType);



    private void UniqueAbilityButton1() => _photonPunRPC.RelaxUnit(_selectorComponentRef.Unref().XYselectedCell);

    #endregion


    #region Economy

    private void BuyUnit(UnitTypes unitType) => _photonPunRPC.BuyUnit(unitType);
    private void ImproveCity() { }

    #endregion


    private void Leave() => _photonManagerScene.LeaveRoom();



    #endregion
}
