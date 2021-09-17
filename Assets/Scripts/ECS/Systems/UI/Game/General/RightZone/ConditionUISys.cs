using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class ConditionUISys : IEcsRunSystem
{
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;
    private EcsFilter<SelectorComponent> _selectorFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;

    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;
        ref var unitZoneUICom = ref _unitZoneUIFilter.Get1(0);

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
        ref var selOwnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
        ref var selBotUnitCom = ref _cellUnitFilter.Get3(idxSelCell);


        unitZoneUICom.SetTextStandInfo(LanguageComComp.GetText(GameLanguageTypes.StandartAbilities));
        unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Protected, LanguageComComp.GetText(GameLanguageTypes.Protect));

        if (selUnitDatCom.HaveUnit)
        {
            if (selOwnUnitCom.HaveOwner)
            {
                if (selUnitDatCom.IsUnitType(UnitTypes.King))
                {
                    unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Relax));
                }

                else if (selUnitDatCom.IsUnitType(UnitTypes.Pawn))
                {
                    if (selUnitDatCom.HaveMaxAmountHealth)
                    {
                        unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Extract));
                    }
                    else
                    {
                        unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Relax));
                    }
                }

                else
                {
                    unitZoneUICom.SetTextToCondition(ConditionUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Relax));
                }


                if (selOwnUnitCom.IsMine)
                {
                    unitZoneUICom.SetActiveUnitZone(UnitUIZoneTypes.Condition, true);

                    if (selUnitDatCom.IsConditionType(ConditionUnitTypes.Protected))
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Protected, Color.yellow);
                    }

                    else
                    {
                        unitZoneUICom.SetColorToConditionButton(ConditionUnitTypes.Protected, Color.white);
                    }

                    if (selUnitDatCom.IsConditionType(ConditionUnitTypes.Relaxed))
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
