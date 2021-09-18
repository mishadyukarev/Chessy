using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

internal sealed class ConditionMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ConditionMasCom> _conditionFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp> _cellUnitFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var neededCondType = _conditionFilter.Get1(0).NeededCondUnitType;
        var idxForCondit = _conditionFilter.Get1(0).IdxForCondition;

        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForCondit);
        ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxForCondit);


        switch (neededCondType)
        {
            case CondUnitTypes.None:
                curCellUnitDataCom.ResetConditionType();
                break;

            case CondUnitTypes.Protected:
                if (curCellUnitDataCom.IsCondType(CondUnitTypes.Protected))
                {
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                    curCellUnitDataCom.ResetConditionType();
                }

                else if (curCellUnitDataCom.HaveMaxAmountSteps)
                {
                    if (curCellUnitDataCom.IsCondType(CondUnitTypes.Relaxed))
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                        curCellUnitDataCom.CondUnitType = neededCondType;

                        curCellUnitDataCom.ResetAmountSteps();
                    }
                    else
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                        curCellUnitDataCom.CondUnitType = neededCondType;

                        curCellUnitDataCom.ResetAmountSteps();
                    }
                }

                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
                break;


            case CondUnitTypes.Relaxed:
                if (curCellUnitDataCom.IsCondType(CondUnitTypes.Relaxed))
                {
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                    curCellUnitDataCom.ResetConditionType();
                }

                else if (curCellUnitDataCom.HaveMaxAmountSteps)
                {
                    if (curCellUnitDataCom.IsCondType(CondUnitTypes.Protected))
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                        curCellUnitDataCom.CondUnitType = neededCondType;
                        curCellUnitDataCom.ResetAmountSteps();
                    }
                    else
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                        curCellUnitDataCom.CondUnitType = neededCondType;
                        curCellUnitDataCom.ResetAmountSteps();
                    }
                }

                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
                break;


            default:
                throw new Exception();
        }
    }
}
