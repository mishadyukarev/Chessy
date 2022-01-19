using ECS;
using System;

namespace Game.Game
{
    public readonly struct CellUnitEs
    {
        static Entity[] _units;
        public static ref C Unit<C>(in byte idx) where C : struct, IUnitCellE => ref _units[idx].Get<C>();


        public static bool CanExtract(in byte idx, out int extract, out EnvironmentTypes env, out ResourceTypes res)
        {
            extract = 0;
            env = EnvironmentTypes.None;
            res = ResourceTypes.None;


            if (CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.AdultForest, idx).Have)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResourceTypes.Wood;
            }
            else return false;


            if (!Unit<UnitTC>(idx).Is(UnitTypes.Pawn) || !Unit<ConditionUnitC>(idx).Is(ConditionUnitTypes.Relaxed)
                || !CellUnitHpEs.HaveMax(idx)) return false;


            var ration = 0f;

            switch (Unit<LevelTC>(idx).Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            extract = (int)(EnvironmentValues.MaxAmount(env) * ration);

            if (extract > CellEnvironmentEs.Environment<AmountC>(env, idx).Amount) extract = CellEnvironmentEs.Environment<AmountC>(env, idx).Amount;

            return true;
        }
        public static bool CanResume(in byte idx, out int resume, out EnvironmentTypes env)
        {
            resume = 0;
            env = EnvironmentTypes.None;

            var twC = CellUnitTWE.UnitTW<ToolWeaponC>(idx);

            if (CellBuildE.Build<BuildingTC>(idx).Have || !Unit<ConditionUnitC>(idx).Is(ConditionUnitTypes.Relaxed) || !CellUnitHpEs.HaveMax(idx)) return false;



            var ration = 0f;

            switch (Unit<UnitTC>(idx).Unit)
            {
                case UnitTypes.Pawn:
                    if (!CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.Hill, idx).Have && !twC.Is(ToolWeaponTypes.Pick)) return false;

                    env = EnvironmentTypes.Hill;

                    switch (Unit<LevelTC>(idx).Level)
                    {
                        case LevelTypes.First: ration = 0.3f; break;
                        case LevelTypes.Second: ration = 0.6f; break;
                        default: throw new Exception();
                    }
                    break;

                case UnitTypes.Elfemale:
                    ration = 0.3f;
                    env = EnvironmentTypes.AdultForest;
                    break;

                default: return false;
            }



            resume = (int)(EnvironmentValues.MaxAmount(env) * ration);

            if (resume > CellEnvironmentEs.Environment<AmountC>(env, idx).Amount)
                resume = CellEnvironmentEs.Environment<AmountC>(env, idx).Amount;

            return true;
        }
        public static int DamageAttack(in byte idx, in AttackTypes attack)
        {
            var tw = CellUnitTWE.UnitTW<ToolWeaponC>(idx).ToolWeapon;
            var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            var standDamage = DamageUnitValues.StandDamage(Unit<UnitTC>(idx).Unit, Unit<LevelTC>(idx).Level);

            if (UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>
                (UnitStatTypes.Damage, Unit<UnitTC>(idx).Unit, Unit<LevelTC>(idx).Level, Unit<PlayerTC>(idx).Player, UpgradeTypes.PickCenter).Have)
            {
                upgPerc = 0.3f;
            }



            float powerDamege = standDamage;

            powerDamege += standDamage * DamageUnitValues.PercentTW(tw);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * DamageUnitValues.UNIQUE_PERCENT_DAMAGE;

            if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public static int DamageOnCell(in byte idx)
        {
            var condition = Unit<ConditionUnitC>(idx).Condition;

            var build = CellBuildE.Build<BuildingTC>(idx).Build;

            float powerDamege = DamageAttack(idx, AttackTypes.Simple);

            var standDamage = DamageUnitValues.StandDamage(Unit<UnitTC>(idx).Unit, Unit<LevelTC>(idx).Level);

            powerDamege += standDamage * DamageUnitValues.ProtRelaxPercent(condition);
            powerDamege += standDamage * DamageUnitValues.ProtectionPercent(build);
            foreach (var item in CellEnvironmentEs.Enviroments) if (CellEnvironmentEs.Environment<HaveEnvironmentC>(item, idx).Have) powerDamege += standDamage * DamageUnitValues.ProtectionPercent(item);
            return (int)powerDamege;
        }


        public CellUnitEs(in EcsWorld gameW)
        {
            _units = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _units.Length; idx++)
            {
                _units[idx] = gameW.NewEntity()
                    .Add(new UnitTC())
                    .Add(new LevelTC())
                    .Add(new PlayerTC())
                    .Add(new ConditionUnitC())
                    .Add(new IsCornedArcherC());
            }
        }

        public static void Shift(in byte idx_from, in byte idx_to)
        {
            var dir = CellSpaceC.GetDirect(CellEs.Cell<XyC>(idx_from).Xy, CellEs.Cell<XyC>(idx_to).Xy);


            CellTrailEs.TrySetNewTrail(idx_to, dir.Invert(), CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.AdultForest, idx_to).Have);
            CellTrailEs.TrySetNewTrail(idx_from, dir, CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.AdultForest, idx_from).Have);

            ref var unit_from = ref Unit<UnitTC>(idx_from);
            ref var level_from = ref Unit<LevelTC>(idx_from);
            ref var own_from = ref Unit<PlayerTC>(idx_from);


            Unit<PlayerTC>(idx_to).Set(Unit<PlayerTC>(idx_from));
            Unit<LevelTC>(idx_to).Set(Unit<LevelTC>(idx_from));


            CellUnitHpEs.Hp<AmountC>(idx_to).Set(CellUnitHpEs.Hp<AmountC>(idx_from));
            CellUnitStepEs.Steps<AmountC>(idx_to).Set(CellUnitStepEs.Steps<AmountC>(idx_from));
            if (Unit<ConditionUnitC>(idx_to).HaveCondition) Unit<ConditionUnitC>(idx_to).Reset();

            CellUnitTWE.Set(idx_from, idx_to);
            CellUnitTWE.UnitTW<LevelTC>(idx_to).Set(CellUnitTWE.UnitTW<LevelTC>(idx_from));
            CellUnitTWE.UnitTW<ProtectionC>(idx_to).Set(CellUnitTWE.UnitTW<ProtectionC>(idx_from));

            foreach (var item in CellUnitEffectsEs.KeysStat)
                CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Have = CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Have;
            CellUnitWaterEs.Water<AmountC>(idx_to).Set(CellUnitWaterEs.Water<AmountC>(idx_from));
            foreach (var item in CellUnitStepsInConditionEs.KeysCondition) CellUnitStepsInConditionEs.Steps<AmountC>(item, idx_to).Reset();
            foreach (var unique in CellUnitAbilityUniqueEs.Keys) CellUnitAbilityUniqueEs.Cooldown<CooldownC>(unique, idx_to).Cooldown = CellUnitAbilityUniqueEs.Cooldown<CooldownC>(unique, idx_from).Cooldown;
            Unit<IsCornedArcherC>(idx_to).Set(Unit<IsCornedArcherC>(idx_from));



            if (CellBuildE.Build<BuildingTC>(idx_to).Is(BuildingTypes.Camp))
            {
                if (!CellBuildE.Build<PlayerTC>(idx_to).Is(Unit<PlayerTC>(idx_to).Player))
                {
                    CellBuildE.Remove(idx_to);
                }
            }


            Unit<UnitTC>(idx_to).Unit = Unit<UnitTC>(idx_from).Unit;
            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(idx_to).Unit, level_from.Level, own_from.Player, idx_to).Have = true;

            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(idx_from).Unit, level_from.Level, own_from.Player, idx_from).Have = false;
            Unit<UnitTC>(idx_from).Reset();

            if (EntityCellRiverPool.River<RiverC>(idx_to).HaveNearRiver) CellUnitWaterEs.Water<AmountC>(idx_to).Amount = 100;
        }
        public static void Kill(in byte idx)
        {
            ref var unit = ref Unit<UnitTC>(idx);
            ref var ownUnit = ref Unit<PlayerTC>(idx);
            ref var levUnit = ref Unit<LevelTC>(idx);

            if (!unit.Have) throw new Exception("It's not got unit");

            if (unit.Is(UnitTypes.King))
            {
                EntityPool.Winner<PlayerTC>().Player = ownUnit.Player;
            }
            else if (unit.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                EntityPool.ScoutHeroCooldown<CooldownC>(unit.Unit, ownUnit.Player).Cooldown = 3;
                InventorUnitsE.Units<AmountC>(unit.Unit, levUnit.Level, ownUnit.Player).Amount += 1;
            }


            EntWhereUnits.HaveUnit<HaveUnitC>(unit.Unit, levUnit.Level, ownUnit.Player, idx).Have = false;
            unit.Reset();
        }
        public static void SetNew(in byte idx, in (UnitTypes, LevelTypes, PlayerTypes) unit)
        {
            if (unit.Item1 == UnitTypes.None) throw new Exception();
            if (Unit<UnitTC>(idx).Have) throw new Exception("It's got unit");

            Unit<UnitTC>(idx).Unit = unit.Item1;
            Unit<LevelTC>(idx).Level = unit.Item2;
            Unit<PlayerTC>(idx).Player = unit.Item3;

            CellUnitHpEs.SetMaxHp(idx);
            CellUnitWaterEs.SetMaxWater(idx);
            CellUnitStepEs.SetMaxSteps(idx);

            foreach (var item in CellUnitEffectsEs.KeysStat) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx).Disable();
            Unit<ConditionUnitC>(idx).Reset();
            foreach (var item in CellUnitStepsInConditionEs.KeysCondition) CellUnitStepsInConditionEs.Steps<AmountC>(item, idx).Reset();

            CellUnitTWE.Reset(idx);

            EntWhereUnits.HaveUnit<HaveUnitC>(unit, idx).Have = true;
        }
        public static void AddToInventor(in byte idx)
        {
            var level = Unit<LevelTC>(idx).Level;
            var owner = Unit<PlayerTC>(idx).Player;

            InventorUnitsE.Units<AmountC>(Unit<UnitTC>(idx).Unit, level, owner).Amount += 1;

            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(idx).Unit, level, owner, idx).Have = false;
            Unit<UnitTC>(idx).Reset();
        }
        public static void SetScout(in byte idx)
        {
            ref var ownUnitC = ref Unit<PlayerTC>(idx);

            ref var twC = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx);
            ref var levTWC = ref CellUnitTWE.UnitTW<LevelTC>(idx);


            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(idx).Unit, Unit<LevelTC>(idx).Level, ownUnitC.Player, idx).Have = false;

            Unit<UnitTC>(idx).Unit = UnitTypes.Scout;
            Unit<LevelTC>(idx).Level = LevelTypes.First;
            if (twC.HaveTW)
            {
                InventorToolWeaponE.ToolWeapons<AmountC>(twC.ToolWeapon, levTWC.Level, ownUnitC.Player).Amount += 1;
                CellUnitTWE.Reset(idx);
            }

            EntWhereUnits.HaveUnit<HaveUnitC>(UnitTypes.Scout, LevelTypes.First, Unit<PlayerTC>(idx).Player, idx).Have = true;
        }
    }
}