using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitE : CellEntityAbstract
    {
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
        //            if (!Ents.EnvironmentEs.Hill( idx).HaveAny && !twC.Is(ToolWeaponTypes.Pick)) return false;

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


        internal CellUnitE(in CellPoolEs cellEs, in EcsWorld gameW) : base(cellEs, gameW)
        {

        }

        //void Set(in byte idx_to, in Entities ents)
        //{
        //    var idx_0 = CellEs.Idx;

        //    ents.UnitTC(idx_to).Unit = UnitTC.Unit;
        //    ents.UnitIsRightArcherC(idx_to).IsRight = IsRightArcherC.IsRight;
        //    ents.UnitPlayerTC(idx_to).Player = PlayerTC.Player;
        //    ents.UnitConditionTC(idx_to).Condition = ConditionTC.Condition;
        //    ents.UnitLevelTC(idx_to).Level = LevelTC.Level;

        //    ents.UnitHpC(idx_to).Health = HealthC.Health;
        //    ents.UnitStepC(idx_to).Steps = StepC.Steps;
        //    ents.UnitWaterC(idx_to).Water = WaterC.Water;

        //    ents.UnitStunC(idx_to).Stun = StunC.Stun;
        //    ents.UnitEffectShield(idx_to).Protection = ShieldEffectC.Protection;
        //    ents.UnitFrozenArrawC(idx_to).Shoots = FrozenArrawC.Shoots;

        //    ents.UnitEs(idx_to).ExtaToolWeaponTC.ToolWeapon = ents.UnitEs(idx_0).ExtaToolWeaponTC.ToolWeapon;
        //    ents.UnitEs(idx_to).ExtraTWLevelTC.Level = ents.UnitEs(idx_0).ExtraTWLevelTC.Level;
        //    ents.UnitEs(idx_to).ExtraTWProtectionShieldC.Protection = ents.UnitEs(idx_0).ExtraTWProtectionShieldC.Protection;

        //    ents.UnitMainTWTC(idx_to).ToolWeapon = ents.UnitMainTWTC(idx_0).ToolWeapon;
        //    ents.UnitMainTWLevelTC(idx_to).Level = ents.UnitMainTWLevelTC(idx_0).Level;

        //    foreach (var abilityT in ents.UnitEs(idx_0).CooldownKeys) ents.UnitEs(idx_to).Ability(abilityT).CooldownC.Cooldown =  ents.UnitEs(idx_0).Ability(abilityT).CooldownC.Cooldown;
        //}
        //public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unit, in Entities ents)
        //{
        //    var idx_0 = CellEs.Idx;

        //    UnitTC.Unit = unit.Item1;
        //    LevelTC.Level = unit.Item2;
        //    PlayerTC.Player = unit.Item3;
        //    ConditionTC.Condition = unit.Item4;
        //    IsRightArcherC.IsRight = unit.Item5;

        //    ents.UnitHpC(idx_0).Health = CellUnitStatHpValues.MAX_HP;
        //    ents.UnitStepC(idx_0).Steps = CellUnitStatStepValues.StandartStepsUnit(CellEs.UnitC.Unit);
        //    WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);

        //    ents.UnitStunC(idx_0).Stun = 0;
        //    ents.UnitEffectShield(idx_0).Protection = 0;
        //    ents.UnitFrozenArrawC(idx_0).Shoots = 0;

        //    foreach (var item in ents.UnitEs(idx_0).CooldownKeys) ents.UnitEs(idx_0).Ability(item).CooldownC.Cooldown = 0;
        //}
        //public void SetNewPawn(in (LevelTypes, PlayerTypes, ConditionUnitTypes) unit, in Entities ents)
        //{
        //    var idx_0 = CellEs.Idx;

        //    SetNew((UnitTypes.Pawn, unit.Item1, unit.Item2, unit.Item3, false), ents);

        //    ents.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
        //    ents.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
        //}
        //public void Teleport(in byte idx_to, in Entities ents)
        //{
        //    Set(idx_to, ents);
        //    UnitTC.Unit = UnitTypes.None;
        //}
        //public void Shift_Master(in byte idx_to, in Player sender, in Entities e)
        //{
        //    var idx_from = CellEs.Idx;
        //    var whoseMove = e.WhoseMovePlayerTC.Player;

        //    if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
        //    {
        //        if (!e.TryGetDirect(idx_from, idx_to, out var dir)) throw new Exception();
        //        e.UnitStepC(idx_from).Take(e.UnitE(idx_from).ForShiftOrAttack(dir, e.CellEs(idx_to)));

        //        e.UnitE(idx_from).Shift(idx_to, true, e);

        //        e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
        //    }
        //}
        //public void Attack_Master(in byte idx_to, in Entities e)
        //{
        //    
        //}
        
        //public void Condition_Master(in ConditionUnitTypes cond, in Player sender, in Entities e)
        //{
        //    var idx_0 = CellEs.Idx;

        //    switch (cond)
        //    {
        //        case ConditionUnitTypes.None:
        //            e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
        //            break;

        //        case ConditionUnitTypes.Protected:
        //            if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
        //            {
        //                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
        //                e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
        //            }

        //            else if (e.UnitStepC(idx_0).HaveSteps)
        //            {
        //                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
        //                e.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_TOGGLE_CONDITION_UNIT);
        //                e.UnitConditionTC(idx_0).Condition = cond;
        //            }

        //            else
        //            {
        //                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        //            }
        //            break;


        //        case ConditionUnitTypes.Relaxed:
        //            if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
        //            {
        //                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
        //                e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
        //            }

        //            else if (e.UnitStepC(idx_0).HaveSteps)
        //            {
        //                e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
        //                e.UnitConditionTC(idx_0).Condition = cond;
        //                e.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_TOGGLE_CONDITION_UNIT);
        //            }

        //            else
        //            {
        //                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        //            }
        //            break;


        //        default:
        //            throw new Exception();
        //    }
        //}
        //public void InvokeSkeletons_Master(in AbilityTypes ability, in Player sender, in Entities ents)
        //{
        //    var idx_0 = CellEs.Idx;

        //    if (ents.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(ability)))
        //    {
        //        ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

        //        foreach (var idx_1 in ents.CellSpaceWorker.GetIdxsAround(idx_0))
        //        {
        //            if (!ents.UnitTC(idx_0).HaveUnit && !ents.MountainC(idx_1).HaveAny)
        //            {
        //                ents.UnitE(idx_1).SetNew((UnitTypes.Skeleton, LevelTypes.First, PlayerTC.Player, ConditionUnitTypes.None, false), ents);
        //            }

        //        }
        //    }
        //    else
        //    {
        //        ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        //    }
        //}

        //public void Take(in Entities ents, in float taking)
        //{

        //}
        //public void Take(in AbilityTypes ability, in Entities e) => Take(e, CellUnitStatHpValues.DamageAfterAbility(ability));
    }
}