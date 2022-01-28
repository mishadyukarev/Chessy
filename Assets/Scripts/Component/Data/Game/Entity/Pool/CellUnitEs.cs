using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitEs
    {
         CellUnitHpE[] _hps;
         CellUnitStepE[] _steps;
         CellUnitWaterE[] _waters;
         CellUnitStunEs[] _stuns;
         CellUnitDefendEffectE[] _defendEffect;
         CellUnitElseE[] _else;
         CellUnitTWE[] _toolWeapons;
         Dictionary<ButtonTypes, CellUnitUniqueButtonsE[]> _uniqueButtons;
         Dictionary<AbilityTypes, CellUnitUniqueAbilityE[]> _cooldownUniques;
         Dictionary<PlayerTypes, CellUnitVisibleE[]> _cellUnitVisibles;


        public  CellUnitHpE Hp(in byte idx) => _hps[idx];
        public  CellUnitStepE Step(in byte idx) => _steps[idx];
        public  CellUnitWaterE Water(in byte idx) => _waters[idx];
        public  CellUnitStunEs Stun(in byte idx) => _stuns[idx];
        public  CellUnitDefendEffectE DefendEffect(in byte idx) => _defendEffect[idx];
        public  CellUnitElseE Else(in byte idx) => _else[idx];
        public  CellUnitTWE ToolWeapon(in byte idx) => _toolWeapons[idx];
        public  CellUnitUniqueButtonsE UniqueButton(in ButtonTypes button, in byte idx) => _uniqueButtons[button][idx];
        public  CellUnitUniqueAbilityE CooldownUnique(in AbilityTypes ability, in byte idx) => _cooldownUniques[ability][idx];
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

        public  int MaxWater(in byte idx)
        {
            var unitT = Else(idx).UnitC.Unit;
            var levelT = Else(idx).LevelC.Level;
            var playerT = Else(idx).OwnerC.Player;

            var maxWater = CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS;

            if (!Else(idx).UnitC.IsAnimal)
            {
                if (Entities.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Water, unitT, levelT, playerT, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    return maxWater += (int)(maxWater * 0.5f);
                }
            }

            return maxWater;
        }

        public  bool CanExtract(in byte idx, out int extract, out EnvironmentTypes env, out ResourceTypes res)
        {
            extract = 0;
            env = EnvironmentTypes.None;
            res = ResourceTypes.None;


            if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx).Resources.Have)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResourceTypes.Wood;
            }
            else return false;


            if (!Else(idx).UnitC.Is(UnitTypes.Pawn) || !Else(idx).ConditionC.Is(ConditionUnitTypes.Relaxed)
                || !Hp(idx).HaveMax) return false;


            var ration = 0f;

            switch (Else(idx).LevelC.Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            extract = (int)(CellEnvironmentValues.MaxResources(env) * ration);

            if (extract > Entities.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount) extract = Entities.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount;

            return true;
        }
        public  bool CanResume(in byte idx, out int resume, out EnvironmentTypes env)
        {
            resume = 0;
            env = EnvironmentTypes.None;

            var twC = ToolWeapon(idx).ToolWeaponC;

            if (Entities.CellEs.BuildEs.Build(idx).BuildTC.Have || !Else(idx).ConditionC.Is(ConditionUnitTypes.Relaxed) || !Hp(idx).HaveMax) return false;



            var ration = 0f;

            switch (Else(idx).UnitC.Unit)
            {
                case UnitTypes.Pawn:
                    if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx).Resources.Have && !twC.Is(ToolWeaponTypes.Pick)) return false;

                    env = EnvironmentTypes.Hill;

                    switch (Else(idx).LevelC.Level)
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

            if (resume > Entities.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount)
                resume = Entities.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount;

            return true;
        }
        public  int DamageAttack(in byte idx, in AttackTypes attack)
        {
            var tw = ToolWeapon(idx).ToolWeaponC.ToolWeapon;
            //var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            var standDamage = UnitDamageValues.StandDamage(Else(idx).UnitC.Unit, Else(idx).LevelC.Level);


            if (!Else(idx).UnitC.IsAnimal)
                if (Entities.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, Else(idx).UnitC.Unit, Else(idx).LevelC.Level, Else(idx).OwnerC.Player, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    upgPerc = 0.3f;
                }



            float powerDamege = standDamage;

            powerDamege += standDamage * UnitDamageValues.PercentTW(tw);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public  int DamageOnCell(in byte idx)
        {
            var condition = Else(idx).ConditionC.Condition;

            var build = Entities.CellEs.BuildEs.Build(idx).BuildTC.Build;

            float powerDamege = DamageAttack(idx, AttackTypes.Simple);

            var standDamage = UnitDamageValues.StandDamage(Else(idx).UnitC.Unit, Else(idx).LevelC.Level);

            powerDamege += standDamage * UnitDamageValues.ProtRelaxPercent(condition);
            powerDamege += standDamage * CellBuildingValues.ProtectionPercent(build);
            foreach (var item in Entities.CellEs.EnvironmentEs.Keys) if (Entities.CellEs.EnvironmentEs.Environment(item, idx).Resources.Have) powerDamege += standDamage * UnitDamageValues.ProtectionPercent(item);
            return (int)powerDamege;
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



            _hps = new CellUnitHpE[CellStartValues.ALL_CELLS_AMOUNT];
            _steps = new CellUnitStepE[CellStartValues.ALL_CELLS_AMOUNT];
            _waters = new CellUnitWaterE[CellStartValues.ALL_CELLS_AMOUNT];
            _stuns = new CellUnitStunEs[CellStartValues.ALL_CELLS_AMOUNT];
            _defendEffect = new CellUnitDefendEffectE[CellStartValues.ALL_CELLS_AMOUNT];
            _else = new CellUnitElseE[CellStartValues.ALL_CELLS_AMOUNT];
            _toolWeapons = new CellUnitTWE[CellStartValues.ALL_CELLS_AMOUNT];


            for (byte idx = 0; idx < _waters.Length; idx++)
            {
                _hps[idx] = new CellUnitHpE(gameW);
                _waters[idx] = new CellUnitWaterE(gameW);
                _steps[idx] = new CellUnitStepE(gameW);
                _stuns[idx] = new CellUnitStunEs(gameW, idx);
                _defendEffect[idx] = new CellUnitDefendEffectE(gameW);
                _else[idx] = new CellUnitElseE(gameW);
                _toolWeapons[idx] = new CellUnitTWE(gameW);

                foreach (var item in _uniqueButtons) _uniqueButtons[item.Key][idx] = new CellUnitUniqueButtonsE(gameW);
                foreach (var item in _cooldownUniques) _cooldownUniques[item.Key][idx] = new CellUnitUniqueAbilityE(gameW);
                foreach (var item in _cellUnitVisibles) _cellUnitVisibles[item.Key][idx] = new CellUnitVisibleE(gameW);
            }
        }


        public  void Shift(in byte idx_from, in byte idx_to, in bool withTrail)
        {
            var dir = CellSpaceSupport.GetDirect(Entities.CellEs.CellE(idx_from).XyC.Xy, Entities.CellEs.CellE(idx_to).XyC.Xy);

            if (withTrail)
            {
                Entities.CellEs.TrailEs.TrySetNewTrail(idx_to, dir.Invert(), Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_to).Resources.Have);
                Entities.CellEs.TrailEs.TrySetNewTrail(idx_from, dir, Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_from).Resources.Have);
            }

            ref var unit_from = ref Else(idx_from).UnitC;
            ref var level_from = ref Else(idx_from).LevelC;
            ref var own_from = ref Else(idx_from).OwnerC;


            Else(idx_to).OwnerC = Else(idx_from).OwnerC;
            Else(idx_to).LevelC = Else(idx_from).LevelC;


            Hp(idx_to).AmountC = Hp(idx_from).AmountC;
            Step(idx_to).Steps = Step(idx_from).Steps;
            if (Else(idx_to).ConditionC.HaveCondition) Else(idx_to).ConditionC.Reset();

            Set(idx_from, idx_to);
            ToolWeapon(idx_to).LevelC = ToolWeapon(idx_from).LevelC;
            ToolWeapon(idx_to).Protection = ToolWeapon(idx_from).Protection;

            //foreach (var item in CellUnitEffectsEs.Keys)
            //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Have = CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Have;
            Water(idx_to).AmountC = Water(idx_from).AmountC;
            foreach (var unique in CooldownKeys) CooldownUnique(unique, idx_to).Cooldown = CooldownUnique(unique, idx_from).Cooldown;
            Else(idx_to).CornedC.Set(Else(idx_from).CornedC);

            Stun(idx_to).ForExitStun.Reset();

            if (!Else(idx_from).UnitC.IsAnimal)
            {
                if (Entities.CellEs.BuildEs.Build(idx_to).BuildTC.Is(BuildingTypes.Camp))
                {
                    if (!Entities.CellEs.BuildEs.Build(idx_to).PlayerTC.Is(Else(idx_to).OwnerC.Player))
                    {
                        Entities.WhereBuildingEs.HaveBuild(Entities.CellEs.BuildEs.Build(idx_to).BuildTC.Build, Entities.CellEs.BuildEs.Build(idx_to).PlayerTC.Player, idx_to).HaveBuilding.Have = false;

                        Entities.CellEs.BuildEs.Build(idx_to).Remove();
                    }
                }
            }

            Else(idx_to).UnitC.Unit = Else(idx_from).UnitC.Unit;
            WhereUnitsE.HaveUnit(Else(idx_to).UnitC.Unit, level_from.Level, own_from.Player, idx_to).Have = true;

            WhereUnitsE.HaveUnit(Else(idx_from).UnitC.Unit, level_from.Level, own_from.Player, idx_from).Have = false;
            Else(idx_from).UnitC.Reset();

            if (Entities.CellEs.RiverEs.River(idx_to).RiverTC.HaveRiver) Water(idx_to).AmountC.Amount = MaxWater(idx_to);
        }
        public  void Kill(in byte idx)
        {
            ref var unit = ref Else(idx).UnitC;
            ref var ownUnit = ref Else(idx).OwnerC;
            ref var levUnit = ref Else(idx).LevelC;

            if (!unit.Have) throw new Exception("It's not got unit");

            if (unit.Is(UnitTypes.King))
            {
                Entities.WinnerE.Winner.Player = ownUnit.Player;
            }
            else if (unit.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                Entities.ScoutHeroCooldownE(unit.Unit, ownUnit.Player).Cooldown.Amount = 3;
                InventorUnitsE.Units(unit.Unit, levUnit.Level, ownUnit.Player).Amount += 1;
            }


            WhereUnitsE.HaveUnit(unit.Unit, levUnit.Level, ownUnit.Player, idx).Have = false;
            unit.Reset();
        }
        public  void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ToolWeaponTypes, LevelTypes) unit, in byte idx)
        {
            if (unit.Item1 == UnitTypes.None) throw new Exception();
            if (Else(idx).UnitC.Have) throw new Exception("It's got unit");

            Else(idx).UnitC.Unit = unit.Item1;
            Else(idx).LevelC.Level = unit.Item2;
            Else(idx).OwnerC.Player = unit.Item3;
            ToolWeapon(idx).ToolWeaponC.ToolWeapon = unit.Item4;
            ToolWeapon(idx).LevelC.Level = unit.Item5;

            Hp(idx).AmountC.Amount = UnitHpValues.MAX_HP;
            Water(idx).AmountC.Amount = MaxWater(idx);
            Step(idx).SetMax(Else(idx));

            //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx).Disable();
            Else(idx).ConditionC.Reset();

            WhereUnitsE.HaveUnit(unit.Item1, unit.Item2, unit.Item3, idx).Have = true;
        }
        public  void AddToInventor(in byte idx)
        {
            var level = Else(idx).LevelC.Level;
            var owner = Else(idx).OwnerC.Player;

            InventorUnitsE.Units(Else(idx).UnitC.Unit, level, owner).Amount += 1;

            WhereUnitsE.HaveUnit(Else(idx).UnitC.Unit, level, owner, idx).Have = false;
            Else(idx).UnitC.Reset();
        }
        public  void SetScout(in byte idx)
        {
            ref var ownUnitC = ref Else(idx).OwnerC;

            ref var twC = ref ToolWeapon(idx).ToolWeaponC;
            ref var levTWC = ref ToolWeapon(idx).LevelC;


            WhereUnitsE.HaveUnit(Else(idx).UnitC.Unit, Else(idx).LevelC.Level, ownUnitC.Player, idx).Have = false;

            Else(idx).UnitC.Unit = UnitTypes.Scout;
            Else(idx).LevelC.Level = LevelTypes.First;
            if (twC.HaveTW)
            {
                InventorToolWeaponE.ToolWeapons<AmountC>(twC.ToolWeapon, levTWC.Level, ownUnitC.Player).Amount += 1;
                Reset(idx);
            }

            WhereUnitsE.HaveUnit(UnitTypes.Scout, LevelTypes.First, Else(idx).OwnerC.Player, idx).Have = true;
        }


        public  void Take(in byte idx, in int taking = 1)
        {
            ref var tw = ref ToolWeapon(idx).ToolWeaponC;
            ref var prot = ref ToolWeapon(idx).Protection;

            if (!tw.IsShield) throw new Exception();
            if (!prot.Have) throw new Exception();

            prot.Take(taking);

            if (!prot.Have) tw.Reset();
        }

        public  void Set(byte idx_from, in byte idx_to)
        {
            ToolWeapon(idx_to).ToolWeaponC.ToolWeapon = ToolWeapon(idx_from).ToolWeaponC.ToolWeapon;
            ToolWeapon(idx_to).LevelC.Level = ToolWeapon(idx_from).LevelC.Level;

            ToolWeapon(idx_to).Protection = ToolWeapon(idx_from).Protection;
        }
        public  void Reset(in byte idx)
        {
            ToolWeapon(idx).ToolWeaponC.Reset();
            ToolWeapon(idx).LevelC.Reset();

            ToolWeapon(idx).Protection.Reset();
        }

        public  void SetNew(in byte idx, in ToolWeaponTypes tw, in LevelTypes level)
        {
            ToolWeapon(idx).ToolWeaponC.ToolWeapon = tw;
            ToolWeapon(idx).LevelC.Level = level;

            if (tw == ToolWeaponTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        ToolWeapon(idx).Protection.Amount = 1;
                        break;

                    case LevelTypes.Second:
                        ToolWeapon(idx).Protection.Amount = 3;
                        break;

                    default: throw new Exception();
                }
            }
        }
    }
}