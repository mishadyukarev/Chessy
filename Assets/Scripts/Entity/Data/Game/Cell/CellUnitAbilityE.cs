using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitAbilityE : CellEntityAbstract
    {
        readonly AbilityTypes _ability;
        public CooldownC CooldownC;

        internal CellUnitAbilityE(in AbilityTypes ability, in CellPoolEs cellEs, in EcsWorld gameW) : base(cellEs, gameW)
        {
            _ability = ability;
        }

        public void ResurrectUnit_Master(in Player sender, in byte idx_to, in Entities e)
        {
            //var idx_from = CellEs.Idx;

            //if (!e.UnitTC(idx_to).HaveUnit)
            //{
            //    if (!e.UnitEs(idx_from).Ability(_ability).CooldownC.HaveCooldown)
            //    {
            //        if (e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(_ability))
            //        {
            //            e.UnitEs(idx_from).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);
            //            e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(_ability);

            //            if (e.LastDiedE(idx_to).UnitTC.HaveUnit)
            //            {
            //                //e.UnitE(idx_to).SetNew((e.LastDiedUnitTC(idx_to).Unit, e.LastDiedLevelTC(idx_to).Level, e.LastDiedPlayerTC(idx_to).Player, ConditionUnitTypes.None, false), e);
            //                e.LastDiedUnitTC(idx_to).Unit = UnitTypes.None;
            //            }
            //        }
            //        else
            //        {
            //            e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //        }
            //    }
            //}
        }
        public void BonusNear_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (!e.UnitEs(idx_0).Ability(_ability).CooldownC.HaveCooldown)
            //{
            //    if (e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(_ability))
            //    {
            //        e.UnitEs(idx_0).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //        e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(_ability);
            //        e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

            //        e.RpcE.SoundToGeneral(sender, _ability);

            //        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
            //        //{
            //        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
            //        //}

            //        foreach (var idx_1 in e.CellEs(idx_0).Idxs)
            //        {
            //            if (e.UnitTC(idx_1).HaveUnit)
            //            {
            //                if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
            //                {
            //                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have)
            //                    //{
            //                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have = true;
            //                    //}
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //    }
            //}

            //else e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void CircularAttack_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;


            //if (!e.UnitEs(idx_0).Ability(_ability).CooldownC.HaveCooldown)
            //{
            //    if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //    {
            //        e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

            //        e.UnitEs(idx_0).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //        foreach (var idx_1 in e.CellEs(idx_0).Idxs)
            //        {
            //            if (e.UnitTC(idx_0).HaveUnit)
            //            {
            //                if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
            //                {
            //                    //foreach (var item in CellUnitEffectsEs.Keys) 
            //                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

            //                    if (e.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
            //                    {
            //                        //e.UnitExtraProtectionShieldTC(idx_1).BreakShield(1, e.UnitExtraTWTC(idx_1));
            //                    }
            //                    else
            //                    {
            //                        //e.UnitE(idx_1).Steps -=_ability, e);
            //                    }
            //                }
            //            }
            //        }

            //        e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);
            //        //foreach (var item in CellUnitEffectsEs.Keys) 
            //        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

            //        e.RpcE.SoundToGeneral(sender, ClipTypes.AttackMelee);


            //        e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
            //    }
            //    else
            //    {
            //        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //    }
            //}
            //else e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void ChangeDirectionWind_Master(in byte idx_to, in Player sender, ref Entities e)
        {
            //var idx_from = CellEs.Idx;

            //if (e.UnitHpC(idx_from).Health >= CellUnitStatHp_Values.MAX_HP)
            //{
            //    if (e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(_ability))
            //    {
            //        e.DirectWindTC.Direct = e.CellEs(e.CenterCloudIdxC.Idx).Direct(idx_to).Direct;

            //        e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(_ability);

            //        e.UnitEs(idx_from).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //        e.RpcE.SoundToGeneral(RpcTarget.All, _ability);

            //    }

            //    else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
            //else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
        public void ActiveSnowyAround_Master(in Player sender, in Entities e)
        {
            //var whoseMove = e.WhoseMove.Player;
            //var idx_0 = CellEs.Idx;

            //if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability) || e.RiverEs(idx_0).RiverE.HaveRiverNear)
            //{
            //    if (!e.RiverEs(idx_0).RiverE.HaveRiverNear) e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);

            //    if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //    {
            //        e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);
            //        e.UnitEs(idx_0).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //        foreach (var idx_1 in e.CellEs(idx_0).Idxs)
            //        {
            //            if (e.UnitTC(idx_0).HaveUnit)
            //            {
            //                if (e.UnitPlayerTC(idx_1).Is(whoseMove))
            //                {
            //                    if (e.UnitEs(idx_1).IsMelee && !e.UnitTC(idx_1).Is(UnitTypes.Camel, UnitTypes.Scout))
            //                    {
            //                        e.UnitEs(idx_1).WaterC.Water = CellUnitStatWater_Values.MAX_WATER;
            //                        e.UnitHpC(idx_1).Health = CellUnitStatHp_Values.MAX_HP;
            //                        e.UnitEffectShield(idx_1).Protection = CellUnitEffectShield_Values.ProtectionAfterAbility(_ability);
            //                    }
            //                    if (e.UnitEs(idx_1).ExtraToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow))
            //                    {
            //                        e.UnitFrozenArrawC(idx_1).Shoots = 0;
            //                    }
            //                }
            //                else
            //                {
            //                    e.UnitEs(idx_1).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(_ability);
            //                }
            //            }

            //            e.EffectEs(idx_1).HaveFire = false;
            //        }
            //    }
            //}
        }
        public void DirectWaveSnowy_Master(in byte idx_to, in Player sender, in Entities e)
        {
            //var whoseMove = e.WhoseMove.Player;
            //var ability = AbilityTypes.DirectWave;

            //var idx_from = CellEs.Idx;


            ////var direct = e.CellEs(idx_from).Direct(idx_to).Direct;

            //if (e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability) || e.RiverEs(idx_from).RiverE.HaveRiverNear)
            //{
            //    if (!e.RiverEs(idx_from).RiverE.HaveRiverNear) e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

            //    if (e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
            //    {
            //        e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(_ability);
            //        e.UnitEs(idx_from).Ability(ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //        var idx_0 = idx_to;

            //        for (var i = 0; i < 3; i++)
            //        {
            //            if (!e.CellEs(idx_0).IsActiveParentSelf) break;

            //            if (e.UnitTC(idx_0).HaveUnit)
            //            {
            //                if (e.UnitPlayerTC(idx_0).Is(whoseMove))
            //                {
            //                    //UnitEffectEs(idx_0).ShieldE.Set(ability);
            //                    //UnitE(idx_0).SetMax();
            //                    //UnitStatEs(idx_0).Water.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
            //                }
            //                else
            //                {
            //                    e.UnitEs(idx_0).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(ability);
            //                }
            //            }

            //            e.HaveFire(idx_0) = false;

            //            //idx_0 = e.CellEs().GetIdxCellByDirect(idx_0, direct_0);
            //        }
            //    }
            //    else
            //    {
            //        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //    }
            //}
        }
        public void BuildFarm_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //var whoseMove = e.WhoseMove.Player;

            //if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //{
            //    if (!e.BuildTC(idx_0).HaveBuilding || e.BuildTC(idx_0).Is(BuildingTypes.Camp))
            //    {
            //        if (!e.AdultForestC(idx_0).HaveAny)
            //        {
            //            //if (e.InventorResourcesEs.CanBuyBuilding_Master(BuildingTypes.Farm, whoseMove, out var needRes))
            //            //{
            //            //    e.InventorResourcesEs.BuyBuilding_Master(BuildingTypes.Farm, whoseMove);

            //            //    e.RpcE.SoundToGeneral(sender, ClipTypes.Building);

            //            //    //e.YoungForestC(idx_0).SetZeroResources();

            //            //    //e.BuildE(idx_0).BuildingE.SetNew(BuildingTypes.Farm, whoseMove);

            //            //    e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);
            //            //}
            //            //else
            //            //{
            //            //    e.RpcE.MistakeEconomyToGeneral(sender, needRes);
            //            //}
            //        }
            //        else
            //        {
            //            e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            //        }
            //    }
            //    else
            //    {
            //        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            //    }
            //}

            //else
            //{
            //    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
        }
        //public void BuildMine_Master(in Player sender, in Entities e)
        //{
        //    var idx_0 = Idx;

        //    var build_0 = e.BuildEs(idx_0).BuildingE.BuildTC;
        //    var whoseMove = e.WhoseMove.WhoseMove.Player;


        //    if (e.UnitE(idx_0).Steps >=_ability))
        //    {
        //        if (!e.EnvAdultForestE(idx_0).HaveAny)
        //        {
        //            if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
        //            {
        //                if (e.EnvironmentEs(idx_0).Hill.HaveAny
        //                    && e.EnvironmentEs(idx_0).Hill.HaveAny)
        //                {
        //                    if (e.InventorResourcesEs.TryBuyBuilding_Master(BuildingTypes.Mine, whoseMove, sender, e))
        //                    {
        //                        e.Rpc.SoundToGeneral(sender, ClipTypes.Building);

        //                        e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Mine, whoseMove);
        //                        //e.WhereBuildingEs.HaveBuild(BuildingTypes.Mine, whoseMove, idx_0).HaveBuilding.Have = true;

        //                        e.UnitE(idx_0).Steps -=_ability);
        //                    }
        //                }

        //                else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
        //            }
        //            else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
        //        }
        //    }
        //    else
        //    {
        //        e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        //    }
        //}
        public void SetIceWallSnowy_Master(in Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability) || e.RiverEs(idx_0).RiverE.HaveRiverNear)
            //{
            //    if (!e.BuildTC(idx_0).HaveBuilding)
            //    {
            //        if (!e.AdultForestC(idx_0).HaveAny)
            //        {
            //            //e.AdultForestC(idx_0).Destroy(e.TrailEs(idx_0).Trails);
            //            e.FertilizeC(idx_0).Resources = 0;

            //            if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //            {
            //                e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);

            //                e.UnitEs(idx_0).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //                //e.BuildTC(idx_0).SetNew(BuildingTypes.IceWall, e.UnitPlayerTC(idx_0).Player);
            //                //e.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, e.UnitPlayerTC(idx_0).PlayerC.Player, idx_0).HaveBuilding.Have = true;
            //            }
            //        }
            //    }
            //}
        }
        public void SetTeleport_Master(ref Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (!e.BuildTC(idx_0).HaveBuilding)
            //{
            //    if (!e.AdultForestC(idx_0).HaveAny)
            //    {
            //        //e.YoungForestC(idx_0).SetZeroResources();
            //        e.FertilizeC(idx_0).Resources = 0;

            //        if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //        {
            //            e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);

            //            if (e.StartTeleportIdxC.Idx > 0)
            //            {
            //                if (e.EndTeleportIdxC.Idx > 0)
            //                {
            //                    //e.BuildTC(e.StartTeleportIdxC).Destroy(e);

            //                    e.StartTeleportIdxC = e.EndTeleportIdxC;

            //                    e.EndTeleportIdxC.Idx = idx_0;
            //                    CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);
            //                }
            //                else
            //                {
            //                    e.EndTeleportIdxC.Idx = idx_0;
            //                    CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);
            //                }
            //            }
            //            else
            //            {
            //                e.StartTeleportIdxC.Idx = idx_0;
            //            }

            //            //e.BuildingE(idx_0).SetNew(BuildingTypes.Teleport, e.UnitPlayerTC(idx_0).Player);
            //        }
            //    }
            //}
        }
        public void Destroy_Master(in Player sender, ref Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (e.UnitStepC(idx_0).HaveAnySteps)
            //{
            //    e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

            //    if (e.BuildTC(idx_0).Is(BuildingTypes.City))
            //    {
            //        e.WinnerC.Player = e.UnitPlayerTC(idx_0).Player;
            //    }
            //    e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);

            //    if (e.BuildTC(idx_0).Is(BuildingTypes.Farm))
            //    {
            //        e.EnvironmentEs(idx_0).FertilizeC.Resources = 0;
            //    }

            //    //e.WhereBuildingEs.HaveBuild(e.BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
            //    //e.BuildTC(idx_0).BuildingE.Destroy(e);
            //}
            //else
            //{
            //    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
        }
        public void ChangeCornerArcher_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (e.UnitHpC(idx_0).Health >= CellUnitStatHp_Values.MAX_HP)
            //{
            //    if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //    {
            //        e.UnitIsRightArcherC(idx_0).ToggleSide();

            //        e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);

            //        e.RpcE.SoundToGeneral(sender, ClipTypes.PickArcher);
            //    }
            //    else
            //    {
            //        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //    }
            //}
            //else
            //{
            //    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            //}
        }
        public void StunElfemale_Master(in byte idx_to, in Player sender, in Entities e)
        {
            //var idx_from = CellEs.Idx;
            //var whoseMove = e.WhoseMove.Player;


            //if (!e.UnitEs(idx_from).Ability(_ability).CooldownC.HaveCooldown)
            //{
            //    if (e.UnitEs(idx_to).ForPlayer(whoseMove).IsVisibleC)
            //    {
            //        if (e.UnitTC(idx_to).HaveUnit)
            //        {
            //            if (e.AdultForestC(idx_to).HaveAny)
            //            {
            //                if (e.UnitHpC(idx_from).Health >= CellUnitStatHp_Values.MAX_HP)
            //                {
            //                    if (e.UnitStepC(idx_from).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //                    {
            //                        if (!e.UnitPlayerTC(idx_from).Is(e.UnitPlayerTC(idx_to).Player))
            //                        {
            //                            e.UnitEs(idx_to).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(_ability);
            //                            e.UnitEs(idx_from).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //                            e.UnitStepC(idx_from).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);

            //                            e.RpcE.SoundToGeneral(RpcTarget.All, _ability);


            //                            foreach (var idx_1 in e.CellEs(idx_to).Idxs)
            //                            {
            //                                //if (e.AdultForestC(idx_1).AdultForest.HaveAny)
            //                                //{
            //                                //    if (e.UnitTC(idx_1).HaveUnit && e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_to).Player))
            //                                //    {
            //                                //        e.UnitE(idx_1).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(_ability);
            //                                //    }
            //                                //}
            //                            }
            //                        }
            //                    }

            //                    else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //                }
            //                else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            //            }
            //        }
            //    }
            //}

            //else e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void FireArcher_Master(byte idx_to, in Player sender, in Entities e)
        {
            //var idx_from = CellEs.Idx;

            //if (e.UnitStepC(idx_from).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //{
            //    if (e.UnitEs(idx_from).ForArson.Contains(idx_to))
            //    {
            //        e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

            //        e.UnitStepC(idx_from).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);
            //        e.HaveFire(idx_to) = false;
            //    }
            //}

            //else
            //{
            //    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
        }
        public void PutOut_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //{
            //    e.HaveFire(idx_0) = false;

            //    e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);
            //}

            //else
            //{
            //    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
        }
        public void FirePawn_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //{
            //    if (e.AdultForestC(idx_0).HaveAny)
            //    {
            //        e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

            //        e.HaveFire(idx_0) = true;
            //        e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }
            //}

            //else
            //{
            //    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
        }
        public void Seed_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;

            //if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //{
            //    if (e.BuildTC(idx_0).HaveBuilding && !e.BuildTC(idx_0).Is(BuildingTypes.Camp))
            //    {
            //        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            //    }
            //    else
            //    {
            //        if (!e.AdultForestC(idx_0).HaveAny)
            //        {
            //            if (!e.YoungForestC(idx_0).HaveAny)
            //            {
            //                e.RpcE.SoundToGeneral(sender, _ability);

            //                //e.YoungForestC(idx_0).SetRandomResources();

            //                e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);
            //            }
            //            else
            //            {
            //                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            //            }
            //        }
            //        else
            //        {
            //            e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            //        }
            //    }
            //}

            //else
            //{
            //    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
        }
        public void GrowElfemale_Master(in Player sender, in Entities e)
        {
            //var idx_0 = CellEs.Idx;



            //if (!e.UnitEs(idx_0).Ability(_ability).CooldownC.HaveCooldown)
            //{
            //    if (e.UnitStepC(idx_0).Steps >=CellUnitStatStep_Values.NeedForAbility(_ability))
            //    {
            //        if (e.YoungForestC(idx_0).HaveAny)
            //        {
            //            e.YoungForestC(idx_0).Resources = 0;

            //            e.AdultForestC(idx_0).Resources = CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES;

            //            e.UnitStepC(idx_0).Steps -=CellUnitStatStep_Values.NeedForAbility(_ability);

            //            e.UnitEs(idx_0).Ability(_ability).CooldownC.Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);

            //            e.RpcE.SoundToGeneral(sender, _ability);

            //            //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
            //            //{
            //            //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
            //            //}
            //            foreach (byte idx_1 in e.CellEs(idx_0).Idxs)
            //            {
            //                if (e.UnitTC(idx_1).HaveUnit)
            //                {
            //                    if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
            //                    {
            //                        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
            //                        //{
            //                        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
            //                        //}
            //                    }
            //                }
            //            }

            //        }

            //        else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            //    }
            //    else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
            //else
            //{
            //    e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
            //}
        }
    }
}