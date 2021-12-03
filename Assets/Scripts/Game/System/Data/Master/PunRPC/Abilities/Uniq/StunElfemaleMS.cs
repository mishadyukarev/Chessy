using Game.Common;
using Leopotam.Ecs;
using Photon.Pun;

namespace Game.Game
{
    public sealed class StunElfemaleMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC, VisibleC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<StunC> _effUnitF = default;
        private EcsFilter<CooldownUniqC> _uniqUnitF = default;

        private EcsFilter<EnvC> _envF = default;

        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = WhoseMoveC.WhoseMove;

            ref var ownUnit_from = ref _unitF.Get2(idx_from);
            ref var hp_from = ref _statUnitF.Get1(idx_from);
            ref var step_from = ref _statUnitF.Get2(idx_from);
            ref var cdUniq_from = ref _uniqUnitF.Get1(idx_from);

            ref var unit_to = ref _unitF.Get1(idx_to);
            ref var ownUnit_to = ref _unitF.Get2(idx_to);
            ref var visUnit_to = ref _unitF.Get3(idx_to);
            ref var env_to = ref _envF.Get1(idx_to);


            if (!cdUniq_from.HaveCooldown(UniqAbilTypes.StunElfemale))
            {
                if (visUnit_to.IsVisibled(playerSend))
                {
                    if (unit_to.Have)
                    {
                        if (env_to.Have(EnvTypes.AdultForest))
                        {
                            if (hp_from.HaveMax)
                            {
                                if (step_from.HaveMin)
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Owner))
                                    {
                                        _effUnitF.Get1(idx_to).SetNewStun();
                                        _uniqUnitF.Get1(idx_from).SetCooldown(UniqAbilTypes.StunElfemale, 3);

                                        step_from.Take();

                                        RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.StunElfemale);
                                    }
                                }

                                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}