using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class UISystem : CellGeneralReduction, IEcsRunSystem
{
    private GameSceneManager _photonManagerScene;
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

    private int[] _xySelectedCell => _eGM.SelectorESelectorC.XYselectedCell;


    internal UISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonManagerScene = Instance.PhotonGameManager.GameSceneManager;
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;



        #region Images

        _rightUpUnitImage = MainGame.Instance.GameObjectPool.RightUpImage;
        _rightMiddleUnitImage = MainGame.Instance.GameObjectPool.RightMiddleImage;
        _leftEconomyImage = MainGame.Instance.GameObjectPool.LeftImage;

        _rightMiddleUnitImage.gameObject.SetActive(false);

        #endregion


        #region Ability zone

        _uniqueAbilityButton1 = MainGame.Instance.GameObjectPool.UniqueAbilityButton1;
        _uniqueAbilityButton2 = MainGame.Instance.GameObjectPool.UniqueAbilityButton2;
        _uniqueAbilityButton3 = MainGame.Instance.GameObjectPool.UniqueAbilityButton3;


        #endregion

        _buttonLeave = MainGame.Instance.GameObjectPool.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

    }


    public void Run()
    {
        if (_eGM.CellUnitEnt_OwnerCom(_xySelectedCell).IsMine)
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).UnitType)
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




        switch (_eGM.CellBuildingEnt_BuildingTypeCom(_xySelectedCell).BuildingType)
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
