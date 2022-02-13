using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitE : CellEntityAbstract
    {
        ref UnitTC UnitTC => ref Ent.Get<UnitTC>();
        ref PlayerTC PlayerTC => ref Ent.Get<PlayerTC>();
        ref LevelTC LevelTC => ref Ent.Get<LevelTC>();
        ref ConditionUnitTC ConditionTC => ref Ent.Get<ConditionUnitTC>();
        ref IsRightArcherC IsRightArcherC => ref Ent.Get<IsRightArcherC>();
        ref HealthC HealthC => ref Ent.Get<HealthC>();
        ref StepC StepC => ref Ent.Get<StepC>();
        ref WaterC WaterC => ref Ent.Get<WaterC>();
        ref StunC StunC => ref Ent.Get<StunC>();
        ref ShieldEffectC ShieldEffectC => ref Ent.Get<ShieldEffectC>();
        ref ShootsFrozenArrawC FrozenArrawC => ref Ent.Get<ShootsFrozenArrawC>();


        public UnitTypes Unit
        {
            get => UnitTC.Unit;
            private set => UnitTC.Unit = value;
        }
        public LevelTypes Level
        {
            get => LevelTC.Level;
            private set => LevelTC.Level = value;
        }
        public PlayerTypes Owner
        {
            get => PlayerTC.Player;
            private set => PlayerTC.Player = value;
        }
        public ConditionUnitTypes Condition
        {
            get => ConditionTC.Condition;
            set => ConditionTC.Condition = value;
        }
        public bool IsRightArcher
        {
            get => IsRightArcherC.IsRight;
            set => IsRightArcherC.IsRight = value;
        }


        public bool Is(params UnitTypes[] unit) => UnitTC.Is(unit);
        public bool Is(params LevelTypes[] level) => LevelTC.Is(level);
        public bool Is(params PlayerTypes[] owners) => PlayerTC.Is(owners);
        public bool Is(params ConditionUnitTypes[] conds) => ConditionTC.Is(conds);

        public bool HaveUnit => UnitTC.HaveUnit;
        public bool IsMelee(in CellUnitMainToolWeaponE mainTWE)
        {
            if (Is(UnitTypes.Pawn))
            {
                if (mainTWE.Is(ToolWeaponTypes.BowCrossbow)) return false;
                else return true;
            }
            else
            {
                return UnitTC.IsMelee;
            }
        }
        public bool IsAnimal => UnitTC.IsAnimal;


        #region Stats

        #region Hp

        public float Health
        {
            get => HealthC.Health;
            private set => HealthC.Health = value;
        }
        public bool IsAlive => HealthC.IsAlive;
        public bool IsHpDeathAfterAttack => Health <= CellUnitMainDamageValues.HP_FOR_DEATH_AFTER_ATTACK;
        public bool HaveMaxHp => Health >= CellUnitStatHpValues.MAX_HP;
        public bool MoreMaxHp => Health > CellUnitStatHpValues.MAX_HP;

        #endregion


        #region Steps

        public float Steps
        {
            get => StepC.Steps;
            private set => StepC.Steps = value;
        }
        public bool HaveSteps => StepC.HaveSteps;
        public bool HaveMaxSteps => Steps >= MaxAmountSteps;
        public float MaxAmountSteps => CellUnitStatStepValues.MaxAmountSteps(Unit, false);

        public bool HaveStepsForAbility(in AbilityTypes ability) => Steps >= CellUnitStatStepValues.NeedSteps(ability);
        public bool HaveStepsAfterCondition(in ConditionUnitTypes cond) => Steps >= CellUnitStatStepValues.NeedSteps(cond);
        public bool HaveStepsAfterGiveTakeTW(in ToolWeaponTypes tw) => Steps >= CellUnitStatStepValues.NeedSteps(tw);
        float StepsForShiftOrAttack(in UnitTypes unit, in DirectTypes dirMove_to, in CellEs cellEs_to)
        {
            var needSteps = 1f;

            if (unit != UnitTypes.Undead)
            {
                if (cellEs_to.EnvironmentEs.Fertilizer.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.Fertilizer.EnvT);
                if (cellEs_to.EnvironmentEs.YoungForest.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.YoungForest.EnvT);
                if (cellEs_to.EnvironmentEs.AdultForest.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.AdultForest.EnvT);
                if (cellEs_to.EnvironmentEs.Hill.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.Hill.EnvT);

                if (cellEs_to.TrailEs.Trail(dirMove_to.Invert()).HaveTrail) needSteps--;
            }

            return needSteps;
        }
        public bool CanShift(in UnitTypes unit, in DirectTypes dirMove_to, in CellEs cellEs_to)
        {
            return StepsForShiftOrAttack(unit, dirMove_to, cellEs_to) <= Steps;
        }

        #endregion


        #region Water

        public float Water
        {
            get => WaterC.Water;
            private set => WaterC.Water = value;
        }
        public bool HaveWater => Water > 0;
        public bool Have(in AbilityTypes ability) => Water >= CellUnitStatWaterValues.Need(ability);
        public float MaxWater(in UnitStatUpgradesEs statUpgEs)
        {
            var maxWater = CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS;

            if (statUpgEs.Upgrade(UnitStatTypes.Water, this, UpgradeTypes.PickCenter).HaveUpgrade.Have)
            {
                return maxWater += (int)(maxWater * 0.5f);
            }

            return maxWater;
        }

        #endregion

        #endregion


        #region Effects

        public int Stun
        {
            get => StunC.Stun;
            set => StunC.Stun = value;
        }
        public bool IsStunned => Stun > 0;


        public int Shield
        {
            get => ShieldEffectC.Protection;
            set => ShieldEffectC.Protection = value;
        }
        public bool HaveShieldEffect => Shield > 0;


        public int ShootsFrozenArraw
        {
            get => FrozenArrawC.Shoots;
            set => FrozenArrawC.Shoots = value;
        }
        public bool HaveFrozenArrawEffect => ShootsFrozenArraw > 0;

        #endregion


        #region Damage

        public float DamageAttack(in CellUnitEs unitEs, in UnitStatUpgradesEs statUpgEs, in AttackTypes attack)
        {
            //var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            if (!IsAnimal) 
                if (statUpgEs.Upgrade(UnitStatTypes.Damage, this, UpgradeTypes.PickCenter).HaveUpgrade.Have) 
                    upgPerc = 0.3f;


            var standDamage = CellUnitMainDamageValues.StandDamage(Unit, Level);
            float powerDamege = standDamage;

            if(unitEs.MainToolWeaponE.HaveToolWeapon) powerDamege += standDamage * CellUnitMainDamageValues.PercentDamageTW(unitEs.MainToolWeaponE);
            if (unitEs.ExtraToolWeaponE.HaveToolWeapon) powerDamege += standDamage * CellUnitMainDamageValues.PercentExtraDamageTW(unitEs.ExtraToolWeaponE);

            if (attack == AttackTypes.Unique) powerDamege += standDamage * CellUnitMainDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return powerDamege;
        }
        public float DamageOnCell(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs)
        {
            float powerDamege = DamageAttack(cellEs.UnitEs, statUpgEs, AttackTypes.Simple);

            var standDamage = CellUnitMainDamageValues.StandDamage(Unit, Level);

            powerDamege += standDamage * CellUnitMainDamageValues.ProtRelaxPercent(Condition);
            if (cellEs.BuildEs.BuildingE.HaveBuilding) powerDamege += standDamage * CellBuildingValues.ProtectionPercent(cellEs.BuildEs.BuildingE.Building);

            float protectionPercent = 0;

            var envEs = cellEs.EnvironmentEs;

            if (envEs.Fertilizer.HaveEnvironment) protectionPercent += envEs.Fertilizer.ProtectionPercent;
            if (envEs.YoungForest.HaveEnvironment) protectionPercent += envEs.YoungForest.ProtectionPercent;
            if (envEs.AdultForest.HaveEnvironment) protectionPercent += envEs.AdultForest.ProtectionPercent;
            if (envEs.Hill.HaveEnvironment) protectionPercent += envEs.Hill.ProtectionPercent;
            if (envEs.Mountain.HaveEnvironment) protectionPercent += envEs.Mountain.ProtectionPercent;

            powerDamege += standDamage * protectionPercent;

            return powerDamege;
        }

        #endregion



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


        internal CellUnitE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {

        }

        void Set(in byte idx_to, in Entities ents)
        {
            var idx_0 = Idx;

            ents.UnitE(idx_to).Unit = Unit;
            ents.UnitE(idx_to).IsRightArcher = IsRightArcher;
            ents.UnitE(idx_to).Owner = Owner;
            ents.UnitE(idx_to).Condition = ConditionUnitTypes.None;
            ents.UnitE(idx_to).Level = Level;

            ents.UnitE(idx_to).Health = Health;
            ents.UnitE(idx_to).Steps = Steps;
            ents.UnitE(idx_to).Water = Water;

            ents.UnitE(idx_to).Stun = ents.UnitE(idx_0).Stun;
            ents.UnitE(idx_to).Shield = ents.UnitE(idx_0).Shield;
            ents.UnitE(idx_to).ShootsFrozenArraw = ents.UnitE(idx_0).ShootsFrozenArraw;

            ents.ExtraTWE(idx_to).ToolWeapon = ents.UnitEs(idx_0).ExtraToolWeaponE.ToolWeapon;
            ents.ExtraTWE(idx_to).LevelT = ents.UnitEs(idx_0).ExtraToolWeaponE.LevelT;
            ents.ExtraTWE(idx_to).Protection = ents.UnitEs(idx_0).ExtraToolWeaponE.Protection;

            ents.UnitEs(idx_to).MainToolWeaponE.ToolWeapon = ents.UnitEs(idx_0).MainToolWeaponE.ToolWeapon;
            ents.UnitEs(idx_to).MainToolWeaponE.Level = ents.UnitEs(idx_0).MainToolWeaponE.Level;

            foreach (var abilityT in ents.UnitEs(idx_0).CooldownKeys) ents.UnitEs(idx_to).Ability(abilityT).Cooldown =  ents.UnitEs(idx_0).Ability(abilityT).Cooldown;
        }
        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit, in UnitStatUpgradesEs statUpgEs, in CellUnitEs unitEs)
        {
            Unit = unit.Item1;
            Level = unit.Item2;
            Owner = unit.Item3;
            Condition = unit.Item4;
            IsRightArcher = unit.Item5;

            SetMaxHp();
            SetMaxSteps();
            SetMaxWater(statUpgEs);

            Stun = 0;
            Shield = 0;
            ShootsFrozenArraw = 0;

            foreach (var item in unitEs.CooldownKeys) unitEs.Ability(item).Cooldown = 0;
        }
        public void SetNewPawn(in (LevelTypes, PlayerTypes, ConditionUnitTypes) unit, in UnitStatUpgradesEs statUpgEs, in CellUnitEs unitEs)
        {
            SetNew((UnitTypes.Pawn, unit.Item1, unit.Item2, unit.Item3, false), statUpgEs, unitEs);

            unitEs.MainToolWeaponE.ToolWeapon = ToolWeaponTypes.Axe;
            unitEs.MainToolWeaponE.Level = LevelTypes.First;
        }
        public void Kill(in Entities ents)
        {
            if (!HaveUnit) throw new Exception("There's not unit");

            var idx_0 = Idx;

            if (Is(UnitTypes.King))
            {
                ents.WinnerE.Winner.Player = Owner;
            }
            else if (Is(UnitTypes.Scout) || UnitTC.IsHero)
            {
                ents.ScoutHeroCooldownE(this).SetCooldownAfterKill(UnitTC.Unit);
                ents.InventorUnitsEs.Units(UnitTC.Unit, Level, Owner).AddUnit();
            }

            ents.UnitEs(idx_0).WhoLastDiedHereE.SetLastDied(this);

            Unit = UnitTypes.None;
        }
        public void AddToInventorAndRemove(in InventorUnitsEs invUnitsEs)
        {
            invUnitsEs.Units(Unit, Level, Owner).AddUnit();

            Unit = UnitTypes.None;
        }
        public void Upgrade()
        {
            if (LevelTC.Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) throw new Exception();

            LevelTC.Level = LevelTypes.Second;
        }
        public void Shift(in byte idx_to, in bool withDestoyBuilding, in Entities ents)
        {
            Set(idx_to, ents);
            Unit = UnitTypes.None;

            if (!ents.CellSpaceWorker.TryGetDirect(Idx, idx_to, out var direct)) throw new Exception();

            if (!ents.UnitE(idx_to).Is(UnitTypes.Undead))
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
                    ents.UnitE(idx_to).SetMaxWater(ents.UnitStatUpgradesEs);
                }
            }

            ents.EffectEs(idx_to).FireE.TryFireAfterShift(ents.Cells);

            if (withDestoyBuilding)
            {
                if (ents.BuildingE(idx_to).HaveBuilding && !ents.BuildingE(idx_to).Is(BuildingTypes.City))
                {
                    if (!ents.BuildingE(idx_to).Is(ents.UnitE(idx_to).Owner))
                    {
                        ents.BuildingE(idx_to).Destroy(ents);
                    }
                }
            }
        }
        public void Teleport(in byte idx_to, in Entities ents)
        {
            Set(idx_to, ents);
            Unit = UnitTypes.None;
        }
        public void ToggleArcherSide() => IsRightArcher = !IsRightArcher;

        public void Shift_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = Idx;
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;

            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                if (!e.CellSpaceWorker.TryGetDirect(idx_from, idx_to, out var dir)) throw new Exception();
                TakeSteps(e.UnitE(idx_from).StepsForShiftOrAttack(e.UnitE(idx_from).Unit, dir, e.CellEs(idx_to)));

                e.UnitE(idx_from).Shift(idx_to, true, e);

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void Attack_Master(in byte idx_to, in Entities e)
        {
            var idx_from = Idx;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, whoseMove, out var attack))
            {
                e.UnitE(idx_from).Steps = 0;
                e.UnitE(idx_from).Condition = ConditionUnitTypes.None;


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += DamageAttack(e.UnitEs(idx_from), e.UnitStatUpgradesEs, attack);

                if (e.UnitEs(idx_from).UnitE.UnitTC.IsMelee)
                    e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += e.UnitE(idx_to).DamageOnCell(e.CellEs(idx_to), e.UnitStatUpgradesEs);


                e.CellSpaceWorker.TryGetDirect(idx_from, idx_to, out var dirAttack);


                if (e.SunSidesE.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in e.SunSidesE.RaysSun)
                    {
                        if (dirAttack == dir) isSunnedUnit = false;
                    }

                    if (isSunnedUnit)
                    {
                        powerDam_from *= 0.9f;
                    }
                }






                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = CellUnitStatHpValues.MAX_HP;
                var minDamage = 0;

                //if (!e.UnitE(idx_to).IsMelee) powerDam_to /= 2;

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


                if (e.UnitE(idx_from).IsMelee(e.MainTWE(idx_from)))
                {
                    if (e.UnitE(idx_from).HaveShieldEffect)
                    {
                        e.UnitE(idx_from).TakeShield();
                    }
                    else if (e.ExtraTWE(idx_from).Is(ToolWeaponTypes.Shield))
                    {
                        e.ExtraTWE(idx_from).BreakShield();
                    }
                    else if (minus_from > 0)
                    {
                        e.UnitE(idx_from).TakeHp(e, minus_from);
                    }
                }
                else
                {
                    if (e.UnitE(idx_from).HaveFrozenArrawEffect)
                    {
                        e.UnitE(idx_from).ShootsFrozenArraw = 0;

                        e.UnitE(idx_to).Stun = 2;
                    }
                }

                if (e.UnitE(idx_to).HaveShieldEffect)
                {
                    e.UnitE(idx_to).TakeShield();
                }
                else if (e.ExtraTWE(idx_to).Is(ToolWeaponTypes.Shield))
                {
                    e.ExtraTWE(idx_to).BreakShield();
                }
                else if (minus_to > 0)
                {
                    var wasUnit = e.UnitE(idx_to).Unit;
                    e.UnitE(idx_to).TakeHp(e, minus_to);

                    if (!e.UnitE(idx_to).HaveUnit)
                    {
                        if (wasUnit == UnitTypes.Camel)
                        {
                            e.InventorResourcesEs.Resource(ResourceTypes.Food, e.UnitE(idx_from).Owner).Add(ResourcesInInventorValues.AMOUNT_FOOD_AFTER_KILL_CAMEL);
                        }

                        if (e.UnitE(idx_from).HaveUnit)
                        {
                            if (e.UnitE(idx_from).IsMelee(e.MainTWE(idx_from)))
                            {
                                e.UnitE(idx_from).Shift(idx_to, true, e);
                            }
                        }

                    }
                }

                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Disable();
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Disable();
            }
        }
        public void SetUnit_Master(in UnitTypes unit, in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;


            if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(whoseMove, idx_0).Can)
            {
                if (unit == UnitTypes.Pawn)
                {
                    e.PeopleInCityE(whoseMove).People--;
                    e.UnitE(idx_0).SetNewPawn((LevelTypes.First, whoseMove, ConditionUnitTypes.None), e.UnitStatUpgradesEs, e.UnitEs(idx_0));
                }
                else
                {
                    var levUnit = LevelTypes.None;

                    if (e.InventorUnitsEs.Units(unit, LevelTypes.Second, whoseMove).HaveUnits)
                    {
                        e.InventorUnitsEs.Units(unit, LevelTypes.Second, whoseMove).TakeUnit();
                        levUnit = LevelTypes.Second;
                    }
                    else
                    {
                        e.InventorUnitsEs.Units(unit, LevelTypes.First, whoseMove).TakeUnit();
                        levUnit = LevelTypes.First;
                    }
                    e.UnitE(idx_0).SetNew((unit, levUnit, whoseMove, ConditionUnitTypes.None, false), e.UnitStatUpgradesEs, e.UnitEs(idx_0));
                }

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void UpgradeUnit_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;

            if (e.UnitE(idx_0).HaveMaxHp)
            {
                //if (UnitE(idx_0).Have(ability))
                //{
                if (e.InventorResourcesEs.CanUpgradeUnit(whoseMove, Unit, out var needRes))
                {
                    e.InventorResourcesEs.BuyUpgradeUnit(whoseMove, Unit);

                    Upgrade();
                    //UnitE(idx_0).Take(ability);

                    SetMaxHp();

                    e.RpcE.SoundToGeneral(sender, ClipTypes.UpgradeMelee);
                }
                else
                {
                    e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                }
                //}
                //else
                //{
                //    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                //}
            }
            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
        public void Condition_Master(in ConditionUnitTypes cond, in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            switch (cond)
            {
                case ConditionUnitTypes.None:
                    e.UnitE(idx_0).Condition = ConditionUnitTypes.None;
                    break;

                case ConditionUnitTypes.Protected:
                    if (e.UnitE(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitE(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitE(idx_0).HaveSteps)
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitE(idx_0).TakeSteps(cond);
                        e.UnitE(idx_0).Condition = cond;
                    }

                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (e.UnitE(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitE(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitE(idx_0).HaveSteps)
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitE(idx_0).Condition = cond;
                        e.UnitE(idx_0).TakeSteps(cond);
                    }

                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
        public void Teleport_Master(in AbilityTypes ability, in Player sender, in Entities ents)
        {
            var idx_0 = Idx;

            if (ents.UnitE(idx_0).HaveStepsForAbility(ability))
            {
                if (ents.BuildingE(idx_0).Is(BuildingTypes.Teleport))
                {
                    var idx_start = ents.StartTeleportE.WhereC.Idx;
                    var idx_end = ents.EndTeleportE.WhereC.Idx;

                    if (ents.EndTeleportE.HaveEnd && idx_start == idx_0)
                    {
                        if (!ents.UnitE(idx_end).HaveUnit)
                        {
                            ents.UnitE(idx_0).TakeSteps(ability);

                            Teleport(idx_end, ents);
                        }
                    }
                    else if (ents.StartTeleportE.HaveStart && idx_end == idx_0)
                    {
                        if (!ents.UnitE(idx_start).HaveUnit)
                        {
                            ents.UnitE(idx_0).TakeSteps(ability);

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

            if (ents.UnitE(idx_0).HaveStepsForAbility(ability))
            {
                ents.UnitE(idx_0).TakeSteps(ability);

                foreach (var idx_1 in ents.CellSpaceWorker.GetIdxsAround(idx_0))
                {
                    if (!ents.UnitE(idx_1).HaveUnit && !ents.MountainE(idx_1).HaveEnvironment)
                    {
                        ents.UnitE(idx_1).SetNew((UnitTypes.Skeleton, LevelTypes.First, Owner, ConditionUnitTypes.None, false), ents.UnitStatUpgradesEs, ents.UnitEs(idx_1));
                    }

                }
            }
            else
            {
                ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }


        #region Stats

        #region Hp

        public void SetMaxHp() => Health = CellUnitStatHpValues.MAX_HP;
        public void SetMinHp() => Health = 0;

        public void AddHp(in float adding = 1)
        {
            if (!HaveMaxHp)
            {
                Health += adding;
                if (MoreMaxHp) SetMaxHp();
            }
        }

        public void TakeHp(in Entities ents, in float taking = 1)
        {
            if (Health > 0)
            {
                Health -= taking;
                if (IsHpDeathAfterAttack) Health = 0;
                if (!IsAlive) Kill(ents);
            }
        }
        public void TakeHp(in AbilityTypes ability, in Entities Es) => TakeHp(Es, CellUnitStatHpValues.Damage(ability));

        #endregion


        #region Steps

        public void SetMaxSteps() => Steps = MaxAmountSteps;
        public void SetMinSteps() => Steps = 0;

        public void TakeSteps(in float taking = 0.1f) => StepC.Take(taking);
        public void TakeSteps(in AbilityTypes ability) => TakeSteps(CellUnitStatStepValues.NeedSteps(ability));
        public void TakeSteps(in RpcMasterTypes rpc) => TakeSteps(CellUnitStatStepValues.NeedSteps(rpc));
        public void TakeSteps(in ConditionUnitTypes cond) => TakeSteps(CellUnitStatStepValues.NeedSteps(cond));
        public void TakeSteps(in ToolWeaponTypes tw)
        {
            Steps -= CellUnitStatStepValues.NeedSteps(tw);
        }

        public void AddSteps(in float adding)
        {
            Steps += adding;
        }

        #endregion


        #region Water

        public void SetMaxWater(in UnitStatUpgradesEs statUpgEs) => Water = MaxWater(statUpgEs);
        public void SetMinWater() => Water = 0;

        public void AddWater(in float adding = 1)
        {
            Water += adding;
        }

        public void TakeWater(in float taking = 1)
        {
            Water -= taking;
        }
        public void TakeWater(in AbilityTypes ability)
        {
            Water -= CellUnitStatWaterValues.Need(ability);
        }

        #endregion

        #endregion


        #region Effects

        public void TakeStun(in int taking = 1)
        {
            Stun -= taking;
        }
        public void SetStunAfterAbility(in AbilityTypes abilityT)
        {
            switch (abilityT)
            {
                case AbilityTypes.StunElfemale:
                    StunC.Stun = 4;
                    break;

                case AbilityTypes.ActiveAroundBonusSnowy:
                    StunC.Stun = 2;
                    break;

                case AbilityTypes.DirectWave:
                    StunC.Stun = 2;
                    break;

                default: throw new System.Exception();
            }
        }

        #region Shield

        public void SetShield(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.ActiveAroundBonusSnowy:
                    Shield = 1;
                    break;

                case AbilityTypes.DirectWave:
                    Shield = 1;
                    break;

                default: throw new Exception();
            }
        }
        public void TakeShield(in int taking = 1)
        {
            Shield -= taking;
        }

        #endregion

        #endregion
    }
}