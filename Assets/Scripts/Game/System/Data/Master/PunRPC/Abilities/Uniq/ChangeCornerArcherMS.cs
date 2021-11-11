﻿using Chessy.Common;
using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ChangeCornerArcherMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, HpC, StepC> _unitFilt = default;
        private EcsFilter<CornerArcherC> _archerFilt = default;

        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var unit_0 = ref _unitFilt.Get1(idx_0);
            ref var hp_0 = ref _unitFilt.Get2(idx_0);
            ref var step_0 = ref _unitFilt.Get3(idx_0);
            ref var corner_0 = ref _archerFilt.Get1(idx_0);

            if (hp_0.HaveMaxHp)
            {
                if (step_0.HaveMinSteps)
                {
                    corner_0.ChangeCorner();

                    step_0.TakeSteps();

                    RpcSys.SoundToGeneral(sender, ClipGameTypes.PickArcher);
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}