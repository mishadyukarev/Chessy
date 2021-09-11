using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class ConditionUISys : IEcsRunSystem
{
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;
    private EcsFilter<SelectorComponent> _selectorFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;

    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelectedCell;
        ref var unitZoneUICom = ref _unitZoneUIFilter.Get1(0);

        ref var selUnitDataCom = ref _cellUnitFilter.Get1(idxSelCell);
        ref var selOwnerUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
        ref var selBotUnitCom = ref _cellUnitFilter.Get3(idxSelCell);


        if (selUnitDataCom.HaveUnit)
        {
            if (selOwnerUnitCom.HaveOwner)
            {
                if (selUnitDataCom.IsUnitType(UnitTypes.King))
                {
                    unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, "Relax");
                }

                else if (selUnitDataCom.IsUnitType(UnitTypes.Pawn))
                {
                    if (selUnitDataCom.HaveMaxAmountHealth)
                    {
                        unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, "Extract");
                    }
                    else
                    {
                        unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, "Relax");
                    }
                }

                else
                {
                    unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, "Relax");
                }


                if (selOwnerUnitCom.IsMine)
                {
                    unitZoneUICom.SetActiveUnitZone(UnitUIZoneTypes.Condition, true);

                    if (selUnitDataCom.IsConditionType(ConditionUnitTypes.Protected))
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Protected, Color.yellow);
                    }

                    else
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Protected, Color.white);
                    }

                    if (selUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Relaxed, Color.green);
                    }
                    else
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Relaxed, Color.white);
                    }
                }

                else
                {
                    unitZoneUICom.SetActiveUnitZone(UnitUIZoneTypes.Condition, false);
                }
            }

            else if (selBotUnitCom.IsBot)
            {
                unitZoneUICom.SetActiveUnitZone(UnitUIZoneTypes.Condition, false);
            }
        }
        else
        {
            unitZoneUICom.SetActiveUnitZone(UnitUIZoneTypes.Condition, false);
        }
    }
}
