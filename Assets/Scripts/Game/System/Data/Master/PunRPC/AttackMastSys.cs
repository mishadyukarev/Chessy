using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class AttackMastSys : IEcsRunSystem
    {
        private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, DamageC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, WaterUnitC> _cellUnitEffFilt = default;

        private EcsFilter<XyCellComponent> _cellXyFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;
        private EcsFilter<CellTrailDataC> _cellTrainFilt = default;

        public void Run()
        {
            ref var forAttackMasCom = ref _forAttackFilter.Get1(0);



            var idx_from = forAttackMasCom.IdxFromCell;
            var idx_to = forAttackMasCom.IdxToCell;


            ref var unit_from = ref _cellUnitFilter.Get1(idx_from);
            ref var levUnit_from = ref _cellUnitMainFilt.Get2(idx_from);
            ref var ownUnit_from = ref _cellUnitMainFilt.Get3(idx_from);
            ref var hpUnitC_from = ref _cellUnitFilter.Get2(idx_from);
            ref var fromDamUnitC = ref _cellUnitFilter.Get3(idx_from);
            ref var stepUnitC_from = ref _cellUnitFilter.Get4(idx_from);
            ref var condUnitC_from = ref _cellUnitEffFilt.Get2(idx_from);
            ref var twUnitC_from = ref _cellUnitEffFilt.Get3(idx_from);
            ref var effUnit_from = ref _cellUnitEffFilt.Get4(idx_from);
            ref var thirUnitC_from = ref _cellUnitEffFilt.Get5(idx_from);

            ref var riverC_from = ref _cellRiverFilt.Get1(idx_from);
            ref var build_from = ref _cellBuildFilter.Get1(idx_from);
            ref var ownBuild_from = ref _cellBuildFilter.Get2(idx_from);


            ref var unit_to = ref _cellUnitFilter.Get1(idx_to);
            ref var levUnitC_to = ref _cellUnitMainFilt.Get2(idx_to);
            ref var ownUnit_to = ref _cellUnitMainFilt.Get3(idx_to);
            ref var hpUnitC_to = ref _cellUnitFilter.Get2(idx_to);
            ref var toDamUnitC =ref _cellUnitFilter.Get3(idx_to);
            ref var stepUnitC_to = ref _cellUnitFilter.Get4(idx_to);
            ref var condUnitC_to = ref _cellUnitEffFilt.Get2(idx_to);
            ref var twUnitC_to = ref _cellUnitEffFilt.Get3(idx_to);
            ref var effUnit_to = ref _cellUnitEffFilt.Get4(idx_to);
            ref var thirUnitC_to = ref _cellUnitEffFilt.Get5(idx_to);


            ref var riverC_to = ref _cellRiverFilt.Get1(idx_to);
            ref var build_to = ref _cellBuildFilter.Get1(idx_to);
            ref var env_to = ref _cellEnvFilter.Get1(idx_to);
            ref var trail_to = ref _cellTrainFilt.Get1(idx_to);



            var simpUniqueType = CellsAttackC.FindByIdx(ownUnit_from.Owner, idx_from, idx_to);

            if (simpUniqueType != default)
            {
                stepUnitC_from.ZeroSteps();
                condUnitC_from.DefCondition();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += fromDamUnitC.DamageAttack(unit_from.Unit, levUnit_from.Level, twUnitC_from, effUnit_from, simpUniqueType, UnitsUpgC.UpgPercent(ownUnit_from.Owner, unit_from.Unit, UnitStatTypes.Damage));

                if (unit_from.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.AttackArcher);



                powerDam_to += toDamUnitC.DamageOnCell(unit_to.Unit, levUnitC_to.Level, condUnitC_to, twUnitC_to, effUnit_to, UnitsUpgC.UpgPercent(ownUnit_to.Owner, unit_to.Unit, UnitStatTypes.Damage), build_to.BuildType, env_to.Envronments);
                


                //var pawnUpg_from = UnitsUpgC.UpgPercent(ownUnit_from.Owner, unit_from.Unit, UnitStatTypes.Hp);
                //var maxHp_from = HpUnitC.MAX_HP;

                //var pawnUpg_to = UnitsUpgC.UpgPercent(ownUnit_to.Owner, unit_to.Unit, UnitStatTypes.Hp);
                //var maxHp_to = HpUnitC.MAX_HP;


                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = 100;
                var minDamage = 0;

                if (!unit_to.IsMelee) powerDam_to /= 2;

                if (powerDam_to > powerDam_from)
                {
                    max_limit = powerDam_to * 2f;
                    min_limit = powerDam_to / 2f;

                    if (min_limit > powerDam_from)
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
                    min_limit = powerDam_from / 2;

                    if (min_limit > powerDam_to)
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
                    if (twUnitC_from.Is(ToolWeaponTypes.Shield))
                    {
                        twUnitC_from.TakeShieldProtect();
                    }
                    else if (minus_from > 0)
                    {
                        hpUnitC_from.TakeHp((int)minus_from);
                        if (hpUnitC_from.IsHpDeathAfterAttack) hpUnitC_from.SetMinHp();
                    }
                }


                if (twUnitC_to.Is(ToolWeaponTypes.Shield))
                {
                    twUnitC_to.TakeShieldProtect();
                }
                else if (minus_to > 0)
                {
                    hpUnitC_to.TakeHp((int)minus_to);
                    if (hpUnitC_to.IsHpDeathAfterAttack) hpUnitC_to.SetMinHp();
                }



                if (!hpUnitC_to.HaveHp)
                {
                    if (unit_to.Is(UnitTypes.King))
                    {
                        EndGameDataUIC.PlayerWinner = ownUnit_from.Owner;
                    }
                    else if (unit_to.Is(UnitTypes.Scout))
                    {
                        InvUnitsC.AddUnit(ownUnit_to.Owner, unit_to.Unit, LevelUnitTypes.Wood);
                    }

                    WhereUnitsC.Remove(ownUnit_to.Owner, unit_to.Unit, levUnitC_to.Level, idx_to);
                    unit_to.NoneUnit();


                    if (unit_from.IsMelee)
                    {
                        if (!hpUnitC_from.HaveHp)
                        {
                            WhereUnitsC.Remove(ownUnit_from.Owner, unit_from.Unit, levUnit_from.Level, idx_from);
                            unit_from.NoneUnit();
                        }
                        else
                        {
                            unit_to.SetUnit(unit_from.Unit);
                            levUnitC_to.SetLevel(levUnit_from.Level);
                            hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                            stepUnitC_to.StepsAmount = stepUnitC_from.StepsAmount;
                            condUnitC_to.CondUnitType = condUnitC_from.CondUnitType;
                            twUnitC_to.Set(twUnitC_from);
                            ownUnit_to.SetOwner(ownUnit_from.Owner);
                            if (riverC_to.HaveNearRiver) thirUnitC_to.SetMaxWater(UnitsUpgC.UpgPercent(ownUnit_to.Owner, unit_to.Unit, UnitStatTypes.Water));
                            WhereUnitsC.Add(ownUnit_to.Owner, unit_to.Unit, levUnitC_to.Level, idx_to);

                            var dir = CellSpaceSupport.GetDirect(_cellXyFilt.Get1(idx_from).XyCell, _cellXyFilt.Get1(idx_to).XyCell);
                            trail_to.TrySetNewTrain(dir.Invert(), env_to);


                            if (build_from.Is(BuildTypes.Camp))
                            {
                                WhereBuildsC.Remove(ownBuild_from.Owner, build_from.BuildType, idx_from);
                                build_from.Reset();
                            }


                            WhereUnitsC.Remove(ownUnit_from.Owner, unit_from.Unit, levUnit_from.Level, idx_from);
                            unit_from.NoneUnit();
                        }
                    }
                }

                else if (!hpUnitC_from.HaveHp)
                {
                    if (unit_from.Is(UnitTypes.King))
                    {
                        EndGameDataUIC.PlayerWinner = ownUnit_to.Owner;
                    }

                    if (build_from.Is(BuildTypes.Camp))
                    {
                        WhereBuildsC.Remove(ownBuild_from.Owner, build_from.BuildType, idx_from);
                        build_from.Reset();
                    }

                    WhereUnitsC.Remove(ownUnit_from.Owner, unit_from.Unit, levUnit_from.Level, idx_from);
                    unit_from.NoneUnit();
                }

                //if(unit_to.HaveUnit) hpUnitC_to.TryTakeBonusHp(unit_to.Unit, effUnit_to.Have(UnitStatTypes.Hp));
                //if (unit_from.HaveUnit) hpUnitC_from.TryTakeBonusHp(unit_from.Unit, effUnit_from.Have(UnitStatTypes.Hp));

                effUnit_from.DefAllEffects();
                effUnit_to.DefAllEffects();
            }
        }
    }
}