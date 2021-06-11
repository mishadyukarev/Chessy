using UnityEngine;
using UnityEngine.UI;
using static Main;

internal class UISystem : RPCGeneralSystemReduction
{
    private SceneManager _photonManagerScene;



    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;

    private Button _buttonLeave;

    private int[] _xySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;


    internal UISystem()
    {
        //_photonManagerScene = Instance.PhotonGameManager.SceneManager;

        //_buttonLeave = GameObject.Find("ButtonLeave").GetComponent<Button>();
        //_buttonLeave.onClick.AddListener(delegate { Leave(); });

    }


    public override void Run()
    {
        base.Run();

        //if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit && _eGM.CellUnitEnt_CellOwnerCom(_xySelectedCell).IsMine)
        //{
        //    switch (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).UnitType)
        //    {
        //        case UnitTypes.None:

        //            break;

        //        case UnitTypes.King:

        //            ActivateUniqueAbilities(default, true);

        //            break;

        //        case UnitTypes.Pawn:
        //            ActivateUniqueAbilities(default, true);

        //            break;

        //        default:
        //            break;
        //    }
        //}
        //else
        //{
        //    ActivateUniqueAbilities(default, false);
        //}

        //GameObject.Find("RightDownUnitImage").GetComponent<Image>().gameObject.SetActive(false);

        //if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit)
        //{
        //    if (_eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).IsActivatedUnitDict[Instance.IsMasterClient])
        //    {
        //        GameObject.Find("RightDownUnitImage").GetComponent<Image>().gameObject.SetActive(true);
        //    }
        //}
    }
    private void ActivateUniqueAbilities(UnitTypes unitType, bool isActive)
    {
        _eGM.Unique1AbilityEnt_ButtonCom.SetActive(isActive);
        _eGM.Unique2AbilityEnt_ButtonCom.SetActive(isActive);
        _eGM.Unique3AbilityEnt_ButtonCom.SetActive(isActive);

    }
    private void Leave() => _photonManagerScene.LeaveRoom();
}
