using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class UISystem : SystemGeneralReduction, IEcsRunSystem
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

    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;


    internal UISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonManagerScene = Instance.PhotonGameManager.GameSceneManager;
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;



        #region Images

        //_rightUpUnitImage = MainGame.Instance.GameObjectPool.RightUpImage;
        //_rightMiddleUnitImage = MainGame.Instance.GameObjectPool.RightMiddleImage;
        _leftEconomyImage = MainGame.Instance.ObjectPool.LeftImage;

        //_rightMiddleUnitImage.gameObject.SetActive(false);

        #endregion


        #region Ability zone

        _uniqueAbilityButton1 = MainGame.Instance.ObjectPool.UniqueFirstAbilityButton;
        _uniqueAbilityButton2 = MainGame.Instance.ObjectPool.UniqueSecondAbilityButton;
        _uniqueAbilityButton3 = MainGame.Instance.ObjectPool.UniqueThirdAbilityButton;


        #endregion

        _buttonLeave = MainGame.Instance.ObjectPool.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

    }


    public void Run()
    {
        if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsMine)
        {
            switch (_eGM.CellEnt_CellUnitCom(_xySelectedCell).UnitType)
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

        Instance.ObjectPool.RightImage.gameObject.SetActive(false);

        if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).HaveUnit)
        {
            if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsActivatedUnitDict[Instance.IsMasterClient])
            {
                Instance.ObjectPool.RightImage.gameObject.SetActive(true);
            }
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
