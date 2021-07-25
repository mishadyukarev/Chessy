﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using UnityEngine;

internal sealed class StandartAbilityUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public override void Run()
    {
        base.Run();


        if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataWorker.IsMine(XySelectedCell))
                {
                    switch (CellUnitsDataWorker.UnitType(XySelectedCell))
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

            else if (CellUnitsDataWorker.IsBot(XySelectedCell))
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
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Protected, XySelectedCell))
                {
                    _eGGUIM.StandartFirstAbilityEnt_ButtonCom.SetColor(Color.yellow);
                }
                else _eGGUIM.StandartFirstAbilityEnt_ButtonCom.SetColor(Color.white);

                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XySelectedCell)) _eGGUIM.StandartSecondAbilityEnt_ButtonCom.SetColor(Color.green);
                else _eGGUIM.StandartSecondAbilityEnt_ButtonCom.SetColor(Color.white);
            }
        }
    }
}
