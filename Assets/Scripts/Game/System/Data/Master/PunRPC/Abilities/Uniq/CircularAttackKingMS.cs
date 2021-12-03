using Leopotam.Ecs;
using Photon.Pun;
using Game.Common;

namespace Game.Game
{
    public sealed class CircularAttackKingMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ConditionC, EffectsC> _effUnitF = default;
        private EcsFilter<UniqAbilC, CooldownUniqC> _uniqUnitF = default;

        private EcsFilter<EnvC> _envF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var unit_0 = ref _statUnitF.Get1(idx_0);

            ref var levUnit_0 = ref _unitF.Get2(idx_0);
            ref var ownUnit_0 = ref _unitF.Get3(idx_0);

            ref var cdUniq_0 = ref _uniqUnitF.Get2(idx_0);

            ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

            ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
            ref var effUnit_0 = ref _effUnitF.Get2(idx_0);


            if (!cdUniq_0.HaveCooldown(UniqAbilTypes.CircularAttack))
            {
                if (stepUnit_0.HaveMin)
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    cdUniq_0.SetCooldown(UniqAbilTypes.CircularAttack, 2);

                    foreach (var xy1 in CellSpaceC.XyAround(EntityPool.Cell<XyC>(idx_0).Xy))
                    {
                        var idx_1 = EntityPool.IdxCell(xy1);

                        ref var unit_1 = ref _unitF.Get1(idx_1);
                        ref var levUnit_1 = ref _unitF.Get2(idx_1);
                        ref var ownUnit_1 = ref _unitF.Get3(idx_1);
                        ref var statUnit_1 = ref EntityPool.UnitStat<UnitStatCellC>(idx_1);
                        ref var hpUnitC_1 = ref _statUnitF.Get1(idx_1);
                        ref var effUnitC_1 = ref _effUnitF.Get2(idx_1);
                        ref var tw_1 = ref EntityPool.UnitToolWeapon<ToolWeaponC>(idx_1);
                        ref var shield_1 = ref EntityPool.UnitShield<UnitShieldCellC>(idx_1);



                        ref var envC_1 = ref _envF.Get1(idx_1);
                        ref var buildC_1 = ref EntityPool.Build<BuildC>(idx_1);


                        if (unit_1.Have)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Owner))
                            {
                                effUnitC_1.DefAllEffects();

                                if (tw_1.Is(TWTypes.Shield))
                                {
                                    shield_1.Take();
                                }
                                else
                                {
                                    hpUnitC_1.Take(25);
                                    if (statUnit_1.IsHpDeathAfterAttack || !hpUnitC_1.Have)
                                    {
                                        EntityPool.Unit<UnitCellC>(idx_1).Kill(levUnit_1.Level, ownUnit_1.Owner);
                                    }
                                }
                            }
                        }
                    }

                    stepUnit_0.Take();
                    effUnit_0.DefAllEffects();

                    RpcSys.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    if (condUnit_0.HaveCondition) condUnit_0.Reset();
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
