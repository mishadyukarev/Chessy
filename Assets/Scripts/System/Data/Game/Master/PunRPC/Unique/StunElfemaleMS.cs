using Photon.Pun;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    public struct StunElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            Entities.MasterEs.StunElfemale<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Entities.MasterEs.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = Entities.WhoseMove.WhoseMove.Player;

            ref var ownUnit_from = ref Entities.CellEs.UnitEs.Else(idx_from).OwnerC;

            ref var unit_to = ref Entities.CellEs.UnitEs.Else(idx_to).UnitC;
            ref var ownUnit_to = ref Entities.CellEs.UnitEs.Else(idx_to).OwnerC;


            if (!Entities.CellEs.UnitEs.CooldownUnique(uniq_cur, idx_from).Cooldown.Have)
            {
                if (Entities.CellEs.UnitEs.VisibleE(playerSend, idx_to).VisibleC.IsVisible)
                {
                    if (unit_to.Have)
                    {
                        if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_to).Resources.Have)
                        {
                            if (Entities.CellEs.UnitEs.Hp(idx_from).HaveMax)
                            {
                                if (Entities.CellEs.UnitEs.Step(idx_from).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Player))
                                    {
                                        Entities.CellEs.UnitEs.Stun(idx_to).ForExitStun.Amount = 4;
                                        Entities.CellEs.UnitEs.CooldownUnique(uniq_cur, idx_from).Cooldown.Amount = 5;

                                        Entities.CellEs.UnitEs.Step(idx_from).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                                        Entities.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);


                                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                                        {
                                            if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_1).Resources.Have)
                                            {
                                                if (Entities.CellEs.UnitEs.Else(idx_1).UnitC.Have && Entities.CellEs.UnitEs.Else(idx_1).OwnerC.Is(Entities.CellEs.UnitEs.Else(idx_to).OwnerC.Player))
                                                {
                                                    Entities.CellEs.UnitEs.Stun(idx_1).ForExitStun.Amount = 4;
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