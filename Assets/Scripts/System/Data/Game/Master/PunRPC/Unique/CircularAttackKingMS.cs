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
            var unitEs = Es.CellEs.UnitEs;


            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;

            ref var hpUnit_0 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health;
            ref var levUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
            ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;
            ref var condUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).ConditionC;


            if (!Es.CellEs.UnitEs.Unique(uniq_cur, idx_0).Cooldown.Have)
            {
                if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    Es.CellEs.UnitEs.Unique(uniq_cur, idx_0).Cooldown.Amount = 2;

                    foreach (var xy1 in Es.CellEs.GetXyAround(Es.CellEs.CellE(idx_0).XyC.Xy))
                    {
                        var idx_1 = Es.CellEs.GetIdxCell(xy1);

                        ref var unit_1 = ref Es.CellEs.UnitEs.Main(idx_1).UnitC;
                        ref var ownUnit_1 = ref Es.CellEs.UnitEs.Main(idx_1).OwnerC;
                        ref var hpUnit_1 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_1).Health;

                        ref var tw_1 = ref Es.CellEs.UnitEs.ToolWeapon(idx_1).ToolWeapon;

                        ref var buildC_1 = ref Es.CellEs.BuildEs.Build(idx_1).BuildTC;


                        if (unit_1.Have)
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
                                    Es.CellEs.UnitEs.StatEs.Hp(idx_1).Health.Take(UnitDamageValues.Damage(uniq_cur));

                                    if (Es.CellEs.UnitEs.StatEs.Hp(idx_1).Health.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK || !hpUnit_1.Have)
                                    {
                                        unitEs.Kill(idx_1, Es);
                                    }
                                }
                            }
                        }
                    }

                    Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    Es.Rpc.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    if (condUnit_0.HaveCondition) condUnit_0.Reset();
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
