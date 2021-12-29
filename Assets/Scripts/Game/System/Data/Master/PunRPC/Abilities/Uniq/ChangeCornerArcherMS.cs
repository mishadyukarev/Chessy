﻿using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class ChangeCornerArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var hpUnitCell_0 = ref Unit<HpUnitWC>(idx_0);
            ref var stepUnit_0 = ref Unit<StepUnitWC>(idx_0);
            ref var corner_0 = ref Unit<CornerArcherC>(idx_0);


            if (hpUnitCell_0.HaveMax)
            {
                if (stepUnit_0.Have(uniq))
                {
                    corner_0.ChangeCorner();

                    Unit<StepUnitWC>(idx_0).Take(uniq);

                    RpcSys.SoundToGeneral(sender, ClipTypes.PickArcher);
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