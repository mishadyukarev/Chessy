using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

internal sealed class ConditionMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter;
    private EcsFilter<ConditionMasCom> _conditionFilter;

    private EcsFilter<IdxUnitsInConditionCom> _idxUnitsInCondFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var neededCondType = _conditionFilter.Get1(0).NeededConditionUnitType;
        var idxForCondit = _conditionFilter.Get1(0).IdxForCondition;
        ref var idxUnitsInCondCom = ref _idxUnitsInCondFilter.Get1(0);

        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForCondit);
        ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxForCondit);


        switch (neededCondType)
        {
            case ConditionUnitTypes.None:
                if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Protected))
                {
                    idxUnitsInCondCom.RemoveUnitInCondition(ConditionUnitTypes.Protected, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);
                }
                else if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                {
                    idxUnitsInCondCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);
                }

                idxUnitsInCondCom.AddUnitInCondition(ConditionUnitTypes.None, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);
                curCellUnitDataCom.ResetConditionType();
                break;

            case ConditionUnitTypes.Protected:
                if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Protected))
                {
                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                    curCellUnitDataCom.ResetConditionType();
                    idxUnitsInCondCom.RemoveUnitInCondition(neededCondType, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);
                }

                else if (curCellUnitDataCom.HaveMaxAmountSteps)
                {
                    if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                    {
                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                        idxUnitsInCondCom.ReplaceCondition(ConditionUnitTypes.Relaxed, ConditionUnitTypes.Protected, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);

                        curCellUnitDataCom.ConditionType = neededCondType;

                        curCellUnitDataCom.ResetAmountSteps();
                    }
                    else
                    {
                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                        curCellUnitDataCom.ConditionType = neededCondType;
                        idxUnitsInCondCom.ReplaceCondition(ConditionUnitTypes.None, neededCondType, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);

                        curCellUnitDataCom.ResetAmountSteps();
                    }
                }

                else
                {
                    RPCGameSystem.MistakeStepsUnitToGeneral(sender);
                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
                break;


            case ConditionUnitTypes.Relaxed:
                if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                {
                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                    curCellUnitDataCom.ResetConditionType();

                    idxUnitsInCondCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);
                }

                else if (curCellUnitDataCom.HaveMaxAmountSteps)
                {
                    if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Protected))
                    {
                        idxUnitsInCondCom.ReplaceCondition(ConditionUnitTypes.Protected, ConditionUnitTypes.Relaxed, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);

                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                        curCellUnitDataCom.ConditionType = neededCondType;
                        curCellUnitDataCom.ResetAmountSteps();
                    }
                    else
                    {
                        idxUnitsInCondCom.ReplaceCondition(ConditionUnitTypes.None, neededCondType, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, idxForCondit);

                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                        curCellUnitDataCom.ConditionType = neededCondType;
                        curCellUnitDataCom.ResetAmountSteps();
                    }
                }

                else
                {
                    RPCGameSystem.MistakeStepsUnitToGeneral(sender);
                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
                break;


            default:
                throw new Exception();
        }
    }
}
