using Photon.Pun;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct StunElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            EntityMPool.StunElfemale<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            ref var ownUnit_from = ref Unit<PlayerTC>(idx_from);

            ref var unit_to = ref Unit<UnitTC>(idx_to);
            ref var ownUnit_to = ref Unit<PlayerTC>(idx_to);
            ref var eff_to = ref CellUnitStunEs.StepsForExitStun(idx_to);


            if (!CellUnitAbilityUniqueEs.Cooldown<CooldownC>(uniq_cur, idx_from).HaveCooldown)
            {
                if (CellUnitVisibleEs.Visible<IsVisibleC>(playerSend, idx_to).IsVisible)
                {
                    if (unit_to.Have)
                    {
                        if (Resources(EnvironmentTypes.AdultForest, idx_to).Have)
                        {
                            if (CellUnitHpEs.HaveMax(idx_from))
                            {
                                if (CellUnitStepEs.Have(idx_from, uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Player))
                                    {
                                        eff_to.Amount = 3;
                                        CellUnitAbilityUniqueEs.Cooldown<CooldownC>(uniq_cur, idx_from).Cooldown = 3;

                                        CellUnitStepEs.Take(idx_from, uniq_cur);

                                        EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, uniq_cur);
                                    }
                                }

                                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}