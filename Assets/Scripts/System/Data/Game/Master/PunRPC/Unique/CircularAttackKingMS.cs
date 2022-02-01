using Photon.Pun;

namespace Game.Game
{
    sealed class CircularAttackKingMS : SystemCellAbstract, IEcsRunSystem
    {
        public CircularAttackKingMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            var uniq_cur = Es.MasterEs.AbilityC.Ability;

            var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;


            if (!UnitEs(idx_0).CooldownAbility(uniq_cur).HaveCooldown)
            {
                if (UnitStatEs(idx_0).StepE.Have(uniq_cur))
                {
                    Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    UnitEs(idx_0).CooldownAbility(uniq_cur).SetAfterAbility();

                    foreach (var xy1 in CellEsWorker.GetXyAround(CellEs(idx_0).CellE.XyC.Xy))
                    {
                        var idx_1 = CellEsWorker.GetIdxCell(xy1);

                        var ownUnit_1 = UnitEs(idx_1).MainE.OwnerC;
                        var tw_1 = UnitEs(idx_1).ToolWeaponE.ToolWeaponTC;


                        if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (tw_1.Is(ToolWeaponTypes.Shield))
                                {
                                    UnitEs(idx_1).ToolWeaponE.BreakShield();
                                }
                                else
                                {
                                    UnitStatEs(idx_1).Hp.Attack(uniq_cur, Es);
                                }
                            }
                        }
                    }

                    UnitStatEs(idx_0).StepE.Take(uniq_cur);
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    Es.Rpc.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    UnitEs(idx_0).MainE.ResetCondition();
                }
                else
                {
                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else Es.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
