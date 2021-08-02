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

        if (CellUnitsDataContainer.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataContainer.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataContainer.IsMine(XySelectedCell))
                {
                    switch (CellUnitsDataContainer.UnitType(XySelectedCell))
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

            else if (CellUnitsDataContainer.IsBot(XySelectedCell))
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
                if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Protected, XySelectedCell))
                {
                    RightUIViewContainer.SetConditionColor(ConditionUnitTypes.Protected, Color.yellow);
                }

                else
                {
                    RightUIViewContainer.SetConditionColor(ConditionUnitTypes.Protected, Color.white);
                }

                if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Relaxed, XySelectedCell))
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
