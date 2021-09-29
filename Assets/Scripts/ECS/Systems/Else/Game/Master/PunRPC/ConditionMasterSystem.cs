using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

internal sealed class ConditionMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoCom> _infoFilter = default;
    private EcsFilter<ConditionMasCom> _conditionFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.sender;
        var neededCondType = _conditionFilter.Get1(0).NeededCondUnitType;
        var idxForCondit = _conditionFilter.Get1(0).IdxForCondition;

        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForCondit);
        ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxForCondit);


        switch (neededCondType)
        {
            case CondUnitTypes.None:
                curCellUnitDataCom.ResetCondType();
                break;

            case CondUnitTypes.Protected:
                if (curCellUnitDataCom.Is(CondUnitTypes.Protected))
                {
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                    curCellUnitDataCom.ResetCondType();
                }

                else if (curCellUnitDataCom.HaveMaxAmountSteps)
                {
                    if (curCellUnitDataCom.Is(CondUnitTypes.Relaxed))
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
                if (curCellUnitDataCom.Is(CondUnitTypes.Relaxed))
                {
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                    curCellUnitDataCom.ResetCondType();
                }

                else if (curCellUnitDataCom.HaveMaxAmountSteps)
                {
                    if (curCellUnitDataCom.Is(CondUnitTypes.Protected))
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
