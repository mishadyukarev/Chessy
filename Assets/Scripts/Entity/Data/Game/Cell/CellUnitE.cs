using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitE : CellEntityAbstract
    {
        public UnitTC UnitTC => Ent.Get<UnitTC>();
        public PlayerTC PlayerTC => Ent.Get<PlayerTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();
        public ref ConditionUnitTC ConditionTC => ref Ent.Get<ConditionUnitTC>();
        public ref IsRightArcherC IsRightArcherC => ref Ent.Get<IsRightArcherC>();
        public ref StunC StunC => ref Ent.Get<StunC>();
        public ShieldEffectC ShieldEffectC => Ent.Get<ShieldEffectC>();
        public ref FrozenArrawC FrozenArrawC => ref Ent.Get<FrozenArrawC>();
        public HealthC HealthC => Ent.Get<HealthC>();
        public StepsC StepC => Ent.Get<StepsC>();
        public WaterC WaterC => Ent.Get<WaterC>();


        public float ForShiftOrAttack(in DirectTypes dirMove_to, in CellEs cellEs_to)
        {
            var needSteps = 0.5f;

            if (!UnitTC.Is(UnitTypes.Undead))
            {
                if (cellEs_to.EnvironmentEs.Fertilizer.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.Fertilizer.EnvironmentT);
                if (cellEs_to.EnvironmentEs.YoungForest.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.YoungForest.EnvironmentT);
                if (cellEs_to.EnvironmentEs.AdultForest.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.AdultForest.EnvironmentT);
                if (cellEs_to.EnvironmentEs.Hill.HaveEnvironment) needSteps += CellUnitStatStepValues.NeedStepsShiftAttackUnit(cellEs_to.EnvironmentEs.Hill.EnvironmentT);

                if (cellEs_to.TrailEs.Trail(dirMove_to.Invert()).HealthC.IsAlive) needSteps -= 0.5f;
            }

            return needSteps;
        }
        public bool CanShift(in DirectTypes dirMove_to, in CellEs cellEs_to)
        {
            return ForShiftOrAttack(dirMove_to, cellEs_to) <= StepC.Steps;
        }


        #region Damage

        public float DamageAttack(in CellUnitEs unitEs, in UnitStatUpgradesEs statUpgEs, in AttackTypes attack)
        {
            //var haveEff = CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx).Have;
            var upgPerc = 0f;

            if (statUpgEs.Upgrade(UnitStatTypes.Damage, this, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                upgPerc = 0.3f;


            var standDamage = CellUnitDamageValues.StandDamage(UnitTC.Unit, LevelTC.Level);
            float powerDamege = standDamage;

            if (unitEs.MainToolWeaponE.ToolWeaponTC.HaveToolWeapon) powerDamege += standDamage * CellUnitDamageValues.ToolWeaponMainPercent(unitEs.MainToolWeaponE.ToolWeaponTC.ToolWeapon, unitEs.MainToolWeaponE.LevelTC.Level);
            if (unitEs.ExtraToolWeaponE.ToolWeaponTC.HaveToolWeapon) powerDamege += standDamage * CellUnitDamageValues.ToolWeaponExtraPercent(unitEs.ExtraToolWeaponE.ToolWeaponTC.ToolWeapon);

            if (attack == AttackTypes.Unique) powerDamege += standDamage * CellUnitDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return powerDamege;
        }
        public float DamageOnCell(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs)
        {
            float powerDamege = DamageAttack(cellEs.UnitEs, statUpgEs, AttackTypes.Simple);

            var standDamage = CellUnitDamageValues.StandDamage(UnitTC.Unit, LevelTC.Level);

            powerDamege += standDamage * CellUnitDamageValues.ProtRelaxPercent(ConditionTC.Condition);
            if (cellEs.BuildEs.BuildingE.HaveBuilding) powerDamege += standDamage * CellBuildingValues.ProtectionPercent(cellEs.BuildEs.BuildingE.Building);

            float protectionPercent = 0;

            var envEs = cellEs.EnvironmentEs;

            if (envEs.Fertilizer.HaveEnvironment) protectionPercent += CellUnitDamageValues.ProtectionPercent(EnvironmentTypes.Fertilizer);
            if (envEs.YoungForest.HaveEnvironment) protectionPercent += CellUnitDamageValues.ProtectionPercent(EnvironmentTypes.YoungForest);
            if (envEs.AdultForest.HaveEnvironment) protectionPercent += CellUnitDamageValues.ProtectionPercent(EnvironmentTypes.AdultForest);
            if (envEs.Hill.HaveEnvironment) protectionPercent += CellUnitDamageValues.ProtectionPercent(EnvironmentTypes.Hill);
            if (envEs.Mountain.HaveEnvironment) protectionPercent += CellUnitDamageValues.ProtectionPercent(EnvironmentTypes.Mountain);

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


        internal CellUnitE(in CellEs cellEs, in EcsWorld gameW) : base(cellEs, gameW)
        {

        }

        void Set(in byte idx_to, in Entities ents)
        {
            var idx_0 = CellEs.Idx;

            ents.UnitTC(idx_to).Unit = UnitTC.Unit;
            ents.UnitIsRightArcherC(idx_to).IsRight = IsRightArcherC.IsRight;
            ents.UnitPlayerTC(idx_to).Player = PlayerTC.Player;
            ents.UnitConditionTC(idx_to).Condition = ConditionTC.Condition;
            ents.UnitLevelTC(idx_to).Level = LevelTC.Level;

            ents.UnitHpC(idx_to).Health = HealthC.Health;
            ents.UnitStepC(idx_to).Steps = StepC.Steps;
            ents.UnitWaterC(idx_to).Water = WaterC.Water;

            ents.UnitStunC(idx_to).Stun = StunC.Stun;
            ents.UnitEffectShield(idx_to).Protection = ShieldEffectC.Protection;
            ents.UnitFrozenArrawC(idx_to).Shoots = FrozenArrawC.Shoots;

            ents.ExtraTWE(idx_to).ToolWeaponTC.ToolWeapon = ents.UnitEs(idx_0).ExtraToolWeaponE.ToolWeaponTC.ToolWeapon;
            ents.ExtraTWE(idx_to).LevelTC.Level = ents.UnitEs(idx_0).ExtraToolWeaponE.LevelTC.Level;
            ents.ExtraTWE(idx_to).ProtectionShieldC.Protection = ents.UnitEs(idx_0).ExtraToolWeaponE.ProtectionShieldC.Protection;

            ents.UnitMainTWTC(idx_to).ToolWeapon = ents.UnitMainTWTC(idx_0).ToolWeapon;
            ents.UnitMainTWLevelTC(idx_to).Level = ents.UnitMainTWLevelTC(idx_0).Level;

            foreach (var abilityT in ents.UnitEs(idx_0).CooldownKeys) ents.UnitEs(idx_to).Ability(abilityT).Cooldown =  ents.UnitEs(idx_0).Ability(abilityT).Cooldown;
        }
        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit, in Entities ents)
        {
            var idx_0 = CellEs.Idx;

            UnitTC.Unit = unit.Item1;
            LevelTC.Level = unit.Item2;
            PlayerTC.Player = unit.Item3;
            ConditionTC.Condition = unit.Item4;
            IsRightArcherC.IsRight = unit.Item5;

            ents.UnitHpC(idx_0).Health = CellUnitStatHpValues.MAX_HP;
            ents.UnitStepC(idx_0).Steps = CellUnitStatStepValues.StandartStepsUnit(CellEs.UnitC.Unit);
            WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);

            ents.UnitStunC(idx_0).Stun = 0;
            ents.UnitEffectShield(idx_0).Protection = 0;
            ents.UnitFrozenArrawC(idx_0).Shoots = 0;

            foreach (var item in ents.UnitEs(idx_0).CooldownKeys) ents.UnitEs(idx_0).Ability(item).Cooldown = 0;
        }
        public void SetNewPawn(in (LevelTypes, PlayerTypes, ConditionUnitTypes) unit, in Entities ents)
        {
            var idx_0 = CellEs.Idx;

            SetNew((UnitTypes.Pawn, unit.Item1, unit.Item2, unit.Item3, false), ents);

            ents.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
            ents.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
        }
        public void Shift(in byte idx_to, in bool withDestoyBuilding, in Entities ents)
        {
            var idx_from = CellEs.Idx;

            Set(idx_to, ents);
            UnitTC.Unit = UnitTypes.None;

            if (!ents.CellSpaceWorker.TryGetDirect(idx_from, idx_to, out var direct)) throw new Exception();

            if (!ents.UnitTC(idx_to).Is(UnitTypes.Undead))
            {
                if (ents.EnvironmentEs(idx_from).AdultForest.HaveEnvironment)
                {
                    ents.CellEs(idx_from).TrailEs.Trail(direct).HealthC.Set(10);
                }
                if (ents.EnvironmentEs(idx_to).AdultForest.HaveEnvironment)
                {
                    ents.CellEs(idx_to).TrailEs.Trail(direct.Invert()).HealthC.Set(10);
                }

                if (ents.RiverEs(idx_to).RiverE.HaveRiverNear)
                {
                    ents.UnitE(idx_to).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                }
            }

            ents.EffectEs(idx_to).FireE.TryFireAfterShift();

            if (withDestoyBuilding)
            {
                if (ents.BuildingE(idx_to).HaveBuilding && !ents.BuildingE(idx_to).Is(BuildingTypes.City))
                {
                    if (!ents.BuildingE(idx_to).Is(ents.UnitPlayerTC(idx_to).Player))
                    {
                        ents.BuildingE(idx_to).Destroy(ents);
                    }
                }
            }
        }
        public void Teleport(in byte idx_to, in Entities ents)
        {
            Set(idx_to, ents);
            UnitTC.Unit = UnitTypes.None;
        }
        public void Shift_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = CellEs.Idx;
            var whoseMove = e.WhoseMovePlayerTC.Player;

            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                if (!e.CellSpaceWorker.TryGetDirect(idx_from, idx_to, out var dir)) throw new Exception();
                e.UnitStepC(idx_from).Take(e.UnitE(idx_from).ForShiftOrAttack(dir, e.CellEs(idx_to)));

                e.UnitE(idx_from).Shift(idx_to, true, e);

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void Attack_Master(in byte idx_to, in Entities e)
        {
            var idx_from = CellEs.Idx;

            var whoseMove = e.WhoseMovePlayerTC.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, whoseMove, out var attack))
            {
                e.UnitStepC(idx_from).Set(0);
                e.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += DamageAttack(e.UnitEs(idx_from), e.UnitStatUpgradesEs, attack);

                if (e.UnitTC(idx_from).IsMelee && e.MainTWE(idx_from).ToolWeaponTC.IsMelee)
                    e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += e.UnitE(idx_to).DamageOnCell(e.CellEs(idx_to), e.UnitStatUpgradesEs);


                e.CellSpaceWorker.TryGetDirect(idx_from, idx_to, out var dirAttack);


                if (e.SunSideTC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in e.SunSideTC.RaysSun)
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


                if (e.UnitTC(idx_from).IsMelee && e.MainTWE(idx_from).ToolWeaponTC.IsMelee)
                {
                    if (e.UnitEffectShield(idx_from).HaveEffect)
                    {
                        e.UnitEffectShield(idx_from).Protection--;
                    }
                    else if (e.ExtraTWE(idx_from).ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                    {
                        e.ExtraTWE(idx_from).BreakShield(1);
                    }
                    else if (minus_from > 0)
                    {
                        e.UnitHpC(idx_from).Take(minus_from);
                        if (e.UnitHpC(idx_from).Health <= CellUnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK) e.UnitHpC(idx_from).Set(0);

                        if (!e.UnitHpC(idx_from).IsAlive)
                        {
                            e.UnitTC(idx_from).TrySetCooldownBeforeKilling(e.ScoutHeroCooldownE(e.UnitE(idx_from)).CooldownC, e.Units(e.UnitE(idx_from)).AmountC, ScoutHeroCooldownValues.AfterKill(e.UnitTC(idx_from).Unit));
                            e.UnitTC(idx_from).KillUnit(e.UnitPlayerTC(idx_from), e.WinnerC);
                            

                            e.UnitEs(idx_from).WhoLastDiedHereE.SetLastDied(e.UnitE(idx_from));
                        }
                    }
                }
                else
                {
                    if (e.UnitFrozenArrawC(idx_from).HaveEffect)
                    {
                        e.UnitFrozenArrawC(idx_from).Shoots = 0;

                        e.UnitStunC(idx_to).Stun = 2;
                    }
                }

                if (e.UnitEffectShield(idx_to).HaveEffect)
                {
                    e.UnitEffectShield(idx_to).Protection--;
                }
                else if (e.ExtraTWE(idx_to).ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                {
                    e.ExtraTWE(idx_to).BreakShield(1);
                }
                else if (minus_to > 0)
                {
                    var wasUnit = e.UnitTC(idx_to).Unit;

                    e.UnitHpC(idx_to).Take(minus_to);
                    if (e.UnitHpC(idx_to).Health <= CellUnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK) e.UnitHpC(idx_to).Set(0);

                    if (!e.UnitHpC(idx_to).IsAlive)
                    {
                        e.UnitTC(idx_to).TrySetCooldownBeforeKilling(e.ScoutHeroCooldownE(e.UnitE(idx_to)).CooldownC, e.Units(e.UnitE(idx_to)).AmountC, ScoutHeroCooldownValues.AfterKill(e.UnitTC(idx_to).Unit));
                        e.UnitTC(idx_to).KillUnit(e.UnitPlayerTC(idx_to), e.WinnerC);                      

                        e.UnitEs(idx_to).WhoLastDiedHereE.SetLastDied(e.UnitE(idx_to));
                    }

                    if (!e.UnitTC(idx_to).HaveUnit)
                    {
                        if (wasUnit == UnitTypes.Camel)
                        {
                            e.InventorResourcesEs.Resource(ResourceTypes.Food, e.UnitPlayerTC(idx_from).Player).ResourceC.Add(ResourcesInInventorValues.AMOUNT_FOOD_AFTER_KILL_CAMEL);
                        }

                        if (e.UnitTC(idx_from).HaveUnit)
                        {
                            if (e.UnitTC(idx_from).IsMelee && e.MainTWE(idx_from).ToolWeaponTC.IsMelee)
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
            var idx_0 = CellEs.Idx;

            var whoseMove = e.WhoseMovePlayerTC.Player;


            if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(whoseMove, idx_0).Can)
            {
                if (unit == UnitTypes.Pawn)
                {
                    e.PeopleInCityE(whoseMove).AmountC.Take(1);
                    e.UnitE(idx_0).SetNewPawn((LevelTypes.First, whoseMove, ConditionUnitTypes.None), e);
                }
                else
                {
                    var levUnit = LevelTypes.None;

                    if (e.Units(unit, LevelTypes.Second, whoseMove).HaveUnits)
                    {
                        e.Units(unit, LevelTypes.Second, whoseMove).AmountC.Take(1);
                        levUnit = LevelTypes.Second;
                    }
                    else
                    {
                        e.Units(unit, LevelTypes.First, whoseMove).AmountC.Take(1);
                        levUnit = LevelTypes.First;
                    }
                    e.UnitE(idx_0).SetNew((unit, levUnit, whoseMove, ConditionUnitTypes.None, false), e);
                }

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void Condition_Master(in ConditionUnitTypes cond, in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            switch (cond)
            {
                case ConditionUnitTypes.None:
                    e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    break;

                case ConditionUnitTypes.Protected:
                    if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitStepC(idx_0).HaveSteps)
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_TOGGLE_CONDITION_UNIT);
                        e.UnitConditionTC(idx_0).Condition = cond;
                    }

                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (e.UnitStepC(idx_0).HaveSteps)
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        e.UnitConditionTC(idx_0).Condition = cond;
                        e.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_TOGGLE_CONDITION_UNIT);
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
            var idx_0 = CellEs.Idx;

            if (ents.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(ability)))
            {
                if (ents.BuildingE(idx_0).Is(BuildingTypes.Teleport))
                {
                    var idx_start = ents.StartTeleportIdxC.Idx;
                    var idx_end = ents.EndTeleportIdxC.Idx;

                    if (ents.EndTeleportIdxC.HaveEnd && idx_start == idx_0)
                    {
                        if (!ents.UnitTC(idx_end).HaveUnit)
                        {
                            ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

                            Teleport(idx_end, ents);
                        }
                    }
                    else if (ents.StartTeleportIdxC.HaveStart && idx_end == idx_0)
                    {
                        if (!ents.UnitTC(idx_start).HaveUnit)
                        {
                            ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

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
            var idx_0 = CellEs.Idx;

            if (ents.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(ability)))
            {
                ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

                foreach (var idx_1 in ents.CellSpaceWorker.GetIdxsAround(idx_0))
                {
                    if (!ents.UnitTC(idx_0).HaveUnit && !ents.MountainE(idx_1).HaveEnvironment)
                    {
                        ents.UnitE(idx_1).SetNew((UnitTypes.Skeleton, LevelTypes.First, PlayerTC.Player, ConditionUnitTypes.None, false), ents);
                    }

                }
            }
            else
            {
                ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }

        public void Take(in Entities ents, in float taking)
        {

        }
        public void Take(in AbilityTypes ability, in Entities e) => Take(e, CellUnitStatHpValues.DamageAfterAbility(ability));
    }
}