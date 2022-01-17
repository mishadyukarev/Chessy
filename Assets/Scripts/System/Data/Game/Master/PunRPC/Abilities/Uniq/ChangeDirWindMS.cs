﻿using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct ChangeDirWindMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToDoingMC.Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var unit_from = ref Unit<UnitTC>(idx_from);

            ref var unitE_from = ref Unit<UnitCellEC>(idx_from);
            ref var stepUnit_from = ref Unit<UnitCellEC>(idx_from);


            if (CellUnitHpEs.HaveMax(idx_from))
            {
                if (CellUnitStepEs.Have(idx_from, uniq_cur))
                {
                    //if (WindC.Have(idx_to))
                    //{
                    //    WindC.Set(idx_to);

                    //    Unit<UnitCellEC>(idx_from).Take(uniq_cur);

                    //    Unit<CooldownC>(uniq_cur, idx_from).Cooldown = 6;

                    //    EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, uniq_cur);
                    //}
                }

                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}