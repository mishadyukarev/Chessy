using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using UnityEngine;

internal sealed class StandartAbilityUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public override void Run()
    {
        base.Run();


        if (CellUnitWorker.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitWorker.IsMine(XySelectedCell))
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

                        case UnitTypes.PawnSword:
                            ActiveStandartAbilities(true);
                            break;

                        case UnitTypes.Rook:
                            ActiveStandartAbilities(true);
                            break;

                        case UnitTypes.RookCrossbow:
                            ActiveStandartAbilities(true);
                            break;

                        case UnitTypes.Bishop:
                            ActiveStandartAbilities(true);
                            break;

                        case UnitTypes.BishopCrossbow:
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
            }

            else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).IsBot)
            {
                ActiveStandartAbilities(false);
            }

        }
        else
        {
            ActiveStandartAbilities(false);
        }

        void ActiveStandartAbilities(bool isActive)
        {
            _eGGUIM.StandartAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(isActive);
            _eGGUIM.StandartFirstAbilityEnt_ButtonCom.SetActive(isActive);
            _eGGUIM.StandartSecondAbilityEnt_ButtonCom.SetActive(isActive);

            if (isActive)
            {
                if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Protected, XySelectedCell))
                {
                    _eGGUIM.StandartFirstAbilityEnt_ButtonCom.SetColor(Color.yellow);
                }
                else _eGGUIM.StandartFirstAbilityEnt_ButtonCom.SetColor(Color.white);

                if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Relaxed, XySelectedCell)) _eGGUIM.StandartSecondAbilityEnt_ButtonCom.SetColor(Color.green);
                else _eGGUIM.StandartSecondAbilityEnt_ButtonCom.SetColor(Color.white);
            }
        }
    }
}
