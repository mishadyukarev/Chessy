using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitE : CellEntityAbstract
    {
        ref PlayerTC OwnerCRef => ref Ent.Get<PlayerTC>();
        ref UnitTC UnitTCRef => ref Ent.Get<UnitTC>();
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();
        ref ConditionUnitTC ConditionTCRef => ref Ent.Get<ConditionUnitTC>();
        ref IsRightArcherC IsCornedRef => ref Ent.Get<IsRightArcherC>();
        ref AmountC HealthRef => ref Ent.Get<AmountC>();
        ref StepC StepsCRef => ref Ent.Get<StepC>();
        ref WaterC WaterCRef => ref Ent.Get<WaterC>();


        public PlayerTC OwnerC => Ent.Get<PlayerTC>();
        public UnitTC UnitTC => Ent.Get<UnitTC>();


        public UnitTypes Unit
        {
            get => UnitTCRef.Unit;
            set => UnitTCRef.Unit = value;
        }
        public LevelTypes Level
        {
            get => LevelTCRef.Level;
            set => LevelTCRef.Level = value;
        }
        public PlayerTypes Owner
        {
            get => OwnerC.Player;
            set => OwnerCRef.Player = value;
        }
        public ConditionUnitTypes Condition
        {
            get => ConditionTCRef.Condition;
            set => ConditionTCRef.Condition = value;
        }
        public bool IsRightArcher
        {
            get => IsCornedRef.IsRight;
            set => IsCornedRef.IsRight = value;
        }
        public int Health
        {
            get => HealthRef.Amount;
            set => HealthRef.Amount = value;
        }
        public int Steps
        {
            get => StepsCRef.Steps;
            set => StepsCRef.Steps = value;
        }
        public int Water
        {
            get => WaterCRef.Water;
            set => WaterCRef.Water = value;
        }

        public bool Is(params UnitTypes[] unit) => UnitTCRef.Is(unit);
        public bool Is(params LevelTypes[] level) => LevelTCRef.Is(level);
        public bool Is(params PlayerTypes[] owners) => OwnerC.Is(owners);
        public bool Is(params ConditionUnitTypes[] conds) => ConditionTCRef.Is(conds);

        public bool HaveUnit => UnitTCRef.Unit != UnitTypes.None && UnitTCRef.Unit != UnitTypes.End;
        public bool IsMelee => UnitTCRef.IsMelee;
        public bool IsAnimal => UnitTCRef.IsAnimal;


        #region Stats

        #region Hp

        public bool IsHpDeathAfterAttack => Health <= CellUnitMainDamageValues.HP_FOR_DEATH_AFTER_ATTACK;
        public bool HaveMaxHp => Health >= CellUnitStatHpValues.MAX_HP;
        public bool IsAlive => Health > 0;

        #endregion


        #region Steps

        public bool HaveSteps => Steps > 0;
        public bool HaveStepsForAbility(in AbilityTypes ability) => Steps >= CellUnitStatStepValues.NeedSteps(ability);
        public bool Have(in ConditionUnitTypes cond) => Steps >= CellUnitStatStepValues.NeedSteps(cond);
        public bool Have(in ToolWeaponTypes tw) => Steps >= CellUnitStatStepValues.NeedSteps(tw);
        public bool HaveMaxSteps => Steps >= MaxAmountSteps;
        public int MaxAmountSteps => CellUnitStatStepValues.MaxAmountSteps(Unit, false);
        int StepsForShiftOrAttack(in UnitTC unitTC_from, in DirectTypes dirMove_to, in CellEs cellEs_to)
        {
            var needSteps = 1;

            if (!unitTC_from.Is(UnitTypes.Undead))
            {
                if (cellEs_to.EnvironmentEs.Fertilizer.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.Fertilizer.EnvT);
                if (cellEs_to.EnvironmentEs.YoungForest.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.YoungForest.EnvT);
                if (cellEs_to.EnvironmentEs.AdultForest.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.AdultForest.EnvT);
                if (cellEs_to.EnvironmentEs.Hill.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.Hill.EnvT);

                if (cellEs_to.TrailEs.Trail(dirMove_to.Invert()).HaveTrail) needSteps--;
            }

            return needSteps;
        }
        public bool CanShift(in UnitTC unitTC_from, in DirectTypes dirMove_to, in CellEs cellEs_to)
        {
            return StepsForShiftOrAttack(unitTC_from, dirMove_to, cellEs_to) <= Steps;
        }

        #endregion


        #region Water

        public bool HaveWater => Water > 0;
        public bool Have(in AbilityTypes ability) => Water >= CellUnitStatWaterValues.Need(ability);
        public int MaxWater(in UnitStatUpgradesEs statUpgEs)
        {
            var maxWater = CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS;

            if (!UnitTCRef.IsAnimal)
            {
                if (statUpgEs.Upgrade(UnitStatTypes.Water, this, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    return maxWater += (int)(maxWater * 0.5f);
                }
            }

            return maxWater;
        }

        #endregion

        #endregion


        #region Damage

        public int DamageAttack(in CellUnitExtraToolWeaponE extraTWE, in UnitStatUpgradesEs statUpgEs, in AttackTypes attack)
        {
            //var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            var standDamage = CellUnitMainDamageValues.StandDamage(Unit, Level);


            if (!IsAnimal)
                if (statUpgEs.Upgrade(UnitStatTypes.Damage, this, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    upgPerc = 0.3f;
                }



            float powerDamege = standDamage;

            powerDamege += standDamage * CellUnitMainDamageValues.PercentExtraDamageTW(extraTWE.ToolWeaponTC.ToolWeapon);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * CellUnitMainDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public int DamageOnCell(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs)
        {
            float powerDamege = DamageAttack(cellEs.UnitEs.ExtraToolWeaponE, statUpgEs, AttackTypes.Simple);

            var standDamage = CellUnitMainDamageValues.StandDamage(Unit, Level);

            powerDamege += standDamage * CellUnitMainDamageValues.ProtRelaxPercent(Condition);
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

        void Reset()
        {
            Unit = UnitTypes.None;
            IsRightArcher = false;
            Owner = PlayerTypes.None;
            Level = LevelTypes.None;
        }
        void Set(in byte idx_to, in Entities ents)
        {
            ents.UnitE(idx_to).Unit = Unit;
            ents.UnitE(idx_to).IsRightArcher = IsRightArcher;
            ents.UnitE(idx_to).Owner = Owner;
            ents.UnitE(idx_to).Condition = ConditionUnitTypes.None;
            ents.UnitE(idx_to).Level = Level;

            ents.UnitE(idx_to).Health = Health;
            ents.UnitE(idx_to).Steps = Steps;
            ents.UnitE(idx_to).Water = Water;

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
        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit, in Entities ents, in (ToolWeaponTypes, LevelTypes, ToolWeaponTypes, LevelTypes) tw = default)
        {
            var idx_0 = Idx;

            Unit = unit.Item1;
            Level = unit.Item2;
            Owner = unit.Item3;
            Condition = unit.Item4;
            IsRightArcher = unit.Item5;

            SetMaxHp();
            SetMaxSteps();
            SetMaxWater(ents.UnitStatUpgradesEs);

            ents.UnitEffectEs(idx_0).StunE.Reset();
            ents.UnitEffectEs(idx_0).ShieldE.Reset();
            ents.UnitEffectEs(idx_0).FrozenArrowE.Disable();

            ents.UnitEs(idx_0).MainToolWeaponE.SetNew(tw.Item1, tw.Item2);
            ents.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tw.Item3, tw.Item4);
            foreach (var item in ents.UnitEs(idx_0).CooldownKeys) ents.UnitEs(idx_0).Ability(item).SetNew();

            //if (ents.UnitE(idx_0).Is(UnitTypes.Pawn))
            //{
            //    MainToolWeaponE.ToolWeapon = ToolWeaponTypes.Axe;
            //    MainToolWeaponE.Level = LevelTypes.First;
            //}
        }



        public void Kill(in Entities ents)
        {
            if (!HaveUnit) throw new Exception("There's not unit");

            var idx_0 = Idx;

            if (UnitTC.Is(UnitTypes.King))
            {
                ents.WinnerE.Winner.Player = Owner;
            }
            else if (UnitTC.Is(UnitTypes.Scout) || UnitTC.IsHero)
            {
                ents.ScoutHeroCooldownE(this).SetCooldownAfterKill(UnitTC.Unit);
                ents.InventorUnitsEs.Units(UnitTC.Unit, Level, Owner).AddUnit();
            }

            ents.UnitEs(idx_0).WhoLastDiedHereE.SetLastDied(this);

            UnitTCRef.Unit = UnitTypes.None;
        }
        public void AddToInventorAndRemove(in Entities e)
        {
            var idx_0 = Idx;

            var level = e.UnitE(idx_0).LevelTCRef.Level;
            var owner = e.UnitE(idx_0).OwnerC.Player;

            e.InventorUnitsEs.Units(UnitTC.Unit, level, owner).AddUnit();


            UnitTCRef.Unit = UnitTypes.None;
        }
        public void Upgrade()
        {
            if (LevelTCRef.Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) throw new Exception();

            LevelTCRef.Level = LevelTypes.Second;
        }
        public void Shift(in byte idx_to, in bool withDestoyBuilding, in Entities ents)
        {
            Set(idx_to, ents);
            Reset();

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
                if (ents.BuildE(idx_to).HaveBuilding && !ents.BuildE(idx_to).Is(BuildingTypes.City))
                {
                    if (!ents.BuildE(idx_to).Is(ents.UnitE(idx_to).Owner))
                    {
                        ents.BuildE(idx_to).Destroy(ents);
                    }
                }
            }
        }
        public void Teleport(in byte idx_to, in Entities ents)
        {
            Set(idx_to, ents);
            Reset();
        }

        public void Shift_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = Idx;
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;

            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                e.UnitE(idx_from).TakeForShift(idx_to, e);

                e.UnitEs(idx_from).Shift(idx_to, true, e);

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void Attack_Master(in byte idx_to, in Entities e)
        {
            var idx_from = Idx;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, whoseMove, out var attack))
            {
                e.UnitE(idx_from).SetStepsAfterAttack();
                e.UnitE(idx_from).Condition = ConditionUnitTypes.None;


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += DamageAttack(e.UnitEs(idx_from).ExtraToolWeaponE, e.UnitStatUpgradesEs, attack);

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

                if (!e.UnitEs(idx_to).UnitE.UnitTC.IsMelee) powerDam_to /= 2;

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


                if (e.UnitEs(idx_from).UnitE.UnitTC.IsMelee)
                {
                    if (e.UnitEffectEs(idx_from).ShieldE.HaveShieldEffect)
                    {
                        e.UnitEffectEs(idx_from).ShieldE.Take();
                    }
                    else if (e.UnitEs(idx_from).ExtraToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                    {
                        e.UnitEs(idx_from).ExtraToolWeaponE.BreakShield();
                    }
                    else if (minus_from > 0)
                    {
                        e.UnitE(idx_from).Attack((int)minus_from);
                    }
                }
                else
                {
                    if (e.UnitEffectEs(idx_from).FrozenArrowE.IsFrozenArraw)
                    {
                        e.UnitEffectEs(idx_from).FrozenArrowE.Disable();

                        e.UnitEffectEs(idx_to).StunE.SetAfterAttackFrozenArrow();
                    }
                }

                if (e.UnitEffectEs(idx_to).ShieldE.HaveShieldEffect)
                {
                    e.UnitEffectEs(idx_to).ShieldE.Take();
                }
                else if (e.UnitEs(idx_to).ExtraToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                {
                    e.UnitEs(idx_to).ExtraToolWeaponE.BreakShield();
                }
                else if (minus_to > 0)
                {
                    e.UnitE(idx_to).Attack((int)minus_to);
                }


                if (!e.UnitE(idx_to).IsAlive)
                {
                    if (e.UnitEs(idx_to).UnitE.UnitTC.IsAnimal)
                    {
                        e.InventorResourcesEs.Resource(ResourceTypes.Food, e.UnitE(idx_from).OwnerC.Player).Add(ResourcesInInventorValues.AMOUNT_FOOD_AFTER_KILL_CAMEL);
                    }

                    e.UnitEs(idx_to).UnitE.Kill(e);


                    if (e.UnitEs(idx_from).UnitE.UnitTC.IsMelee)
                    {
                        if (!e.UnitE(idx_from).IsAlive)
                        {
                            e.UnitEs(idx_from).UnitE.Kill(e);
                        }
                        else
                        {
                            e.UnitEs(idx_from).Shift(idx_to, true, e);
                        }
                    }
                }

                else if (!e.UnitE(idx_from).IsAlive)
                {
                    e.UnitEs(idx_from).UnitE.Kill(e);
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
                    e.UnitEs(idx_0).SetNew((unit, LevelTypes.First, whoseMove, ConditionUnitTypes.None, false), e);
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
                    e.UnitEs(idx_0).SetNew((unit, levUnit, whoseMove, ConditionUnitTypes.None, false), e);
                }

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void UpgradeUnit_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var unit_0 = UnitTC;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;

            if (e.UnitE(idx_0).HaveMaxHp)
            {
                //if (UnitE(idx_0).Have(ability))
                //{
                if (e.InventorResourcesEs.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                {
                    e.InventorResourcesEs.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                    e.UnitE(idx_0).Upgrade();
                    //UnitE(idx_0).Take(ability);

                    e.UnitE(idx_0).SetMaxHp();

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
                        e.UnitE(idx_0).Take(cond);
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
                        e.UnitE(idx_0).Take(cond);
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
                if (ents.BuildE(idx_0).Is(BuildingTypes.Teleport))
                {
                    var idx_start = ents.StartTeleportE.WhereC.Idx;
                    var idx_end = ents.EndTeleportE.WhereC.Idx;

                    if (ents.EndTeleportE.HaveEnd && idx_start == idx_0)
                    {
                        if (!ents.UnitE(idx_end).HaveUnit)
                        {
                            ents.UnitE(idx_0).Take(ability);

                            Teleport(idx_end, ents);
                        }
                    }
                    else if (ents.StartTeleportE.HaveStart && idx_end == idx_0)
                    {
                        if (!ents.UnitE(idx_start).HaveUnit)
                        {
                            ents.UnitE(idx_0).Take(ability);

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
                ents.UnitE(idx_0).Take(ability);

                foreach (var idx_1 in ents.CellSpaceWorker.GetIdxsAround(idx_0))
                {
                    if (!ents.UnitE(idx_1).HaveUnit && !ents.EnvMountainE(idx_1).HaveEnvironment)
                    {
                        ents.UnitE(idx_1).SetNew((UnitTypes.Skeleton, LevelTypes.First, Owner, ConditionUnitTypes.None, false), ents);
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

        void AddHp(in int adding = 1)
        {
            HealthRef.Amount += adding;
            if (HaveMaxHp) SetMaxHp();
        }
        void TakeHp(in Entities ents, in int taking = 1)
        {
            HealthRef.Amount -= taking;
            if (IsHpDeathAfterAttack) HealthRef.Amount = 0;
            if (!IsAlive) ents.UnitEs(Idx).UnitE.Kill(ents);
        }

        public void Attack(in AbilityTypes ability, in Entities Es)
        {
            HealthRef.Amount -= CellUnitStatHpValues.Damage(ability);

            if (IsHpDeathAfterAttack || !IsAlive)
            {
                Es.CellEs(Idx).UnitEs.UnitE.Kill(Es);
            }
        }
        public void Attack(in int damage)
        {
            HealthRef.Amount -= damage;
            if (IsHpDeathAfterAttack) HealthRef.Amount = 0;
        }
        public void TakeHpHellWithNearWater(in Entities ents)
        {
            TakeHp(ents, 15);
        }
        public void TakeHpHellWithCloud(in Entities ents)
        {
            TakeHp(ents, 15);
        }
        public void TakeHpHellWithIceWall(in Entities ents)
        {
            TakeHp(ents, 15);
        }
        public void Thirsty(in Entities es)
        {
            float percent = CellUnitStatHpValues.ThirstyPercent(es.CellEs(Idx).UnitEs.UnitE.UnitTC.Unit);

            HealthRef.Amount -= (int)(CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS * percent);

            if (!es.UnitE(Idx).IsAlive)
            {
                if (es.CellEs(Idx).BuildEs.BuildingE.BuildTC.Is(BuildingTypes.Camp))
                {
                    es.CellEs(Idx).BuildEs.BuildingE.Destroy(es);
                }

                es.CellEs(Idx).UnitEs.UnitE.Kill(es);
            }
        }
        public void Fire(in Entities es)
        {
            if (es.UnitEs(Idx).UnitE.UnitTC.Is(UnitTypes.Hell))
            {
                SetMaxHp();
            }
            else
            {
                HealthRef.Amount -= CellUnitStatHpValues.FIRE_DAMAGE;
                if (!IsAlive) es.CellEs(Idx).UnitEs.UnitE.Kill(es);
            }
        }
        public void SetMaxHp()
        {
            HealthRef.Amount = CellUnitStatHpValues.MAX_HP;
        }

        #endregion


        #region Steps

        public void SetMaxSteps() => Steps = MaxAmountSteps;
        public void SetStepsAfterAttack()
        {
            StepsCRef.Steps = 0;
        }
        public void Take(in AbilityTypes ability)
        {
            StepsCRef.Steps -= CellUnitStatStepValues.NeedSteps(ability);
        }
        public void Take(in RpcMasterTypes rpc)
        {
            Steps -= CellUnitStatStepValues.NeedSteps(rpc);
        }
        public void Take(in ConditionUnitTypes cond)
        {
            Steps -= CellUnitStatStepValues.NeedSteps(cond);
        }
        public void Take(in ToolWeaponTypes tw)
        {
            Steps -= CellUnitStatStepValues.NeedSteps(tw);
        }
        public void TakeForShift(in byte idx_to, in Entities es)
        {
            if (!es.CellSpaceWorker.TryGetDirect(Idx, idx_to, out var dir)) throw new Exception();
            Steps -= es.UnitE(Idx).StepsForShiftOrAttack(es.UnitEs(Idx).UnitE.UnitTC, dir, es.CellEs(idx_to));
        }

        #endregion


        #region Water

        public void SetMaxWater(in UnitStatUpgradesEs statUpgEs) => Water = MaxWater(statUpgEs);
        public void TakeWater(in AbilityTypes ability)
        {
            Water -= CellUnitStatWaterValues.Need(ability);
        }

        #endregion

        #endregion
    }
}