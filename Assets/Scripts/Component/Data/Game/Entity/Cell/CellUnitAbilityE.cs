﻿using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitAbilityE : CellEntityAbstract
    {
        readonly AbilityTypes _ability;
        ref AmountC CooldownRef => ref Ent.Get<AmountC>();
        public AmountC Cooldown => Ent.Get<AmountC>();

        public bool HaveCooldown => Cooldown.Amount > 0;

        internal CellUnitAbilityE(in AbilityTypes ability, in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
            _ability = ability;
        }

        internal void SetNew()
        {
            CooldownRef.Amount = 0;
        }
        internal void Shift(in CellUnitAbilityE cooldownE_from)
        {
            CooldownRef = cooldownE_from.Cooldown;
            cooldownE_from.CooldownRef.Amount = 0;
        }

        public void SetAfterAbility()
        {
            CooldownRef.Amount = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);
        }
        public void TakeAfterUpdate()
        {
            CooldownRef.Amount--;
        }
        public void SyncRpc(in int cooldown)
        {
            CooldownRef.Amount = cooldown;
        }



        public void ResurrectUnit_Master(in Player sender, in byte idx_to, in Entities e)
        {
            var idx_from = Idx;

            if (!e.UnitEs(idx_to).UnitE.HaveUnit)
            {
                if (!e.UnitEs(idx_from).Ability(_ability).HaveCooldown)
                {
                    if (e.UnitE(idx_from).HaveStepsForAbility(_ability))
                    {
                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();
                        e.UnitE(idx_from).Take(_ability);

                        if (e.UnitEs(idx_to).WhoLastDiedHereE.HaveDeadUnit)
                        {
                            var whoLast_to = e.UnitEs(idx_to).WhoLastDiedHereE;

                            e.UnitEs(idx_to).SetNew((whoLast_to.UnitTC.Unit, whoLast_to.LevelTC.Level, whoLast_to.OwnerC.Player, ConditionUnitTypes.None, false), e);
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
            var idx_0 = Idx;

            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
                {
                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    e.UnitE(idx_0).Take(_ability);
                    e.UnitE(idx_0).Condition = ConditionUnitTypes.None;

                    e.RpcE.SoundToGeneral(sender, _ability);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = e.CellSpaceWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = e.CellSpaceWorker.GetIdxCell(xy);

                        var ownUnit_1 = e.UnitE(idx_1).OwnerC;

                        if (e.UnitEs(idx_1).UnitE.HaveUnit)
                        {
                            if (ownUnit_1.Is(e.UnitE(idx_0).OwnerC.Player))
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
            var idx_0 = Idx;

            var ownUnit_0 = e.UnitE(idx_0).OwnerC;


            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
                {
                    e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    foreach (var xy1 in e.CellSpaceWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy))
                    {
                        var idx_1 = e.CellSpaceWorker.GetIdxCell(xy1);

                        var ownUnit_1 = e.UnitE(idx_1).OwnerC;
                        var tw_1 = e.UnitEs(idx_1).ExtraToolWeaponE.ToolWeaponTC;


                        if (e.UnitEs(idx_1).UnitE.HaveUnit)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (tw_1.Is(ToolWeaponTypes.Shield))
                                {
                                    e.UnitEs(idx_1).ExtraToolWeaponE.BreakShield();
                                }
                                else
                                {
                                    e.UnitE(idx_1).Attack(_ability, e);
                                }
                            }
                        }
                    }

                    e.UnitE(idx_0).Take(_ability);
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    e.RpcE.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    e.UnitE(idx_0).Condition = ConditionUnitTypes.None;
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
            var idx_from = Idx;

            if (e.UnitE(idx_from).HaveMaxHp)
            {
                if (e.UnitE(idx_from).HaveStepsForAbility(_ability))
                {
                    e.CellSpaceWorker.TryGetDirect(e.WindCloudE.CenterCloud.Idx, idx_to, out var newDir);

                    if (newDir != DirectTypes.None)
                    {
                        e.WindCloudE.DirectWind.Direct = newDir;

                        e.UnitE(idx_from).Take(_ability);

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
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;
            var idx_0 = Idx;

            if (e.UnitE(idx_0).HaveStepsForAbility(_ability) || e.RiverEs(idx_0).RiverE.HaveRiverNear)
            {
                if (!e.RiverEs(idx_0).RiverE.HaveRiverNear) e.UnitE(idx_0).Take(_ability);

                if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
                {
                    e.UnitE(idx_0).Take(_ability);
                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    foreach (var idx_1 in e.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (e.UnitEs(idx_1).UnitE.HaveUnit)
                        {
                            if (e.UnitE(idx_1).OwnerC.Is(whoseMove))
                            {
                                if (e.UnitEs(idx_1).UnitE.UnitTC.IsMelee && !e.UnitEs(idx_1).UnitE.UnitTC.Is(UnitTypes.Camel, UnitTypes.Scout))
                                {
                                    e.UnitE(idx_1).SetMaxWater(e.UnitStatUpgradesEs);
                                    e.UnitE(idx_1).SetMaxHp();
                                    e.UnitEffectEs(idx_1).ShieldE.Set(_ability);
                                }
                                if (e.UnitExtraTWE(idx_1).Is(ToolWeaponTypes.BowCrossbow))
                                {
                                    e.UnitEffectEs(idx_1).FrozenArrowE.Enable();
                                }
                            }
                            else
                            {
                                e.UnitEffectEs(idx_1).StunE.Set(_ability);
                            }
                        }

                        e.EffectEs(idx_1).FireE.Disable();
                    }
                }
            }
        }
        public void DirectWaveSnowy_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;
            var ability = AbilityTypes.DirectWave;

            var idx_from = Idx;

            if (e.CellSpaceWorker.TryGetDirect(idx_from, idx_to, out var direct_0))
            {
                if (e.UnitE(idx_from).HaveStepsForAbility(ability) || e.RiverEs(idx_from).RiverE.HaveRiverNear)
                {
                    if (!e.RiverEs(idx_from).RiverE.HaveRiverNear) e.UnitE(idx_from).Take(ability);

                    if (e.UnitE(idx_from).HaveStepsForAbility(ability))
                    {
                        e.UnitE(idx_from).Take(ability);
                        e.UnitEs(idx_from).Ability(ability).SetAfterAbility();

                        var idx_0 = idx_to;

                        for (var i = 0; i < 3; i++)
                        {
                            if (!e.CellEs(idx_0).ParentE.IsActiveSelf.IsActive) break;

                            if (e.UnitEs(idx_0).UnitE.HaveUnit)
                            {
                                if (e.UnitE(idx_0).OwnerC.Is(whoseMove))
                                {
                                    //UnitEffectEs(idx_0).ShieldE.Set(ability);
                                    //UnitE(idx_0).SetMax();
                                    //UnitStatEs(idx_0).Water.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
                                }
                                else
                                {
                                    e.UnitEffectEs(idx_0).StunE.Set(ability);
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
            var idx_0 = Idx;

            var whoseMove = e.WhoseMoveE.WhoseMove.Player;
            var buildC = e.BuildEs(idx_0).BuildingE.BuildTC;

            if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
            {
                if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                {
                    if (!e.EnvAdultForestE(idx_0).HaveEnvironment)
                    {
                        if (e.InventorResourcesEs.CanBuyBuilding_Master(BuildingTypes.Farm, whoseMove, out var needRes))
                        {
                            e.InventorResourcesEs.BuyBuilding_Master(BuildingTypes.Farm, whoseMove);

                            e.RpcE.SoundToGeneral(sender, ClipTypes.Building);

                            e.EnvYoungForestE(idx_0).Destroy();

                            e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Farm, whoseMove);

                            e.UnitE(idx_0).Take(_ability);
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
            var idx_0 = Idx;
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;


            if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
            {
                if (!e.EnvAdultForestE(idx_0).HaveEnvironment)
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

                        e.UnitE(idx_0).Take(_ability);


                        e.EffectEs(idx_0).FireE.Disable();


                        e.EnvironmentEs(idx_0).AdultForest.Destroy(e.TrailEs(idx_0).Trails);
                        e.EnvironmentEs(idx_0).Fertilizer.Destroy();
                        e.EnvironmentEs(idx_0).YoungForest.Destroy();
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
            var idx_0 = Idx;

            if (e.UnitE(idx_0).HaveStepsForAbility(_ability) || e.RiverEs(idx_0).RiverE.HaveRiverNear)
            {
                if (!e.BuildE(idx_0).HaveBuilding)
                {
                    if (!e.EnvAdultForestE(idx_0).HaveEnvironment)
                    {
                        e.EnvAdultForestE(idx_0).Destroy(e.TrailEs(idx_0).Trails);
                        e.EnvFertilizeE(idx_0).Destroy();

                        if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
                        {
                            e.UnitE(idx_0).Take(_ability);

                            e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                            e.BuildE(idx_0).SetNew(BuildingTypes.IceWall, e.UnitE(idx_0).OwnerC.Player);
                            //e.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, e.UnitE(idx_0).OwnerC.Player, idx_0).HaveBuilding.Have = true;
                        }
                    }
                }
            }
        }
        public void SetTeleport_Master(in Entities e)
        {
            var idx_0 = Idx;

            if (!e.BuildE(idx_0).HaveBuilding)
            {
                if (!e.EnvAdultForestE(idx_0).HaveEnvironment)
                {
                    e.EnvYoungForestE(idx_0).Destroy();
                    e.EnvFertilizeE(idx_0).Destroy();

                    if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
                    {
                        e.UnitE(idx_0).Take(_ability);

                        if (e.StartTeleportE.HaveStart)
                        {
                            if (e.EndTeleportE.HaveEnd)
                            {
                                e.BuildE(e.StartTeleportE.WhereC.Idx).Destroy(e);
                                e.StartTeleportE.Set(e.EndTeleportE);
                                e.EndTeleportE.Set(idx_0, e.UnitE(idx_0).OwnerC.Player);
                                SetAfterAbility();
                            }
                            else
                            {
                                e.EndTeleportE.Set(idx_0, e.UnitE(idx_0).OwnerC.Player);
                                SetAfterAbility();
                            }
                        }
                        else
                        {
                            e.StartTeleportE.Set(idx_0, e.UnitE(idx_0).OwnerC.Player);
                        }

                        e.BuildE(idx_0).SetNew(BuildingTypes.Teleport, e.UnitE(idx_0).OwnerC.Player);
                    }
                }
            }
        }
        public void Destroy_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;
            var ownUnit_0 = e.UnitE(idx_0).OwnerC;
            var buildC_0 = e.BuildEs(idx_0).BuildingE.BuildTC;


            if (e.UnitE(idx_0).HaveSteps)
            {
                e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    e.WinnerE.Winner.Player = ownUnit_0.Player;
                }
                e.UnitE(idx_0).Take(AbilityTypes.DestroyBuilding);

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    e.EnvironmentEs(idx_0).Fertilizer.Destroy();
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
            var idx_0 = Idx;

            if (e.UnitE(idx_0).HaveMaxHp)
            {
                if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
                {
                    e.UnitE(idx_0).IsRightArcher = !e.UnitE(idx_0).IsRightArcher;

                    e.UnitE(idx_0).Take(_ability);

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
            var idx_from = Idx;
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;


            if (!e.UnitEs(idx_from).Ability(_ability).HaveCooldown)
            {
                if (e.UnitEs(idx_to).VisibleE(whoseMove).IsVisibleC.IsVisible)
                {
                    if (e.UnitEs(idx_to).UnitE.HaveUnit)
                    {
                        if (e.EnvironmentEs(idx_to).AdultForest.HaveEnvironment)
                        {
                            if (e.UnitE(idx_from).HaveMaxHp)
                            {
                                if (e.UnitE(idx_from).HaveStepsForAbility(_ability))
                                {
                                    if (!e.UnitE(idx_from).OwnerC.Is(e.UnitE(idx_to).OwnerC.Player))
                                    {
                                        e.UnitEffectEs(idx_to).StunE.Set(_ability);
                                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();

                                        e.UnitE(idx_from).Take(_ability);

                                        e.RpcE.SoundToGeneral(RpcTarget.All, _ability);


                                        foreach (var idx_1 in e.CellSpaceWorker.GetIdxsAround(idx_to))
                                        {
                                            if (e.EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                                            {
                                                if (e.UnitEs(idx_1).UnitE.HaveUnit && e.UnitE(idx_1).OwnerC.Is(e.UnitE(idx_to).OwnerC.Player))
                                                {
                                                    e.UnitEffectEs(idx_1).StunE.Set(_ability);
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
            var idx_from = Idx;

            if (e.UnitE(idx_from).HaveStepsForAbility(_ability))
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    e.UnitE(idx_from).Take(_ability);
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
            var idx_0 = Idx;

            if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
            {
                e.EffectEs(idx_0).FireE.Disable();

                e.UnitE(idx_0).Take(_ability);
            }

            else
            {
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void FirePawn_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
            {
                if (e.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                {
                    e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    e.EffectEs(idx_0).FireE.Enable();
                    e.UnitE(idx_0).Take(_ability);
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
            var idx_0 = Idx;

            if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
            {
                if (e.BuildE(idx_0).HaveBuilding && !e.BuildE(idx_0).Is(BuildingTypes.Camp))
                {
                    e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    if (!e.EnvAdultForestE(idx_0).HaveEnvironment)
                    {
                        if (!e.EnvYoungForestE(idx_0).HaveEnvironment)
                        {
                            e.RpcE.SoundToGeneral(sender, _ability);

                            e.EnvYoungForestE(idx_0).SetRandomResources();

                            e.UnitE(idx_0).Take(_ability);
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
            var idx_0 = Idx;

            var ownUnit_0 = e.UnitE(idx_0).OwnerC;


            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitE(idx_0).HaveStepsForAbility(_ability))
                {
                    if (e.EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                    {
                        e.EnvironmentEs(idx_0).YoungForest.Destroy();

                        e.EnvironmentEs(idx_0).AdultForest.SetNewMax();

                        e.UnitE(idx_0).Take(_ability);

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

                            var ownUnit_1 = e.UnitE(idx_1).OwnerC;

                            if (e.UnitEs(idx_1).UnitE.HaveUnit)
                            {
                                if (ownUnit_1.Is(ownUnit_0.Player))
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