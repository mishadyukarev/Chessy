using Chessy.Common;
using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public sealed class StunElfemaleMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitMainFilt = default;
        private EcsFilter<UnitC, HpC, StepC> _unitStatFilt = default;
        private EcsFilter<UnitC, StunC> _unitEffFilt = default;
        private EcsFilter<UnitC, UniqAbilC> _unitUniqFilt = default;
        private EcsFilter<UnitC, VisibleC> _unitVisFilt = default;

        private EcsFilter<EnvC> _envFilt = default;

        public void Run()
        {
            FromToMC.Get(out var idx_from, out var idx_to);

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = WhoseMoveC.WhoseMove;

            ref var ownUnit_from = ref _unitMainFilt.Get2(idx_from);
            ref var hp_from = ref _unitStatFilt.Get2(idx_from);
            ref var step_from = ref _unitStatFilt.Get3(idx_from);
            ref var uniqUnit_from = ref _unitUniqFilt.Get2(idx_from);

            ref var unit_to = ref _unitMainFilt.Get1(idx_to);
            ref var ownUnit_to = ref _unitMainFilt.Get2(idx_to);
            ref var visUnit_to = ref _unitVisFilt.Get2(idx_to);
            ref var env_to = ref _envFilt.Get1(idx_to);


            if (!uniqUnit_from.HaveCooldown(UniqAbilTypes.StunElfemale))
            {
                if (visUnit_to.IsVisibled(playerSend))
                {
                    if (unit_to.HaveUnit)
                    {
                        if (env_to.Have(EnvTypes.AdultForest))
                        {
                            if (hp_from.HaveMaxHp)
                            {
                                if (step_from.HaveMinSteps)
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Owner))
                                    {
                                        _unitEffFilt.Get2(idx_to).SetNewStun();
                                        _unitUniqFilt.Get2(idx_from).SetCooldown(UniqAbilTypes.StunElfemale, 5);

                                        step_from.TakeSteps();

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

            else RpcSys.SoundToGeneral(sender, ClipGameTypes.Mistake);
        }
    }
}