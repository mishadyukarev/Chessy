using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitAbilityE : CellEntityAbstract
    {
        readonly AbilityTypes _ability;
        ref AmountC CooldownRef => ref Ent.Get<AmountC>();

        public int Cooldown
        {
            get => CooldownRef.Amount;
            set => CooldownRef.Amount = value;
        }

        public bool HaveCooldown => Cooldown > 0;

        internal CellUnitAbilityE(in AbilityTypes ability, in CellEs cellEs, in EcsWorld gameW) : base(cellEs, gameW)
        {
            _ability = ability;
        }

        public void SetAfterAbility()
        {
            Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);
        }



        public void ResurrectUnit_Master(in Player sender, in byte idx_to, in Entities e)
        {
            var idx_from = CellEs.Idx;

            if (!e.UnitTC(idx_to).HaveUnit)
            {
                if (!e.UnitEs(idx_from).Ability(_ability).HaveCooldown)
                {
                    if (e.UnitStepC(idx_from).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                    {
                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();
                        e.UnitStepC(idx_from).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                        if (e.UnitEs(idx_to).WhoLastDiedHereE.HaveDeadUnit)
                        {
                            var whoLast_to = e.UnitEs(idx_to).WhoLastDiedHereE;

                            e.UnitE(idx_to).SetNew((whoLast_to.UnitTC.Unit, whoLast_to.LevelTC.Level, whoLast_to.PlayerTC.Player, ConditionUnitTypes.None, false), e);
                            e.UnitEs(idx_to).WhoLastDiedHereE.Clear();
                        }
                    }
                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                }
            }
        }
        public void BonusNear_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                {
                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                    e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                    e.RpcE.SoundToGeneral(sender, _ability);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = e.CellSpaceWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = e.CellSpaceWorker.GetIdxCell(xy);

                        if (e.UnitTC(idx_1).HaveUnit)
                        {
                            if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                            {
                                //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have)
                                //{
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have = true;
                                //}
                            }
                        }
                    }
                }
                else
                {
                    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void CircularAttack_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;


            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                {
                    e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    foreach (var xy1 in e.CellSpaceWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy))
                    {
                        var idx_1 = e.CellSpaceWorker.GetIdxCell(xy1);


                        if (e.UnitTC(idx_0).HaveUnit)
                        {
                            if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (e.ExtraTWE(idx_1).ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                                {
                                    e.UnitEs(idx_1).ExtraToolWeaponE.BreakShield(1);
                                }
                                else
                                {
                                    e.UnitE(idx_1).Take(_ability, e);
                                }
                            }
                        }
                    }

                    e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    e.RpcE.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                }
                else
                {
                    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void ChangeDirectionWind_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = CellEs.Idx;

            if (e.UnitHpC(idx_from).Health >= CellUnitStatHpValues.MAX_HP)
            {
                if (e.UnitStepC(idx_from).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                {
                    e.CellSpaceWorker.TryGetDirect(e.CenterCloudIdxC.Idx, idx_to, out var newDir);

                    if (newDir != DirectTypes.None)
                    {
                        e.DirectWind.Direct = newDir;

                        e.UnitStepC(idx_from).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();

                        e.RpcE.SoundToGeneral(RpcTarget.All, _ability);
                    }
                }

                else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
        public void ActiveSnowyAround_Master(in Player sender, in Entities e)
        {
            var whoseMove = e.WhoseMovePlayerTC.Player;
            var idx_0 = CellEs.Idx;

            if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)) || e.RiverEs(idx_0).RiverE.HaveRiverNear)
            {
                if (!e.RiverEs(idx_0).RiverE.HaveRiverNear) e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                {
                    e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    foreach (var idx_1 in e.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (e.UnitTC(idx_0).HaveUnit)
                        {
                            if (e.UnitPlayerTC(idx_1).Is(whoseMove))
                            {
                                if (e.UnitTC(idx_1).IsMelee && e.MainTWE(idx_1).ToolWeaponTC.IsMelee && !e.UnitTC(idx_1).Is(UnitTypes.Camel, UnitTypes.Scout))
                                {
                                    e.UnitE(idx_1).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                                    e.UnitHpC(idx_1).Health = CellUnitStatHpValues.MAX_HP;
                                    e.UnitEffectShield(idx_1).Protection = CellUnitEffectShield_Values.ProtectionAfterAbility(_ability);
                                }
                                if (e.ExtraTWE(idx_1).ToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow))
                                {
                                    e.UnitFrozenArrawC(idx_1).Shoots = 0;
                                }
                            }
                            else
                            {
                                e.UnitE(idx_1).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(_ability);
                            }
                        }

                        e.EffectEs(idx_1).FireE.Disable();
                    }
                }
            }
        }
        public void DirectWaveSnowy_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var whoseMove = e.WhoseMovePlayerTC.Player;
            var ability = AbilityTypes.DirectWave;

            var idx_from = CellEs.Idx;

            if (e.CellSpaceWorker.TryGetDirect(idx_from, idx_to, out var direct_0))
            {
                if (e.UnitStepC(idx_from).Have(CellUnitStatStepValues.NeedForAbility(ability)) || e.RiverEs(idx_from).RiverE.HaveRiverNear)
                {
                    if (!e.RiverEs(idx_from).RiverE.HaveRiverNear) e.UnitStepC(idx_from).Take(CellUnitStatStepValues.NeedForAbility(ability));

                    if (e.UnitStepC(idx_from).Have(CellUnitStatStepValues.NeedForAbility(ability)))
                    {
                        e.UnitStepC(idx_from).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                        e.UnitEs(idx_from).Ability(ability).SetAfterAbility();

                        var idx_0 = idx_to;

                        for (var i = 0; i < 3; i++)
                        {
                            if (!e.CellEs(idx_0).ParentE.IsActiveSelf.IsActive) break;

                            if (e.UnitTC(idx_0).HaveUnit)
                            {
                                if (e.UnitPlayerTC(idx_0).Is(whoseMove))
                                {
                                    //UnitEffectEs(idx_0).ShieldE.Set(ability);
                                    //UnitE(idx_0).SetMax();
                                    //UnitStatEs(idx_0).Water.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
                                }
                                else
                                {
                                    e.UnitE(idx_0).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(ability);
                                }
                            }

                            e.EffectEs(idx_0).FireE.Disable();

                            idx_0 = e.CellSpaceWorker.GetIdxCellByDirect(idx_0, direct_0);
                        }
                    }
                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                }
            }
        }
        public void BuildFarm_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            var whoseMove = e.WhoseMovePlayerTC.Player;

            if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
            {
                if (!e.BuildingE(idx_0).HaveBuilding || e.BuildingE(idx_0).Is(BuildingTypes.Camp))
                {
                    if (!e.AdultForestE(idx_0).HaveEnvironment)
                    {
                        if (e.InventorResourcesEs.CanBuyBuilding_Master(BuildingTypes.Farm, whoseMove, out var needRes))
                        {
                            e.InventorResourcesEs.BuyBuilding_Master(BuildingTypes.Farm, whoseMove);

                            e.RpcE.SoundToGeneral(sender, ClipTypes.Building);

                            e.YoungForestE(idx_0).SetZeroResources();

                            e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Farm, whoseMove);

                            e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                        }
                        else
                        {
                            e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                        }
                    }
                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                }
                else
                {
                    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
            }

            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        //public void BuildMine_Master(in Player sender, in Entities e)
        //{
        //    var idx_0 = Idx;

        //    var build_0 = e.BuildEs(idx_0).BuildingE.BuildTC;
        //    var whoseMove = e.WhoseMove.WhoseMove.Player;


        //    if (e.UnitE(idx_0).Have(_ability))
        //    {
        //        if (!e.EnvAdultForestE(idx_0).HaveEnvironment)
        //        {
        //            if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
        //            {
        //                if (e.EnvironmentEs(idx_0).Hill.HaveEnvironment
        //                    && e.EnvironmentEs(idx_0).Hill.HaveEnvironment)
        //                {
        //                    if (e.InventorResourcesEs.TryBuyBuilding_Master(BuildingTypes.Mine, whoseMove, sender, e))
        //                    {
        //                        e.Rpc.SoundToGeneral(sender, ClipTypes.Building);

        //                        e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Mine, whoseMove);
        //                        //e.WhereBuildingEs.HaveBuild(BuildingTypes.Mine, whoseMove, idx_0).HaveBuilding.Have = true;

        //                        e.UnitE(idx_0).Take(_ability);
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
        public void BuildCity_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;
            var whoseMove = e.WhoseMovePlayerTC.Player;


            if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
            {
                if (!e.AdultForestE(idx_0).HaveEnvironment)
                {
                    bool haveNearBorder = false;

                    foreach (var idx_1 in e.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (!e.CellEs(idx_1).ParentE.IsActiveSelf.IsActive)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        e.RpcE.SoundToGeneral(sender, ClipTypes.Building);
                        e.RpcE.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.City, whoseMove);
                        //e.WhereBuildingEs.HaveBuild(BuildingTypes.City, whoseMove, idx_0).HaveBuilding.Have = true;

                        e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));


                        e.EffectEs(idx_0).FireE.Disable();


                        e.EnvironmentEs(idx_0).AdultForest.Destroy(e.TrailEs(idx_0).Trails);
                        e.EnvironmentEs(idx_0).Fertilizer.SetZeroResources();
                        e.EnvironmentEs(idx_0).YoungForest.SetZeroResources();
                    }

                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                    }
                }
                else
                {
                    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
            }
            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void SetIceWallSnowy_Master(in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)) || e.RiverEs(idx_0).RiverE.HaveRiverNear)
            {
                if (!e.BuildingE(idx_0).HaveBuilding)
                {
                    if (!e.AdultForestE(idx_0).HaveEnvironment)
                    {
                        e.AdultForestE(idx_0).Destroy(e.TrailEs(idx_0).Trails);
                        e.FertilizeE(idx_0).SetZeroResources();

                        if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                        {
                            e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                            e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                            e.BuildingE(idx_0).SetNew(BuildingTypes.IceWall, e.UnitPlayerTC(idx_0).Player);
                            //e.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, e.UnitPlayerTC(idx_0).PlayerC.Player, idx_0).HaveBuilding.Have = true;
                        }
                    }
                }
            }
        }
        public void SetTeleport_Master(in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (!e.BuildingE(idx_0).HaveBuilding)
            {
                if (!e.AdultForestE(idx_0).HaveEnvironment)
                {
                    e.YoungForestE(idx_0).SetZeroResources();
                    e.FertilizeE(idx_0).SetZeroResources();

                    if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                    {
                        e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                        if (e.StartTeleportIdxC.HaveStart)
                        {
                            if (e.EndTeleportIdxC.HaveEnd)
                            {
                                e.BuildingE(e.StartTeleportIdxC.Idx).Destroy(e);

                                e.StartTeleportIdxC.Idx = e.EndTeleportIdxC.Idx;

                                e.EndTeleportIdxC.Idx = idx_0;
                                SetAfterAbility();
                            }
                            else
                            {
                                e.EndTeleportIdxC.Idx = idx_0;
                                SetAfterAbility();
                            }
                        }
                        else
                        {
                            e.StartTeleportIdxC.Idx = idx_0;
                        }

                        e.BuildingE(idx_0).SetNew(BuildingTypes.Teleport, e.UnitPlayerTC(idx_0).Player);
                    }
                }
            }
        }
        public void Destroy_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (e.UnitStepC(idx_0).HaveSteps)
            {
                e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (e.BuildingE(idx_0).Is(BuildingTypes.City))
                {
                    e.WinnerC.Player = e.UnitPlayerTC(idx_0).Player;
                }
                e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                if (e.BuildingE(idx_0).Is(BuildingTypes.Farm))
                {
                    e.EnvironmentEs(idx_0).Fertilizer.SetZeroResources();
                }

                //e.WhereBuildingEs.HaveBuild(e.BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                e.BuildEs(idx_0).BuildingE.Destroy(e);
            }
            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void ChangeCornerArcher_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (e.UnitHpC(idx_0).Health >= CellUnitStatHpValues.MAX_HP)
            {
                if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                {
                    e.UnitIsRightArcherC(idx_0).ToggleSide();

                    e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                    e.RpcE.SoundToGeneral(sender, ClipTypes.PickArcher);
                }
                else
                {
                    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
        public void StunElfemale_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = CellEs.Idx;
            var whoseMove = e.WhoseMovePlayerTC.Player;


            if (!e.UnitEs(idx_from).Ability(_ability).HaveCooldown)
            {
                if (e.UnitEs(idx_to).VisibleE(whoseMove).IsVisible)
                {
                    if (e.UnitTC(idx_to).HaveUnit)
                    {
                        if (e.EnvironmentEs(idx_to).AdultForest.HaveEnvironment)
                        {
                            if (e.UnitHpC(idx_from).Health >= CellUnitStatHpValues.MAX_HP)
                            {
                                if (e.UnitStepC(idx_from).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                                {
                                    if (!e.UnitPlayerTC(idx_from).Is(e.UnitPlayerTC(idx_to).Player))
                                    {
                                        e.UnitE(idx_to).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(_ability);
                                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();

                                        e.UnitStepC(idx_from).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                                        e.RpcE.SoundToGeneral(RpcTarget.All, _ability);


                                        foreach (var idx_1 in e.CellSpaceWorker.GetIdxsAround(idx_to))
                                        {
                                            if (e.EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                                            {
                                                if (e.UnitTC(idx_1).HaveUnit && e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_to).Player))
                                                {
                                                    e.UnitE(idx_1).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(_ability);
                                                }
                                            }
                                        }
                                    }
                                }

                                else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void FireArcher_Master(byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = CellEs.Idx;

            if (e.UnitStepC(idx_from).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    e.UnitStepC(idx_from).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                    e.EffectEs(idx_to).FireE.Enable();
                }
            }

            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void PutOut_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
            {
                e.EffectEs(idx_0).FireE.Disable();

                e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));
            }

            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void FirePawn_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
            {
                if (e.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                {
                    e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    e.EffectEs(idx_0).FireE.Enable();
                    e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                }
                else
                {
                    throw new Exception();
                }
            }

            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void Seed_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;

            if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
            {
                if (e.BuildingE(idx_0).HaveBuilding && !e.BuildingE(idx_0).Is(BuildingTypes.Camp))
                {
                    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    if (!e.AdultForestE(idx_0).HaveEnvironment)
                    {
                        if (!e.YoungForestE(idx_0).HaveEnvironment)
                        {
                            e.RpcE.SoundToGeneral(sender, _ability);

                            e.YoungForestE(idx_0).SetRandomResources();

                            e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));
                        }
                        else
                        {
                            e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                    }
                    else
                    {
                        e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                }
            }

            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void GrowElfemale_Master(in Player sender, in Entities e)
        {
            var idx_0 = CellEs.Idx;



            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(_ability)))
                {
                    if (e.EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                    {
                        e.EnvironmentEs(idx_0).YoungForest.SetZeroResources();

                        e.EnvironmentEs(idx_0).AdultForest.SetMaxResources();

                        e.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(_ability));

                        e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                        e.RpcE.SoundToGeneral(sender, _ability);

                        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        //{
                        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        //}
                        var around = e.CellSpaceWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = e.CellSpaceWorker.GetIdxCell(xy_1);

                            if (e.UnitTC(idx_1).HaveUnit)
                            {
                                if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                                {
                                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                    //{
                                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                    //}
                                }
                            }
                        }

                    }

                    else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}