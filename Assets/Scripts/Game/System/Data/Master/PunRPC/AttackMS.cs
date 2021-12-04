using Leopotam.Ecs;
using Photon.Pun;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class AttackMS : IEcsRunSystem
    {
        private readonly EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private readonly EcsFilter<HpC, StepC, WaterC> _statUnitF = default;
        private readonly EcsFilter<ConditionC, MoveInCondC, EffectsC, StunC> _effUnitF = default;
        private readonly EcsFilter<CooldownUniqC> _uniqUnitF = default;

        private readonly EcsFilter<EnvC> _envF = default;
        private readonly EcsFilter<RiverC> _riverF = default;


        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            #region Unit

            ref var unit_from = ref _unitF.Get1(idx_from);
            ref var levUnit_from = ref _unitF.Get2(idx_from);
            ref var ownUnit_from = ref _unitF.Get3(idx_from);

            ref var hpUnitCell_from = ref Unit<HpUnitC>(idx_from);
            ref var hpUnit_from = ref _statUnitF.Get1(idx_from);
            ref var stepUnit_from = ref Unit<StepUnitC>(idx_from);
            ref var waterUnit_from = ref _statUnitF.Get3(idx_from);

            ref var condUnit_from = ref _effUnitF.Get1(idx_from);
            ref var moveCond_from = ref _effUnitF.Get2(idx_from);
            ref var effUnit_from = ref _effUnitF.Get3(idx_from);

            ref var tw_from = ref UnitToolWeapon<ToolWeaponC>(idx_from);
            ref var twShield_from = ref UnitToolWeapon<ProtectionC>(idx_from);



            ref var unit_to = ref _unitF.Get1(idx_to);
            ref var levUnit_to = ref _unitF.Get2(idx_to);
            ref var ownUnit_to = ref _unitF.Get3(idx_to);

            ref var hpUnitCell_to = ref Unit<HpUnitC>(idx_to);
            ref var hpUnit_to = ref _statUnitF.Get1(idx_to);
            ref var stepUnit_to = ref _statUnitF.Get2(idx_to);
            ref var waterUnit_to = ref _statUnitF.Get3(idx_to);

            ref var condUnit_to = ref _effUnitF.Get1(idx_to);
            ref var moveCond_to = ref _effUnitF.Get2(idx_to);
            ref var effUnit_to = ref _effUnitF.Get3(idx_to);
            ref var stun_to = ref _effUnitF.Get4(idx_to);

            ref var tw_to = ref UnitToolWeapon<ToolWeaponC>(idx_to);
            ref var twShield_to = ref UnitToolWeapon<ProtectionC>(idx_to);

            #endregion


            ref var river_from = ref _riverF.Get1(idx_from);
            ref var build_from = ref Build<BuildC>(idx_from);
            ref var ownBuild_from = ref Build<OwnerC>(idx_from);
            ref var env_from = ref _envF.Get1(idx_from);
            ref var trail_from = ref Trail<TrailC>(idx_from);
            ref var cdUniq_from = ref _uniqUnitF.Get1(idx_from);


            ref var river_to = ref _riverF.Get1(idx_to);
            ref var build_to = ref Build<BuildC>(idx_to);
            ref var ownBuild_to = ref Build<OwnerC>(idx_to);
            ref var env_to = ref _envF.Get1(idx_to);
            ref var trail_to = ref Trail<TrailC>(idx_to);
            ref var cdUniq_to = ref _uniqUnitF.Get1(idx_to);



            var simpUniqueType = AttackCellsC.WhichAttack(ownUnit_from.Owner, idx_from, idx_to);

            if (simpUniqueType != default)
            {
                stepUnit_from.Reset();
                condUnit_from.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += Unit<DamageUnitC>(idx_from).DamageAttack(simpUniqueType);

                if (unit_from.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);



                powerDam_to += Unit<DamageUnitC>(idx_to).DamageOnCell;


                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = HpUnitC.MAX;
                var minDamage = HpC.MIN;

                if (!unit_to.IsMelee) powerDam_to /= 2;

                if (powerDam_to > powerDam_from)
                {
                    max_limit = powerDam_to * 2;
                    min_limit = powerDam_to / 3;

                    if (min_limit >= powerDam_from)
                    {
                        minus_from = maxDamage;
                        powerDam_to = minDamage;
                    }
                    else
                    {
                        minus_to = maxDamage * powerDam_from / max_limit;

                        max_limit = powerDam_from * 2;
                        minus_from = maxDamage * powerDam_to / max_limit;
                    }
                }
                else
                {
                    max_limit = powerDam_from * 2;
                    min_limit = powerDam_from / 3;

                    if (min_limit >= powerDam_to)
                    {
                        minus_to = maxDamage;
                        minus_from = minDamage;
                    }
                    else
                    {
                        minus_from = maxDamage * powerDam_to / max_limit;

                        max_limit = powerDam_to * 2f;
                        minus_to = maxDamage * powerDam_from / max_limit;
                    }
                }


                if (unit_from.IsMelee)
                {
                    if (tw_from.Is(TWTypes.Shield))
                    {
                        UnitToolWeapon<ShieldC>(idx_from).Take();
                    }
                    else if (minus_from > 0)
                    {
                        hpUnitCell_from.TakeAttack((int)minus_from);
                    }
                }


                if (tw_to.Is(TWTypes.Shield))
                {
                    UnitToolWeapon<ShieldC>(idx_to).Take();
                }
                else if (minus_to > 0)
                {
                    hpUnitCell_to.TakeAttack((int)minus_to);
                }



                if (!hpUnit_to.Have)
                {
                    Unit<UnitCellC>(idx_to).Kill(levUnit_to.Level, ownUnit_to.Owner);


                    if (unit_from.IsMelee)
                    {
                        if (!hpUnit_from.Have)
                        {
                            Unit<UnitCellC>(idx_from).Kill(levUnit_from.Level, ownUnit_from.Owner);
                        }
                        else
                        {
                            var dir = CellSpaceC.GetDirect(Cell<XyC>(idx_from).Xy, Cell<XyC>(idx_to).Xy);
                            Unit<UnitCellC>(idx_to).Shift(idx_from, dir);
                        }
                    }
                }

                else if (!hpUnit_from.Have)
                {
                    Unit<UnitCellC>(idx_from).Kill(levUnit_from.Level, ownUnit_from.Owner);
                }

                effUnit_from.DefAllEffects();
                effUnit_to.DefAllEffects();
            }
        }
    }
}