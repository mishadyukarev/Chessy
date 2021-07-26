using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class ConditionAbilitiesUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public void Run()
    {

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
            UIRightWorker.SetActive(isActive, ConditionUnitTypes.Protected);
            UIRightWorker.SetActive(isActive, ConditionUnitTypes.Relaxed);

            if (isActive)
            {
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, XySelectedCell))
                {
                    UIRightWorker.SetConditionColor(ConditionUnitTypes.Protected, Color.yellow);
                }

                else
                {
                    UIRightWorker.SetConditionColor(ConditionUnitTypes.Protected, Color.white);
                }

                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, XySelectedCell))
                {
                    UIRightWorker.SetConditionColor(ConditionUnitTypes.Relaxed, Color.green);
                }
                else
                {
                    UIRightWorker.SetConditionColor(ConditionUnitTypes.Relaxed, Color.white);
                }
            }
        }
    }
}
