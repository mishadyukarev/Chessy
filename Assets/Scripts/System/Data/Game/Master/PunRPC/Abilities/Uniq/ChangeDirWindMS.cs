using Leopotam.Ecs;
using Photon.Pun;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class ChangeDirWindMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToDoingMC.Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var unit_from = ref Unit<UnitC>(idx_from);

            ref var unitE_from = ref Unit<UnitCellEC>(idx_from);
            ref var stepUnit_from = ref Unit<UnitCellEC>(idx_from);
            ref var uniq_from = ref Unit<CooldownUniqC>(idx_from);


            if (unitE_from.HaveMax)
            {
                if (stepUnit_from.Have(uniq_cur))
                {
                    if (WindC.Have(idx_to))
                    {
                        WindC.Set(idx_to);

                        Unit<UnitCellEC>(idx_from).Take(uniq_cur);

                        uniq_from.SetCooldown(uniq_cur, 6);

                        RpcSys.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}