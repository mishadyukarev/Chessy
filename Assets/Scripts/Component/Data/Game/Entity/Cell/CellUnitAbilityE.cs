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

            if (!e.UnitEs(idx_to).MainE.HaveUnit)
            {
                if (!e.UnitEs(idx_from).Ability(_ability).HaveCooldown)
                {
                    if (e.UnitStatStepEs(idx_from).Have(_ability))
                    {
                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();
                        e.UnitStatStepEs(idx_from).Take(_ability);

                        if (e.UnitEs(idx_to).WhoLastDiedHereE.HaveDeadUnit)
                        {
                            var whoLast_to = e.UnitEs(idx_to).WhoLastDiedHereE;

                            e.UnitEs(idx_to).MainE.SetNew((whoLast_to.UnitTC.Unit, whoLast_to.LevelTC.Level, whoLast_to.OwnerC.Player, ConditionUnitTypes.None, false), e);
                            e.UnitEs(idx_to).WhoLastDiedHereE.Clear();
                        }
                    }
                    else
                    {
                        e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                }
            }
        }
        public void BonusNear_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitStatEs(idx_0).StepE.Have(_ability))
                {
                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    e.UnitStatEs(idx_0).StepE.Take(_ability);
                    e.UnitEs(idx_0).ConditionE.Reset();

                    e.Rpc.SoundToGeneral(sender, _ability);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = e.CellWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = e.CellWorker.GetIdxCell(xy);

                        var ownUnit_1 = e.UnitEs(idx_1).OwnerE.OwnerC;

                        if (e.UnitEs(idx_1).MainE.HaveUnit)
                        {
                            if (ownUnit_1.Is(e.UnitEs(idx_0).OwnerE.OwnerC.Player))
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
                    e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else e.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void CircularAttack_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var ownUnit_0 = e.UnitEs(idx_0).OwnerE.OwnerC;


            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitStatEs(idx_0).StepE.Have(_ability))
                {
                    e.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    foreach (var xy1 in e.CellWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy))
                    {
                        var idx_1 = e.CellWorker.GetIdxCell(xy1);

                        var ownUnit_1 = e.UnitEs(idx_1).OwnerE.OwnerC;
                        var tw_1 = e.UnitEs(idx_1).ToolWeaponE.ToolWeaponTC;


                        if (e.UnitEs(idx_1).MainE.HaveUnit)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //foreach (var item in CellUnitEffectsEs.Keys) 
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_1).Disable();

                                if (tw_1.Is(ToolWeaponTypes.Shield))
                                {
                                    e.UnitEs(idx_1).ToolWeaponE.BreakShield();
                                }
                                else
                                {
                                    e.UnitStatEs(idx_1).Hp.Attack(_ability, e);
                                }
                            }
                        }
                    }

                    e.UnitStatEs(idx_0).StepE.Take(_ability);
                    //foreach (var item in CellUnitEffectsEs.Keys) 
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Disable();

                    e.Rpc.SoundToGeneral(sender, ClipTypes.AttackMelee);


                    e.UnitEs(idx_0).ConditionE.Reset();
                }
                else
                {
                    e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else e.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void ChangeDirectionWind_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = Idx;

            if (e.UnitStatEs(idx_from).Hp.HaveMax)
            {
                if (e.UnitStatEs(idx_from).StepE.Have(_ability))
                {
                    e.CellWorker.TryGetDirect(e.WindE.CenterCloud.Idx, idx_to, out var newDir);

                    if (newDir != DirectTypes.None)
                    {
                        e.WindE.DirectWind.Direct = newDir;

                        e.UnitStatEs(idx_from).StepE.Take(_ability);

                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();

                        e.Rpc.SoundToGeneral(RpcTarget.All, _ability);
                    }
                }

                else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
        public void ActiveSnowyAround_Master(in Player sender, in Entities e)
        {
            var whoseMove = e.WhoseMove.WhoseMove.Player;
            var idx_0 = Idx;

            if (e.UnitStatEs(idx_0).WaterE.Have(_ability) || e.RiverEs(idx_0).River.HaveRiverNear)
            {
                if (!e.RiverEs(idx_0).River.HaveRiverNear) e.UnitStatEs(idx_0).WaterE.Take(_ability);

                if (e.UnitStatEs(idx_0).StepE.Have(_ability))
                {
                    e.UnitStatEs(idx_0).StepE.Take(_ability);
                    e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                    foreach (var idx_1 in e.CellWorker.GetIdxsAround(idx_0))
                    {
                        if (e.UnitEs(idx_1).MainE.HaveUnit)
                        {
                            if (e.UnitEs(idx_1).OwnerE.OwnerC.Is(whoseMove))
                            {
                                if (e.UnitEs(idx_1).MainE.UnitTC.IsMelee && !e.UnitEs(idx_1).MainE.UnitTC.Is(UnitTypes.Camel, UnitTypes.Scout))
                                {
                                    e.UnitStatEs(idx_1).WaterE.SetMax(e.UnitEs(idx_1), e.UnitStatUpgradesEs);
                                    e.UnitStatEs(idx_1).Hp.SetMax();
                                    e.UnitEffectEs(idx_1).ShieldE.Set(_ability);
                                }
                                if (e.UnitEs(idx_1).MainE.UnitTC.Is(UnitTypes.Archer))
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
            var whoseMove = e.WhoseMove.WhoseMove.Player;
            var ability = AbilityTypes.DirectWave;

            var idx_from = Idx;

            if (e.CellWorker.TryGetDirect(idx_from, idx_to, out var direct_0))
            {
                if (e.UnitStatEs(idx_from).WaterE.Have(ability) || e.RiverEs(idx_from).River.HaveRiverNear)
                {
                    if (!e.RiverEs(idx_from).River.HaveRiverNear) e.UnitStatEs(idx_from).WaterE.Take(ability);

                    if (e.UnitStatEs(idx_from).StepE.Have(ability))
                    {
                        e.UnitStatEs(idx_from).StepE.Take(ability);
                        e.UnitEs(idx_from).Ability(ability).SetAfterAbility();

                        var idx_0 = idx_to;

                        for (var i = 0; i < 3; i++)
                        {
                            if (!e.CellEs(idx_0).ParentE.IsActiveSelf.IsActive) break;

                            if (e.UnitEs(idx_0).MainE.HaveUnit)
                            {
                                if (e.UnitEs(idx_0).OwnerE.OwnerC.Is(whoseMove))
                                {
                                    //UnitEffectEs(idx_0).ShieldE.Set(ability);
                                    //UnitStatEs(idx_0).Hp.SetMax();
                                    //UnitStatEs(idx_0).Water.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
                                }
                                else
                                {
                                    e.UnitEffectEs(idx_0).StunE.Set(ability);
                                }
                            }

                            e.EffectEs(idx_0).FireE.Disable();

                            idx_0 = e.CellWorker.GetIdxCellByDirect(idx_0, direct_0);
                        }
                    }
                    else
                    {
                        e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                }
            }
        }
        public void BuildFarm_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var whoseMove = e.WhoseMove.WhoseMove.Player;
            var buildC = e.BuildEs(idx_0).BuildingE.BuildTC;

            if (e.UnitStatEs(idx_0).StepE.Have(_ability))
            {
                if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                {
                    if (!e.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        if (e.InventorResourcesEs.TryBuyBuilding_Master(BuildingTypes.Farm, whoseMove, sender, e))
                        {
                            e.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                            e.EnvironmentEs(idx_0).YoungForest.Destroy(e.WhereEnviromentEs);

                            if (e.EnvironmentEs(idx_0).Fertilizer.HaveEnvironment)
                            {
                                e.EnvironmentEs(idx_0).Fertilizer.AddAfterBuildingFarm();
                            }
                            else
                            {
                                e.EnvironmentEs(idx_0).Fertilizer.SetNewRandom(e.WhereEnviromentEs);
                            }

                            e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Farm, whoseMove, e.BuildEs(idx_0), e.WhereBuildingEs);

                            e.UnitStatEs(idx_0).StepE.Take(_ability);
                        }
                    }
                    else
                    {
                        e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                }
                else
                {
                    e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
            }

            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void BuildMine_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var build_0 = e.BuildEs(idx_0).BuildingE.BuildTC;
            var whoseMove = e.WhoseMove.WhoseMove.Player;


            if (e.UnitStatEs(idx_0).StepE.Have(_ability))
            {
                if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                {
                    if (e.EnvironmentEs(idx_0).Hill.HaveEnvironment
                        && e.EnvironmentEs(idx_0).Hill.HaveEnvironment)
                    {
                        if (e.InventorResourcesEs.TryBuyBuilding_Master(BuildingTypes.Mine, whoseMove, sender, e))
                        {
                            e.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                            e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Mine, whoseMove, e.BuildEs(idx_0), e.WhereBuildingEs);
                            e.WhereBuildingEs.HaveBuild(BuildingTypes.Mine, whoseMove, idx_0).HaveBuilding.Have = true;

                            e.UnitStatEs(idx_0).StepE.Take(_ability);
                        }
                    }

                    else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            }
            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void BuildCity_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;
            var whoseMove = e.WhoseMove.WhoseMove.Player;


            if (e.UnitStatEs(idx_0).StepE.Have(_ability))
            {
                bool haveNearBorder = false;

                foreach (var idx_1 in e.CellWorker.GetIdxsAround(idx_0))
                {
                    if (!e.CellEs(idx_1).ParentE.IsActiveSelf.IsActive)
                    {
                        haveNearBorder = true;
                        break;
                    }
                }

                if (!haveNearBorder)
                {
                    e.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                    e.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                    e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.City, whoseMove, e.BuildEs(idx_0), e.WhereBuildingEs);
                    e.WhereBuildingEs.HaveBuild(BuildingTypes.City, whoseMove, idx_0).HaveBuilding.Have = true;

                    e.UnitStatEs(idx_0).StepE.Take(_ability);


                    e.EffectEs(idx_0).FireE.Disable();


                    e.EnvironmentEs(idx_0).AdultForest.Destroy(e.TrailEs(idx_0).Trails, e.WhereEnviromentEs);
                    e.EnvironmentEs(idx_0).Fertilizer.Destroy(e.WhereEnviromentEs);
                    e.EnvironmentEs(idx_0).YoungForest.Destroy(e.WhereEnviromentEs);
                }

                else
                {
                    e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                }
            }
            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void SetIceWallSnowy_Master(in Entities e)
        {
            var idx_0 = Idx;

            if (e.UnitStatEs(idx_0).WaterE.Have(_ability) || e.RiverEs(idx_0).River.HaveRiverNear)
            {
                if (!e.BuildEs(idx_0).BuildingE.HaveBuilding)
                {
                    e.EnvironmentEs(idx_0).AdultForest.Destroy(e.TrailEs(idx_0).Trails, e.WhereEnviromentEs);
                    e.EnvironmentEs(idx_0).Fertilizer.Destroy(e.WhereEnviromentEs);

                    if (e.UnitStatEs(idx_0).StepE.Have(_ability))
                    {
                        e.UnitStatEs(idx_0).StepE.Take(_ability);

                        e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                        e.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.IceWall, e.UnitEs(idx_0).OwnerE.OwnerC.Player, e.BuildEs(idx_0), e.WhereBuildingEs);
                        e.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, e.UnitEs(idx_0).OwnerE.OwnerC.Player, idx_0).HaveBuilding.Have = true;
                    }
                }
            }
        }
        public void Destroy_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;
            var ownUnit_0 = e.UnitEs(idx_0).OwnerE.OwnerC;
            var buildC_0 = e.BuildEs(idx_0).BuildingE.BuildTC;


            if (e.UnitStatEs(idx_0).StepE.HaveSteps)
            {
                e.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    e.WinnerE.Winner.Player = ownUnit_0.Player;
                }
                e.UnitStatEs(idx_0).StepE.Take(AbilityTypes.DestroyBuilding);

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    e.EnvironmentEs(idx_0).Fertilizer.Destroy(e.WhereEnviromentEs);
                }

                e.WhereBuildingEs.HaveBuild(e.BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                e.BuildEs(idx_0).BuildingE.Destroy(e.BuildEs(idx_0), e.WhereBuildingEs);
            }
            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void ChangeCornerArcher_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            if (e.UnitStatEs(idx_0).Hp.HaveMax)
            {
                if (e.UnitStatEs(idx_0).StepE.Have(_ability))
                {
                    e.UnitEs(idx_0).CornedE.IsCornered = !e.UnitEs(idx_0).CornedE.IsCornered;

                    e.UnitStatEs(idx_0).StepE.Take(_ability);

                    e.Rpc.SoundToGeneral(sender, ClipTypes.PickArcher);
                }
                else
                {
                    e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
        public void StunElfemale_Master(in byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = Idx;
            var whoseMove = e.WhoseMove.WhoseMove.Player;


            if (!e.UnitEs(idx_from).Ability(_ability).HaveCooldown)
            {
                if (e.UnitEs(idx_to).VisibleE(whoseMove).IsVisibleC.IsVisible)
                {
                    if (e.UnitEs(idx_to).MainE.HaveUnit)
                    {
                        if (e.EnvironmentEs(idx_to).AdultForest.HaveEnvironment)
                        {
                            if (e.UnitStatEs(idx_from).Hp.HaveMax)
                            {
                                if (e.UnitStatEs(idx_from).StepE.Have(_ability))
                                {
                                    if (!e.UnitEs(idx_from).OwnerE.OwnerC.Is(e.UnitEs(idx_to).OwnerE.OwnerC.Player))
                                    {
                                        e.UnitEffectEs(idx_to).StunE.Set(_ability);
                                        e.UnitEs(idx_from).Ability(_ability).SetAfterAbility();

                                        e.UnitStatEs(idx_from).StepE.Take(_ability);

                                        e.Rpc.SoundToGeneral(RpcTarget.All, _ability);


                                        foreach (var idx_1 in e.CellWorker.GetIdxsAround(idx_to))
                                        {
                                            if (e.EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                                            {
                                                if (e.UnitEs(idx_1).MainE.HaveUnit && e.UnitEs(idx_1).OwnerE.OwnerC.Is(e.UnitEs(idx_to).OwnerE.OwnerC.Player))
                                                {
                                                    e.UnitEffectEs(idx_1).StunE.Set(_ability);
                                                }
                                            }
                                        }
                                    }
                                }

                                else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                    }
                }
            }

            else e.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
        public void FireArcher_Master(byte idx_to, in Player sender, in Entities e)
        {
            var idx_from = Idx;

            if (e.UnitStatEs(idx_from).StepE.Have(_ability))
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    e.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    e.UnitStatEs(idx_from).StepE.Take(_ability);
                    e.EffectEs(idx_to).FireE.Enable();
                }
            }

            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void PutOut_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            if (e.UnitStatEs(idx_0).StepE.Have(_ability))
            {
                e.EffectEs(idx_0).FireE.Disable();

                e.UnitStatEs(idx_0).StepE.Take(_ability);
            }

            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void FirePawn_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            if (e.UnitStatEs(idx_0).StepE.Have(_ability))
            {
                if (e.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                {
                    e.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    e.EffectEs(idx_0).FireE.Enable();
                    e.UnitStatEs(idx_0).StepE.Take(_ability);
                }
                else
                {
                    throw new Exception();
                }
            }

            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void Seed_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            if (e.UnitStatEs(idx_0).StepE.Have(_ability))
            {
                if (e.BuildEs(idx_0).BuildingE.HaveBuilding && !e.BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.Camp))
                {
                    e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    if (!e.EnvironmentEs(idx_0).Fertilizer.HaveEnvironment)
                    {
                        if (!e.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)

                            if (!e.EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                            {
                                e.Rpc.SoundToGeneral(sender, _ability);

                                e.EnvironmentEs(idx_0).YoungForest.SetNewRandom(e.WhereEnviromentEs);

                                e.UnitStatEs(idx_0).StepE.Take(_ability);
                            }
                            else
                            {
                                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            }
                        else
                        {
                            e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                    }
                    else
                    {
                        e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }

                }
            }

            else
            {
                e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
        public void GrowElfemale_Master(in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var ownUnit_0 = e.UnitEs(idx_0).OwnerE.OwnerC;


            if (!e.UnitEs(idx_0).Ability(_ability).HaveCooldown)
            {
                if (e.UnitStatEs(idx_0).StepE.Have(_ability))
                {
                    if (e.EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                    {
                        e.EnvironmentEs(idx_0).YoungForest.Destroy(e.WhereEnviromentEs);

                        e.EnvironmentEs(idx_0).AdultForest.SetNewMax(e.WhereEnviromentEs);

                        e.UnitStatEs(idx_0).StepE.Take(_ability);

                        e.UnitEs(idx_0).Ability(_ability).SetAfterAbility();

                        e.Rpc.SoundToGeneral(sender, _ability);

                        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        //{
                        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        //}
                        var around = e.CellWorker.GetXyAround(e.CellEs(idx_0).CellE.XyC.Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = e.CellWorker.GetIdxCell(xy_1);

                            var ownUnit_1 = e.UnitEs(idx_1).OwnerE.OwnerC;

                            if (e.UnitEs(idx_1).MainE.HaveUnit)
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

                    else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else e.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                e.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}