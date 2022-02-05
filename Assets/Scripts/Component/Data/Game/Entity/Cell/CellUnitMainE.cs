using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitMainE : CellEntityAbstract
    {
        ref UnitTC UnitTCRef => ref Ent.Get<UnitTC>();
        public UnitTC UnitTC => Ent.Get<UnitTC>();

        public bool HaveUnit => UnitTC.Unit != UnitTypes.None && UnitTC.Unit != UnitTypes.End;
        public bool Is(params UnitTypes[] unit) => UnitTC.Is(unit);


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

            var standDamage = CellUnitMainDamageValues.StandDamage(UnitTC.Unit, cellEs.UnitEs.LevelE.LevelTC.Level);


            if (!UnitTC.IsAnimal)
                if (statUpgEs.Upgrade(UnitStatTypes.Damage, cellEs.UnitEs, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    upgPerc = 0.3f;
                }



            float powerDamege = standDamage;

            powerDamege += standDamage * CellUnitMainDamageValues.PercentTW(cellEs.UnitEs.ToolWeaponE.ToolWeaponTC.ToolWeapon);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * CellUnitMainDamageValues.UNIQUE_PERCENT_DAMAGE;

            //if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public int DamageOnCell(in CellEs cellEs, in UnitStatUpgradesEs statUpgEs)
        {
            var idx_0 = Idx;

            float powerDamege = DamageAttack(cellEs, statUpgEs, AttackTypes.Simple);

            var standDamage = CellUnitMainDamageValues.StandDamage(UnitTC.Unit, cellEs.UnitEs.LevelE.LevelTC.Level);

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



        internal CellUnitMainE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {

        }


        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit, in Entities ents, in (ToolWeaponTypes, LevelTypes) tw = default)
        {
            var idx_0 = Idx;

            UnitTCRef.Unit = unit.Item1;
            ents.UnitEs(idx_0).LevelE.SetLevel(unit.Item2);
            ents.UnitEs(idx_0).OwnerE.Set(unit.Item3);
            ents.UnitEs(idx_0).ConditionE.Set(unit.Item4);
            ents.UnitEs(idx_0).CornedE.IsCornered = unit.Item5;

            ents.CellEs(idx_0).UnitEs.StatEs.Hp.SetMax();
            ents.CellEs(idx_0).UnitEs.StatEs.StepE.SetMax(this);
            ents.UnitStatWaterE(idx_0).SetMax(ents.UnitEs(idx_0), ents.UnitStatUpgradesEs);

            ents.UnitEffectEs(idx_0).StunE.Reset();
            ents.UnitEffectEs(idx_0).ShieldE.Reset();
            ents.UnitEffectEs(idx_0).FrozenArrowE.Disable();

            ents.CellEs(idx_0).UnitEs.ToolWeaponE.SetNew(tw);
            foreach (var item in ents.CellEs(idx_0).UnitEs.CooldownKeys) ents.CellEs(idx_0).UnitEs.Ability(item).SetNew();
            //ents.WhereUnitsEs.WhereUnit(unit.Item1, unit.Item2, unit.Item3, Idx).HaveUnit.Have = true;
        }
        public void Kill(in Entities ents)
        {
            if (!HaveUnit) throw new Exception("There's not unit");

            var idx_0 = Idx;

            if (UnitTC.Is(UnitTypes.King))
            {
                ents.WinnerE.Winner.Player = ents.UnitEs(idx_0).OwnerE.OwnerC.Player;
            }
            else if (UnitTC.Is(UnitTypes.Scout) || UnitTC.IsHero)
            {
                ents.ScoutHeroCooldownE(ents.UnitEs(idx_0)).SetCooldownAfterKill(UnitTC.Unit);
                ents.InventorUnitsEs.Units(UnitTC.Unit, ents.UnitEs(idx_0).LevelE.LevelTC.Level, ents.UnitEs(idx_0).OwnerE.OwnerC.Player).AddUnit();
            }

            ents.UnitEs(idx_0).WhoLastDiedHereE.SetLastDied(ents.UnitEs(idx_0));

            //ents.WhereUnitsEs.WhereUnit(ents.UnitEs(idx_0)).HaveUnit.Have = false;

            UnitTCRef.Unit = UnitTypes.None;
        }
        public void Clear(in Entities e)
        {
            //e.WhereUnitsEs.WhereUnit(e.UnitEs(Idx)).HaveUnit.Have = false;

            UnitTCRef.Unit = UnitTypes.None;
        }
        public void Shift(in byte idx_to, in Entities ents)
        {
            var idx_from = Idx;

            //ents.WhereUnitsEs.WhereUnit(ents.UnitEs(idx_from)).HaveUnit.Have = false;


            ents.UnitEs(idx_to).MainE.UnitTCRef = UnitTC;

            ents.UnitEs(idx_to).CornedE.IsCornered = ents.UnitEs(idx_from).CornedE.IsCornered;

            ents.UnitEs(idx_to).OwnerE.Set(ents.UnitEs(idx_from).OwnerE.OwnerC.Player);
            ents.UnitEs(idx_to).ConditionE.Reset();
            ents.UnitEs(idx_to).LevelE.SetLevel(ents.UnitEs(idx_from).LevelE.LevelTC.Level);

            UnitTCRef.Unit = UnitTypes.None;



            ents.UnitStatHpE(idx_to).Shift(ents.UnitStatHpE(idx_from));
            ents.UnitStatStepE(idx_to).Shift(ents.CellEs(Idx).UnitEs.StatEs.StepE);
            ents.UnitStatWaterE(idx_to).Shift(ents.CellEs(Idx).UnitEs.StatEs.WaterE);

            ents.UnitEffectEs(idx_to).StunE.Shift(ents.UnitEffectEs(Idx).StunE);
            ents.UnitEffectEs(idx_to).ShieldE.Shift(ents.UnitEffectEs(Idx).ShieldE);
            ents.UnitEffectEs(idx_to).FrozenArrowE.Shift(ents.UnitEffectEs(Idx).FrozenArrowE);

            ents.CellEs(idx_to).UnitEs.ToolWeaponE.Set(ents.CellEs(Idx).UnitEs.ToolWeaponE);

            foreach (var abilityT in ents.CellEs(Idx).UnitEs.CooldownKeys)
                ents.CellEs(idx_to).UnitEs.Ability(abilityT).Shift(ents.CellEs(Idx).UnitEs.Ability(abilityT));

            if (!ents.CellWorker.TryGetDirect(Idx, idx_to, out var direct)) throw new Exception();


            if (!ents.UnitEs(idx_to).MainE.UnitTC.Is(UnitTypes.Undead))
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
                    ents.CellEs(idx_to).UnitEs.StatEs.WaterE.SetMax(ents.UnitEs(idx_to), ents.UnitStatUpgradesEs);
                }
            }

            ents.EffectEs(idx_to).FireE.TryFireAfterShift(ents.Cells);
            if (ents.BuildEs(idx_to).BuildingE.HaveBuilding && !ents.BuildEs(idx_to).BuildingE.BuildTC.Is(BuildingTypes.City))
            {
                if (!ents.BuildEs(idx_to).BuildingE.OwnerC.Is(ents.UnitEs(idx_to).OwnerE.OwnerC.Player))
                {
                    ents.BuildEs(idx_to).BuildingE.Destroy();
                }
            }

            //ents.WhereUnitsEs.WhereUnit(ents.UnitEs(idx_to)).HaveUnit.Have = true;
        }
        public void AddToInventorAndRemove(in Entities e)
        {
            var idx_0 = Idx;

            var level = e.UnitEs(idx_0).LevelE.LevelTC.Level;
            var owner = e.UnitEs(idx_0).OwnerE.OwnerC.Player;

            e.InventorUnitsEs.Units(UnitTC.Unit, level, owner).AddUnit();


            UnitTCRef.Unit = UnitTypes.None;
        }
        public void SyncRpc(in (UnitTypes, LevelTypes, PlayerTypes, bool) unit)
        {
            //UnitTCRef.Unit = unit.Item1;
            //LevelTCRef.Level = unit.Item2;
            //OwnerCRef.Player = unit.Item3;
            //IsCornedRef.Is = unit.Item4;
        }

        public void Shift_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = Idx;
            var whoseMove = e.WhoseMove.WhoseMove.Player;

            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                e.UnitStatEs(idx_from).StepE.TakeForShift(idx_to, e);

                e.UnitEs(idx_from).MainE.Shift(idx_to, e);

                e.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void Attack_Master(in byte idx_to, in Entities e)
        {
            var idx_from = Idx;

            var whoseMove = e.WhoseMove.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, whoseMove, out var attack))
            {
                e.UnitStatEs(idx_from).StepE.SetStepsAfterAttack();
                e.UnitEs(idx_from).ConditionE.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += e.UnitEs(idx_from).MainE.DamageAttack(e.CellEs(idx_from), e.UnitStatUpgradesEs, attack);

                if (e.UnitEs(idx_from).MainE.UnitTC.IsMelee)
                    e.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else e.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += e.UnitEs(idx_to).MainE.DamageOnCell(e.CellEs(idx_to), e.UnitStatUpgradesEs);


                e.CellWorker.TryGetDirect(idx_from, idx_to, out var dirAttack);


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

                var maxDamage = CellUnitStatHpE.MAX_HP;
                var minDamage = 0;

                if (!e.UnitEs(idx_to).MainE.UnitTC.IsMelee) powerDam_to /= 2;

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


                if (e.UnitEs(idx_from).MainE.UnitTC.IsMelee)
                {
                    if (e.UnitEffectEs(idx_from).ShieldE.HaveShieldEffect)
                    {
                        e.UnitEffectEs(idx_from).ShieldE.Take();
                    }
                    else if (e.UnitEs(idx_from).ToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                    {
                        e.UnitEs(idx_from).ToolWeaponE.BreakShield();
                    }
                    else if (minus_from > 0)
                    {
                        e.UnitStatEs(idx_from).Hp.Attack((int)minus_from);
                    }
                }
                else
                {
                    if (e.UnitEffectEs(idx_from).FrozenArrowE.HaveEffect)
                    {
                        e.UnitEffectEs(idx_from).FrozenArrowE.Disable();

                        e.UnitEffectEs(idx_to).StunE.SetAfterAttackFrozenArrow();
                    }
                }

                if (e.UnitEffectEs(idx_to).ShieldE.HaveShieldEffect)
                {
                    e.UnitEffectEs(idx_to).ShieldE.Take();
                }
                else if (e.UnitEs(idx_to).ToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                {
                    e.UnitEs(idx_to).ToolWeaponE.BreakShield();
                }
                else if (minus_to > 0)
                {
                    e.UnitStatEs(idx_to).Hp.Attack((int)minus_to);
                }


                if (!e.UnitStatEs(idx_to).Hp.IsAlive)
                {
                    if (e.UnitEs(idx_to).MainE.UnitTC.IsAnimal)
                    {
                        e.InventorResourcesEs.Resource(ResourceTypes.Food, e.UnitEs(idx_from).OwnerE.OwnerC.Player).Resources.Amount += ResourcesInInventorValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }

                    e.UnitEs(idx_to).MainE.Kill(e);


                    if (e.UnitEs(idx_from).MainE.UnitTC.IsMelee)
                    {
                        if (!e.UnitStatEs(idx_from).Hp.IsAlive)
                        {
                            e.UnitEs(idx_from).MainE.Kill(e);
                        }
                        else
                        {
                            e.UnitEs(idx_from).MainE.Shift(idx_to, e);
                        }
                    }
                }

                else if (!e.UnitStatEs(idx_from).Hp.IsAlive)
                {
                    e.UnitEs(idx_from).MainE.Kill(e);
                }

                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Disable();
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Disable();
            }
        }
        public void SetUnit_Master(in UnitTypes unit, in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var whoseMove = e.WhoseMove.WhoseMove.Player;


            if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(whoseMove, idx_0).Can)
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
                e.UnitEs(idx_0).MainE.SetNew((unit, levUnit, whoseMove, ConditionUnitTypes.None, false), e);


                //if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                e.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void UpgradeUnit_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var unit_0 = UnitTC;

            var whoseMove = e.WhoseMove.WhoseMove.Player;

            if (e.UnitStatEs(idx_0).Hp.HaveMax)
            {
                //if (UnitStatEs(idx_0).StepE.Have(ability))
                //{
                if (e.InventorResourcesEs.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                {
                    e.InventorResourcesEs.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                    e.UnitEs(idx_0).LevelE.Upgrade();
                    //UnitStatEs(idx_0).StepE.Take(ability);

                    e.UnitStatEs(idx_0).Hp.SetMax();

                    e.Rpc.SoundToGeneral(sender, ClipTypes.UpgradeMelee);
                }
                else
                {
                    e.Rpc.MistakeEconomyToGeneral(sender, needRes);
                }
                //}
                //else
                //{
                //    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                //}
            }
            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}