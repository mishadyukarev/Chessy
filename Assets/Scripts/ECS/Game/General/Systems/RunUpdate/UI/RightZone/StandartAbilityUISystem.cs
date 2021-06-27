using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

internal sealed class StandartAbilityUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

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
            _eGM.StandartAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(isActive);
            _eGM.StandartFirstAbilityEnt_ButtonCom.SetActive(isActive);
            _eGM.StandartSecondAbilityEnt_ButtonCom.SetActive(isActive);

            if (isActive)
            {
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsProtected) _eGM.StandartFirstAbilityEnt_ButtonCom.SetColor(Color.yellow);
                else _eGM.StandartFirstAbilityEnt_ButtonCom.SetColor(Color.white);

                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsRelaxed) _eGM.StandartSecondAbilityEnt_ButtonCom.SetColor(Color.green);
                else _eGM.StandartSecondAbilityEnt_ButtonCom.SetColor(Color.white);
            }
        }
    }
}
