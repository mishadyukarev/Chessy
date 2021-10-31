using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    public sealed class ConditionMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, StepComponent, ConditionUnitC, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);

            var neededCondType = ForCondMasCom.NeededCondUnitType;
            var idxForCondit = ForCondMasCom.IdxForCondition;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForCondit);
            ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get3(idxForCondit);


            switch (neededCondType)
            {
                case CondUnitTypes.None:
                    _cellUnitFilter.Get3(idxForCondit).DefCondition();
                    break;

                case CondUnitTypes.Protected:
                    if (_cellUnitFilter.Get3(idxForCondit).Is(CondUnitTypes.Protected))
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                        _cellUnitFilter.Get3(idxForCondit).DefCondition();
                    }

                    else if (_cellUnitFilter.Get2(idxForCondit).HaveMinSteps)
                    {
                        if (_cellUnitFilter.Get3(idxForCondit).Is(CondUnitTypes.Relaxed))
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                            _cellUnitFilter.Get3(idxForCondit).CondUnitType = neededCondType;

                            _cellUnitFilter.Get2(idxForCondit).TakeSteps();
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                            _cellUnitFilter.Get3(idxForCondit).CondUnitType = neededCondType;

                            _cellUnitFilter.Get2(idxForCondit).TakeSteps();
                        }
                    }

                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case CondUnitTypes.Relaxed:
                    if (_cellUnitFilter.Get3(idxForCondit).Is(CondUnitTypes.Relaxed))
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                        _cellUnitFilter.Get3(idxForCondit).DefCondition();
                    }

                    else if (_cellUnitFilter.Get2(idxForCondit).HaveMinSteps)
                    {
                        if (_cellUnitFilter.Get3(idxForCondit).Is(CondUnitTypes.Protected))
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                            _cellUnitFilter.Get3(idxForCondit).CondUnitType = neededCondType;
                            _cellUnitFilter.Get2(idxForCondit).TakeSteps();
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
                            _cellUnitFilter.Get3(idxForCondit).CondUnitType = neededCondType;
                            _cellUnitFilter.Get2(idxForCondit).TakeSteps();
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