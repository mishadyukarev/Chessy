using System;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityCellFirePool;
using static Game.Game.EntityCellRiverPool;

namespace Game.Game
{
    public struct UnitCellEC : IUnitCellE
    {
        readonly byte _idx;
        readonly DamageUnitValues _damageValues;
        readonly StepUnitValues _stepValues;

        const int MIN_STEPS = 1;
        public const int MAX_HP = 100;


        #region Properties

        #region Damage

        UnitTypes Unit
        {
            get => Unit<UnitTC>(_idx).Unit;
            set => Unit<UnitTC>(_idx).Unit = value;
        }
        LevelTypes Level
        {
            get => Unit<LevelTC>(_idx).Level;
            set => Unit<LevelTC>(_idx).Level = value;
        }
        PlayerTypes Owner
        {
            get => Unit<PlayerTC>(_idx).Player;
            set => Unit<PlayerTC>(_idx).Player = value;
        }
        public int DamageAttack(AttackTypes attack)
        {
            var tw = CellUnitTWE.UnitTW<ToolWeaponC>(_idx).ToolWeapon;
            var haveEff = Unit<HaveEffectC>(UnitStatTypes.Damage, _idx).Have;
            //var upgPerc = UnitUpgC.UpgDamagePercent(Unit, Level, Owner);


            var standDamage = _damageValues.StandDamage(Unit, Level);

            float powerDamege = standDamage;

            powerDamege += standDamage * _damageValues.PercentTW(tw);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * DamageUnitValues.UNIQUE_PERCENT_DAMAGE;

            if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage /** upgPerc*/;

            return (int)powerDamege;
        }
        public int DamageOnCell
        {
            get
            {
                var condition = Unit<ConditionUnitC>(_idx).Condition;

                var build = Build<BuildingC>(_idx).Build;

                float powerDamege = DamageAttack(AttackTypes.Simple);

                var standDamage = _damageValues.StandDamage(Unit, Level);

                powerDamege += standDamage * _damageValues.ProtRelaxPercent(condition);
                powerDamege += standDamage * _damageValues.ProtectionPercent(build);
                foreach (var item in Enviroments) if (Environment<HaveEnvironmentC>(item, _idx).Have) powerDamege += standDamage * _damageValues.ProtectionPercent(item);
                return (int)powerDamege;
            }
        }

        #endregion


        #region Hp

        public bool HaveMax => Unit<HpC>(_idx).Hp >= MAX_HP;

        #endregion


        #region Steps

        int Steps
        {
            get => Unit<StepC>(_idx).Steps;
            set => Unit<StepC>(_idx).Steps = value;
        }

        public int MaxAmountSteps => _stepValues.MaxAmountSteps(Unit, Unit<HaveEffectC>(UnitStatTypes.Steps, _idx).Have/*, UnitUpgC.Steps(Unit, Level, Owner)*/);
        public bool HaveMaxSteps => Steps >= MaxAmountSteps;
        public int StepsForDoing(in byte idx_to)
        {
            var idx_from = _idx;

            var needSteps = 1;

            if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_to).Have)
            {
                needSteps += _stepValues.NeedAmountSteps(EnvTypes.AdultForest);
                if (Trail<TrailCellEC>(idx_to).Have(CellSpaceC.GetDirect(idx_from, idx_to).Invert())) needSteps -= 1;
            }

            if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_to).Have)
                needSteps += _stepValues.NeedAmountSteps(EnvTypes.Hill);

            return needSteps;
        }
        public bool HaveStepsForDoing(in byte idx_to) => Steps >= StepsForDoing(idx_to);

        public int NeedSteps(UniqueAbilityTypes uniq)
        {
            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: return MIN_STEPS;
                case UniqueAbilityTypes.BonusNear: return MIN_STEPS;
                case UniqueAbilityTypes.FirePawn: return MIN_STEPS;
                case UniqueAbilityTypes.PutOutFirePawn: return MIN_STEPS;
                case UniqueAbilityTypes.Seed: return MIN_STEPS;
                case UniqueAbilityTypes.FireArcher: return MIN_STEPS;
                case UniqueAbilityTypes.ChangeCornerArcher: return MIN_STEPS;
                case UniqueAbilityTypes.GrowAdultForest: return MIN_STEPS;
                case UniqueAbilityTypes.StunElfemale: return MIN_STEPS;
                case UniqueAbilityTypes.ChangeDirWind: return MIN_STEPS;
                default: throw new Exception();
            }
        }
        public int NeedSteps(BuildTypes build)
        {
            return MIN_STEPS;
        }

        public bool Have(UniqueAbilityTypes uniq) => Steps >= NeedSteps(uniq);
        public bool Have(BuildTypes build) => Steps >= NeedSteps(build);
        public bool HaveMin => Steps >= MIN_STEPS;

        #endregion


        #region Else

        //public bool CanShift(in PlayerTypes whoseMove, in byte idx_to)
        //{
        //    var idx_from = _idx;

        //    if (idx_from == idx_to) return false;

        //    ref var stepUnit_from = ref Unit<UnitCellEC>(idx_from);

        //    if (!Unit<NeedStepsForExitStunC>(idx_from).IsStunned && Unit<UnitTC>(idx_from).Have && Unit<PlayerTC>(idx_from).Is(whoseMove))
        //    {
        //        CellSpaceC.TryGetIdxAround(idx_from, out var directs);

        //        foreach (var item_1 in directs)
        //        {
        //            if (idx_to == item_1.Value && !Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_to).Have && !Unit<UnitTC>(idx_to).Have)
        //            {
        //                var one = stepUnit_from.HaveStepsForDoing(idx_to);
        //                var two = stepUnit_from.HaveMaxSteps;

        //                if (one || two) return true;
        //            }
        //        }
        //        return false;
        //    }
        //    else return false;
        //}
        public bool CanAttack(in PlayerTypes whoseMove, in byte idx_to, out AttackTypes attack)
        {
            attack = AttackTypes.None;
            var idx_from = _idx;

            ref var unit_from = ref Unit<UnitTC>(idx_from);
            ref var level_from = ref Unit<LevelTC>(idx_from);
            ref var ownUnit_from = ref Unit<PlayerTC>(idx_from);
            ref var stepUnit_from = ref Unit<StepC>(idx_from);
            ref var stunUnit_from = ref Unit<NeedStepsForExitStunC>(idx_from);
            ref var corner_from = ref Unit<IsCornedArcherC>(idx_from);

            if (!stunUnit_from.IsStunned)
            {
                if (unit_from.Is(UnitTypes.King))
                {
                    if (whoseMove == ownUnit_from.Player)
                    {

                        DirectTypes curDir_1 = default;

                        CellSpaceC.TryGetIdxAround(idx_from, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            var idx_1 = item_1.Value;

                            if (idx_to != idx_1) continue;

                            curDir_1 += 1;

                            ref var unit_1 = ref Unit<UnitTC>(idx_1);
                            ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);

                            ref var trail_1 = ref Trail<TrailCellEC>(idx_1);


                            if (!Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_1).Have)
                            {
                                if (Unit<UnitCellEC>(idx_from).HaveStepsForDoing(idx_1)
                                    || Unit<UnitCellEC>(idx_from).HaveMaxSteps)
                                {
                                    if (unit_1.Have)
                                    {
                                        if (!ownUnit_1.Is(ownUnit_from.Player))
                                        {
                                            attack = AttackTypes.Simple;
                                            return true;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                else if (unit_from.Is(UnitTypes.Archer, UnitTypes.Elfemale))
                {
                    var xy_from = Cell<XyC>(idx_from).Xy;

                    for (var dir_1 = DirectTypes.First; dir_1 < DirectTypes.End; dir_1++)
                    {
                        var xy_1 = CellSpaceC.GetXyCellByDirect(xy_from, dir_1);
                        var idx_1 = IdxCell(xy_1);


                        ref var unit_1 = ref Unit<UnitTC>(idx_1);
                        ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);




                        if (Cell<IsActiveC>(idx_1).IsActive && !Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_1).Have)
                        {
                            if (unit_1.Have)
                            {
                                if (!ownUnit_1.Is(ownUnit_from.Player))
                                {
                                    if (unit_from.Is(UnitTypes.Archer))
                                    {
                                        if (corner_from.IsCornered)
                                        {
                                            if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                            {
                                                attack = AttackTypes.Unique;
                                            }
                                            else attack = AttackTypes.Simple;
                                        }
                                        else
                                        {
                                            if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                            {
                                                attack = AttackTypes.Unique;
                                            }
                                            else attack = AttackTypes.Simple;
                                        }

                                        return true;
                                    }
                                }
                            }


                            var xy_2 = CellSpaceC.GetXyCellByDirect(xy_1, dir_1);
                            var idx_2 = IdxCell(xy_2);


                            ref var unit_2 = ref Unit<UnitTC>(idx_2);
                            ref var ownUnit_2 = ref Unit<PlayerTC>(idx_2);



                            if (Cell<IsActiveC>(idx_2).IsActive && unit_2.Have
                                && Unit<IsVisibledC>(ownUnit_from.Player, idx_2).IsVisibled && !ownUnit_2.Is(ownUnit_from.Player))
                            {
                                if (unit_from.Is(UnitTypes.Archer))
                                {
                                    if (corner_from.IsCornered)
                                    {
                                        if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                        {
                                            attack = AttackTypes.Simple;
                                        }

                                        else attack = AttackTypes.Unique;
                                    }
                                    else
                                    {
                                        if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                        {
                                            attack = AttackTypes.Simple;
                                        }

                                        else attack = AttackTypes.Unique;
                                    }
                                    return true;
                                }
                                //else
                                //{
                                //    AttackCellsC.Add(AttackTypes.Simple, ownUnit_from.Owner, idx_from, idx_2);
                                //}
                            }
                        }
                    }

                }
            }

            return false;
        }
        public bool CanExtract(out int extract, out EnvTypes env, out ResTypes res)
        {
            extract = 0;
            env = EnvTypes.None;
            res = ResTypes.None;


            if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
            {
                env = EnvTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else return false;


            if (!Unit<UnitTC>(_idx).Is(UnitTypes.Pawn) || !Unit<ConditionUnitC>(_idx).Is(ConditionUnitTypes.Relaxed)
                || !Unit<UnitCellEC>(_idx).HaveMax) return false;


            var ration = 0f;

            switch (Unit<LevelTC>(_idx).Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            var envResC = Environment<EnvCellEC>(env, _idx);

            extract = (int)(envResC.Max() * ration);

            if (extract > Environment<AmountResourcesC>(env, _idx).Resources) extract = Environment<AmountResourcesC>(env, _idx).Resources;

            return true;
        }
        public bool CanResume(out int resume, out EnvTypes env)
        {
            resume = 0;
            env = EnvTypes.None;

            var twC = CellUnitTWE.UnitTW<ToolWeaponC>(_idx);

            if (Build<BuildingC>(_idx).Have || !Unit<ConditionUnitC>(_idx).Is(ConditionUnitTypes.Relaxed) || !Unit<UnitCellEC>(_idx).HaveMax) return false;



            var ration = 0f;

            switch (Unit<UnitTC>(_idx).Unit)
            {
                case UnitTypes.Pawn:
                    if (!Environment<HaveEnvironmentC>(EnvTypes.Hill, _idx).Have && !twC.Is(TWTypes.Pick)) return false;

                    env = EnvTypes.Hill;

                    switch (Unit<LevelTC>(_idx).Level)
                    {
                        case LevelTypes.First: ration = 0.3f; break;
                        case LevelTypes.Second: ration = 0.6f; break;
                        default: throw new Exception();
                    }
                    break;

                case UnitTypes.Elfemale:
                    ration = 0.3f;
                    env = EnvTypes.AdultForest;
                    break;

                default: return false;
            }



            resume = (int)(Environment<EnvCellEC>(env, _idx).Max() * ration);

            if (resume > Environment<AmountResourcesC>(env, _idx).Resources) resume = Environment<AmountResourcesC>(env, _idx).Resources;

            return true;
        }
        public bool CanArson(in PlayerTypes whoseMove, in byte idx_to)
        {
            var idx_from = _idx;

            ref var unit_from = ref Unit<UnitTC>(idx_from);
            ref var ownUnit_from = ref Unit<PlayerTC>(idx_from);
            ref var stun_from = ref Unit<NeedStepsForExitStunC>(idx_from);

            if (!stun_from.IsStunned)
            {
                if (unit_from.Is(UnitTypes.Archer))
                {
                    foreach (var idx_1 in CellSpaceC.IdxAround(idx_from))
                    {
                        if (idx_to != idx_1) continue;
                        ref var fire_1 = ref Fire<HaveEffectC>(idx_1);

                        if (!fire_1.Have)
                        {
                            if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        #endregion


        #region Water

        //float UpgadeWaterPercent => UnitUpgC.UpgWaterPercent(Unit, Level, Owner);

        public bool IsHpDeathAfterAttack => Unit<HpC>(_idx).Hp <= DamageUnitValues.HP_FOR_DEATH_AFTER_ATTACK;

        public bool NeedWater => Unit<WaterC>(_idx).Water <= 100 * 0.4f;
        public int MaxWater => (int)(100 + 100 /** UpgadeWaterPercent*/);
        public bool HaveMaxWater => Unit<WaterC>(_idx).Water >= MaxWater;


        #endregion

        #endregion


        internal UnitCellEC(in byte idx)
        {
            _idx = idx;
            _damageValues = new DamageUnitValues();
        }


        #region Methods

        #region Hp

        public void SetMaxHp() => Unit<HpC>(_idx).Hp = 100;
        public void Take(in UniqueAbilityTypes uniq)
        {
            var damage = 0;

            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: damage = 25; break;
                default: throw new Exception();
            }

            Unit<HpC>(_idx).Take(damage);
        }
        public void TakeAttack(in int damage)
        {
            Unit<HpC>(_idx).Take(damage);
            if (IsHpDeathAfterAttack) Unit<HpC>(_idx).SetMinHp();
        }
        public void TakeFire()
        {
            Unit<HpC>(_idx).Take(40);


            if (!Unit<HpC>(_idx).Have)
            {
                Unit<UnitCellEC>(_idx).Kill();
            }
        }

        #endregion


        #region Steps

        public void SetMaxSteps() => Unit<StepC>(_idx).Steps = MaxAmountSteps;
        public void Reset() => Unit<StepC>(_idx).Reset();

        public void TakeStepsForDoing(in byte idx_to) => Unit<StepC>(_idx).Take(StepsForDoing(idx_to));
        public void TakeForBuild() => Unit<StepC>(_idx).Take();
        public void Take(UniqueAbilityTypes uniq) => Unit<StepC>(_idx).Take(NeedSteps(uniq));
        public void Take(BuildTypes build) => Unit<StepC>(_idx).Take(NeedSteps(build));
        public void TakeMin() => Unit<StepC>(_idx).Take(MIN_STEPS);

        #endregion


        #region Water

        public void SetMaxWater() => Unit<WaterC>(_idx).Water = MaxWater;
        public void ExecuteThirsty()
        {
            float percent = 0;
            switch (Unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: percent = 0.4f; break;
                case UnitTypes.Pawn: percent = 0.5f; break;
                case UnitTypes.Archer: percent = 0.5f; break;
                case UnitTypes.Scout: percent = 0.5f; break;
                case UnitTypes.Elfemale: percent = 0.5f; break;
                default: throw new Exception();
            }

            Unit<HpC>(_idx).Take((int)(UnitCellEC.MAX_HP * percent));
        }
        public void TakeWater() => Unit<WaterC>(_idx).Take((int)(100 * 0.15f));

        #endregion


        #region Else

        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes) unit)
        {
            if (unit.Item1 == UnitTypes.None) throw new Exception();
            if (Unit<UnitTC>(_idx).Have) throw new Exception("It's got unit");

            Unit<UnitTC>(_idx).Unit = unit.Item1;
            Unit<LevelTC>(_idx).Level = unit.Item2;
            Unit<PlayerTC>(_idx).Player = unit.Item3;

            SetMaxHp();
            SetMaxWater();
            SetMaxSteps();

            foreach (var item in KeysStat) Unit<HaveEffectC>(item, _idx).Disable();
            Unit<ConditionUnitC>(_idx).Reset();
            foreach (var item in KeysCondition) AmountStepsInCondition<StepC>(item, _idx).Reset();

            CellUnitTWE.UnitTW<UnitTWCellEC>(_idx).Reset();

            EntWhereUnits.HaveUnit<HaveUnitC>(unit, _idx).Have = true;
        }
        public void Kill()
        {
            ref var unit = ref Unit<UnitTC>(_idx);
            ref var ownUnit = ref Unit<PlayerTC>(_idx);
            ref var levUnit = ref Unit<LevelTC>(_idx);

            if (!unit.Have) throw new Exception("It's not got unit");

            if (unit.Is(UnitTypes.King))
            {
                EntityPool.Winner<PlayerTC>().Player = ownUnit.Player;
            }
            else if (unit.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                EntityPool.ScoutHeroCooldown<CooldownC>(unit.Unit, ownUnit.Player).Cooldown = 3;
                EntInventorUnits.Units<AmountC>(unit.Unit, levUnit.Level, ownUnit.Player).Amount += 1;
            }


            EntWhereUnits.HaveUnit<HaveUnitC>(unit.Unit, levUnit.Level, ownUnit.Player, _idx).Have = false;
            unit.Reset();
        }
        public void Shift(in byte idx_to)
        {
            var idx_from = _idx;

            var dir = CellSpaceC.GetDirect(Cell<XyC>(idx_from).Xy, Cell<XyC>(idx_to).Xy);


            Trail<TrailCellEC>(idx_to).TrySetNewTrail(dir.Invert(), Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_to).Have);
            Trail<TrailCellEC>(idx_from).TrySetNewTrail(dir, Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_from).Have);

            ref var unit_from = ref Unit<UnitTC>(idx_from);
            ref var level_from = ref Unit<LevelTC>(idx_from);
            ref var own_from = ref Unit<PlayerTC>(idx_from);


            Unit<PlayerTC>(idx_to).Set(Unit<PlayerTC>(idx_from));
            Unit<LevelTC>(idx_to).Set(Unit<LevelTC>(idx_from));


            Unit<HpC>(idx_to).Set(Unit<HpC>(idx_from));
            Unit<StepC>(idx_to).Set(Unit<StepC>(idx_from));
            if (Unit<ConditionUnitC>(idx_to).HaveCondition) Unit<ConditionUnitC>(idx_to).Reset();

            CellUnitTWE.UnitTW<UnitTWCellEC>(idx_to).Set(idx_from);
            CellUnitTWE.UnitTW<LevelTC>(idx_to).Set(CellUnitTWE.UnitTW<LevelTC>(idx_from));
            CellUnitTWE.UnitTW<ProtectionC>(idx_to).Set(CellUnitTWE.UnitTW<ProtectionC>(idx_from));

            foreach (var item in KeysStat) Unit<HaveEffectC>(item, idx_to).Have = Unit<HaveEffectC>(item, idx_from).Have;
            Unit<WaterC>(idx_to).Set(Unit<WaterC>(idx_from));
            foreach (var item in KeysCondition) AmountStepsInCondition<StepC>(item, idx_to).Reset();
            foreach (var unique in KeysUnique) Unit<CooldownC>(unique, idx_to).Cooldown = Unit<CooldownC>(unique, idx_from).Cooldown;
            Unit<IsCornedArcherC>(idx_to).Set(Unit<IsCornedArcherC>(idx_from));



            if (Build<BuildingC>(idx_to).Is(BuildTypes.Camp))
            {
                if (!Build<PlayerTC>(idx_to).Is(Unit<PlayerTC>(idx_to).Player))
                {
                    Build<BuildCellEC>(idx_to).Remove();
                }
            }


            Unit<UnitTC>(idx_to).Unit = Unit<UnitTC>(idx_from).Unit;
            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(idx_to).Unit, level_from.Level, own_from.Player, idx_to).Have = true;

            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(idx_from).Unit, level_from.Level, own_from.Player, idx_from).Have = false;
            Unit<UnitTC>(idx_from).Reset();


            if (River<RiverC>(idx_to).HaveNearRiver) SetMaxWater();
        }
        public void AddToInventor()
        {
            var level = Unit<LevelTC>(_idx).Level;
            var owner = Unit<PlayerTC>(_idx).Player;

            EntInventorUnits.Units<AmountC>(Unit<UnitTC>(_idx).Unit, level, owner).Amount += 1;

            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(_idx).Unit, level, owner, _idx).Have = false;
            Unit<UnitTC>(_idx).Reset();
        }
        public void Upgrade()
        {
            ref var levC = ref Unit<LevelTC>(_idx);

            if (levC.Is(LevelTypes.First))
            {
                levC.Level = LevelTypes.Second;
            }
            else throw new Exception();
        }
        public void SetScout()
        {
            ref var ownUnitC = ref Unit<PlayerTC>(_idx);

            ref var twUnitC = ref CellUnitTWE.UnitTW<UnitTWCellEC>(_idx);
            ref var twC = ref CellUnitTWE.UnitTW<ToolWeaponC>(_idx);
            ref var levTWC = ref CellUnitTWE.UnitTW<LevelTC>(_idx);


            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(_idx).Unit, Unit<LevelTC>(_idx).Level, ownUnitC.Player, _idx).Have = false;

            Unit<UnitTC>(_idx).Unit = UnitTypes.Scout;
            Unit<LevelTC>(_idx).Level = LevelTypes.First;
            if (twC.HaveTW)
            {
                EntInventorToolWeapon.ToolWeapons<AmountC>(twC.ToolWeapon, levTWC.Level, ownUnitC.Player).Amount += 1;
                twUnitC.Reset();
            }

            EntWhereUnits.HaveUnit<HaveUnitC>(UnitTypes.Scout, LevelTypes.First, Unit<PlayerTC>(_idx).Player, _idx).Have = true;
        }
        public void SetHero(in byte idx_from, in UnitTypes unit, in LevelTypes lev)
        {
            var idx_to = _idx;

            EntWhereUnits.HaveUnit<HaveUnitC>(UnitTypes.Archer, Unit<LevelTC>(idx_from).Level, Unit<PlayerTC>(idx_from).Player, idx_from).Have = false;
            Unit<UnitTC>(idx_from).Reset();

            EntWhereUnits.HaveUnit<HaveUnitC>(UnitTypes.Archer, Unit<LevelTC>(idx_to).Level, Unit<PlayerTC>(idx_to).Player, idx_to).Have = false;
            Unit<UnitTC>(idx_to).Reset();


            Unit<UnitTC>(idx_to).Unit = unit;
            Unit<LevelTC>(idx_to).Level = lev;

            EntWhereUnits.HaveUnit<HaveUnitC>(unit, lev, Unit<PlayerTC>(idx_to).Player, idx_to).Have = true;


            EntInventorUnits.Units<AmountC>(unit, lev,  Unit<PlayerTC>(idx_to).Player).Amount -= 1;
        }

        public void Sync(in UnitTypes unit, in LevelTypes lev, in PlayerTypes owner, in int hp, in int steps, in int water)
        {
            Unit<UnitTC>(_idx).Unit = unit;
            Unit<LevelTC>(_idx).Level = lev;
            Unit<PlayerTC>(_idx).Player = owner;
            Unit<HpC>(_idx).Hp = hp;
            Unit<StepC>(_idx).Steps = steps;
            Unit<WaterC>(_idx).Water = water;
        }

        #endregion

        #endregion
    }
}