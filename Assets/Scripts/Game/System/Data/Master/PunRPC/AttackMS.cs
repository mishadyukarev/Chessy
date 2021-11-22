using Leopotam.Ecs;
using Photon.Pun;
using Game.Common;

namespace Game.Game
{
    public sealed class AttackMS : IEcsRunSystem
    {
        private readonly EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private readonly EcsFilter<HpC, DamageC, StepC, WaterC> _statUnitF = default;
        private readonly EcsFilter<ConditionUnitC, MoveInCondC, UnitEffectsC, StunC> _effUnitF = default;
        private readonly EcsFilter<ToolWeaponC> _twUnitF = default;
        private readonly EcsFilter<CooldownUniqC> _uniqUnitF = default;

        private readonly EcsFilter<XyC> _xyF = default;
        private readonly EcsFilter<BuildC, OwnerC> _buildF = default;
        private readonly EcsFilter<EnvC> _envF = default;
        private readonly EcsFilter<RiverC> _riverF = default;
        private readonly EcsFilter<TrailC> _trailF = default;


        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            #region Unit

            ref var unit_from = ref _unitF.Get1(idx_from);
            ref var levUnit_from = ref _unitF.Get2(idx_from);
            ref var ownUnit_from = ref _unitF.Get3(idx_from);

            ref var hpUnit_from = ref _statUnitF.Get1(idx_from);
            ref var damUnit_from = ref _statUnitF.Get2(idx_from);
            ref var stepUnit_from = ref _statUnitF.Get3(idx_from);
            ref var waterUnit_from = ref _statUnitF.Get4(idx_from);  

            ref var condUnit_from = ref _effUnitF.Get1(idx_from);
            ref var moveCond_from = ref _effUnitF.Get2(idx_from);
            ref var effUnit_from = ref _effUnitF.Get3(idx_from);

            ref var twUnit_from = ref _twUnitF.Get1(idx_from);



            ref var unit_to = ref _unitF.Get1(idx_to);
            ref var levUnit_to = ref _unitF.Get2(idx_to);
            ref var ownUnit_to = ref _unitF.Get3(idx_to);

            ref var hpUnit_to = ref _statUnitF.Get1(idx_to);
            ref var damUnit_to = ref _statUnitF.Get2(idx_to);
            ref var stepUnit_to = ref _statUnitF.Get3(idx_to);
            ref var waterUnit_to = ref _statUnitF.Get4(idx_to);

            ref var condUnit_to = ref _effUnitF.Get1(idx_to);
            ref var moveCond_to = ref _effUnitF.Get2(idx_to);
            ref var effUnit_to = ref _effUnitF.Get3(idx_to);
            ref var stun_to = ref _effUnitF.Get4(idx_to);

            ref var twUnit_to = ref _twUnitF.Get1(idx_to);
            
            #endregion


            ref var river_from = ref _riverF.Get1(idx_from);
            ref var build_from = ref _buildF.Get1(idx_from);
            ref var ownBuild_from = ref _buildF.Get2(idx_from);
            ref var env_from = ref _envF.Get1(idx_from);
            ref var trail_from = ref _trailF.Get1(idx_from);
            ref var cdUniq_from = ref _uniqUnitF.Get1(idx_from);


            ref var river_to = ref _riverF.Get1(idx_to);
            ref var build_to = ref _buildF.Get1(idx_to);
            ref var ownBuild_to = ref _buildF.Get2(idx_to);
            ref var env_to = ref _envF.Get1(idx_to);
            ref var trail_to = ref _trailF.Get1(idx_to);
            ref var cdUniq_to = ref _uniqUnitF.Get1(idx_to);



            var simpUniqueType = AttackCellsC.WhichAttack(ownUnit_from.Owner, idx_from, idx_to);

            if (simpUniqueType != default)
            {
                stepUnit_from.DefSteps();
                if(condUnit_from.HaveCondition) condUnit_from.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += damUnit_from.DamageAttack(unit_from.Unit, levUnit_from.Level, twUnit_from, effUnit_from, simpUniqueType, UnitUpgC.UpgPercent(UnitStatTypes.Damage, unit_from.Unit, levUnit_from.Level, ownUnit_from.Owner));

                if (unit_from.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);



                powerDam_to += damUnit_to.DamageOnCell(unit_to.Unit, levUnit_to.Level, condUnit_to, twUnit_to, effUnit_to, UnitUpgC.UpgPercent(UnitStatTypes.Damage, unit_to.Unit, levUnit_to.Level, ownUnit_to.Owner), build_to.Build, env_to.Envronments);   


                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = HpC.MAX_HP;
                var minDamage = HpC.MIN_HP;

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
                    if (twUnit_from.Is(TWTypes.Shield))
                    {
                        twUnit_from.TakeShieldProtect();
                    }
                    else if (minus_from > 0)
                    {
                        hpUnit_from.TakeHp((int)minus_from);
                        if (hpUnit_from.IsHpDeathAfterAttack) hpUnit_from.SetMinHp();
                    }
                }


                if (twUnit_to.Is(TWTypes.Shield))
                {
                    twUnit_to.TakeShieldProtect();
                }
                else if (minus_to > 0)
                {
                    hpUnit_to.TakeHp((int)minus_to);
                    if (hpUnit_to.IsHpDeathAfterAttack) hpUnit_to.SetMinHp();
                }



                if (!hpUnit_to.HaveHp)
                {
                    unit_to.Kill(levUnit_to.Level, ownUnit_to.Owner);


                    if (unit_from.IsMelee)
                    {
                        if (!hpUnit_from.HaveHp)
                        {
                            unit_from.Kill(levUnit_from.Level, ownUnit_from.Owner);
                        }
                        else
                        {
                            unit_to = unit_from;
                            levUnit_to.SetLevel(levUnit_from.Level);
                            hpUnit_to = hpUnit_from;
                            stepUnit_to = stepUnit_from;
                            condUnit_to = condUnit_from;
                            twUnit_to = twUnit_from;
                            ownUnit_to = ownUnit_from;
                            waterUnit_to = waterUnit_from;
                            moveCond_to.ResetAll();
                            stun_to.Reset();
                            cdUniq_to.Replace(cdUniq_from);
                            if (river_to.HaveNearRiver) waterUnit_to.SetMaxWater(UnitUpgC.UpgPercent(UnitStatTypes.Water, unit_to.Unit, levUnit_to.Level, ownUnit_to.Owner));

                            var dir = CellSpace.GetDirect(_xyF.Get1(idx_from).Xy, _xyF.Get1(idx_to).Xy);
                            trail_to.TrySetNewTrail(dir.Invert(), env_to);
                            trail_from.TrySetNewTrail(dir, env_from);


                            if (build_to.Is(BuildTypes.Camp))
                            {
                                if (!ownBuild_to.Is(ownUnit_to.Owner))
                                {
                                    build_to.Remove(ownBuild_to.Owner);
                                }
                            }


                            unit_from.Clean(levUnit_from.Level, ownUnit_from.Owner);
                        }
                    }
                }

                else if (!hpUnit_from.HaveHp)
                {
                    unit_from.Kill(levUnit_from.Level, ownUnit_from.Owner);
                }

                effUnit_from.DefAllEffects();
                effUnit_to.DefAllEffects();
            }
        }
    }
}