using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitMainE : CellEntityAbstract
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

        public bool HaveUnit(in CellUnitStatEs statEs) => statEs.Hp.IsAlive && HaveUnitT;
        public bool HaveUnitT => UnitTC.Unit != UnitTypes.None && UnitTC.Unit != UnitTypes.End;


        public int StepsForShiftOrAttack(in DirectTypes dirMove, in CellEnvironmentEs envEs, in CellTrailEs trailsEs)
        {
            var needSteps = 1;

            if (envEs.Fertilizer.HaveEnvironment) needSteps += envEs.Fertilizer.NeedStepsShiftAttackUnit;
            if (envEs.YoungForest.HaveEnvironment) needSteps += envEs.YoungForest.NeedStepsShiftAttackUnit;
            if (envEs.AdultForest.HaveEnvironment) needSteps += envEs.AdultForest.NeedStepsShiftAttackUnit;
            if (envEs.Hill.HaveEnvironment) needSteps += envEs.Fertilizer.NeedStepsShiftAttackUnit;

            if (trailsEs.Trail(dirMove.Invert()).HaveTrail) needSteps--;

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
        //            if (!Ents.EnvironmentEs.Hill( idx).HaveEnvironment && !twC.Is(ToolWeaponTypes.Pick)) return false;

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

        //    if (resume > Ents.EnvironmentEs.Environment(env, idx).Resources.Amount)
        //        resume = Ents.EnvironmentEs.Environment(env, idx).Resources.Amount;

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

            powerDamege += standDamage * UnitDamageValues.PercentTW(cellEs.UnitEs.ToolWeaponE.ToolWeaponTC.ToolWeapon);
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
            if (cellEs.BuildEs.BuildingE.HaveBuilding) powerDamege += standDamage * CellBuildingValues.ProtectionPercent(cellEs.BuildEs.BuildingE.BuildTC.Build);

            float protectionPercent = 0;

            var envEs = cellEs.EnvironmentEs;

            if (envEs.Fertilizer.HaveEnvironment) protectionPercent += envEs.Fertilizer.ProtectionPercent;
            if (envEs.YoungForest.HaveEnvironment) protectionPercent += envEs.YoungForest.ProtectionPercent;
            if (envEs.AdultForest.HaveEnvironment) protectionPercent += envEs.AdultForest.ProtectionPercent;
            if (envEs.Hill.HaveEnvironment) protectionPercent += envEs.Hill.ProtectionPercent;
            if (envEs.Mountain.HaveEnvironment) protectionPercent += envEs.Mountain.ProtectionPercent;

            powerDamege += standDamage * protectionPercent;

            return (int)powerDamege;
        }

        public bool CanExtractPawnAdultForest(in CellUnitStatEs statE, in CellEnvironmentEs cellEnvEs)
        {
            if (cellEnvEs.AdultForest.HaveEnvironment
                && UnitTC.Is(UnitTypes.Pawn)
                && ConditionTC.Is(ConditionUnitTypes.Relaxed)
                && statE.Hp.HaveMax)
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
            ents.CellEs(Idx).UnitEs.StatEs.Hp.SetMax();
            ents.CellEs(Idx).UnitEs.StatEs.StepE.SetMax(this);
            ents.CellEs(Idx).UnitEs.StatEs.Water.SetMax(this, ents.UnitStatUpgradesEs);
            ents.UnitEffectEs(Idx).StunE.Reset();
            ents.CellEs(Idx).UnitEs.ToolWeaponE.SetNew(tw);
            foreach (var item in ents.CellEs(Idx).UnitEs.CooldownKeys) ents.CellEs(Idx).UnitEs.CooldownAbility(item).SetNew();
            ents.WhereUnitsEs.WhereUnit(unit.Item1, unit.Item2, unit.Item3, Idx).HaveUnit.Have = true;
        }
        public void Kill(in Entities ents)
        {
            if (!HaveUnitT) throw new Exception("There's not unit");

            if (UnitTC.Is(UnitTypes.King))
            {
                ents.WinnerE.Winner.Player = OwnerC.Player;
            }
            else if (UnitTC.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                ents.ScoutHeroCooldownE(this).Cooldown.Amount = 3;
                ents.InventorUnitsEs.Units(UnitTC.Unit, LevelTC.Level, OwnerC.Player).AddUnit();
            }
            //else if (UnitTC.IsAnimal)
            //{
            //    ents.InventorResourcesEs.Resource(ResourceTypes.Food, whoKill).Resources.Amount += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
            //}

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


            whereUnitsEs.WhereUnit(this, Idx).HaveUnit.Have = false;

            ents.CellEs(idx_to).UnitEs.MainE.Shift(this);
            ents.CellEs(idx_to).UnitEs.StatEs.Hp.Shift(ents.CellEs(Idx).UnitEs.StatEs.Hp);
            ents.CellEs(idx_to).UnitEs.StatEs.StepE.Shift(ents.CellEs(Idx).UnitEs.StatEs.StepE);
            ents.CellEs(idx_to).UnitEs.StatEs.Water.Shift(ents.CellEs(Idx).UnitEs.StatEs.Water);
            ents.UnitEffectEs(idx_to).StunE.Shift(ents.UnitEffectEs(Idx).StunE);
            ents.UnitEffectEs(idx_to).ShieldE.Shift(ents.UnitEffectEs(Idx).ShieldE);

            ents.CellEs(idx_to).UnitEs.ToolWeaponE.Set(ents.CellEs(Idx).UnitEs.ToolWeaponE);

            foreach (var abilityT in ents.CellEs(Idx).UnitEs.CooldownKeys)
                ents.CellEs(idx_to).UnitEs.CooldownAbility(abilityT).Shift(ents.CellEs(Idx).UnitEs.CooldownAbility(abilityT));

            if (ents.CellEs(Idx).EnvironmentEs.AdultForest.HaveEnvironment)
            {
                ents.CellEs(Idx).TrailEs.Trail(ents.CellEsWorker.GetDirect(Idx, idx_to)).SetNew();
            }
            if (ents.CellEs(idx_to).EnvironmentEs.AdultForest.HaveEnvironment)
            {
                ents.CellEs(idx_to).TrailEs.Trail(ents.CellEsWorker.GetDirect(Idx, idx_to).Invert()).SetNew();
            }

            if (ents.CellEs(idx_to).RiverEs.River.RiverTC.HaveRiver)
            {
                ents.CellEs(idx_to).UnitEs.StatEs.Water.SetMax(ents.CellEs(idx_to).UnitEs.MainE, statEs);
            }

            whereUnitsEs.WhereUnit(ents.CellEs(idx_to).UnitEs.MainE, idx_to).HaveUnit.Have = true;
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