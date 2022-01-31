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

            var ownUnit_from = UnitEs.Main(idx_from).OwnerC;

            var unit_to = UnitEs.Main(idx_to).UnitTC;
            var ownUnit_to = UnitEs.Main(idx_to).OwnerC;


            if (!UnitEs.CooldownAbility(uniq_cur, idx_from).Cooldown.Have)
            {
                if (UnitEs.VisibleE(playerSend, idx_to).IsVisibleC.IsVisible)
                {
                    if (unit_to.Have)
                    {
                        if (CellEs.EnvironmentEs.AdultForest( idx_to).HaveEnvironment)
                        {
                            if (UnitEs.StatEs.Hp(idx_from).HaveMax)
                            {
                                if (UnitEs.StatEs.Step(idx_from).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Player))
                                    {
                                        UnitEs.Stun(idx_to).Set(uniq_cur);
                                        UnitEs.CooldownAbility(uniq_cur, idx_from).SetAfterAbility();

                                        UnitEs.StatEs.Step(idx_from).Steps.Amount -= CellUnitStepValues.NeedSteps(uniq_cur);

                                        Es.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);


                                        foreach (var idx_1 in CellEs.GetIdxsAround(idx_to))
                                        {
                                            if (CellEs.EnvironmentEs.AdultForest( idx_1).HaveEnvironment)
                                            {
                                                if (UnitEs.Main(idx_1).UnitTC.Have && UnitEs.Main(idx_1).OwnerC.Is(UnitEs.Main(idx_to).OwnerC.Player))
                                                {
                                                    UnitEs.Stun(idx_1).Set(uniq_cur);
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