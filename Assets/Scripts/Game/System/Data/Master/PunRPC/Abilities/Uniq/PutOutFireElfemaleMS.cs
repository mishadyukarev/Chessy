using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public class PutOutFireElfemaleMS : IEcsRunSystem
    {
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<CooldownUniqC> _uniqUnitF = default;
        private EcsFilter<FireC> _fireF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToMC.Get(out var idx_from, out var idx_to);

            ref var unit_from = ref _statUnitF.Get1(idx_from);
            ref var hp_from = ref _statUnitF.Get1(idx_from);
            ref var step_from = ref _statUnitF.Get2(idx_from);
            ref var uniq_from = ref _uniqUnitF.Get1(idx_from);

            ref var fire_to = ref _fireF.Get1(idx_to);


            if (hp_from.HaveMaxHp)
            {
                if (step_from.HaveMinSteps)
                {
                    if (fire_to.Have)
                    {
                        fire_to.Disable();

                        step_from.TakeSteps();


                        uniq_from.SetCooldown(UniqAbilTypes.PutOutFireElfemale, 6);

                        RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.PutOutFireElfemale);
                    }
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}