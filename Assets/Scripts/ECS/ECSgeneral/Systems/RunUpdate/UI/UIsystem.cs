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



        #region Images

        _rightUpUnitImage = MainGame.InstanceGame.GameObjectPool.RightUpImage;
        _rightMiddleUnitImage = MainGame.InstanceGame.GameObjectPool.RightMiddleImage;
        _rightDownUnitImage = MainGame.InstanceGame.GameObjectPool.RightDownImage;
        _leftEconomyImage = MainGame.InstanceGame.GameObjectPool.LeftImage;

        _rightMiddleUnitImage.gameObject.SetActive(false);

        #endregion


        #region Ability zone

        _uniqueAbilityButton1 = MainGame.InstanceGame.GameObjectPool.UniqueAbilityButton1;
        _uniqueAbilityButton2 = MainGame.InstanceGame.GameObjectPool.UniqueAbilityButton2;
        _uniqueAbilityButton3 = MainGame.InstanceGame.GameObjectPool.UniqueAbilityButton3;


        #endregion

        _buttonLeave = MainGame.InstanceGame.GameObjectPool.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

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

                    break;

                default:
                    break;
            }
        }
        else
        {
            ActivateUniqueAbilities(default, false);
            //_rightDownUnitImage.gameObject.SetActive(false);
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
                }
        }
    }
    private void ActivateUniqueAbilities(UnitTypes unitType, bool isActive)
    {
        _uniqueAbilityButton1.gameObject.SetActive(isActive);
        _uniqueAbilityButton2.gameObject.SetActive(isActive);
        _uniqueAbilityButton3.gameObject.SetActive(isActive);

    }
    private void Leave() => _photonManagerScene.LeaveRoom();
}
