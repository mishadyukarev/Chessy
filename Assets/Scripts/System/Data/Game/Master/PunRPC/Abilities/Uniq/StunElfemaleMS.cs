using Photon.Pun;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class StunElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = WhoseMoveC.WhoseMove;

            ref var ownUnit_from = ref Unit<OwnerC>(idx_from);

            ref var unitE_from = ref Unit<UnitCellEC>(idx_from);
            ref var step_from = ref Unit<UnitCellEC>(idx_from);
            ref var cdUniq_from = ref Unit<CooldownUniqC>(idx_from);

            ref var unit_to = ref Unit<UnitC>(idx_to);
            ref var ownUnit_to = ref Unit<OwnerC>(idx_to);
            ref var visUnit_to = ref Unit<VisibleC>(idx_to);
            ref var env_to = ref Environment<EnvironmentC>(idx_to);
            ref var eff_to = ref Unit<StunC>(idx_to);


            if (!cdUniq_from.HaveCooldown(uniq_cur))
            {
                if (visUnit_to.IsVisibled(playerSend))
                {
                    if (unit_to.Have)
                    {
                        if (env_to.Have(EnvTypes.AdultForest))
                        {
                            if (unitE_from.HaveMax)
                            {
                                if (step_from.Have(uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Owner))
                                    {
                                        eff_to.SetNewStun();
                                        Unit<CooldownUniqC>(idx_from).SetCooldown(uniq_cur, 3);

                                        step_from.Take(uniq_cur);

                                        RpcS.SoundToGeneral(RpcTarget.All, uniq_cur);
                                    }
                                }

                                else RpcS.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else RpcS.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else RpcS.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}