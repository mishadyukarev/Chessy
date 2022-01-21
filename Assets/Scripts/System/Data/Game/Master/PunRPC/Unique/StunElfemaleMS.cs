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
            var playerSend = WhoseMoveE.WhoseMove.Player;

            ref var ownUnit_from = ref CellUnitElseEs.Owner(idx_from);

            ref var unit_to = ref Unit(idx_to);
            ref var ownUnit_to = ref CellUnitElseEs.Owner(idx_to);


            if (!CellUnitAbilityUniqueEs.Cooldown(uniq_cur, idx_from).Have)
            {
                if (CellUnitVisibleEs.Visible(playerSend, idx_to).IsVisible)
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
                                        CellUnitStunEs.ForExitStun(idx_to).Amount = 4;
                                        CellUnitAbilityUniqueEs.Cooldown(uniq_cur, idx_from).Amount = 5;

                                        CellUnitStepEs.Take(idx_from, uniq_cur);

                                        EntityPool.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);


                                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                                        {
                                            if(CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx_1).Have)
                                            {
                                                if (CellUnitEs.Unit(idx_1).Have && CellUnitElseEs.Owner(idx_1).Is(CellUnitElseEs.Owner(idx_to).Player))
                                                {
                                                    CellUnitStunEs.ForExitStun(idx_1).Amount = 4;
                                                }
                                            }
                                        }
                                    }
                                }

                                else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}