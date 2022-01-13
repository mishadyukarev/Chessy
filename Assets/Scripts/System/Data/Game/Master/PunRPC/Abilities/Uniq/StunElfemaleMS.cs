using Photon.Pun;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntCellUnit;

namespace Game.Game
{
    public sealed class StunElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);

            var sender = InfoC.Sender(MGOTypes.Master);
            //var playerSend = WhoseMoveC.WhoseMove;

            //ref var ownUnit_from = ref Unit<PlayerC>(idx_from);

            //ref var unitE_from = ref Unit<UnitCellEC>(idx_from);
            //ref var step_from = ref Unit<UnitCellEC>(idx_from);

            //ref var unit_to = ref Unit<UnitC>(idx_to);
            //ref var ownUnit_to = ref Unit<PlayerC>(idx_to);
            //ref var eff_to = ref Unit<StunC>(idx_to);


            //if (!Unit<CooldownC>(uniq_cur, idx_from).HaveCooldown)
            //{
            //    if (Unit<VisibledC>(playerSend, idx_to).IsVisibled)
            //    {
            //        if (unit_to.Have)
            //        {
            //            if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_to).Have)
            //            {
            //                if (unitE_from.HaveMax)
            //                {
            //                    if (step_from.Have(uniq_cur))
            //                    {
            //                        if (!ownUnit_from.Is(ownUnit_to.Player))
            //                        {
            //                            eff_to.SetNewStun();
            //                            Unit<CooldownC>(uniq_cur, idx_from).Cooldown = 3;

            //                            step_from.Take(uniq_cur);

            //                            EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, uniq_cur);
            //                        }
            //                    }

            //                    else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //                }
            //                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            //            }
            //        }
            //    }
            //}

            //else EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}