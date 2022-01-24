using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitEs
    {
        static Entity[] Ents;

        public static ref UnitTC Unit(in byte idx) => ref Ents[idx].Get<UnitTC>();


        public static bool CanExtract(in byte idx, out int extract, out EnvironmentTypes env, out ResourceTypes res)
        {
            extract = 0;
            env = EnvironmentTypes.None;
            res = ResourceTypes.None;


            if (CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx).Have)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResourceTypes.Wood;
            }
            else return false;


            if (!Unit(idx).Is(UnitTypes.Pawn) || !EntitiesPool.UnitElse.Condition(idx).Is(ConditionUnitTypes.Relaxed)
                || !EntitiesPool.UnitHps[idx].HaveMax) return false;


            var ration = 0f;

            switch (EntitiesPool.UnitElse.Level(idx).Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            extract = (int)(CellEnvironmentValues.MaxResources(env) * ration);

            if (extract > CellEnvironmentEs.Resources(env, idx).Amount) extract = CellEnvironmentEs.Resources(env, idx).Amount;

            return true;
        }
        public static bool CanResume(in byte idx, out int resume, out EnvironmentTypes env)
        {
            resume = 0;
            env = EnvironmentTypes.None;

            var twC = CellUnitTWE.UnitTW<ToolWeaponC>(idx);

            if (CellBuildE.Build<BuildingTC>(idx).Have || !EntitiesPool.UnitElse.Condition(idx).Is(ConditionUnitTypes.Relaxed) || !EntitiesPool.UnitHps[idx].HaveMax) return false;



            var ration = 0f;

            switch (Unit(idx).Unit)
            {
                case UnitTypes.Pawn:
                    if (!CellEnvironmentEs.Resources(EnvironmentTypes.Hill, idx).Have && !twC.Is(ToolWeaponTypes.Pick)) return false;

                    env = EnvironmentTypes.Hill;

                    switch (EntitiesPool.UnitElse.Level(idx).Level)
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



            resume = (int)(CellEnvironmentValues.MaxResources(env) * ration);

            if (resume > CellEnvironmentEs.Resources(env, idx).Amount)
                resume = CellEnvironmentEs.Resources(env, idx).Amount;

            return true;
        }
        public static int DamageAttack(in byte idx, in AttackTypes attack)
        {
            var tw = CellUnitTWE.UnitTW<ToolWeaponC>(idx).ToolWeapon;
            var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            var standDamage = UnitDamageValues.StandDamage(Unit(idx).Unit, EntitiesPool.UnitElse.Level(idx).Level);


            if (!Unit(idx).IsAnimal)
                if (UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Damage, Unit(idx).Unit, EntitiesPool.UnitElse.Level(idx).Level, EntitiesPool.UnitElse.Owner(idx).Player, UpgradeTypes.PickCenter).Have)
                {
                    upgPerc = 0.3f;
                }



            float powerDamege = standDamage;

            powerDamege += standDamage * UnitDamageValues.PercentTW(tw);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitDamageValues.UNIQUE_PERCENT_DAMAGE;

            if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public static int DamageOnCell(in byte idx)
        {
            var condition = EntitiesPool.UnitElse.Condition(idx).Condition;

            var build = CellBuildE.Build<BuildingTC>(idx).Build;

            float powerDamege = DamageAttack(idx, AttackTypes.Simple);

            var standDamage = UnitDamageValues.StandDamage(Unit(idx).Unit, EntitiesPool.UnitElse.Level(idx).Level);

            powerDamege += standDamage * UnitDamageValues.ProtRelaxPercent(condition);
            powerDamege += standDamage * UnitDamageValues.ProtectionPercent(build);
            foreach (var item in CellEnvironmentEs.Keys) if (CellEnvironmentEs.Resources(item, idx).Have) powerDamege += standDamage * UnitDamageValues.ProtectionPercent(item);
            return (int)powerDamege;
        }


        public CellUnitEs(in EcsWorld gameW)
        {
            Ents = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            for (byte idx = 0; idx < Ents.Length; idx++)
            {
                Ents[idx] = gameW.NewEntity()
                    .Add(new UnitTC());
            }
        }

        public static void Shift(in byte idx_from, in byte idx_to, in bool withTrail)
        {
            var dir = CellSpaceSupport.GetDirect(CellEs.Cell<XyC>(idx_from).Xy, CellEs.Cell<XyC>(idx_to).Xy);

            if (withTrail)
            {
                CellTrailEs.TrySetNewTrail(idx_to, dir.Invert(), CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx_to).Have);
                CellTrailEs.TrySetNewTrail(idx_from, dir, CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx_from).Have);
            }

            ref var unit_from = ref Unit(idx_from);
            ref var level_from = ref EntitiesPool.UnitElse.Level(idx_from);
            ref var own_from = ref EntitiesPool.UnitElse.Owner(idx_from);


            EntitiesPool.UnitElse.Owner(idx_to) = EntitiesPool.UnitElse.Owner(idx_from);
            EntitiesPool.UnitElse.Level(idx_to) = EntitiesPool.UnitElse.Level(idx_from);


            EntitiesPool.UnitHps[idx_to].Hp = EntitiesPool.UnitHps[idx_from].Hp;
            EntitiesPool.UnitStep.Steps(idx_to) = EntitiesPool.UnitStep.Steps(idx_from);
            if (EntitiesPool.UnitElse.Condition(idx_to).HaveCondition) EntitiesPool.UnitElse.Condition(idx_to).Reset();

            CellUnitTWE.Set(idx_from, idx_to);
            CellUnitTWE.UnitTW<LevelTC>(idx_to) = CellUnitTWE.UnitTW<LevelTC>(idx_from);
            CellUnitTWE.UnitTW<ProtectionC>(idx_to) = CellUnitTWE.UnitTW<ProtectionC>(idx_from);

            foreach (var item in CellUnitEffectsEs.Keys)
                CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Have = CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Have;
            EntitiesPool.UnitWaters[idx_to].Water = EntitiesPool.UnitWaters[idx_from].Water;
            foreach (var item in CellUnitStepsInConditionEs.Keys) CellUnitStepsInConditionEs.Steps(item, idx_to).Reset();
            foreach (var unique in CellUnitAbilityUniqueEs.Keys) CellUnitAbilityUniqueEs.Cooldown(unique, idx_to).Amount = CellUnitAbilityUniqueEs.Cooldown(unique, idx_from).Amount;
            EntitiesPool.UnitElse.Corned(idx_to).Set(EntitiesPool.UnitElse.Corned(idx_from));


            EntitiesPool.UnitStuns[idx_to].ForExitStun.Reset();

            if (!Unit(idx_from).IsAnimal)
            {
                if (CellBuildE.Build<BuildingTC>(idx_to).Is(BuildingTypes.Camp))
                {
                    if (!CellBuildE.Build<PlayerTC>(idx_to).Is(EntitiesPool.UnitElse.Owner(idx_to).Player))
                    {
                        CellBuildE.Remove(idx_to);
                    }
                }
            }



            Unit(idx_to).Unit = Unit(idx_from).Unit;
            WhereUnitsE.HaveUnit(Unit(idx_to).Unit, level_from.Level, own_from.Player, idx_to).Have = true;

            WhereUnitsE.HaveUnit(Unit(idx_from).Unit, level_from.Level, own_from.Player, idx_from).Have = false;
            Unit(idx_from).Reset();

            if (CellRiverE.River(idx_to).HaveRiver) EntitiesPool.UnitWaters[idx_to].SetMaxWater();
        }
        public static void Kill(in byte idx)
        {
            ref var unit = ref Unit(idx);
            ref var ownUnit = ref EntitiesPool.UnitElse.Owner(idx);
            ref var levUnit = ref EntitiesPool.UnitElse.Level(idx);

            if (!unit.Have) throw new Exception("It's not got unit");

            if (unit.Is(UnitTypes.King))
            {
                EntityPool.Winner.Player = ownUnit.Player;
            }
            else if (unit.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                EntityPool.ScoutHeroCooldown(unit.Unit, ownUnit.Player).Amount = 3;
                InventorUnitsE.Units(unit.Unit, levUnit.Level, ownUnit.Player).Amount += 1;
            }


            WhereUnitsE.HaveUnit(unit.Unit, levUnit.Level, ownUnit.Player, idx).Have = false;
            unit.Reset();
        }
        public static void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ToolWeaponTypes, LevelTypes) unit, in byte idx)
        {
            if (unit.Item1 == UnitTypes.None) throw new Exception();
            if (Unit(idx).Have) throw new Exception("It's got unit");

            Unit(idx).Unit = unit.Item1;
            EntitiesPool.UnitElse.Level(idx).Level = unit.Item2;
            EntitiesPool.UnitElse.Owner(idx).Player = unit.Item3;
            CellUnitTWE.UnitTW<ToolWeaponC>(idx).ToolWeapon = unit.Item4;
            CellUnitTWE.UnitTW<LevelTC>(idx).Level = unit.Item5;

            EntitiesPool.UnitHps[idx].Hp.Amount = UnitHpValues.MAX_HP;
            EntitiesPool.UnitWaters[idx].SetMaxWater();
            EntitiesPool.UnitStep.SetMaxSteps(idx);

            foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx).Disable();
            EntitiesPool.UnitElse.Condition(idx).Reset();
            foreach (var item in CellUnitStepsInConditionEs.Keys) CellUnitStepsInConditionEs.Steps(item, idx).Reset();

            WhereUnitsE.HaveUnit(unit.Item1, unit.Item2, unit.Item3, idx).Have = true;
        }
        public static void AddToInventor(in byte idx)
        {
            var level = EntitiesPool.UnitElse.Level(idx).Level;
            var owner = EntitiesPool.UnitElse.Owner(idx).Player;

            InventorUnitsE.Units(Unit(idx).Unit, level, owner).Amount += 1;

            WhereUnitsE.HaveUnit(Unit(idx).Unit, level, owner, idx).Have = false;
            Unit(idx).Reset();
        }
        public static void SetScout(in byte idx)
        {
            ref var ownUnitC = ref EntitiesPool.UnitElse.Owner(idx);

            ref var twC = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx);
            ref var levTWC = ref CellUnitTWE.UnitTW<LevelTC>(idx);


            WhereUnitsE.HaveUnit(Unit(idx).Unit, EntitiesPool.UnitElse.Level(idx).Level, ownUnitC.Player, idx).Have = false;

            Unit(idx).Unit = UnitTypes.Scout;
            EntitiesPool.UnitElse.Level(idx).Level = LevelTypes.First;
            if (twC.HaveTW)
            {
                InventorToolWeaponE.ToolWeapons<AmountC>(twC.ToolWeapon, levTWC.Level, ownUnitC.Player).Amount += 1;
                CellUnitTWE.Reset(idx);
            }

            WhereUnitsE.HaveUnit(UnitTypes.Scout, LevelTypes.First, EntitiesPool.UnitElse.Owner(idx).Player, idx).Have = true;
        }
    }
}