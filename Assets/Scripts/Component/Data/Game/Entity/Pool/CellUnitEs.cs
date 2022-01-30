using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellUnitEs
    {
        readonly CellUnitStunEs[] _stuns;
        readonly CellUnitDefendEffectE[] _defendEffect;
        readonly CellUnitMainE[] _else;
        readonly CellUnitTWE[] _toolWeapons;
        readonly Dictionary<ButtonTypes, CellUnitUniqueButtonsE[]> _uniqueButtons;
        readonly Dictionary<AbilityTypes, CellUnitUniqueAbilityE[]> _cooldownUniques;
        readonly Dictionary<PlayerTypes, CellUnitVisibleE[]> _cellUnitVisibles;


        public CellUnitMainE Main(in byte idx) => _else[idx];
        public  CellUnitStunEs Stun(in byte idx) => _stuns[idx];
        public  CellUnitDefendEffectE DefendEffect(in byte idx) => _defendEffect[idx];
        public  CellUnitTWE ToolWeapon(in byte idx) => _toolWeapons[idx];
        public  CellUnitUniqueButtonsE UniqueButton(in ButtonTypes button, in byte idx) => _uniqueButtons[button][idx];
        public  CellUnitUniqueAbilityE Unique(in AbilityTypes ability, in byte idx) => _cooldownUniques[ability][idx];
        public  CellUnitVisibleE VisibleE(in PlayerTypes player, in byte idx) => _cellUnitVisibles[player][idx];

        public  HashSet<AbilityTypes> CooldownKeys
        {
            get
            {
                var keys = new HashSet<AbilityTypes>();
                foreach (var item in _cooldownUniques) keys.Add(item.Key);
                return keys;
            }
        }

        public readonly CellUnitStatEs StatEs;


        public bool IsAlive(in byte idx) => Main(idx).UnitC.Have && StatEs.Hp(idx).Health.Have;
        public int DamageAttack(in byte idx, in UnitStatUpgradesEs statUpgEs, in AttackTypes attack)
        {
            //var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            var standDamage = UnitDamageValues.StandDamage(Main(idx).UnitC.Unit, Main(idx).LevelC.Level);


            if (!Main(idx).UnitC.IsAnimal)
                if (statUpgEs.Upgrade(UnitStatTypes.Damage, Main(idx), UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    upgPerc = 0.3f;
                }



            float powerDamege = standDamage;

            powerDamege += standDamage * UnitDamageValues.PercentTW(ToolWeapon(idx).ToolWeapon.ToolWeapon);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }

        public int DamageOnCell(in byte idx, in CellEs cellEs, in UnitStatUpgradesEs statUpgEs)
        {
            float powerDamege = DamageAttack(idx, statUpgEs, AttackTypes.Simple);

            var standDamage = UnitDamageValues.StandDamage(Main(idx).UnitC.Unit, Main(idx).LevelC.Level);

            powerDamege += standDamage * UnitDamageValues.ProtRelaxPercent(Main(idx).ConditionC.Condition);
            if (cellEs.BuildEs.Build(idx).Health.Have) powerDamege += standDamage * CellBuildingValues.ProtectionPercent(cellEs.BuildEs.Build(idx).BuildTC.Build);

            float protectionPercent = 0;

            var envEs = cellEs.EnvironmentEs;

            if (envEs.Fertilizer(idx).HaveEnvironment) protectionPercent += envEs.Fertilizer(idx).ProtectionPercent;
            if (envEs.YoungForest(idx).HaveEnvironment) protectionPercent += envEs.YoungForest(idx).ProtectionPercent;
            if (envEs.AdultForest(idx).HaveEnvironment) protectionPercent += envEs.AdultForest(idx).ProtectionPercent;
            if (envEs.Hill(idx).HaveEnvironment) protectionPercent += envEs.Hill(idx).ProtectionPercent;
            if (envEs.Mountain(idx).HaveEnvironment) protectionPercent += envEs.Mountain(idx).ProtectionPercent;

            powerDamege += standDamage * protectionPercent;

            return (int)powerDamege;
        }

        public bool CanExtract(in byte idx, in CellEnvironmentEs cellEnvEs, out int extract, out EnvironmentTypes env, out ResourceTypes res)
        {
            extract = 0;
            env = EnvironmentTypes.None;
            res = ResourceTypes.None;


            if (cellEnvEs.AdultForest(idx).HaveEnvironment)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResourceTypes.Wood;
            }
            else return false;


            if (!Main(idx).UnitC.Is(UnitTypes.Pawn) || !Main(idx).ConditionC.Is(ConditionUnitTypes.Relaxed) || !StatEs.Hp(idx).HaveMax) return false;


            var ration = 0f;

            switch (Main(idx).LevelC.Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            extract = (int)(CellEnvironmentValues.MaxResources(env) * ration);

            if (extract > cellEnvEs.AdultForest(idx).AmountResources) extract = cellEnvEs.AdultForest(idx).AmountResources;

            return true;
        }

        //public bool CanResume(in byte idx, out int resume, out EnvironmentTypes env)
        //{
        //    resume = 0;
        //    env = EnvironmentTypes.None;

        //    var twC = ToolWeapon(idx).ToolWeaponTC;

        //    if (Ents.CellEs.BuildEs.Build(idx).BuildTC.Have || !Else(idx).ConditionC.Is(ConditionUnitTypes.Relaxed) || !Hp(idx).HaveMax) return false;



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

        public int StepsForShiftOrAttack(in byte idx, in DirectTypes dirMove, in CellEnvironmentEs envEs, in CellTrailEs trailsEs)
        {
            var needSteps = 1;

            if (envEs.Fertilizer(idx).HaveEnvironment) needSteps += envEs.Fertilizer(idx).NeedStepsShiftAttackUnit;
            if (envEs.YoungForest(idx).HaveEnvironment) needSteps += envEs.YoungForest(idx).NeedStepsShiftAttackUnit;
            if (envEs.AdultForest(idx).HaveEnvironment) needSteps += envEs.AdultForest(idx).NeedStepsShiftAttackUnit;
            if (envEs.Hill(idx).HaveEnvironment) needSteps += envEs.Fertilizer(idx).NeedStepsShiftAttackUnit;

            if (trailsEs.Trail(dirMove.Invert(), idx).HaveTrail) needSteps--;

            return needSteps;
        }

        public CellUnitEs(in EcsWorld gameW)
        {
            _uniqueButtons = new Dictionary<ButtonTypes, CellUnitUniqueButtonsE[]>();
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _uniqueButtons.Add(buttonT, new CellUnitUniqueButtonsE[CellStartValues.ALL_CELLS_AMOUNT]);
            }


            _cooldownUniques = new Dictionary<AbilityTypes, CellUnitUniqueAbilityE[]>();
            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _cooldownUniques.Add(ability, new CellUnitUniqueAbilityE[CellStartValues.ALL_CELLS_AMOUNT]);
            }


            _cellUnitVisibles = new Dictionary<PlayerTypes, CellUnitVisibleE[]>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _cellUnitVisibles[player] = new CellUnitVisibleE[CellStartValues.ALL_CELLS_AMOUNT];
            }


            _stuns = new CellUnitStunEs[CellStartValues.ALL_CELLS_AMOUNT];
            _defendEffect = new CellUnitDefendEffectE[CellStartValues.ALL_CELLS_AMOUNT];
            _else = new CellUnitMainE[CellStartValues.ALL_CELLS_AMOUNT];
            _toolWeapons = new CellUnitTWE[CellStartValues.ALL_CELLS_AMOUNT];


            for (byte idx = 0; idx < _stuns.Length; idx++)
            {
                _stuns[idx] = new CellUnitStunEs(gameW, idx);
                _defendEffect[idx] = new CellUnitDefendEffectE(gameW);
                _else[idx] = new CellUnitMainE(gameW);
                _toolWeapons[idx] = new CellUnitTWE(gameW);

                foreach (var item in _uniqueButtons) _uniqueButtons[item.Key][idx] = new CellUnitUniqueButtonsE(gameW);
                foreach (var item in _cooldownUniques) _cooldownUniques[item.Key][idx] = new CellUnitUniqueAbilityE(gameW);
                foreach (var item in _cellUnitVisibles) _cellUnitVisibles[item.Key][idx] = new CellUnitVisibleE(gameW);
            }


            StatEs = new CellUnitStatEs(gameW);
        }


        public void SetNew(in(UnitTypes, LevelTypes, PlayerTypes) unit, in Entities ents, in byte idx, in (ToolWeaponTypes, LevelTypes) tw = default)
        {
            Main(idx).SetNew(unit);
            StatEs.Hp(idx).SetMax();
            StatEs.Step(idx).SetMax(Main(idx));
            StatEs.Water(idx).SetMax(Main(idx), ents.UnitStatUpgradesEs);
            Stun(idx).ForExitStun.Reset();
            ToolWeapon(idx).SetNew(tw);
            foreach (var item in CooldownKeys) Unique(item, idx).Cooldown.Reset();
            ents.WhereUnitsEs.WhereUnit(unit, idx).HaveUnit.Have = true;
        }
        public void Shift(byte idx_from, in byte idx_to, in Entities ents)
        {
            var statEs = ents.UnitStatUpgradesEs;  
            var whereUnitsEs = ents.WhereUnitsEs;
            var cellEs = ents.CellEs;


            whereUnitsEs.WhereUnit(Main(idx_from), idx_from).HaveUnit.Have = false;

            Main(idx_to).Shift(Main(idx_from));
            StatEs.Hp(idx_to).Shift(StatEs.Hp(idx_from));
            StatEs.Step(idx_to).Shift(StatEs.Step(idx_from));
            StatEs.Water(idx_to).Shift(StatEs.Water(idx_from));
            Stun(idx_to).Shift(Stun(idx_from));
            ToolWeapon(idx_to).Shift(ToolWeapon(idx_from));
            foreach (var abilityT in CooldownKeys) Unique(abilityT, idx_to).Shift(Unique(abilityT, idx_from));

            if (cellEs.EnvironmentEs.AdultForest(idx_from).HaveEnvironment)
            {
                cellEs.TrailEs.Trail(cellEs.GetDirect(idx_from, idx_to), idx_from).SetNew();
            }
            if (cellEs.EnvironmentEs.AdultForest(idx_to).HaveEnvironment)
            {
                cellEs.TrailEs.Trail(cellEs.GetDirect(idx_from, idx_to).Invert(), idx_to).SetNew();
            }

            if (cellEs.RiverEs.River(idx_to).RiverTC.HaveRiver)
            {
                StatEs.Water(idx_to).SetMax(Main(idx_to), statEs);
            }

            whereUnitsEs.WhereUnit(Main(idx_to), idx_to).HaveUnit.Have = true;
        }

        public void Kill(in byte idx, in Entities ents)
        {
            ref var unit = ref Main(idx).UnitC;
            ref var ownUnit = ref Main(idx).OwnerC;
            ref var levUnit = ref Main(idx).LevelC;

            if (!unit.Have) throw new Exception("It's not got unit");

            if (unit.Is(UnitTypes.King))
            {
                ents.WinnerE.Winner.Player = ownUnit.Player;
            }
            else if (unit.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                ents.ScoutHeroCooldownE(Main(idx)).Cooldown.Amount = 3;
                ents.InventorUnitsEs.Units(unit.Unit, levUnit.Level, ownUnit.Player).Units.Amount += 1;
            }

            ents.WhereUnitsEs.WhereUnit(unit.Unit, levUnit.Level, ownUnit.Player, idx).HaveUnit.Have = false;
            Main(idx).Reset();
        }
        public void AddToInventorAndRemove(in byte idx, in InventorUnitsEs invUnits, in WhereUnitsEs whereUnits)
        {
            var level = Main(idx).LevelC.Level;
            var owner = Main(idx).OwnerC.Player;

            invUnits.Units(Main(idx).UnitC.Unit, level, owner).Units.Amount++;

            whereUnits.WhereUnit(Main(idx).UnitC.Unit, level, owner, idx).HaveUnit.Have = false;
            Main(idx).Reset();
        }
    }
}