using Photon.Pun;

namespace Game.Game
{
    sealed class StunElfemaleMS : SystemAbstract, IEcsRunSystem
    {
        public StunElfemaleMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            Es.MasterEs.StunElfemale<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = Es.WhoseMove.WhoseMove.Player;

            ref var ownUnit_from = ref Es.CellEs.UnitEs.Main(idx_from).OwnerC;

            ref var unit_to = ref Es.CellEs.UnitEs.Main(idx_to).UnitC;
            ref var ownUnit_to = ref Es.CellEs.UnitEs.Main(idx_to).OwnerC;


            if (!Es.CellEs.UnitEs.Unique(uniq_cur, idx_from).Cooldown.Have)
            {
                if (Es.CellEs.UnitEs.VisibleE(playerSend, idx_to).VisibleC.IsVisible)
                {
                    if (unit_to.Have)
                    {
                        if (Es.CellEs.EnvironmentEs.AdultForest( idx_to).HaveEnvironment)
                        {
                            if (Es.CellEs.UnitEs.StatEs.Hp(idx_from).HaveMax)
                            {
                                if (Es.CellEs.UnitEs.StatEs.Step(idx_from).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Player))
                                    {
                                        Es.CellEs.UnitEs.Stun(idx_to).ForExitStun.Amount = 4;
                                        Es.CellEs.UnitEs.Unique(uniq_cur, idx_from).Cooldown.Amount = 5;

                                        Es.CellEs.UnitEs.StatEs.Step(idx_from).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                                        Es.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);


                                        foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_to))
                                        {
                                            if (Es.CellEs.EnvironmentEs.AdultForest( idx_1).HaveEnvironment)
                                            {
                                                if (Es.CellEs.UnitEs.Main(idx_1).UnitC.Have && Es.CellEs.UnitEs.Main(idx_1).OwnerC.Is(Es.CellEs.UnitEs.Main(idx_to).OwnerC.Player))
                                                {
                                                    Es.CellEs.UnitEs.Stun(idx_1).ForExitStun.Amount = 4;
                                                }
                                            }
                                        }
                                    }
                                }

                                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else Es.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}