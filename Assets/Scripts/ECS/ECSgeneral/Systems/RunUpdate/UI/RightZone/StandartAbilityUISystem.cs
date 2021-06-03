
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

internal sealed class StandartAbilityUISystem : RPCGeneralSystemReduction
{
    private Button _standartAbilityButton1;
    private Button _standartAbilityButton2;

    private int[] XySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal StandartAbilityUISystem()
    {
        _standartAbilityButton1 = Main.Instance.CanvasGameManager.StandartAbilityButton1;
        _standartAbilityButton1.onClick.AddListener(delegate { StandartAbilityButton1(); });
        _standartAbilityButton2 = Main.Instance.CanvasGameManager.StandartAbilityButton2;
        _standartAbilityButton2.onClick.AddListener(delegate { StandartAbilityButton2(); });
    }

    public override void Run()
    {
        base.Run();


        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit && _eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType)
            {
                case UnitTypes.None:
                    ActiveStandartAbilities(false);
                    break;

                case UnitTypes.King:
                    ActiveStandartAbilities(true);
                    break;

                case UnitTypes.Pawn:
                    ActiveStandartAbilities(true);
                    break;

                case UnitTypes.Rook:
                    ActiveStandartAbilities(true);
                    break;

                case UnitTypes.Bishop:
                    ActiveStandartAbilities(true);
                    break;

                default:
                    break;
            }
        }
        else
        {
            ActiveStandartAbilities(false);
        }

        void ActiveStandartAbilities(bool isActive)
        {
            _standartAbilityButton1.gameObject.SetActive(isActive);
            _standartAbilityButton2.gameObject.SetActive(isActive);

            if (isActive)
            {
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsProtected) _standartAbilityButton1.image.color = Color.yellow;
                else _standartAbilityButton1.image.color = Color.white;

                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsRelaxed) _standartAbilityButton2.image.color = Color.green;
                else _standartAbilityButton2.image.color = Color.white;
            }
        }
    }


    private void StandartAbilityButton1() => _photonPunRPC.ProtectUnitToMaster(!_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsProtected, XySelectedCell);
    private void StandartAbilityButton2() => _photonPunRPC.RelaxUnitToMaster(!_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsRelaxed, XySelectedCell);
}
