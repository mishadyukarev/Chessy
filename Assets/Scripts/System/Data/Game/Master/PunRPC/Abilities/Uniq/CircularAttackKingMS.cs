using Photon.Pun;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;

namespace Game.Game
{
    struct CircularAttackKingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var hpUnit_0 = ref Unit<HpC>(idx_0);
            ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
            ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
            ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);


            if (!Unit<CooldownC>(uniq_cur, idx_0).HaveCooldown)
            {
                if (stepUnit_0.Have(uniq_cur))
                {
                    EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    Unit<CooldownC>(uniq_cur, idx_0).Cooldown = 2;

                    foreach (var xy1 in CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy))
                    {
                        var idx_1 = IdxCell(xy1);

                        ref var unit_1 = ref Unit<UnitTC>(idx_1);
                        ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);
                        ref var waterUnit_1 = ref Unit<UnitCellEC>(idx_1);
                        ref var hpUnit_1 = ref Unit<HpC>(idx_1);

                        ref var tw_1 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_1);
                        ref var shield_1 = ref CellUnitTWE.UnitTW<ShieldEC>(idx_1);

                        ref var buildC_1 = ref Build<BuildingC>(idx_1);


                        if (unit_1.Have)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                foreach (var item in KeysStat) Unit<HaveEffectC>(item, idx_1).Disable();

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
                    foreach (var item in KeysStat) Unit<HaveEffectC>(item, idx_0).Disable();

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
