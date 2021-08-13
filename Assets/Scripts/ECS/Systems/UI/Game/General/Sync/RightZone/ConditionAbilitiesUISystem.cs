using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using System;
using UnityEngine;

internal sealed class ConditionAbilitiesUISystem : IEcsRunSystem
{
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;
    private EcsFilter<SelectorComponent> _selectorFilter = default;

    private EcsFilter<CellUnitComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;

    public void Run()
    {
        var idxSelectedCell = _selectorFilter.Get1(0).IdxSelectedCell;
        ref var unitZoneUICom = ref _unitZoneUIFilter.Get1(0);

        ref var selCellUnitDataCom = ref _cellUnitFilter.Get1(idxSelectedCell);
        ref var selOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxSelectedCell);
        ref var selBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(idxSelectedCell);


        if (selCellUnitDataCom.HaveUnit)
        {
            if (selOwnerCellUnitCom.HaveOwner)
            {
                if (selOwnerCellUnitCom.IsMine)
                {
                    unitZoneUICom.SetActiveUnitZone(UnitUIZoneTypes.Condition, true);

                    if (selCellUnitDataCom.IsConditionType(ConditionUnitTypes.Protected))
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Protected, Color.yellow);
                    }

                    else
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Protected, Color.white);
                    }

                    if (selCellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
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

            else if (selBotOwnerCellUnitCom.IsBot)
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
