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
            var uniq_cur = Es.MasterEs.AbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);
            var playerSend = Es.WhoseMove.WhoseMove.Player;

            var ownUnit_from = UnitEs(idx_from).MainE.OwnerC;

            var ownUnit_to = UnitEs(idx_to).MainE.OwnerC;


            if (!UnitEs(idx_from).CooldownAbility(uniq_cur).HaveCooldown)
            {
                if (UnitEs(idx_to).VisibleE(playerSend).IsVisibleC.IsVisible)
                {
                    if (UnitEs(idx_to).MainE.HaveUnit(UnitStatEs(idx_to)))
                    {
                        if (EnvironmentEs(idx_to).AdultForest.HaveEnvironment)
                        {
                            if (UnitStatEs(idx_from).Hp.HaveMax)
                            {
                                if (UnitStatEs(idx_from).StepE.Have(uniq_cur))
                                {
                                    if (!ownUnit_from.Is(ownUnit_to.Player))
                                    {
                                        UnitEffectEs(idx_to).StunE.Set(uniq_cur);
                                        UnitEs(idx_from).CooldownAbility(uniq_cur).SetAfterAbility();

                                        UnitStatEs(idx_from).StepE.Take(uniq_cur);

                                        Es.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);


                                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_to))
                                        {
                                            if (EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                                            {
                                                if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)) && UnitEs(idx_1).MainE.OwnerC.Is(UnitEs(idx_to).MainE.OwnerC.Player))
                                                {
                                                    UnitEffectEs(idx_1).StunE.Set(uniq_cur);
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