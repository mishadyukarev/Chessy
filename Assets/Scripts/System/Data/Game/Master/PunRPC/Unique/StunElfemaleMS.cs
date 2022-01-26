using Photon.Pun;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    public struct StunElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            EntitiesMaster.StunElfemale<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = EntitiesMaster.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = Entities.WhoseMoveE.WhoseMove.Player;

            ref var ownUnit_from = ref CellUnitEs.Else(idx_from).OwnerC;

            ref var unit_to = ref CellUnitEs.Else(idx_to).UnitC;
            ref var ownUnit_to = ref CellUnitEs.Else(idx_to).OwnerC;


            if (!CellUnitEs.CooldownUnique(uniq_cur, idx_from).Cooldown.Have)
            {
                if (CellUnitEs.VisibleE(playerSend, idx_to).VisibleC.IsVisible)
                {
                    if (unit_to.Have)
                    {
                        if (Environment(EnvironmentTypes.AdultForest, idx_to).Resources.Have)
                        {
                            if (CellUnitEs.Hp(idx_from).HaveMax)
                            {
                                if (CellUnitEs.Step(idx_from).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Player))
                                    {
                                        CellUnitEs.Stun(idx_to).ForExitStun.Amount = 4;
                                        CellUnitEs.CooldownUnique(uniq_cur, idx_from).Cooldown.Amount = 5;

                                        CellUnitEs.Step(idx_from).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                                        Entities.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);


                                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                                        {
                                            if (CellEnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_1).Resources.Have)
                                            {
                                                if (CellUnitEs.Else(idx_1).UnitC.Have && CellUnitEs.Else(idx_1).OwnerC.Is(CellUnitEs.Else(idx_to).OwnerC.Player))
                                                {
                                                    CellUnitEs.Stun(idx_1).ForExitStun.Amount = 4;
                                                }
                                            }
                                        }
                                    }
                                }

                                else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else Entities.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}