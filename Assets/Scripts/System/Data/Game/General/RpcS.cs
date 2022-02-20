using Game.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public sealed class RpcS : MonoBehaviour
    {
        static Entities _e;
        static Systems _systems;
        static Action _updateView;
        static Action _updateUI;
        int _idx_cur;

        public static List<string> NamesMethods
        {
            get
            {
                var list = new List<string>();
                list.Add(nameof(MasterRPC));
                list.Add(nameof(GeneralRpc));
                list.Add(nameof(OtherRpc));
                return list;
            }
        }

        public RpcS GiveData(in Entities ents, in Systems systems, in Action updateView, in Action updateUI)
        {
            _e = ents;
            _systems = systems;
            _updateUI = updateUI;
            _updateView = updateView;
            return this;
        }


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var sender = infoFrom.Sender;
            var obj = objects[_idx_cur++];
            var whoseMove = _e.WhoseMove.Player;

            if (obj is AbilityTypes ability)
            {
                switch (ability)
                {
                    case AbilityTypes.CircularAttack:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_0).CoolDownC(ability).HaveCooldown)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                {
                                    _e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                                    _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                    foreach (var idxC_0 in _e.CellEs(idx_0).AroundCellIdxsC)
                                    {
                                        var idx_1 = idxC_0.Idx;

                                        if (_e.UnitTC(idx_0).HaveUnit)
                                        {
                                            if (!_e.UnitPlayerTC(idx_1).Is(_e.UnitPlayerTC(idx_0).Player))
                                            {
                                                if (_e.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
                                                {
                                                    _e.UnitExtraProtectionTC(idx_1).Protection -= 1;
                                                    if (!_e.UnitExtraProtectionTC(idx_1).HaveAnyProtection)
                                                        _e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                                                }
                                                else
                                                {
                                                    _e.UnitStepC(idx_1).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                                }
                                            }
                                        }
                                    }

                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                    _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                                    _e.RpcE.SoundToGeneral(sender, ClipTypes.AttackMelee);
                                }
                                else
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                            else _e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
                        }
                        break;

                    case AbilityTypes.BonusNear:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_0).CoolDownC(ability).HaveCooldown)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                {
                                    _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                    _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                                    _e.RpcE.SoundToGeneral(sender, ability);

                                    foreach (var idx_1 in _e.CellEs(idx_0).Idxs)
                                    {
                                        if (_e.UnitTC(idx_1).HaveUnit)
                                        {
                                            if (_e.UnitPlayerTC(idx_1).Is(_e.UnitPlayerTC(idx_0).Player))
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
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }

                            else _e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
                        }
                        break;

                    case AbilityTypes.FirePawn:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                if (_e.AdultForestC(idx_0).HaveAny)
                                {
                                    _e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                                    _e.HaveFire(idx_0) = true;
                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }

                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.PutOutFirePawn:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                _e.HaveFire(idx_0) = false;

                                _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                            }

                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.Seed:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                if (_e.BuildTC(idx_0).HaveBuilding && !_e.BuildTC(idx_0).Is(BuildingTypes.Camp))
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                                else
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAny)
                                    {
                                        if (!_e.YoungForestC(idx_0).HaveAny)
                                        {
                                            _e.RpcE.SoundToGeneral(sender, ability);

                                            _e.YoungForestC(idx_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;

                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                        }
                                        else
                                        {
                                            _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                        }
                                    }
                                    else
                                    {
                                        _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                }
                            }

                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.SetFarm:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                if (!_e.BuildTC(idx_0).HaveBuilding || _e.BuildTC(idx_0).Is(BuildingTypes.Camp))
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAny)
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuild = true;

                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                        {
                                            var need = ResourcesEconomy_Values.ForBuild(BuildingTypes.Farm, resT);
                                            needRes.Add(resT, need);
                                            if (need > _e.PlayerE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                                        }

                                        if (canBuild)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            {
                                                _e.PlayerE(whoseMove).ResourcesC(resT).Resources -= ResourcesEconomy_Values.ForBuild(BuildingTypes.Farm, resT);
                                            }

                                            _e.RpcE.SoundToGeneral(sender, ClipTypes.Building);

                                            _e.YoungForestC(idx_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;

                                            _e.BuildTC(idx_0).Build = BuildingTypes.Farm;
                                            _e.BuildLevelTC(idx_0).Level = LevelTypes.First;
                                            _e.BuildPlayerTC(idx_0).Player = whoseMove;
                                            _e.BuildHpC(idx_0).Health = CellBuilding_Values.MaxHealth(BuildingTypes.Farm);

                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                        }
                                        else
                                        {
                                            _e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                                        }
                                    }
                                    else
                                    {
                                        _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                }
                                else
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }

                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.SetCity:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                if (!_e.AdultForestC(idx_0).HaveAny)
                                {
                                    bool haveNearBorder = false;

                                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                                    {
                                        var idx_1 = _e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                                        if (!_e.CellEs(idx_1).IsActiveParentSelf)
                                        {
                                            haveNearBorder = true;
                                            break;
                                        }
                                    }

                                    if (!haveNearBorder)
                                    {
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.Building);
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.AfterBuildTown);

                                        _e.BuildTC(idx_0).Build = BuildingTypes.City;
                                        _e.BuildPlayerTC(idx_0).Player = whoseMove;
                                        _e.BuildHpC(idx_0).Health = CellBuilding_Values.MaxHealth(BuildingTypes.City);
                                        _e.BuildLevelTC(idx_0).Level = LevelTypes.First;

                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                        _e.HaveFire(idx_0) = false;

                                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                                        {
                                            _e.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                                        }

                                        _e.AdultForestC(idx_0).Resources = 0;
                                        _e.FertilizeC(idx_0).Resources = 0;
                                        _e.YoungForestC(idx_0).Resources = 0;
                                    }

                                    else
                                    {
                                        _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                                    }
                                }
                                else
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        
                        break;

                    case AbilityTypes.DestroyBuilding:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).HaveAnySteps)
                            {
                                _e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                                if (_e.BuildTC(idx_0).Is(BuildingTypes.City))
                                {
                                    _e.WinnerC.Player = _e.UnitPlayerTC(idx_0).Player;
                                }
                                _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                if (_e.BuildTC(idx_0).Is(BuildingTypes.Farm))
                                {
                                    _e.FertilizeC(idx_0).Resources = 0;
                                }

                                _e.BuildTC(idx_0).Build = BuildingTypes.None;
                            }
                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.FireArcher:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                if (_e.UnitEs(idx_from).ForArson.Contains(idx_to))
                                {
                                    _e.RpcE.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                                    _e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                    _e.HaveFire(idx_to) = false;
                                }
                            }

                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.GrowAdultForest:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_0).CoolDownC(ability).HaveCooldown)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                {
                                    if (_e.YoungForestC(idx_0).HaveAny)
                                    {
                                        _e.YoungForestC(idx_0).Resources = 0;

                                        _e.AdultForestC(idx_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;

                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                        _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                        _e.RpcE.SoundToGeneral(sender, ability);

     
                                        foreach (var idxC_1 in _e.CellEs(idx_0).AroundCellIdxsC)
                                        {
                                            var idx_1 = idxC_1.Idx;

                                            if (_e.UnitTC(idx_1).HaveUnit)
                                            {
                                                if (_e.UnitPlayerTC(idx_1).Is(_e.UnitPlayerTC(idx_0).Player))
                                                {
                                                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                                    //{
                                                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                                    //}
                                                }
                                            }
                                        }

                                    }

                                    else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                                else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else
                            {
                                _e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
                            }
                        }
                        break;

                    case AbilityTypes.StunElfemale:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_from).CoolDownC(ability).HaveCooldown)
                            {
                                if (_e.UnitEs(idx_to).ForPlayer(whoseMove).IsVisible)
                                {
                                    if (_e.UnitTC(idx_to).HaveUnit)
                                    {
                                        if (_e.AdultForestC(idx_to).HaveAny)
                                        {
                                            if (_e.UnitHpC(idx_from).Health >= CellUnitStatHp_Values.MAX_HP)
                                            {
                                                if (_e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                                {
                                                    if (!_e.UnitPlayerTC(idx_from).Is(_e.UnitPlayerTC(idx_to).Player))
                                                    {
                                                        _e.UnitEffectStunC(idx_to).Stun = CellUnitEffectStun_Values.ForStunAfterAbility(ability);
                                                        _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                                        _e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                                        _e.RpcE.SoundToGeneral(RpcTarget.All, ability);


                                                        foreach (var idx_1 in _e.CellEs(idx_to).AroundCellIdxsC)
                                                        {
                                                            //if (e.AdultForestC(idx_1).AdultForest.HaveAny)
                                                            //{
                                                            //    if (e.UnitTC(idx_1).HaveUnit && e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_to).Player))
                                                            //    {
                                                            //        e.UnitE(idx_1).StunC.Stun = CellUnitEffectStun_Values.ForStunAfterAbility(_ability);
                                                            //    }
                                                            //}
                                                        }
                                                    }
                                                }

                                                else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                            }
                                            else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                                        }
                                    }
                                }
                            }

                            else _e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
                        }
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (_e.UnitHpC(idx_from).Health >= CellUnitStatHp_Values.MAX_HP)
                            {
                                if (_e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                {
                                    _e.DirectWindTC.Direct = _e.CellEs(_e.CenterCloudIdxC.Idx).Direct(idx_to);

                                    _e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                    _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                    _e.RpcE.SoundToGeneral(RpcTarget.All, ability);

                                }

                                else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitHpC(idx_0).Health >= CellUnitStatHp_Values.MAX_HP)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                {
                                    _e.UnitIsRightArcherC(idx_0).ToggleSide();

                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                    _e.RpcE.SoundToGeneral(sender, ClipTypes.PickArcher);
                                }
                                else
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                            }
                        }
                        break;

                    case AbilityTypes.IceWall:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability) || _e.RiverEs(idx_0).RiverTC.HaveRiverNear)
                            {
                                if (!_e.BuildTC(idx_0).HaveBuilding)
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAny)
                                    {
                                        //e.AdultForestC(idx_0).Destroy(e.TrailEs(idx_0).Trails);
                                        _e.FertilizeC(idx_0).Resources = 0;

                                        if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                        {
                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                            _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                            //e.BuildTC(idx_0).SetNew(BuildingTypes.IceWall, e.UnitPlayerTC(idx_0).Player);
                                            //e.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, e.UnitPlayerTC(idx_0).PlayerC.Player, idx_0).HaveBuilding.Have = true;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.ActiveAroundBonusSnowy:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability) || _e.RiverEs(idx_0).RiverTC.HaveRiverNear)
                            {
                                if (!_e.RiverEs(idx_0).RiverTC.HaveRiverNear) _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                {
                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                    _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                    foreach (var idx_1 in _e.CellEs(idx_0).Idxs)
                                    {
                                        if (_e.UnitTC(idx_0).HaveUnit)
                                        {
                                            if (_e.UnitPlayerTC(idx_1).Is(whoseMove))
                                            {
                                                if (_e.UnitMainE(idx_1).IsMelee && !_e.UnitTC(idx_1).Is(UnitTypes.Camel, UnitTypes.Scout))
                                                {
                                                    _e.UnitWaterC(idx_1).Water = CellUnitStatWater_Values.MAX;
                                                    _e.UnitHpC(idx_1).Health = CellUnitStatHp_Values.MAX_HP;
                                                    _e.UnitEffectShield(idx_1).Protection = CellUnitEffectShield_Values.ProtectionAfterAbility(ability);
                                                }
                                                if (_e.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.BowCrossbow))
                                                {
                                                    _e.UnitEffectFrozenArrawC(idx_1).Shoots = 0;
                                                }
                                            }
                                            else
                                            {
                                                _e.UnitEffectStunC(idx_1).Stun = CellUnitEffectStun_Values.ForStunAfterAbility(ability);
                                            }
                                        }

                                        _e.EffectEs(idx_1).HaveFire = false;
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.DirectWave:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            //var direct = e.CellEs(idx_from).Direct(idx_to).Direct;

                            if (_e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability) || _e.RiverEs(idx_from).RiverTC.HaveRiverNear)
                            {
                                if (!_e.RiverEs(idx_from).RiverTC.HaveRiverNear) _e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                if (_e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                {
                                    _e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);
                                    _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);

                                    var idx_0 = idx_to;

                                    for (var i = 0; i < 3; i++)
                                    {
                                        if (!_e.CellEs(idx_0).IsActiveParentSelf) break;

                                        if (_e.UnitTC(idx_0).HaveUnit)
                                        {
                                            if (_e.UnitPlayerTC(idx_0).Is(whoseMove))
                                            {
                                                //UnitEffectEs(idx_0).ShieldE.Set(ability);
                                                //UnitE(idx_0).SetMax();
                                                //UnitStatEs(idx_0).Water.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
                                            }
                                            else
                                            {
                                                _e.UnitEffectStunC(idx_0).Stun = CellUnitEffectStun_Values.ForStunAfterAbility(ability);
                                            }
                                        }

                                        _e.HaveFire(idx_0) = false;

                                        //idx_0 = e.CellEs().GetIdxCellByDirect(idx_0, direct_0);
                                    }
                                }
                                else
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                        }
                        break;

                    case AbilityTypes.Resurrect:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (!_e.UnitTC(idx_to).HaveUnit)
                            {
                                if (!_e.UnitEs(idx_from).CoolDownC(ability).HaveCooldown)
                                {
                                    if (_e.UnitStepC(idx_from).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                    {
                                        _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);
                                        _e.UnitStepC(idx_from).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                        if (_e.LastDiedE(idx_to).UnitTC.HaveUnit)
                                        {
                                            //e.UnitE(idx_to).SetNew((e.LastDiedUnitTC(idx_to).Unit, e.LastDiedLevelTC(idx_to).Level, e.LastDiedPlayerTC(idx_to).Player, ConditionUnitTypes.None, false), e);
                                            _e.LastDiedUnitTC(idx_to).Unit = UnitTypes.None;
                                        }
                                    }
                                    else
                                    {
                                        _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.SetTeleport:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.BuildTC(idx_0).HaveBuilding)
                            {
                                if (!_e.AdultForestC(idx_0).HaveAny)
                                {
                                    _e.YoungForestC(idx_0).Resources = 0;
                                    _e.FertilizeC(idx_0).Resources = 0;

                                    if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                                    {
                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                        if (_e.StartTeleportIdxC.Idx > 0)
                                        {
                                            if (_e.EndTeleportIdxC.Idx > 0)
                                            {
                                                _e.BuildTC(_e.StartTeleportIdxC.Idx).Build = BuildingTypes.None;

                                                _e.StartTeleportIdxC = _e.EndTeleportIdxC;

                                                _e.EndTeleportIdxC.Idx = idx_0;
                                                _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);
                                            }
                                            else
                                            {
                                                _e.EndTeleportIdxC.Idx = idx_0;
                                                _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldownValues.NeedAfterAbility(ability);
                                            }
                                        }
                                        else
                                        {
                                            _e.StartTeleportIdxC.Idx = idx_0;
                                        }

                                        _e.BuildTC(idx_0).Build = BuildingTypes.Teleport;
                                        _e.BuildLevelTC(idx_0).Level = LevelTypes.First;
                                        _e.BuildPlayerTC(idx_0).Player = whoseMove;
                                        _e.BuildHpC(idx_0).Health = CellBuilding_Values.MaxHealth(BuildingTypes.Teleport);
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.Teleport:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                if (_e.BuildTC(idx_0).Is(BuildingTypes.Teleport))
                                {
                                    var idx_start = _e.StartTeleportIdxC.Idx;
                                    var idx_end = _e.EndTeleportIdxC.Idx;

                                    if (_e.EndTeleportIdxC.Idx > 0 && idx_start == idx_0)
                                    {
                                        if (!_e.UnitTC(idx_end).HaveUnit)
                                        {
                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                            //Teleport(idx_end, ents);
                                        }
                                    }
                                    else if (_e.StartTeleportIdxC.Idx > 0 && idx_end == idx_0)
                                    {
                                        if (!_e.UnitTC(idx_start).HaveUnit)
                                        {
                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                            //Teleport(idx_start, _e);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.InvokeSkeletons:
                        {
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
                            //_e.UnitE((byte)objects[_idx_cur++]).InvokeSkeletons_Master(ability, sender, _e);
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else if (obj is BuildingTypes buildT)
            {
                var idx_0 = (byte)objects[_idx_cur++];
                var idx_1 = (byte)objects[_idx_cur++];


                if (_e.CellEs(idx_1).Player(whoseMove).CanCityBuildHere)
                {
                    var needRes = new Dictionary<ResourceTypes, float>();
                    var canBuild = true;

                    for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                    {
                        var need = ResourcesEconomy_Values.ForBuild(buildT, resT);
                        needRes.Add(resT, need);
                        if (need > _e.PlayerE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                    }

                    if (canBuild)
                    {
                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                        {
                            _e.PlayerE(whoseMove).ResourcesC(resT).Resources -= ResourcesEconomy_Values.ForBuild(buildT, resT);
                        }

                        _e.BuildTC(idx_1).Build = buildT;
                        _e.BuildLevelTC(idx_1).Level = LevelTypes.First;
                        _e.BuildPlayerTC(idx_1).Player = whoseMove;
                        _e.BuildHpC(idx_1).Health = CellBuilding_Values.MaxHealth(buildT);
                    }

                    else
                    {
                        _e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                    }


                    

                    //if (_e.InventorResourcesEs.CanBuyBuilding_Master(buildT, whoseMove, out var needRes))
                    //{
                    //_e.InventorResourcesEs.BuyBuilding_Master(buildT, whoseMove);

                    //    if (buildT == BuildingTypes.House)
                    //    {
                    //        //ents.BuildingE(idx_to_0).SetNewHouse(whoseMove, ents.MaxAvailablePawnsE(whoseMove));
                    //    }
                    //    else if (buildT == BuildingTypes.Smelter)
                    //    {
                    //        //ents.BuildingE(idx_to_0).SetNewSmelter(whoseMove);
                    //    }
                    //    else
                    //    {
                    //        //ents.BuildingE(idx_to_0).SetNew(buildT, whoseMove);
                    //    }



                    //    break;
                    //}
                    //else
                    //{
                    //    ents.RpcE.MistakeEconomyToGeneral(sender, needRes);
                    //}
                }
            }

            else if (obj is MarketBuyTypes marketBuy)
            {
                var needRes = new Dictionary<ResourceTypes, float>();

                needRes.Add(ResourceTypes.Food, 0);
                needRes.Add(ResourceTypes.Wood, 0);
                needRes.Add(ResourceTypes.Ore, 0);
                needRes.Add(ResourceTypes.Iron, 0);
                needRes.Add(ResourceTypes.Gold, 0);

                switch (marketBuy)
                {
                    case MarketBuyTypes.FoodToWood:
                        needRes[ResourceTypes.Food] = ResourcesEconomy_Values.ResourcesForBuyFromMarket(marketBuy);
                        break;

                    case MarketBuyTypes.WoodToFood:
                        needRes[ResourceTypes.Wood] = ResourcesEconomy_Values.ResourcesForBuyFromMarket(marketBuy);
                        break;

                    case MarketBuyTypes.GoldToFood:
                        needRes[ResourceTypes.Gold] = ResourcesEconomy_Values.ResourcesForBuyFromMarket(marketBuy);
                        break;

                    case MarketBuyTypes.GoldToWood:
                        needRes[ResourceTypes.Gold] = ResourcesEconomy_Values.ResourcesForBuyFromMarket(marketBuy);
                        break;
                }

                var canBuy = true;

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    if (needRes[resT] > _e.PlayerE(_e.WhoseMove.Player).ResourcesC(resT).Resources)
                    {
                        canBuy = false; 
                        break;
                    }
                }

                if (canBuy)
                {
                    for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                    {
                        _e.PlayerE(_e.WhoseMove.Player).ResourcesC(resT).Resources -= needRes[resT];
                    }
                    switch (marketBuy)
                    {
                        case MarketBuyTypes.FoodToWood:
                            _e.PlayerE(_e.WhoseMove.Player).ResourcesC(ResourceTypes.Wood).Resources 
                                += ResourcesEconomy_Values.ResourcesAfterBuyInMarket(marketBuy);
                            break;

                        case MarketBuyTypes.WoodToFood:
                            _e.PlayerE(_e.WhoseMove.Player).ResourcesC(ResourceTypes.Food).Resources 
                                += ResourcesEconomy_Values.ResourcesAfterBuyInMarket(marketBuy);
                            break;

                        case MarketBuyTypes.GoldToFood:
                            _e.PlayerE(_e.WhoseMove.Player).ResourcesC(ResourceTypes.Food).Resources
                                += ResourcesEconomy_Values.ResourcesAfterBuyInMarket(marketBuy);
                            break;

                        case MarketBuyTypes.GoldToWood:
                            _e.PlayerE(_e.WhoseMove.Player).ResourcesC(ResourceTypes.Wood).Resources
                                += ResourcesEconomy_Values.ResourcesAfterBuyInMarket(marketBuy);
                            break;
                    }
                }
                else
                {
                    _e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                }
            }

            else if (obj is RpcMasterTypes rpcT)
            {
                byte idx_0;

                switch (rpcT)
                {
                    case RpcMasterTypes.None:
                        throw new Exception();

                    case RpcMasterTypes.Ready:
                        {
                            var playerSend = sender.GetPlayer();

                            _e.PlayerE(playerSend).IsReadyC = !_e.PlayerE(playerSend).IsReadyC;

                            if (_e.PlayerE(PlayerTypes.First).IsReadyC
                                && _e.PlayerE(PlayerTypes.Second).IsReadyC)
                            {
                                _e.IsStartedGame = true;
                            }

                            else
                            {
                                _e.IsStartedGame = false;
                            }
                        }       
                        break;

                    case RpcMasterTypes.Done:
                        {
                            _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);

                            if (PhotonNetwork.OfflineMode)
                            {
                                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                {
                                    for (byte idx = 0; idx < StartValues.ALL_CELLS_AMOUNT; idx++)
                                    {
                                        _e.UnitEffectStunC(idx).Stun -= 2;
                                        //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                                    }
                                    _systems.SystemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                                    _e.RpcE.ActiveMotionZoneToGen(sender);
                                }

                                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                                {
                                    for (byte idx = 0; idx < StartValues.ALL_CELLS_AMOUNT; idx++)
                                    {
                                        _e.UnitEffectStunC(idx).Stun -= 2;
                                        //EntitiesPool.IceWalls[idx_0].Hp.Take();
                                    }

                                    var curPlayer = _e.CurPlayerI.Player;
                                    var nextPlayer = _e.WhoseMove.NextPlayerFrom(curPlayer);

                                    if (nextPlayer == PlayerTypes.First)
                                    {
                                        _systems.SystemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                                        _e.RpcE.ActiveMotionZoneToGen(sender);
                                    }

                                    _e.WhoseMove.Player = nextPlayer;


                                    curPlayer = _e.CurPlayerI.Player;

                                    //ViewDataSC.RotateAll.Invoke();

                                    _e.FriendIsActive = true;
                                }
                            }
                            else
                            {
                                //if (WhoseMoveC.WhoseMove == playerSend)
                                //{
                                //    //if (!EntInventorUnits.Have(UnitTypes.King, LevelTypes.First, sender.GetPlayer()))
                                //    //{
                                //    //    if (playerSend == PlayerTypes.Second)
                                //    //    {
                                //    //        SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Update);

                                //    //        Ents.Rpc.ActiveMotionZoneToGen(PlayerTypes.First.GetPlayer());
                                //    //        Ents.Rpc.ActiveMotionZoneToGen(PlayerTypes.Second.GetPlayer());
                                //    //    }

                                //    //    WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                                //    //}
                                //}
                            }
                        }
                        break;

                    case RpcMasterTypes.Shift:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];


                            if (_e.UnitEs(idx_from).ForShift.Contains(idx_to) && _e.UnitPlayerTC(idx_from).Is(_e.WhoseMove.Player))
                            {
                                _e.UnitStepC(idx_from).Steps -= _e.UnitEs(idx_from).NeedSteps(idx_to).Steps;

                                _e.UnitEs(idx_to).Set(_e.UnitEs(idx_from));
                                _e.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

                                _e.UnitTC(idx_from).Unit = UnitTypes.None;


                                var direct = _e.CellEs(idx_from).Direct(idx_to);

                                if (!_e.UnitTC(idx_to).Is(UnitTypes.Undead))
                                {
                                    if (_e.AdultForestC(idx_from).HaveAny)
                                    {
                                        _e.CellEs(idx_from).TrailHealthC(direct).Health = CellTrail_Values.HEALTH_TRAIL;
                                    }
                                    if (_e.AdultForestC(idx_to).HaveAny)
                                    {
                                        _e.CellEs(idx_to).TrailHealthC(direct.Invert()).Health = CellTrail_Values.HEALTH_TRAIL;
                                    }

                                    if (_e.RiverEs(idx_to).RiverTC.HaveRiverNear)
                                    {
                                        _e.UnitWaterC(idx_to).Water = CellUnitStatWater_Values.MAX;
                                    }
                                }

                                if (_e.UnitTC(idx_to).Is(UnitTypes.Hell))
                                {
                                    if (_e.AdultForestC(idx_to).HaveAny)
                                    {
                                        _e.EffectEs(idx_to).HaveFire = true;
                                    }
                                }

                                if (_e.BuildTC(idx_to).HaveBuilding && !_e.BuildTC(idx_to).Is(BuildingTypes.City))
                                {
                                    if (!_e.BuildPlayerTC(idx_to).Is(_e.UnitPlayerTC(idx_to).Player))
                                    {
                                        _e.BuildTC(idx_to).Build = BuildingTypes.None;
                                    }
                                }

                                _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            }
                        }
                        break;

                    case RpcMasterTypes.Attack:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            var canAttack = _e.UnitEs(idx_from).ForAttack(AttackTypes.Unique).Contains(idx_to)
                                || _e.UnitEs(idx_from).ForAttack(AttackTypes.Simple).Contains(idx_to);

                            if (canAttack && _e.UnitPlayerTC(idx_from).Is(whoseMove))
                            {
                                _e.UnitStepC(idx_from).Steps = 0;
                                _e.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

                                if (_e.UnitMainE(idx_from).IsMelee)
                                    _e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                                else _e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


                                float powerDam_from = _e.UnitDamageAttackC(idx_from).Damage;
                                float powerDam_to = _e.UnitDamageAttackC(idx_to).Damage;


                                if (_e.UnitEs(idx_from).ForAttack(AttackTypes.Unique).Contains(idx_to))
                                {
                                    powerDam_from += powerDam_from * CellUnitDamage_Values.UNIQUE_PERCENT_DAMAGE;
                                }

                                var dirAttack = _e.CellEs(idx_from).Direct(idx_to);

                                if (_e.SunSideTC.IsAcitveSun)
                                {
                                    var isSunnedUnit = true;

                                    foreach (var dir in _e.SunSideTC.RaysSun)
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

                                var maxDamage = CellUnitStatHp_Values.MAX_HP;
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


                                var player_from = _e.UnitPlayerTC(idx_from).Player;

                                if (_e.UnitMainE(idx_from).IsMelee)
                                {
                                    if (_e.UnitEffectShield(idx_from).HaveAnyProtection)
                                    {
                                        _e.UnitEffectShield(idx_from).Protection--;
                                    }
                                    else if (_e.UnitExtraTWTC(idx_from).Is(ToolWeaponTypes.Shield))
                                    {
                                        _e.UnitEffectShield(idx_from).Protection--;
                                        if (!_e.UnitEffectShield(idx_from).HaveAnyProtection)
                                            _e.UnitExtraTWTC(idx_from).ToolWeapon = ToolWeaponTypes.None;
                                    }
                                    else if (minus_from > 0)
                                    {
                                        _e.UnitHpC(idx_from).Health -= minus_from;
                                        if (_e.UnitHpC(idx_from).Health <= CellUnitDamage_Values.HP_FOR_DEATH_AFTER_ATTACK)
                                            _e.UnitHpC(idx_from).Health = 0;

                                        if (!_e.UnitHpC(idx_from).IsAlive)
                                        {
                                            if (_e.UnitTC(idx_from).Is(UnitTypes.Scout) || _e.UnitMainE(idx_from).IsHero)
                                            {
                                                _e.UnitInfo(_e.UnitPlayerTC(idx_from).Player, _e.UnitTC(idx_from).Unit).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(_e.UnitTC(idx_from).Unit);
                                                _e.UnitInfo(_e.UnitPlayerTC(idx_from).Player, _e.UnitTC(idx_from).Unit).HaveInInventor = true;
                                            }

                                            if (_e.UnitTC(idx_from).Is(UnitTypes.King)) _e.WinnerC.Player = _e.UnitPlayerTC(idx_to).Player;

                                            _e.LastDiedE(idx_from).Set(_e.UnitTC(idx_from), _e.UnitLevelTC(idx_from), _e.UnitPlayerTC(idx_from));

                                            _e.UnitTC(idx_from).Unit = UnitTypes.None;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_e.UnitEffectFrozenArrawC(idx_from).HaveEffect)
                                    {
                                        _e.UnitEffectFrozenArrawC(idx_from).Shoots = 0;

                                        _e.UnitEffectStunC(idx_to).Stun = 2;
                                    }
                                }

                                if (_e.UnitEffectShield(idx_to).HaveAnyProtection)
                                {
                                    _e.UnitEffectShield(idx_to).Protection--;
                                }
                                else if (_e.UnitExtraTWTC(idx_to).Is(ToolWeaponTypes.Shield))
                                {
                                    _e.UnitEffectShield(idx_to).Protection--;
                                    if (!_e.UnitEffectShield(idx_to).HaveAnyProtection)
                                        _e.UnitExtraTWTC(idx_to).ToolWeapon = ToolWeaponTypes.None;
                                }
                                else if (minus_to > 0)
                                {
                                    _e.UnitHpC(idx_to).Health -= minus_to;
                                    if (_e.UnitHpC(idx_to).Health <= CellUnitDamage_Values.HP_FOR_DEATH_AFTER_ATTACK) 
                                        _e.UnitHpC(idx_to).Health = 0;

                                    if (!_e.UnitHpC(idx_to).IsAlive)
                                    {
                                        if (_e.UnitTC(idx_to).Is(UnitTypes.Scout) || _e.UnitMainE(idx_to).IsHero)
                                        {
                                            _e.UnitInfo(_e.UnitPlayerTC(idx_to).Player, _e.UnitTC(idx_to).Unit).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(_e.UnitTC(idx_to).Unit);
                                            _e.UnitInfo(_e.UnitPlayerTC(idx_to).Player, _e.UnitTC(idx_to).Unit).HaveInInventor = true;
                                        }

                                        if (_e.UnitTC(idx_to).Is(UnitTypes.King)) _e.WinnerC.Player = player_from;

                                        if (_e.UnitTC(idx_to).Is(UnitTypes.Camel))
                                        {
                                            _e.ResourcesC(_e.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += ResourcesEconomy_Values.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                                        }

                                        _e.LastDiedE(idx_to).Set(_e.UnitTC(idx_to), _e.UnitLevelTC(idx_to), _e.UnitPlayerTC(idx_to));


                                        _e.UnitTC(idx_to).Unit = UnitTypes.None;

                                        if (_e.UnitTC(idx_from).HaveUnit)
                                        {
                                            if (_e.UnitMainE(idx_from).IsMelee)
                                            {
                                                _e.UnitEs(idx_to).Set(_e.UnitEs(idx_from));
                                                _e.UnitTC(idx_from).Unit = UnitTypes.None;
                                            }
                                        }          
                                    }
                                }
                            }

                        }
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        {
                            idx_0 = (byte)objects[_idx_cur++];
                            var condT = (ConditionUnitTypes)objects[_idx_cur++];

                            switch (condT)
                            {
                                case ConditionUnitTypes.None:
                                    _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                                    break;

                                case ConditionUnitTypes.Protected:
                                    if (_e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                                    {
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                                    }

                                    else if (_e.UnitStepC(idx_0).HaveAnySteps)
                                    {
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_TOGGLE_CONDITION_UNIT;
                                        _e.UnitConditionTC(idx_0).Condition = condT;
                                    }

                                    else
                                    {
                                        _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                    break;


                                case ConditionUnitTypes.Relaxed:
                                    if (_e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                                    {
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                                    }

                                    else if (_e.UnitStepC(idx_0).HaveAnySteps)
                                    {
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitConditionTC(idx_0).Condition = condT;
                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_TOGGLE_CONDITION_UNIT;
                                    }

                                    else
                                    {
                                        _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                    break;


                                default:
                                    throw new Exception();
                            }


                        }
                        break;

                    case RpcMasterTypes.SetUnit:
                        {
                            idx_0 = (byte)objects[_idx_cur++];
                            var unitT = (UnitTypes)objects[_idx_cur++];

                            if (_e.UnitEs(idx_0).ForPlayer(whoseMove).CanSetUnitHere)
                            {
                                _e.UnitTC(idx_0).Unit = unitT;
                                _e.UnitPlayerTC(idx_0).Player = whoseMove;
                                _e.UnitLevelTC(idx_0).Level = LevelTypes.First;
                                _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                                _e.UnitIsRightArcherC(idx_0).IsRight = false;
                                _e.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                                _e.UnitStepC(idx_0).Steps = CellUnitStatStep_Values.StandartForUnit(unitT);
                                _e.UnitWaterC(idx_0).Water = CellUnitStatWater_Values.MAX;
                                _e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                                _e.UnitExtraLevelTC(idx_0).Level = LevelTypes.None;
                                _e.UnitExtraProtectionTC(idx_0).Protection = 0;
                                _e.UnitEffectStunC(idx_0).Stun = 0;
                                _e.UnitEffectShield(idx_0).Protection = 0;
                                _e.UnitEffectFrozenArrawC(idx_0).Shoots = 0;


                                if (unitT == UnitTypes.Pawn)
                                {
                                    _e.PlayerE(whoseMove).PeopleInCity -= 1;

                                    _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                    _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
                                }
                                else
                                {
                                    _e.PlayerE(whoseMove).UnitsInfoE(unitT).HaveInInventor = false;

                                    _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                                    _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.None;
                                }

                                _e.PlayerE(whoseMove).UnitsInfoE(unitT).UnitsInGame++;

                                _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            }
                        }
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        {
                            idx_0 = (byte)objects[_idx_cur++];
                            var twT = (ToolWeaponTypes)objects[_idx_cur++];
                            var levTW = (LevelTypes)objects[_idx_cur++];

                            if (_e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            {
                                if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON)
                                {
                                    if (twT == ToolWeaponTypes.BowCrossbow)
                                    {
                                        if (_e.UnitExtraTWTC(idx_0).HaveToolWeapon)
                                        {
                                            _e.ToolWeaponsC(_e.UnitPlayerTC(idx_0).Player, _e.UnitExtraLevelTC(idx_0).Level, _e.UnitExtraTWTC(idx_0).ToolWeapon).Amount++;
                                            _e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;

                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;
                                        }
                                        else
                                        {
                                            if (_e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                                            {
                                                if (_e.UnitMainTWLevelTC(idx_0).Is(LevelTypes.First))
                                                {
                                                    if (_e.PlayerE(whoseMove).LevelE(levTW).ToolWeapons(twT).HaveAny)
                                                    {
                                                        _e.ToolWeaponsC(whoseMove, levTW, twT).Amount--;
                                                        _e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                                        _e.UnitMainTWLevelTC(idx_0).Level = levTW;

                                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                    }
                                                    else
                                                    {
                                                        var needRes = new Dictionary<ResourceTypes, float>();
                                                        var canBuy = true;

                                                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                                        {
                                                            var difAmountRes = _e.PlayerE(whoseMove).ResourcesC(res).Resources - ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, res);
                                                            needRes.Add(res, ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, res));

                                                            if (canBuy) canBuy = difAmountRes >= 0;
                                                        }

                                                        if (canBuy)
                                                        {
                                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                                _e.PlayerE(whoseMove).ResourcesC(resT).Resources -= ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, resT);

                                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                                            _e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                                            _e.UnitMainTWLevelTC(idx_0).Level = levTW;

                                                            _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                        }
                                                        else
                                                        {
                                                            _e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    _e.ToolWeaponsC(whoseMove, _e.UnitMainTWLevelTC(idx_0).Level, _e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                                    _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                                    _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;
                                                }
                                            }

                                            else
                                            {
                                                _e.ToolWeaponsC(whoseMove, _e.UnitMainTWLevelTC(idx_0).Level, _e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                                _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                                _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                                _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;
                                            }
                                        }
                                    }

                                    else if (twT == ToolWeaponTypes.Axe)
                                    {
                                        if (_e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                                        {
                                            if (_e.UnitMainTWLevelTC(idx_0).Is(LevelTypes.First))
                                            {
                                                if (_e.PlayerE(whoseMove).LevelE(levTW).ToolWeapons(twT).HaveAny)
                                                {
                                                    _e.ToolWeaponsC(whoseMove, levTW, twT).Amount--;
                                                    _e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                                    _e.UnitMainTWLevelTC(idx_0).Level = levTW;

                                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                                    _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                }
                                                else
                                                {
                                                    var needRes = new Dictionary<ResourceTypes, float>();
                                                    var canBuy = true;

                                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                                    {
                                                        var difAmountRes = _e.PlayerE(whoseMove).ResourcesC(res).Resources - ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, res);
                                                        needRes.Add(res, ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, res));

                                                        if (canBuy) canBuy = difAmountRes >= 0;
                                                    }

                                                    if (canBuy)
                                                    {
                                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                            _e.PlayerE(whoseMove).ResourcesC(resT).Resources -= ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, resT);

                                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                                        _e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                                        _e.UnitMainTWLevelTC(idx_0).Level = levTW;

                                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                    }
                                                    else
                                                    {
                                                        _e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                _e.ToolWeaponsC(whoseMove, _e.UnitMainTWLevelTC(idx_0).Level, _e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                                _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                                _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                                _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;
                                            }
                                        }

                                        else
                                        {
                                            _e.ToolWeaponsC(whoseMove, _e.UnitMainTWLevelTC(idx_0).Level, _e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                            _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                            _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                            _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                        }
                                    }

                                    else
                                    {
                                        var ownUnit_0 = _e.UnitPlayerTC(idx_0).Player;

                                        if (_e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            _e.ToolWeaponsC(_e.UnitPlayerTC(idx_0).Player, _e.UnitMainTWLevelTC(idx_0).Level, _e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                            _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                            _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;
                                        }

                                        else
                                        {
                                            if (_e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                                            {
                                                if (_e.UnitExtraTWTC(idx_0).HaveToolWeapon)
                                                {
                                                    _e.PlayerE(ownUnit_0).LevelE(_e.UnitExtraLevelTC(idx_0).Level).ToolWeapons(_e.UnitExtraTWTC(idx_0).ToolWeapon).Amount++;
                                                    _e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;

                                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                                    _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                }

                                                else if (_e.ToolWeaponsC(ownUnit_0, levTW, twT).HaveAny)
                                                {
                                                    _e.PlayerE(ownUnit_0).LevelE(levTW).ToolWeapons(twT).Amount--;

                                                    _e.UnitExtraTWE(idx_0).Set(twT, levTW, _e.UnitExtraProtectionTC(idx_0).Protection);

                                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                                    _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                }

                                                else
                                                {
                                                    var needRes = new Dictionary<ResourceTypes, float>();
                                                    var canCreatBuild = true;

                                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                                    {
                                                        var difAmountRes = _e.PlayerE(whoseMove).ResourcesC(res).Resources - ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, res);
                                                        needRes.Add(res, ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, res));

                                                        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
                                                    }

                                                    if (canCreatBuild)
                                                    {
                                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                            _e.PlayerE(whoseMove).ResourcesC(resT).Resources -= ResourcesEconomy_Values.ForBuyToolWeapon(twT, levTW, resT);

                                                        var protection = 0f;

                                                        if (twT == ToolWeaponTypes.Shield)
                                                        {
                                                            protection = levTW == LevelTypes.First ? CellUnitToolWeapon_Values.SHIELD_PROTECTION_LEVEL_FIRST
                                                                : CellUnitToolWeapon_Values.SHIELD_PROTECTION_LEVEL_SECOND;
                                                        }

                                                        _e.UnitExtraTWE(idx_0).Set(twT, levTW, protection);

                                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_TOOLWEAPON;

                                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                    }
                                                    else
                                                    {
                                                        _e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                        }
                        break;

                    case RpcMasterTypes.GetHero:
                        //_e.Units((UnitTypes)objects[_idx_cur++], LevelTypes.First, whoseMove).AmountC.Add(1);
                        _e.PlayerE(whoseMove).HaveCenterHeroC = false;
                        break;

                    case RpcMasterTypes.UpgCenterUnits:
                        //var whoseMove = es.WhoseMove.Player;


                        //if (unit == UnitTypes.Scout)
                        //{
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //}
                        //else
                        //{
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //}

                        //es.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).Have = false;
                        //es.AvailableCenterUpgradeEs.HaveUnitUpgrade(unit, whoseMove).Have = false;

                        //es.RpcE.SoundToGeneral(sender, ClipTypes.PickUpgrade);
                        break;

                    case RpcMasterTypes.UpgCenterBuild:
                        //buildT = (BuildingTypes)objects[_idx_cur++];

                        //_e.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).Have = false;
                        //_e.HaveBuildingUpgrade(buildT, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //_e.AvailableCenterUpgradeEs.HaveBuildUpgrade(buildT, whoseMove).Have = false;

                        //_e.RpcE.SoundToGeneral(sender, ClipTypes.PickUpgrade);
                        break;

                    case RpcMasterTypes.UpgWater:
                        //var whoseMove = e.WhoseMove.Player;

                        //for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
                        //{
                        //    for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                        //    {
                        //        e.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Water, unit, level, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //    }
                        //}
                        //e.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).Have = false;
                        //e.AvailableCenterUpgradeEs.HaveWaterUpgrade(whoseMove).Have = false;

                        //e.RpcE.SoundToGeneral(sender, ClipTypes.PickUpgrade);
                        break;

                    default:
                        throw new Exception();
                }
            }

            else throw new Exception();

            SyncAllMaster();
        }

        [PunRPC]
        void GeneralRpc(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var obj = objects[_idx_cur++];

            if (obj is MistakeTypes mistakeT)
            {
                _e.MistakeE.Set(mistakeT, 0);
                _e.Sound(ClipTypes.Mistake).Action.Invoke();

                if (mistakeT == MistakeTypes.Economy)
                {
                    _e.MistakeEconomy(ResourceTypes.Food).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Wood).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Ore).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Iron).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Gold).Resources = 0;

                    var needRes = (float[])objects[_idx_cur++];

                    _e.MistakeEconomy(ResourceTypes.Food).Resources = needRes[0];
                    _e.MistakeEconomy(ResourceTypes.Wood).Resources = needRes[1];
                    _e.MistakeEconomy(ResourceTypes.Ore).Resources = needRes[2];
                    _e.MistakeEconomy(ResourceTypes.Iron).Resources = needRes[3];
                    _e.MistakeEconomy(ResourceTypes.Gold).Resources = needRes[4];
                }
            }
            else if (obj is RpcGeneralTypes rpcT)
            {
                switch (rpcT)
                {
                    case RpcGeneralTypes.None:
                        throw new Exception();

                    case RpcGeneralTypes.SoundEff:
                        _e.Sound((ClipTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.SoundUniqueAbility:
                        _e.Sound((AbilityTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.SoundRpcMaster:
                        //Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.ActiveMotion:
                        _e.MotionIsActive = true;
                        break;

                    default:
                        throw new Exception();
                }
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _e.RpcE.OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


             for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                objs.Add(_e.UnitTC(idx_0).Unit);
                //objs.Add(_e.CellEs(idx_0).UnitEs.MainE.LevelTC.Level);
                objs.Add(_e.UnitPlayerTC(idx_0).Player);

                objs.Add(_e.UnitHpC(idx_0).Health);
                objs.Add(_e.UnitStepC(idx_0).Steps);
                objs.Add(_e.UnitWaterC(idx_0).Water);

                objs.Add(_e.UnitConditionTC(idx_0).Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have);


                //objs.Add(_e.CellEs(idx_0).UnitEs.ExtraToolWeaponTC.ToolWeapon);
                //objs.Add(_e.CellEs(idx_0).UnitEs.ExtraTWLevelTC.Level);
                //objs.Add(_e.CellEs(idx_0).UnitEs.ExtraTWShieldC.Protection);

                objs.Add(_e.UnitEffectStunC(idx_0).Stun);

                objs.Add(_e.UnitIsRightArcherC(idx_0).IsRight);

                //foreach (var item in _e.CellEs(idx_0).UnitEs.CooldownKeys) objs.Add(_e.CellEs(idx_0).UnitEs.Ability(item).CooldownC);





                //objs.Add(_e.BuildingE(idx_0).Building);
                //objs.Add(_e.BuildingE(idx_0).Owner);



                //foreach (var env in _e.EnvironmentEs.Keys)
                //{
                //    objs.Add(_e.EnvironmentEs.Environment(env, idx_0));
                //}




                objs.Add(_e.CellEs(idx_0).RiverEs.RiverTC.River);
                //foreach (var item_0 in _e.CellEs(idx_0).RiverEs.Keys)
                //    objs.Add(_e.CellEs(idx_0).RiverEs.HaveRive(item_0).HaveRiver.Have);


                //foreach (var item_0 in _e.CellEs(idx_0).TrailEs.Keys)
                //    objs.Add(_e.CellEs(idx_0).TrailEs.Trail(item_0));

                //objs.Add(_e.CellEs(idx_0).EffectEs.FireE.HaveFireC);
            }

            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).CooldownC.Amount);
            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).CooldownC.Amount);
            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).CooldownC.Amount);
            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).CooldownC.Amount);



            //foreach (var key in _e.UnitStatUpgradesEs.Keys) objs.Add(_e.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            //foreach (var key in _e.InventorResourcesEs.Keys) objs.Add(_e.InventorResourcesEs.Resource(key).Resources);
            //foreach (var key in _e.InventorUnitsEs.Keys) objs.Add(_e.Units(key).Units.Amount);
            //foreach (var key in _e.InventorToolWeaponEs.Keys) objs.Add(_e.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount);


            //foreach (var key in _e.WhereUnitsEs.Keys) objs.Add(_e.WhereUnitsEs.WhereUnit(key).HaveUnit.Have);
            //foreach (var key in _e.WhereBuildingEs.Keys) objs.Add(_e.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have);
            //foreach (var key in _e.WhereEnviromentEs.Keys) objs.Add(_e.WhereEnviromentEs.Info(key).HaveEnv.Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            //objs.Add(_e.WhoseMove.Player);
            //objs.Add(_e.WinnerC.Player);
            //objs.Add(_e.IsStartedGameC.Is);
            //objs.Add(_e.PeopleInCityE(PlayerTypes.Second).IsReadyC.IsReady);

            //objs.Add(_e.MotionsC.Amount);

            //objs.Add(_e.CenterCloudIdxC);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            _e.RpcE.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            _e.RpcE.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                //_e.CellEs(idx_0).UnitEs.Main.UnitTC.Unit = (UnitTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.LevelC.Level = (LevelTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.OwnerC.Player = (PlayerTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.ConditionTC.Condition = (ConditionUnitTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.IsCorned.Is = (bool)objects[_idx_cur++];

                //_e.CellEs(idx_0).UnitEs.StatEs.Hp.Health.Amount = (int)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.StatEs.Step.Steps.Amount = (int)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.StatEs.Water.Water.Amount = (int)objects[_idx_cur++];

               
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have = (bool)objects[_idx_cur++];


                //_e.CellEs(idx_0).UnitEs.ToolWeapon.ToolWeaponTC.ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.ToolWeapon.LevelTC.Level = (LevelTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.ToolWeapon.Protection.Amount = (int)objects[_idx_cur++];


                //_e.UnitE(idx_0).SyncRpc((int)objects[_idx_cur++]);

                

                //foreach (var item in _e.CellEs(idx_0).UnitEs.CooldownKeys) _e.CellEs(idx_0).UnitEs.Ability(item).Cooldown = (int)objects[_idx_cur++];



                //_e.BuildingE(idx_0).Sync((int)objects[_idx_cur++], (BuildingTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);

                //foreach (var item_0 in _e.EnvironmentEs.Keys)
                //{
                //    _e.EnvironmentEs.Environment(item_0, idx_0).Resources.Amount = (int)objects[_idx_cur++];
                //}

                //_e.CellEs(idx_0).RiverEs.RiverTC.River = (RiverTypes)objects[_idx_cur++];
                //foreach (var dir in _e.CellEs(idx_0).RiverEs.Keys)
                //    _e.CellEs(idx_0).RiverEs.HaveRive(dir).HaveRiver.Have = (bool)objects[_idx_cur++];



                //foreach (var item_0 in _e.CellEs(idx_0).TrailEs.Keys)
                //    _e.TrailEs(idx_0).Trail(item_0).Sync((int)objects[_idx_cur++]);



                //_e.CellEs(idx_0).EffectEs.FireE.SyncRpc((bool)objects[_idx_cur++]);
            }


            //_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);
            //_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);



            //foreach (var key in _e.UnitStatUpgradesEs.Keys) _e.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            //foreach (var key in _e.InventorResourcesEs.Keys) _e.InventorResourcesEs.Resource(key).Set((int)objects[_idx_cur++]);
            //foreach (var key in _e.InventorUnitsEs.Keys) _e.Units(key).Sync((int)objects[_idx_cur++]);
            //foreach (var key in _e.InventorToolWeaponEs.Keys) _e.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount = (int)objects[_idx_cur++];


            //foreach (var key in _e.WhereUnitsEs.Keys) _e.WhereUnitsEs.WhereUnit(key).HaveUnit.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _e.WhereBuildingEs.Keys) _e.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _e.WhereEnviromentEs.Keys) _e.WhereEnviromentEs.Info(key).HaveEnv.Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            //_e.WhoseMove.Player = (PlayerTypes)objects[_idx_cur++];
            //_e.WinnerC.Player = (PlayerTypes)objects[_idx_cur++];
            //_e.IsStartedGameC = (bool)objects[_idx_cur++];
            //_e.ReadyE(_e.WhoseMovePlayerTC.CurPlayerI).IsReadyC.IsReady = (bool)objects[_idx_cur++];


            //_e.Motion.AmountMotionsC.Amount = (int)objects[_idx_cur++];

            //_e.CenterCloudIdxC.Set((byte)objects[_idx_cur++]);
            //foreach (var item in WindC.Directs) WindC.Sync(item.Key, (byte)objects[_idx_cur++]);
            //WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }


        [PunRPC]
        void UpdateDataAndView(object[] objects)
        {
            _systems.Run(SystemDataTypes.RunAfterDoing);
            _updateUI.Invoke();
            _updateView.Invoke();
        }

        #endregion


        #region Serialize

        //public void Init()
        //{
        //    PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
        //}

        //public static object DeserializeVector2Int(byte[] data)
        //{
        //    Vector2Int result = new Vector2Int();

        //    result.x = BitConverter.ToInt32(data, 0);
        //    result.y = BitConverter.ToInt32(data, 4);

        //    return result;

        //}
        //public static byte[] SerializeVector2Int(object obj)
        //{
        //    Vector2Int vector = (Vector2Int)obj;
        //    byte[] result = new byte[8];

        //    BitConverter.GetBytes(vector.x).CopyTo(result, 0);
        //    BitConverter.GetBytes(vector.y).CopyTo(result, 4);

        //    return result;
        //}

        #endregion
    }
}