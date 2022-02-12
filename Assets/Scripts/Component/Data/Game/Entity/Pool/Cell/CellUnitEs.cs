using ECS;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellUnitEs
    {
        readonly Dictionary<ButtonTypes, CellUnitAbilityButtonsE> _uniqueButtons;
        readonly Dictionary<AbilityTypes, CellUnitAbilityE> _abilities;
        readonly Dictionary<PlayerTypes, CellUnitVisibleE> _cellUnitVisibles;
        public CellUnitAbilityButtonsE AbilityButton(in ButtonTypes button) => _uniqueButtons[button];
        public CellUnitAbilityE Ability(in AbilityTypes ability) => _abilities[ability];
        public CellUnitVisibleE VisibleE(in PlayerTypes player) => _cellUnitVisibles[player];
        public HashSet<AbilityTypes> CooldownKeys
        {
            get
            {
                var keys = new HashSet<AbilityTypes>();
                foreach (var item in _abilities) keys.Add(item.Key);
                return keys;
            }
        }

        public readonly byte Idx;

        public readonly CellUnitE TypeE;
        public readonly CellUnitLevelE LevelE;
        public readonly CellUnitOwnerE OwnerE;
        public readonly CellUnitConditonE ConditionE;
        public readonly CellUnitCornedE CornedE;
        public readonly CellUnitWhoLastDiedHereE WhoLastDiedHereE;

        public readonly CellUnitExtraToolWeaponE ExtraToolWeaponE;
        public readonly CellUnitMainToolWeaponE MainToolWeaponE;

        public readonly CellUnitStatEs StatEs;
        public readonly CellUnitEffectEs EffectEs;


        public int DamageAttack(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs, in AttackTypes attack)
        {
            //var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            var standDamage = CellUnitMainDamageValues.StandDamage(TypeE.UnitTC.Unit, cellEs.UnitEs.LevelE.LevelTC.Level);


            if (!TypeE.UnitTC.IsAnimal)
                if (statUpgEs.Upgrade(UnitStatTypes.Damage, cellEs.UnitEs, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    upgPerc = 0.3f;
                }



            float powerDamege = standDamage;

            powerDamege += standDamage * CellUnitMainDamageValues.PercentExtraDamageTW(cellEs.UnitEs.ExtraToolWeaponE.ToolWeaponTC.ToolWeapon);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * CellUnitMainDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public int DamageOnCell(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs)
        {
            float powerDamege = DamageAttack(cellEs, statUpgEs, AttackTypes.Simple);

            var standDamage = CellUnitMainDamageValues.StandDamage(TypeE.UnitTC.Unit, cellEs.UnitEs.LevelE.LevelTC.Level);

            powerDamege += standDamage * CellUnitMainDamageValues.ProtRelaxPercent(cellEs.UnitEs.ConditionE.ConditionTC.Condition);
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
        

        internal CellUnitEs(in byte idx, in EcsWorld gameW)
        {
            Idx = idx;

            _uniqueButtons = new Dictionary<ButtonTypes, CellUnitAbilityButtonsE>();
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _uniqueButtons.Add(buttonT, new CellUnitAbilityButtonsE(gameW));
            }

            _abilities = new Dictionary<AbilityTypes, CellUnitAbilityE>();
            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _abilities.Add(ability, new CellUnitAbilityE(ability, idx, gameW));
            }

            _cellUnitVisibles = new Dictionary<PlayerTypes, CellUnitVisibleE>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _cellUnitVisibles[player] = new CellUnitVisibleE(gameW);
            }

            TypeE = new CellUnitE(idx, gameW);
            ConditionE = new CellUnitConditonE(idx, gameW);
            LevelE = new CellUnitLevelE(idx, gameW);
            ExtraToolWeaponE = new CellUnitExtraToolWeaponE(idx, gameW);
            MainToolWeaponE = new CellUnitMainToolWeaponE(idx, gameW);
            CornedE = new CellUnitCornedE(idx, gameW);
            WhoLastDiedHereE = new CellUnitWhoLastDiedHereE(idx, gameW);

            StatEs = new CellUnitStatEs(idx, gameW);
            EffectEs = new CellUnitEffectEs(idx, gameW);
        }

        void Reset()
        {
            TypeE.UnitT = UnitTypes.None;
            CornedE.IsRight = false;
            OwnerE.PlayerT = PlayerTypes.None;
            LevelE.LevelT = LevelTypes.None;
        }
        void Set(in byte idx_to, in Entities ents)
        {
            ents.UnitTypeE(idx_to).UnitT = TypeE.UnitT;
            ents.UnitEs(idx_to).CornedE.IsRight = CornedE.IsRight;
            ents.UnitOwnerE(idx_to).PlayerT = OwnerE.OwnerC.Player;
            ents.UnitEs(idx_to).ConditionE.Reset();
            ents.UnitEs(idx_to).LevelE.Set(LevelE.LevelTC.Level);

            ents.UnitStatHpE(idx_to).Health = StatEs.Hp.Health;
            ents.UnitStatStepE(idx_to).Steps = StatEs.StepE.Steps;
            ents.UnitStatWaterE(idx_to).Water = StatEs.WaterE.Water;

            ents.UnitEffectEs(idx_to).StunE.Stun = EffectEs.StunE.Stun;
            ents.UnitEffectEs(idx_to).ShieldE.Shield = EffectEs.ShieldE.Shield;
            ents.UnitEffectEs(idx_to).FrozenArrowE.IsFrozenArraw = EffectEs.FrozenArrowE.IsFrozenArraw;

            ents.UnitExtraTWE(idx_to).ToolWeaponT = ExtraToolWeaponE.ToolWeaponT;
            ents.UnitExtraTWE(idx_to).LevelT = ExtraToolWeaponE.LevelT;
            ents.UnitExtraTWE(idx_to).Protection = ExtraToolWeaponE.Protection;

            ents.UnitEs(idx_to).MainToolWeaponE.ToolWeapon = MainToolWeaponE.ToolWeapon;
            ents.UnitEs(idx_to).MainToolWeaponE.Level = MainToolWeaponE.Level;

            foreach (var abilityT in CooldownKeys) ents.UnitEs(idx_to).Ability(abilityT).Shift(Ability(abilityT));
        }
        public void Teleport(in byte idx_to, in Entities ents)
        {
            Set(idx_to, ents);
            Reset();
        }
        public void Shift(in byte idx_to, in bool withDestoyBuilding, in Entities ents)
        {
            Set(idx_to, ents);
            Reset();

            if (!ents.CellSpaceWorker.TryGetDirect(Idx, idx_to, out var direct)) throw new Exception();

            if (!ents.UnitTypeE(idx_to).Is(UnitTypes.Undead))
            {
                if (ents.EnvironmentEs(Idx).AdultForest.HaveEnvironment)
                {
                    ents.CellEs(Idx).TrailEs.Trail(direct).SetNew();
                }
                if (ents.EnvironmentEs(idx_to).AdultForest.HaveEnvironment)
                {
                    ents.CellEs(idx_to).TrailEs.Trail(direct.Invert()).SetNew();
                }

                if (ents.RiverEs(idx_to).RiverE.HaveRiverNear)
                {
                    ents.UnitStatWaterE(idx_to).SetMax(ents.UnitEs(idx_to), ents.UnitStatUpgradesEs);
                }
            }

            ents.EffectEs(idx_to).FireE.TryFireAfterShift(ents.Cells);

            if (withDestoyBuilding)
            {
                if (ents.BuildE(idx_to).HaveBuilding && !ents.BuildE(idx_to).Is(BuildingTypes.City))
                {
                    if (!ents.BuildE(idx_to).Is(ents.UnitEs(idx_to).OwnerE.OwnerC.Player))
                    {
                        ents.BuildE(idx_to).Destroy(ents);
                    }
                }
            }
        }
        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit, in Entities ents, in (ToolWeaponTypes, LevelTypes, ToolWeaponTypes, LevelTypes) tw = default)
        {
            TypeE.UnitT = unit.Item1;
            LevelE.Set(unit.Item2);
            OwnerE.PlayerT = unit.Item3;
            ConditionE.Set(unit.Item4);
            CornedE.IsRight = unit.Item5;

            StatEs.Hp.SetMax();
            StatEs.StepE.SetMax(TypeE);
            StatEs.WaterE.SetMax(this, ents.UnitStatUpgradesEs);

            EffectEs.StunE.Reset();
            EffectEs.ShieldE.Reset();
            EffectEs.FrozenArrowE.Disable();

            MainToolWeaponE.SetNew(tw.Item1, tw.Item2);
            ExtraToolWeaponE.SetNew(tw.Item3, tw.Item4);
            foreach (var item in CooldownKeys) Ability(item).SetNew();

            if (TypeE.Is(UnitTypes.Pawn))
            {
                MainToolWeaponE.ToolWeapon = ToolWeaponTypes.Axe;
                MainToolWeaponE.Level = LevelTypes.First;
            }
        }


        public void Teleport_Master(in AbilityTypes ability, in Player sender, in Entities ents)
        {
            var idx_0 = Idx;

            if (StatEs.StepE.Have(ability))
            {
                if (ents.BuildE(idx_0).Is(BuildingTypes.Teleport))
                {
                    var idx_start = ents.StartTeleportE.WhereC.Idx;
                    var idx_end = ents.EndTeleportE.WhereC.Idx;

                    if (ents.EndTeleportE.HaveEnd && idx_start == idx_0)
                    {
                        if (!ents.UnitTypeE(idx_end).HaveUnit)
                        {
                            StatEs.StepE.Take(ability);

                            Teleport(idx_end, ents);
                        }
                    }
                    else if (ents.StartTeleportE.HaveStart && idx_end == idx_0)
                    {
                        if (!ents.UnitTypeE(idx_start).HaveUnit)
                        {
                            StatEs.StepE.Take(ability);

                            Teleport(idx_start, ents);
                        }
                    }
                }
            }
            else
            {
                ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void InvokeSkeletons_Master(in AbilityTypes ability, in Player sender, in Entities ents)
        {
            var idx_0 = Idx;

            if (StatEs.StepE.Have(ability))
            {
                StatEs.StepE.Take(ability);

                foreach (var idx_1 in ents.CellSpaceWorker.GetIdxsAround(idx_0))
                {
                    if (!ents.UnitTypeE(idx_1).HaveUnit && !ents.EnvMountainE(idx_1).HaveEnvironment)
                    {
                        ents.UnitEs(idx_1).SetNew((UnitTypes.Skeleton, LevelTypes.First, OwnerE.PlayerT, ConditionUnitTypes.None, false), ents);
                    }
                   
                }
            }
            else
            {
                ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}