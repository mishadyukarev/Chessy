using Photon.Pun;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct CircularAttackKingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            var uniq_cur = Entities.MasterEs.UniqueAbilityC.Ability;

            ref var hpUnit_0 = ref Entities.CellEs.UnitEs.Hp(idx_0).AmountC;
            ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
            ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;
            ref var condUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).ConditionC;


            if (!Entities.CellEs.UnitEs.CooldownUnique(uniq_cur, idx_0).Cooldown.Have)
            {
                if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    Entities.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    Entities.CellEs.UnitEs.CooldownUnique(uniq_cur, idx_0).Cooldown.Amount = 2;

                    foreach (var xy1 in CellSpaceSupport.GetXyAround(Entities.CellEs.CellE(idx_0).XyC.Xy))
                    {
                        var idx_1 = Entities.CellEs.IdxCell(xy1);

                        ref var unit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).OwnerC;
                        ref var hpUnit_1 = ref Entities.CellEs.UnitEs.Hp(idx_1).AmountC;

                        ref var tw_1 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_1).ToolWeaponC;

                        ref var buildC_1 = ref Entities.CellEs.BuildEs.Build(idx_1).BuildTC;


                        if (unit_1.Have)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (tw_1.Is(ToolWeaponTypes.Shield))
                                {
                                    Entities.CellEs.UnitEs.Take(idx_1);
                                }
                                else
                                {
                                    Entities.CellEs.UnitEs.Hp(idx_1).AmountC.Take(UnitDamageValues.Damage(uniq_cur));

                                    if (Entities.CellEs.UnitEs.Hp(idx_1).AmountC.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK || !hpUnit_1.Have)
                                    {
                                        Entities.CellEs.UnitEs.Kill(idx_1);
                                    }
                                }
                            }
                        }
                    }

                    Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    Entities.Rpc.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    if (condUnit_0.HaveCondition) condUnit_0.Reset();
                }
                else
                {
                    Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else Entities.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
