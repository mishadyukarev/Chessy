using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitTypeE : CellEntityAbstract
    {
        ref UnitTC UnitTCRef => ref Ent.Get<UnitTC>();
        public UnitTC UnitTC => Ent.Get<UnitTC>();

        public UnitTypes UnitT
        {
            get => UnitTCRef.Unit;
            internal set => UnitTCRef.Unit = value;
        }
        public bool HaveUnit => UnitTC.Unit != UnitTypes.None && UnitTC.Unit != UnitTypes.End;
        public bool Is(params UnitTypes[] unit) => UnitTC.Is(unit);
        public bool IsMelee => UnitTC.IsMelee;


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



        internal CellUnitTypeE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {

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

            UnitTCRef.Unit = UnitTypes.None;
        }
        public void AddToInventorAndRemove(in Entities e)
        {
            var idx_0 = Idx;

            var level = e.UnitEs(idx_0).LevelE.LevelTC.Level;
            var owner = e.UnitEs(idx_0).OwnerE.OwnerC.Player;

            e.InventorUnitsEs.Units(UnitTC.Unit, level, owner).AddUnit();


            UnitTCRef.Unit = UnitTypes.None;
        } 

        public void Shift_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = Idx;
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;

            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                e.UnitStatStepE(idx_from).TakeForShift(idx_to, e);

                e.UnitEs(idx_from).Shift(idx_to, e);

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void Attack_Master(in byte idx_to, in Entities e)
        {
            var idx_from = Idx;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, whoseMove, out var attack))
            {
                e.UnitStatEs(idx_from).StepE.SetStepsAfterAttack();
                e.UnitEs(idx_from).ConditionE.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += e.UnitEs(idx_from).DamageAttack(e.CellEs(idx_from), e.UnitStatUpgradesEs, attack);

                if (e.UnitEs(idx_from).TypeE.UnitTC.IsMelee)
                    e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += e.UnitEs(idx_to).DamageOnCell(e.CellEs(idx_to), e.UnitStatUpgradesEs);


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

                if (!e.UnitEs(idx_to).TypeE.UnitTC.IsMelee) powerDam_to /= 2;

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


                if (e.UnitEs(idx_from).TypeE.UnitTC.IsMelee)
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
                    if (e.UnitEs(idx_to).TypeE.UnitTC.IsAnimal)
                    {
                        e.InventorResourcesEs.Resource(ResourceTypes.Food, e.UnitEs(idx_from).OwnerE.OwnerC.Player).Add(ResourcesInInventorValues.AMOUNT_FOOD_AFTER_KILL_CAMEL);
                    }

                    e.UnitEs(idx_to).TypeE.Kill(e);


                    if (e.UnitEs(idx_from).TypeE.UnitTC.IsMelee)
                    {
                        if (!e.UnitStatEs(idx_from).Hp.IsAlive)
                        {
                            e.UnitEs(idx_from).TypeE.Kill(e);
                        }
                        else
                        {
                            e.UnitEs(idx_from).Shift(idx_to, e);
                        }
                    }
                }

                else if (!e.UnitStatEs(idx_from).Hp.IsAlive)
                {
                    e.UnitEs(idx_from).TypeE.Kill(e);
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


                //if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
        public void UpgradeUnit_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var unit_0 = UnitTC;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;

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
    }
}