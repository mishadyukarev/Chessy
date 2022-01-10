using Photon.Pun;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    sealed class CircularAttackKingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var hpUnit_0 = ref Unit<HpC>(idx_0);
            ref var levUnit_0 = ref Unit<LevelC>(idx_0);
            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
            ref var cdUniq_0 = ref Unit<CooldownUniqC>(idx_0);
            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
            ref var condUnit_0 = ref Unit<ConditionC>(idx_0);
            ref var effUnit_0 = ref Unit<EffectsC>(idx_0);


            if (!cdUniq_0.HaveCooldown(uniq_cur))
            {
                if (stepUnit_0.Have(uniq_cur))
                {
                    EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    cdUniq_0.SetCooldown(uniq_cur, 2);

                    foreach (var xy1 in CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy))
                    {
                        var idx_1 = IdxCell(xy1);

                        ref var unit_1 = ref Unit<UnitC>(idx_1);
                        ref var ownUnit_1 = ref Unit<OwnerC>(idx_1);
                        ref var waterUnit_1 = ref Unit<UnitCellEC>(idx_1);
                        ref var hpUnit_1 = ref Unit<HpC>(idx_1);
                        ref var effUnitC_1 = ref Unit<EffectsC>(idx_1);

                        ref var tw_1 = ref UnitTW<ToolWeaponC>(idx_1);
                        ref var shield_1 = ref UnitTW<ShieldC>(idx_1);

                        ref var envC_1 = ref Environment<EnvironmentC>(idx_1);
                        ref var buildC_1 = ref Build<BuildC>(idx_1);


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
                                    Unit<UnitCellEC>(idx_1).Take(uniq_cur);

                                    if (waterUnit_1.IsHpDeathAfterAttack || !hpUnit_1.Have)
                                    {
                                        Unit<UnitCellEC>(idx_1).Kill();
                                    }
                                }
                            }
                        }
                    }

                    stepUnit_0.Take(uniq_cur);
                    effUnit_0.DefAllEffects();

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.AttackMelee);


                    if (condUnit_0.HaveCondition) condUnit_0.Reset();
                }
                else
                {
                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
