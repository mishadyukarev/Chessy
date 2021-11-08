using Leopotam.Ecs;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class ConditionMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, StepComponent, ConditionUnitC> _cellUnitFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var neededCondType = ForCondMasCom.NeededCondUnitType;
            var idxForCondit = ForCondMasCom.IdxForCondition;

            ref var stepUnit_0 = ref _cellUnitFilter.Get2(idxForCondit);
            ref var condUnit_0 = ref _cellUnitFilter.Get3(idxForCondit);


            switch (neededCondType)
            {
                case CondUnitTypes.None:
                    condUnit_0.Def();
                    break;

                case CondUnitTypes.Protected:
                    if (condUnit_0.Is(CondUnitTypes.Protected))
                    {
                        RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);

                        condUnit_0.Def();
                    }

                    else if (stepUnit_0.HaveMinSteps)
                    {
                        if (condUnit_0.Is(CondUnitTypes.Relaxed))
                        {
                            RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);

                            condUnit_0.SetNew(neededCondType);

                            stepUnit_0.TakeSteps();
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);

                            condUnit_0.SetNew(neededCondType);

                            stepUnit_0.TakeSteps();
                        }
                    }

                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case CondUnitTypes.Relaxed:
                    if (condUnit_0.Is(CondUnitTypes.Relaxed))
                    {
                        RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);
                        condUnit_0.Def();
                    }

                    else if (stepUnit_0.HaveMinSteps)
                    {
                        if (condUnit_0.Is(CondUnitTypes.Protected))
                        {
                            RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);
                            condUnit_0.SetNew(neededCondType);
                            stepUnit_0.TakeSteps();
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);
                            condUnit_0.SetNew(neededCondType);
                            stepUnit_0.TakeSteps();
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