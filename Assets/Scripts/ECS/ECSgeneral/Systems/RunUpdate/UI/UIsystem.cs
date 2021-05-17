using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class UISystem : CellGeneralReduction, IEcsRunSystem
{
    private PhotonManagerScene _photonManagerScene;
    private PhotonPunRPC _photonPunRPC;



    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;
    private Image _leftEconomyImage;

    private Button _buttonLeave;

    #region Ability zone



    private Button _uniqueAbilityButton1;
    private Button _uniqueAbilityButton2;
    private Button _uniqueAbilityButton3;

    #endregion

    private int[] _xySelectedCell => _eGM.SelectorComponentSelectorEnt.XYselectedCell;


    internal UISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonManagerScene = InstanceGame.PhotonGameManager.PhotonManagerScene;
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;



        #region Images

        _rightUpUnitImage = MainGame.InstanceGame.GameObjectPool.RightUpImage;
        _rightMiddleUnitImage = MainGame.InstanceGame.GameObjectPool.RightMiddleImage;
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
        if (_eGM.CellUnitComponent(_xySelectedCell).IsMine)
        {
            switch (_eGM.CellUnitComponent(_xySelectedCell).UnitType)
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
        }




        switch (_eGM.CellBuildingComponent(_xySelectedCell).BuildingType)
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
