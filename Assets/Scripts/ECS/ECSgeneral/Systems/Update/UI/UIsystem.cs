using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class UISystem : CellReduction, IEcsRunSystem
{
    private PhotonManagerScene _photonManagerScene;
    private PhotonPunRPC _photonPunRPC;



    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;
    private Image _rightDownUnitImage;
    private Image _leftEconomyImage;

    private Button _buyPawnButton;
    private Button _improveCityButton;
    private Button _buttonLeave;


    #region Ability zone



    private Button _uniqueAbilityButton1;
    private Button _uniqueAbilityButton2;
    private Button _uniqueAbilityButton3;

    #endregion

    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private EcsComponentRef<EconomyComponent.BuildingComponent> _economyBuildingsComponentRef;
    private EcsComponentRef<EconomyComponent.UnitComponent> _economyUnitsComponentRef;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;


    internal UISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonManagerScene = InstanceGame.PhotonGameManager.PhotonManagerScene;
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;


        #region ComponetRefs

        _economyBuildingsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyBuildingsComponentRef;
        _economyUnitsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyUnitsComponentRef;

        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;

        #endregion


        #region Texts


        #endregion


        #region Images

        _rightUpUnitImage = MainGame.InstanceGame.GameObjectPool.RightUpUnitImage;
        _rightMiddleUnitImage = MainGame.InstanceGame.GameObjectPool.RightMiddleUnitImage;
        _rightDownUnitImage = MainGame.InstanceGame.GameObjectPool.AbilitiesImage;
        _leftEconomyImage = MainGame.InstanceGame.GameObjectPool.LeftEconomyImage;

        _rightMiddleUnitImage.gameObject.SetActive(false);

        #endregion


        #region Ability zone

        _uniqueAbilityButton1 = MainGame.InstanceGame.GameObjectPool.UniqueAbilityButton1;
        _uniqueAbilityButton2 = MainGame.InstanceGame.GameObjectPool.UniqueAbilityButton2;
        _uniqueAbilityButton3 = MainGame.InstanceGame.GameObjectPool.UniqueAbilityButton3;


        #endregion


        _buttonLeave = MainGame.InstanceGame.GameObjectPool.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

        _buyPawnButton = MainGame.InstanceGame.GameObjectPool.BuyPawnButton;
        _buyPawnButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });

        _improveCityButton = MainGame.InstanceGame.GameObjectPool.ImproveCityButton;
        _improveCityButton.onClick.AddListener(delegate { ImproveCity(); });



        #region Ready Zone



        #endregion

    }


    public void Run()
    {
        var xySelectedCell = _selectorComponentRef.Unref().XYselectedCell;


        if (CellUnitComponent(xySelectedCell).IsMine)
        {
            switch (CellUnitComponent(xySelectedCell).UnitType)
            {
                case UnitTypes.None:

                    break;

                case UnitTypes.King:

                    ActivateUniqueAbilities(default, true);

                    break;

                case UnitTypes.Pawn:
                    ActivateUniqueAbilities(default, true);

                    if (_economyBuildingsComponentRef.Unref().IsSettedCity)
                    {

                    }
                    else
                    {

                    }

                    break;

                default:
                    break;
            }
        }
        else
        {
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



    //private void UniqueAbilityButton1() => _photonPunRPC.RelaxUnit(CellUnitComponent(_xySelectedCell).IsProtected _selectorComponentRef.Unref().XYselectedCell);

    #endregion


    #region Economy

    private void BuyUnit(UnitTypes unitType) => _photonPunRPC.BuyUnit(unitType);
    private void ImproveCity() { }

    #endregion


    private void Leave() => _photonManagerScene.LeaveRoom();



    #endregion
}
