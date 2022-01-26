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
            var uniq_cur = EntitiesMaster.UniqueAbilityC.Ability;

            ref var hpUnit_0 = ref CellUnitEs.Hp(idx_0).AmountC;
            ref var levUnit_0 = ref CellUnitEs.Else(idx_0).LevelC;
            ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;
            ref var condUnit_0 = ref CellUnitEs.Else(idx_0).ConditionC;


            if (!CellUnitEs.CooldownUnique(uniq_cur, idx_0).Cooldown.Have)
            {
                if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    Entities.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    CellUnitEs.CooldownUnique(uniq_cur, idx_0).Cooldown.Amount = 2;

                    foreach (var xy1 in CellSpaceSupport.GetXyAround(Cell(idx_0).XyC.Xy))
                    {
                        var idx_1 = IdxCell(xy1);

                        ref var unit_1 = ref CellUnitEs.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref CellUnitEs.Else(idx_1).OwnerC;
                        ref var hpUnit_1 = ref CellUnitEs.Hp(idx_1).AmountC;

                        ref var tw_1 = ref CellUnitEs.ToolWeapon(idx_1).ToolWeaponC;

                        ref var buildC_1 = ref CellBuildEs.Build(idx_1).BuildTC;


                        if (unit_1.Have)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (tw_1.Is(ToolWeaponTypes.Shield))
                                {
                                    CellUnitEs.Take(idx_1);
                                }
                                else
                                {
                                    CellUnitEs.Hp(idx_1).AmountC.Take(UnitDamageValues.Damage(uniq_cur));

                                    if (CellUnitEs.Hp(idx_1).AmountC.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK || !hpUnit_1.Have)
                                    {
                                        CellUnitEs.Kill(idx_1);
                                    }
                                }
                            }
                        }
                    }

                    CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));
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
