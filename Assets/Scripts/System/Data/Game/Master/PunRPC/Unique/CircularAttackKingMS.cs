using Photon.Pun;
using static Game.Game.CellEs;
using static Game.Game.CellBuildE;

namespace Game.Game
{
    struct CircularAttackKingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;

            ref var hpUnit_0 = ref CellUnitEntities.Hp(idx_0).AmountC;
            ref var levUnit_0 = ref CellUnitEntities.Else(idx_0).LevelC;
            ref var ownUnit_0 = ref CellUnitEntities.Else(idx_0).OwnerC;
            ref var condUnit_0 = ref CellUnitEntities.Else(idx_0).ConditionC;


            if (!CellUnitEntities.CooldownUnique(uniq_cur, idx_0).Cooldown.Have)
            {
                if (CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    EntityPool.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    CellUnitEntities.CooldownUnique(uniq_cur, idx_0).Cooldown.Amount = 2;

                    foreach (var xy1 in CellSpaceSupport.GetXyAround(Cell<XyC>(idx_0).Xy))
                    {
                        var idx_1 = IdxCell(xy1);

                        ref var unit_1 = ref CellUnitEntities.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref CellUnitEntities.Else(idx_1).OwnerC;
                        ref var hpUnit_1 = ref CellUnitEntities.Hp(idx_1).AmountC;

                        ref var tw_1 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_1);

                        ref var buildC_1 = ref Build<BuildingTC>(idx_1);


                        if (unit_1.Have)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (tw_1.Is(ToolWeaponTypes.Shield))
                                {
                                    CellUnitTWE.Take(idx_1);
                                }
                                else
                                {
                                    CellUnitEntities.Hp(idx_1).AmountC.Take(UnitDamageValues.Damage(uniq_cur));

                                    if (CellUnitEntities.Hp(idx_1).AmountC.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK || !hpUnit_1.Have)
                                    {
                                        CellUnitEntities.Kill(idx_1);
                                    }
                                }
                            }
                        }
                    }

                    CellUnitEntities.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    if (condUnit_0.HaveCondition) condUnit_0.Reset();
                }
                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
