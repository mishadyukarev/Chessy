using Leopotam.Ecs;
using Photon.Pun;

namespace Game.Game
{
    public sealed class ChangeDirWindMS : IEcsRunSystem
    {
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<CooldownUniqC> _uniqUnitF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            ref var unit_from = ref _statUnitF.Get1(idx_from);
            ref var hp_from = ref _statUnitF.Get1(idx_from);
            ref var step_from = ref _statUnitF.Get2(idx_from);
            ref var uniq_from = ref _uniqUnitF.Get1(idx_from);


            if (hp_from.HaveMax)
            {
                if (step_from.HaveMin)
                {
                    if (WindC.Have(idx_to))
                    {
                        WindC.Set(idx_to);

                        step_from.TakeSteps();

                        uniq_from.SetCooldown(UniqAbilTypes.ChangeDirWind, 6);

                        RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.ChangeDirWind);
                    }
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}