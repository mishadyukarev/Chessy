﻿using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class ConditionMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ConditionMasCom> _conditionFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

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
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}