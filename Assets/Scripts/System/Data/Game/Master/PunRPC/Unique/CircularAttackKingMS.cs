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
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;

            var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;


            if (!UnitEs.CooldownAbility(uniq_cur, idx_0).HaveCooldown)
            {
                if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    UnitEs.CooldownAbility(uniq_cur, idx_0).SetAfterAbility();

                    foreach (var xy1 in CellEs.GetXyAround(CellEs.CellE(idx_0).XyC.Xy))
                    {
                        var idx_1 = CellEs.GetIdxCell(xy1);

                        var ownUnit_1 = UnitEs.Main(idx_1).OwnerC;
                        var tw_1 = UnitEs.ToolWeapon(idx_1).ToolWeaponTC;


                        if (UnitEs.Main(idx_1).HaveUnit(UnitStatEs))
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (tw_1.Is(ToolWeaponTypes.Shield))
                                {
                                    UnitEs.ToolWeapon(idx_1).BreakShield();
                                }
                                else
                                {
                                    UnitEs.StatEs.Hp(idx_1).Health.Amount -= UnitDamageValues.Damage(uniq_cur);

                                    if (UnitEs.StatEs.Hp(idx_1).Health.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK || !UnitEs.StatEs.Hp(idx_1).IsAlive)
                                    {
                                        UnitEs.Main(idx_1).Kill(Es);
                                    }
                                }
                            }
                        }
                    }

                    UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(uniq_cur);
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    Es.Rpc.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    UnitEs.Main(idx_0).ResetCondition();
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
