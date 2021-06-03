using UnityEngine.UI;
using static Main;

internal class UISystem : RPCGeneralReduction
{
    private SceneManager _photonManagerScene;



    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;

    private Button _buttonLeave;

    #region Ability zone



    private Button _uniqueAbilityButton1;
    private Button _uniqueAbilityButton2;
    private Button _uniqueAbilityButton3;

    #endregion

    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;


    internal UISystem()
    {
        _photonManagerScene = Instance.PhotonGameManager.SceneManager;


        #region Ability zone

        _uniqueAbilityButton1 = Main.Instance.CanvasGameManager.UniqueFirstAbilityButton;
        _uniqueAbilityButton2 = Main.Instance.CanvasGameManager.UniqueSecondAbilityButton;
        _uniqueAbilityButton3 = Main.Instance.CanvasGameManager.UniqueThirdAbilityButton;


        #endregion

        _buttonLeave = Main.Instance.CanvasGameManager.ButtonLeave;
        _buttonLeave.onClick.AddListener(delegate { Leave(); });

    }


    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit && _eGM.CellUnitEnt_CellOwnerCom(_xySelectedCell).IsMine)
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

        Instance.CanvasGameManager.RightImage.gameObject.SetActive(false);

        if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit)
        {
            if (_eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).IsActivatedUnitDict[Instance.IsMasterClient])
            {
                Instance.CanvasGameManager.RightImage.gameObject.SetActive(true);
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
