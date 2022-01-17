﻿using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct ChangeCornerArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var unitE_0 = ref Unit<UnitCellEC>(idx_0);
            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
            ref var corner_0 = ref Unit<IsCornedArcherC>(idx_0);


            if (CellUnitHpEs.HaveMax(idx_0))
            {
                if (CellUnitStepEs.Have(idx_0, uniq))
                {
                    corner_0.ChangeCorner();

                    Unit<UnitCellEC>(idx_0).Take(uniq);

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickArcher);
                }
                else
                {
                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}