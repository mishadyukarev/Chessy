using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class ConditionAbilitiesUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorSystem.XySelectedCell;

    public void Run()
    {

        if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataSystem.IsMine(XySelectedCell))
                {
                    switch (CellUnitsDataSystem.UnitType(XySelectedCell))
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

            else if (CellUnitsDataSystem.IsBot(XySelectedCell))
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
            RightUIViewContainer.SetActiveConditionButton(isActive, ConditionUnitTypes.Protected);
            RightUIViewContainer.SetActiveConditionButton(isActive, ConditionUnitTypes.Relaxed);

            if (isActive)
            {
                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XySelectedCell))
                {
                    RightUIViewContainer.SetConditionColor(ConditionUnitTypes.Protected, Color.yellow);
                }

                else
                {
                    RightUIViewContainer.SetConditionColor(ConditionUnitTypes.Protected, Color.white);
                }

                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XySelectedCell))
                {
                    RightUIViewContainer.SetConditionColor(ConditionUnitTypes.Relaxed, Color.green);
                }
                else
                {
                    RightUIViewContainer.SetConditionColor(ConditionUnitTypes.Relaxed, Color.white);
                }
            }
        }
    }
}
