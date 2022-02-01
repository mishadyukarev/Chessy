﻿using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitMainE : CellAbstE
    {
        ref UnitTC UnitTCRef => ref Ent.Get<UnitTC>();
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();
        ref PlayerTC OwnerCRef => ref Ent.Get<PlayerTC>();
        ref ConditionUnitC ConditionTCRef => ref Ent.Get<ConditionUnitC>();
        ref IsC IsCornedRef => ref Ent.Get<IsC>();

        public UnitTC UnitTC => Ent.Get<UnitTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();
        public ConditionUnitC ConditionTC => Ent.Get<ConditionUnitC>();
        public IsC IsCorned => Ent.Get<IsC>();

        public bool HaveUnit(in CellUnitStatEs statEs) => statEs.Hp(Idx).IsAlive && HaveUnitT;
        public bool HaveUnitT => UnitTC.Unit != UnitTypes.None && UnitTC.Unit != UnitTypes.End;


        public int StepsForShiftOrAttack(in DirectTypes dirMove, in CellEnvironmentEs envEs, in CellTrailEs trailsEs)
        {
            var needSteps = 1;

            if (envEs.Fertilizer(Idx).HaveEnvironment) needSteps += envEs.Fertilizer(Idx).NeedStepsShiftAttackUnit;
            if (envEs.YoungForest(Idx).HaveEnvironment) needSteps += envEs.YoungForest(Idx).NeedStepsShiftAttackUnit;
            if (envEs.AdultForest(Idx).HaveEnvironment) needSteps += envEs.AdultForest(Idx).NeedStepsShiftAttackUnit;
            if (envEs.Hill(Idx).HaveEnvironment) needSteps += envEs.Fertilizer(Idx).NeedStepsShiftAttackUnit;

            if (trailsEs.Trail(dirMove.Invert(), Idx).HaveTrail) needSteps--;

            return needSteps;
        }
        //public bool CanResume(in byte idx, out int resume, out EnvironmentTypes env)
        //{
        //    resume = 0;
        //    env = EnvironmentTypes.None;

        //    var twC = ToolWeapon(idx).ToolWeaponTC;

        //    if (Ents.BuildEs.Build(idx).BuildTC.Have || !Else(idx).ConditionC.Is(ConditionUnitTypes.Relaxed) || !Hp(idx).HaveMax) return false;



        //    var ration = 0f;

        //    switch (Else(idx).UnitC.Unit)
        //    {
        //        case UnitTypes.Pawn:
        //            if (!Ents.CellEs.EnvironmentEs.Hill( idx).HaveEnvironment && !twC.Is(ToolWeaponTypes.Pick)) return false;

        //            env = EnvironmentTypes.Hill;

        //            switch (Else(idx).LevelC.Level)
        //            {
        //                case LevelTypes.First: ration = 0.3f; break;
        //                case LevelTypes.Second: ration = 0.6f; break;
        //                default: throw new Exception();
        //            }
        //            break;

        //        case UnitTypes.Elfemale:
        //            ration = 0.3f;
        //            env = EnvironmentTypes.AdultForest;
        //            break;

        //        default: return false;
        //    }



        //    resume = (int)(CellEnvironmentValues.MaxResources(env) * ration);

        //    if (resume > Ents.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount)
        //        resume = Ents.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount;

        //    return true;
        //}
        public int DamageAttack(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs, in AttackTypes attack)
        {
            //var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            var standDamage = UnitDamageValues.StandDamage(UnitTC.Unit, LevelTC.Level);


            if (!UnitTC.IsAnimal)
                if (statUpgEs.Upgrade(UnitStatTypes.Damage, this, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    upgPerc = 0.3f;
                }



            float powerDamege = standDamage;

            powerDamege += standDamage * UnitDamageValues.PercentTW(cellEs.UnitEs.ToolWeapon(Idx).ToolWeaponTC.ToolWeapon);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }

        public int DamageOnCell(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs)
        {
            float powerDamege = DamageAttack(cellEs, statUpgEs, AttackTypes.Simple);

            var standDamage = UnitDamageValues.StandDamage(UnitTC.Unit, LevelTC.Level);

            powerDamege += standDamage * UnitDamageValues.ProtRelaxPercent(ConditionTC.Condition);
            if (cellEs.BuildEs.BuildingE(Idx).HaveBuilding) powerDamege += standDamage * CellBuildingValues.ProtectionPercent(cellEs.BuildEs.BuildingE(Idx).BuildTC.Build);

            float protectionPercent = 0;

            var envEs = cellEs.EnvironmentEs;

            if (envEs.Fertilizer(Idx).HaveEnvironment) protectionPercent += envEs.Fertilizer(Idx).ProtectionPercent;
            if (envEs.YoungForest(Idx).HaveEnvironment) protectionPercent += envEs.YoungForest(Idx).ProtectionPercent;
            if (envEs.AdultForest(Idx).HaveEnvironment) protectionPercent += envEs.AdultForest(Idx).ProtectionPercent;
            if (envEs.Hill(Idx).HaveEnvironment) protectionPercent += envEs.Hill(Idx).ProtectionPercent;
            if (envEs.Mountain(Idx).HaveEnvironment) protectionPercent += envEs.Mountain(Idx).ProtectionPercent;

            powerDamege += standDamage * protectionPercent;

            return (int)powerDamege;
        }

        public bool CanExtractPawnAdultForest(in CellUnitStatEs statE, in CellEnvironmentEs cellEnvEs)
        {
            if (cellEnvEs.AdultForest(Idx).HaveEnvironment
                && UnitTC.Is(UnitTypes.Pawn)
                && ConditionTC.Is(ConditionUnitTypes.Relaxed)
                && statE.Hp(Idx).HaveMax)
            {
                return true;
            }
            else return false;
        }


        internal CellUnitMainE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {

        }

        void Shift(CellUnitMainE unitElse_from)
        {
            OwnerCRef = unitElse_from.OwnerC;
            LevelTCRef = unitElse_from.LevelTC;
            ConditionTCRef.Condition = ConditionUnitTypes.None;
            IsCornedRef = unitElse_from.IsCorned;

            UnitTCRef.Unit = unitElse_from.UnitTC.Unit;
            unitElse_from.UnitTCRef.Unit = UnitTypes.None;
        }
        void Set(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit)
        {
            UnitTCRef.Unit = unit.Item1;
            LevelTCRef.Level = unit.Item2;
            OwnerCRef.Player = unit.Item3;
            ConditionTCRef.Condition = unit.Item4;
            IsCornedRef.Is = unit.Item5;
        }
        void Reset()
        {
            UnitTCRef.Unit = UnitTypes.None;
            LevelTCRef.Level = LevelTypes.None;
            OwnerCRef.Player = PlayerTypes.None;
            ConditionTCRef.Condition = ConditionUnitTypes.None;
            IsCornedRef.Is = false;
        }

        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit, in Entities ents, in (ToolWeaponTypes, LevelTypes) tw = default)
        {
            Set(unit);
            ents.CellEs.UnitEs.StatEs.Hp(Idx).SetMax();
            ents.CellEs.UnitEs.StatEs.Step(Idx).SetMax(this);
            ents.CellEs.UnitEs.StatEs.Water(Idx).SetMax(this, ents.UnitStatUpgradesEs);
            ents.CellEs.UnitEs.Stun(Idx).Reset();
            ents.CellEs.UnitEs.ToolWeapon(Idx).SetNew(tw);
            foreach (var item in ents.CellEs.UnitEs.CooldownKeys) ents.CellEs.UnitEs.CooldownAbility(item, Idx).SetNew();
            ents.WhereUnitsEs.WhereUnit(unit.Item1, unit.Item2, unit.Item3, Idx).HaveUnit.Have = true;
        }
        public void Kill(in Entities ents)
        {
            var unit = UnitTC;
            var ownUnit = OwnerC;

            if (!HaveUnit(ents.CellEs.UnitEs.StatEs)) throw new Exception("It's not got unit");

            if (unit.Is(UnitTypes.King))
            {
                ents.WinnerE.Winner.Player = ownUnit.Player;
            }
            else if (unit.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                ents.ScoutHeroCooldownE(this).Cooldown.Amount = 3;
                ents.InventorUnitsEs.Units(unit.Unit, LevelTC.Level, ownUnit.Player).AddUnit();
            }

            ents.WhereUnitsEs.WhereUnit(this, Idx).HaveUnit.Have = false;
            Reset();
        }
        public void Clear(in WhereUnitsEs whereUnitsEs)
        {
            whereUnitsEs.WhereUnit(this, Idx).HaveUnit.Have = false;
            Reset();
        }

        public void Shift(in byte idx_to, in Entities ents)
        {
            var statEs = ents.UnitStatUpgradesEs;
            var whereUnitsEs = ents.WhereUnitsEs;
            var cellEs = ents.CellEs;
            var unitEs = cellEs.UnitEs;


            whereUnitsEs.WhereUnit(this, Idx).HaveUnit.Have = false;

            unitEs.Main(idx_to).Shift(this);
            unitEs.StatEs.Hp(idx_to).Shift(unitEs.StatEs.Hp(Idx));
            unitEs.StatEs.Step(idx_to).Shift(unitEs.StatEs.Step(Idx));
            unitEs.StatEs.Water(idx_to).Shift(unitEs.StatEs.Water(Idx));
            unitEs.Stun(idx_to).Shift(unitEs.Stun(Idx));

            unitEs.ToolWeapon(idx_to).Set(unitEs.ToolWeapon(Idx));
            foreach (var abilityT in unitEs.CooldownKeys) 
                unitEs.CooldownAbility(abilityT, idx_to).Shift(unitEs.CooldownAbility(abilityT, Idx));

            if (cellEs.EnvironmentEs.AdultForest(Idx).HaveEnvironment)
            {
                cellEs.TrailEs.Trail(cellEs.GetDirect(Idx, idx_to), Idx).SetNew();
            }
            if (cellEs.EnvironmentEs.AdultForest(idx_to).HaveEnvironment)
            {
                cellEs.TrailEs.Trail(cellEs.GetDirect(Idx, idx_to).Invert(), idx_to).SetNew();
            }

            if (cellEs.RiverEs.River(idx_to).RiverTC.HaveRiver)
            {
                unitEs.StatEs.Water(idx_to).SetMax(unitEs.Main(idx_to), statEs);
            }

            whereUnitsEs.WhereUnit(unitEs.Main(idx_to), idx_to).HaveUnit.Have = true;
        }

        public void AddToInventorAndRemove(in InventorUnitsEs invUnits, in WhereUnitsEs whereUnits)
        {
            var level = LevelTC.Level;
            var owner = OwnerC.Player;

            invUnits.Units(UnitTC.Unit, level, owner).AddUnit();

            whereUnits.WhereUnit(UnitTC.Unit, level, owner, Idx).HaveUnit.Have = false;

            Reset();
        }
        public void Upgrade()
        {
            if (LevelTC.Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) throw new Exception();

            LevelTCRef.Level = LevelTypes.Second;
        }
        public void SetLevel(in LevelTypes level) => LevelTCRef.Level = level;

        public void ResetCondition() => ConditionTCRef.Condition = ConditionUnitTypes.None;
        public void SetCondition(in ConditionUnitTypes cond) => ConditionTCRef.Condition = cond;

        public void ChangeCorner() => IsCornedRef.Is = !IsCorned.Is;

        public void SyncRpc(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit) => Set(unit);
    }
}