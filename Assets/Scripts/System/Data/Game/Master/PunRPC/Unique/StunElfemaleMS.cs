using Photon.Pun;
using static Game.Game.CellEnvironmentEs;

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

            ref var ownUnit_from = ref CellUnitEntities.Else(idx_from).OwnerC;

            ref var unit_to = ref CellUnitEntities.Else(idx_to).UnitC;
            ref var ownUnit_to = ref CellUnitEntities.Else(idx_to).OwnerC;


            if (!CellUnitEntities.CooldownUnique(uniq_cur, idx_from).Cooldown.Have)
            {
                if (CellUnitVisibleEs.Visible(playerSend, idx_to).IsVisible)
                {
                    if (unit_to.Have)
                    {
                        if (Resources(EnvironmentTypes.AdultForest, idx_to).Have)
                        {
                            if (CellUnitEntities.Hp(idx_from).HaveMax)
                            {
                                if (CellUnitEntities.Step(idx_from).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Player))
                                    {
                                        CellUnitEntities.Stun(idx_to).ForExitStun.Amount = 4;
                                        CellUnitEntities.CooldownUnique(uniq_cur, idx_from).Cooldown.Amount = 5;

                                        CellUnitEntities.Step(idx_from).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                                        EntityPool.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);


                                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                                        {
                                            if(CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx_1).Have)
                                            {
                                                if (CellUnitEntities.Else(idx_1).UnitC.Have && CellUnitEntities.Else(idx_1).OwnerC.Is(CellUnitEntities.Else(idx_to).OwnerC.Player))
                                                {
                                                    CellUnitEntities.Stun(idx_1).ForExitStun.Amount = 4;
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