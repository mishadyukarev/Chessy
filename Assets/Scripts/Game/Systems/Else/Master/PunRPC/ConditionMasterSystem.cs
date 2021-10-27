using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class ConditionMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);

            var neededCondType = ForCondMasCom.NeededCondUnitType;
            var idxForCondit = ForCondMasCom.IdxForCondition;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForCondit);
            ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxForCondit);


            switch (neededCondType)
            {
                case CondUnitTypes.None:
                    curCellUnitDataCom.DefCondType();
                    break;

                case CondUnitTypes.Protected:
                    if (curCellUnitDataCom.Is(CondUnitTypes.Protected))
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                        curCellUnitDataCom.DefCondType();
                    }

                    else if (curCellUnitDataCom.HaveMinAmountSteps)
                    {
                        if (curCellUnitDataCom.Is(CondUnitTypes.Relaxed))
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                            curCellUnitDataCom.CondUnitType = neededCondType;

                            curCellUnitDataCom.TakeAmountSteps();
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                            curCellUnitDataCom.CondUnitType = neededCondType;

                            curCellUnitDataCom.TakeAmountSteps();
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
                        curCellUnitDataCom.DefCondType();
                    }

                    else if (curCellUnitDataCom.HaveMinAmountSteps)
                    {
                        if (curCellUnitDataCom.Is(CondUnitTypes.Protected))
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                            curCellUnitDataCom.CondUnitType = neededCondType;
                            curCellUnitDataCom.TakeAmountSteps();
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                            curCellUnitDataCom.CondUnitType = neededCondType;
                            curCellUnitDataCom.TakeAmountSteps();
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