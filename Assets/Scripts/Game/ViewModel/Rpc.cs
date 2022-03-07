using Chessy.Game.EventsUI;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class Rpc : MonoBehaviour
    {
        static EntitiesModel _e;
        static Action _updateView;
        static Action _updateUI;
        static Action _runAfterDoing;
        static EventsUIManager _eventsUI;

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

        public Rpc GiveData(in EntitiesModel ents, in Action updateView, in Action updateUI, in Action runAfterDoing, in EventsUIManager eventsUI)
        {
            _e = ents;
            _updateUI = updateUI;
            _updateView = updateView;
            _runAfterDoing = runAfterDoing;
            _eventsUI = eventsUI;

            return this;
        }


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var sender = infoFrom.Sender;
            _e.RpcPoolEs.SenderC.Player = sender;
            var obj = objects[_idx_cur++];
            var whoseMove = _e.WhoseMove.Player;

            if (obj is byte idx)
            {
                var obj_1 = objects[_idx_cur++];

                if (obj_1 is ToolWeaponTypes twT)
                {
                    new GiveTakeToolWeaponMS(twT, (LevelTypes)objects[_idx_cur++], idx, sender, _e);
                }
            }

            else if (obj is AbilityTypes ability)
            {
                switch (ability)
                {
                    case AbilityTypes.CircularAttack:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_0).CoolDownC(ability).HaveCooldown)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability))
                                {
                                    _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                                    _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);
                                    _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);


                                    foreach (var idxC_0 in _e.CellEs(idx_0).AroundCellIdxsC)
                                    {
                                        var idx_1 = idxC_0.Idx;

                                        if (_e.UnitTC(idx_1).HaveUnit)
                                        {
                                            if (!_e.UnitPlayerTC(idx_1).Is(_e.UnitPlayerTC(idx_0).Player))
                                            {
                                                if (_e.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
                                                {
                                                    _e.UnitExtraTWE(idx_1).DamageBrokeShieldC.Damage = 1f;
                                                }

                                                else
                                                {
                                                    _e.AttackUnitE(idx_1).Set(CellUnitStatHp_VALUES.HP / 4, _e.UnitPlayerTC(idx_0).Player);
                                                }
                                            }
                                        }
                                    }

                                    _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);
                                    _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                                    _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AttackMelee);
                                }

                                else
                                {
                                    _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }

                            else _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
                        }
                        break;

                    case AbilityTypes.BonusNear:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_0).CoolDownC(ability).HaveCooldown)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability))
                                {
                                    _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);

                                    _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);
                                    _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                                    _e.RpcPoolEs.SoundToGeneral(sender, ability);

                                    foreach (var idx_1 in _e.CellEs(idx_0).IdxsAround)
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
                                    _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }

                            else _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
                        }
                        break;

                    case AbilityTypes.FirePawn:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NEED_FOR_PAWN_FIRE)
                            {
                                if (_e.AdultForestC(idx_0).HaveAnyResources)
                                {
                                    _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                                    _e.HaveFire(idx_0) = true;
                                    _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NEED_FOR_PAWN_FIRE;
                                }

                                else
                                {
                                    throw new Exception();
                                }
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.PutOutFirePawn:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NEED_FOR_PAWN_PUT_OUT_FIRE)
                            {
                                _e.HaveFire(idx_0) = false;

                                _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NEED_FOR_PAWN_PUT_OUT_FIRE;
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.Seed:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability))
                            {
                                if (_e.BuildingTC(idx_0).HaveBuilding && !_e.BuildingTC(idx_0).Is(BuildingTypes.Camp))
                                {
                                    _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                                else
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAnyResources)
                                    {
                                        if (!_e.YoungForestC(idx_0).HaveAnyResources)
                                        {
                                            _e.RpcPoolEs.SoundToGeneral(sender, ability);

                                            _e.YoungForestC(idx_0).Resources = Environment_Values.ENVIRONMENT_MAX;

                                            _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);
                                        }
                                        else
                                        {
                                            _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                        }
                                    }
                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                }
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.SetFarm:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability))
                            {
                                if (!_e.BuildingTC(idx_0).HaveBuilding || _e.BuildingTC(idx_0).Is(BuildingTypes.Camp))
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAnyResources)
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuild = true;

                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                        {
                                            if (resT == ResourceTypes.Wood)
                                            {
                                                needRes.Add(resT, ECONOMY_VALUES.WOOD_FOR_BUILDING_FARM);
                                            }
                                            else
                                            {
                                                needRes.Add(resT, 0);
                                            }

                                            if (needRes[resT] > _e.PlayerE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                                        }

                                        if (canBuild)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            {
                                                _e.PlayerE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                                            }

                                            _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);

                                            _e.YoungForestC(idx_0).Resources = 0;

                                            _e.BuildingTC(idx_0).Building = BuildingTypes.Farm;
                                            _e.BuildingLevelTC(idx_0).Level = LevelTypes.First;
                                            _e.BuildingPlayerTC(idx_0).Player = whoseMove;
                                            _e.BuildHpC(idx_0).Health = Building_Values.MaxHealth(BuildingTypes.Farm);

                                            _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);
                                        }

                                        else
                                        {
                                            _e.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                        }
                                    }

                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                }

                                else
                                {
                                    _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.SetCity:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NEED_FOR_BUILDING_CITY)
                            {
                                if (!_e.AdultForestC(idx_0).HaveAnyResources)
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
                                        _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);
                                        _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AfterBuildTown);

                                        _e.BuildingMainE(idx_0).Set(BuildingTypes.City, LevelTypes.First, Building_Values.HELTH_CITY, whoseMove);

                                        _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NEED_FOR_BUILDING_CITY;

                                        _e.HaveFire(idx_0) = false;

                                        _e.AdultForestC(idx_0).Resources = 0;
                                        _e.FertilizeC(idx_0).Resources = 0;
                                        _e.YoungForestC(idx_0).Resources = 0;
                                    }

                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                                    }
                                }
                                else
                                {
                                    _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }

                        break;

                    case AbilityTypes.DestroyBuilding:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).HaveAnySteps)
                            {
                                _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                                _e.BuildingMainE(idx_0).AttackBuildingC.Damage = 1;
                                _e.BuildingMainE(idx_0).KillerC.Player = _e.UnitPlayerTC(idx_0).Player;

                                _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NEED_FOR_DESTROY_BUILDING;
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.FireArcher:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_from).Steps >= UnitStep_Values.ARCHER_FIRE)
                            {
                                if (_e.UnitEs(idx_from).ForArson.Contains(idx_to))
                                {
                                    _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                                    _e.UnitStepC(idx_from).Steps -= UnitStep_Values.ARCHER_FIRE;
                                    _e.HaveFire(idx_to) = false;
                                }
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.GrowAdultForest:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_0).CoolDownC(ability).HaveCooldown)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NEED_FOR_GROW_ADULT_FOREST)
                                {
                                    if (_e.YoungForestC(idx_0).HaveAnyResources)
                                    {
                                        _e.YoungForestC(idx_0).Resources = 0;

                                        _e.AdultForestC(idx_0).Resources = Environment_Values.ENVIRONMENT_MAX;

                                        _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NEED_FOR_GROW_ADULT_FOREST;

                                        _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.AFTER_GROW_ADULT_FOREST;


                                        foreach (var idx_1 in _e.CellEs(idx_0).IdxsAround)
                                        {
                                            if (_e.YoungForestC(idx_1).HaveAnyResources)
                                            {
                                                _e.AdultForestC(idx_1).Resources = Environment_Values.ENVIRONMENT_MAX;
                                            }
                                        }



                                        _e.RpcPoolEs.SoundToGeneral(sender, ability);


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

                                    else _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }

                                else _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }

                            else
                            {
                                _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
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
                                        if (_e.AdultForestC(idx_to).HaveAnyResources)
                                        {
                                            if (_e.UnitHpC(idx_from).Health >= CellUnitStatHp_VALUES.HP)
                                            {
                                                if (_e.UnitStepC(idx_from).Steps >= UnitStep_Values.NeedForAbility(ability))
                                                {
                                                    if (!_e.UnitPlayerTC(idx_from).Is(_e.UnitPlayerTC(idx_to).Player))
                                                    {
                                                        _e.UnitEffectStunC(idx_to).Stun = CellUnitEffectStun_Values.ForStunAfterAbility(ability);
                                                        _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);

                                                        _e.UnitStepC(idx_from).Steps -= UnitStep_Values.NeedForAbility(ability);

                                                        _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ability);


                                                        foreach (var idx_1 in _e.CellEs(idx_to).IdxsAround)
                                                        {
                                                            if (_e.AdultForestC(idx_1).HaveAnyResources)
                                                            {
                                                                if (_e.UnitTC(idx_1).HaveUnit && _e.UnitPlayerTC(idx_1).Is(_e.UnitPlayerTC(idx_to).Player))
                                                                {
                                                                    _e.UnitEffectStunC(idx_1).Stun = CellUnitEffectStun_Values.ForStunAfterAbility(ability);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                else _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                            }

                                            else _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                                        }
                                    }
                                }
                            }

                            else _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
                        }
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (_e.UnitHpC(idx_from).Health >= CellUnitStatHp_VALUES.HP)
                            {
                                if (_e.UnitStepC(idx_from).Steps >= UnitStep_Values.NeedForAbility(ability))
                                {
                                    _e.DirectWindTC.Direct = _e.CellEs(_e.CenterCloudIdxC.Idx).Direct(idx_to);

                                    _e.UnitStepC(idx_from).Steps -= UnitStep_Values.NeedForAbility(ability);

                                    _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);

                                    _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ability);

                                }

                                else _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                            else _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                        }
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitHpC(idx_0).Health >= CellUnitStatHp_VALUES.HP)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.CHANGE_CORNER_ARCHER)
                                {
                                    _e.UnitIsRightArcherC(idx_0).ToggleSide();

                                    _e.UnitStepC(idx_0).Steps -= UnitStep_Values.CHANGE_CORNER_ARCHER;

                                    _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickArcher);
                                }
                                else
                                {
                                    _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
                            }
                        }
                        break;

                    case AbilityTypes.IceWall:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NEED_FOR_BUILDING_ICEWALL || _e.RiverEs(idx_0).RiverTC.HaveRiverNear)
                            {
                                if (!_e.BuildingTC(idx_0).HaveBuilding)
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAnyResources)
                                    {
                                        _e.AdultForestC(idx_0).Resources = 0;
                                        _e.FertilizeC(idx_0).Resources = 0;

                                        if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NEED_FOR_BUILDING_ICEWALL)
                                        {
                                            _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NEED_FOR_BUILDING_ICEWALL;

                                            _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.AFTER_ICE_WALL;

                                            _e.BuildingMainE(idx_0).Set(BuildingTypes.IceWall, LevelTypes.First, CellUnitStatHp_VALUES.HP, _e.UnitPlayerTC(idx_0).Player);
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.ActiveAroundBonusSnowy:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability) || _e.RiverEs(idx_0).RiverTC.HaveRiverNear)
                            {
                                if (!_e.RiverEs(idx_0).RiverTC.HaveRiverNear) _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);

                                if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability))
                                {
                                    _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);
                                    _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);

                                    foreach (var idx_1 in _e.CellEs(idx_0).IdxsAround)
                                    {
                                        if (_e.UnitTC(idx_0).HaveUnit)
                                        {
                                            if (_e.UnitPlayerTC(idx_1).Is(whoseMove))
                                            {
                                                if (_e.UnitMainE(idx_1).IsMelee && !_e.UnitTC(idx_1).Is(UnitTypes.Camel))
                                                {
                                                    _e.UnitWaterC(idx_1).Water = _e.UnitInfo(_e.UnitPlayerTC(idx_1), _e.UnitLevelTC(idx_1), _e.UnitTC(idx_1)).WaterMax;
                                                    _e.UnitHpC(idx_1).Health = CellUnitStatHp_VALUES.HP;
                                                    _e.UnitEffectShield(idx_1).Protection = CellUnitEffectShield_Values.AFTER_DIRECT_WAVE;
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

                            if (_e.UnitStepC(idx_from).Steps >= UnitStep_Values.NeedForAbility(ability) || _e.RiverEs(idx_from).RiverTC.HaveRiverNear)
                            {
                                if (!_e.RiverEs(idx_from).RiverTC.HaveRiverNear) _e.UnitStepC(idx_from).Steps -= UnitStep_Values.NeedForAbility(ability);

                                if (_e.UnitStepC(idx_from).Steps >= UnitStep_Values.NeedForAbility(ability))
                                {
                                    _e.UnitStepC(idx_from).Steps -= UnitStep_Values.NeedForAbility(ability);
                                    _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);

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
                                    _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
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
                                    if (_e.UnitStepC(idx_from).Steps >= UnitStep_Values.NeedForAbility(ability))
                                    {
                                        _e.UnitEs(idx_from).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);
                                        _e.UnitStepC(idx_from).Steps -= UnitStep_Values.NeedForAbility(ability);

                                        if (_e.LastDiedE(idx_to).UnitTC.HaveUnit)
                                        {
                                            //e.UnitE(idx_to).SetNew((e.LastDiedUnitTC(idx_to).Unit, e.LastDiedLevelTC(idx_to).Level, e.LastDiedPlayerTC(idx_to).Player, ConditionUnitTypes.None, false), e);
                                            _e.LastDiedUnitTC(idx_to).Unit = UnitTypes.None;
                                        }
                                    }
                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.SetTeleport:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.BuildingTC(idx_0).HaveBuilding)
                            {
                                if (!_e.AdultForestC(idx_0).HaveAnyResources)
                                {
                                    _e.YoungForestC(idx_0).Resources = 0;
                                    _e.FertilizeC(idx_0).Resources = 0;

                                    if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability))
                                    {
                                        _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);

                                        if (_e.StartTeleportIdxC.Idx > 0)
                                        {
                                            if (_e.EndTeleportIdxC.Idx > 0)
                                            {
                                                _e.BuildingTC(_e.StartTeleportIdxC.Idx).Building = BuildingTypes.None;

                                                _e.StartTeleportIdxC = _e.EndTeleportIdxC;

                                                _e.EndTeleportIdxC.Idx = idx_0;
                                                _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);
                                            }
                                            else
                                            {
                                                _e.EndTeleportIdxC.Idx = idx_0;
                                                _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = CellUnitAbilityCooldown_Values.NeedAfterAbility(ability);
                                            }
                                        }
                                        else
                                        {
                                            _e.StartTeleportIdxC.Idx = idx_0;
                                        }

                                        _e.BuildingTC(idx_0).Building = BuildingTypes.Teleport;
                                        _e.BuildingLevelTC(idx_0).Level = LevelTypes.First;
                                        _e.BuildingPlayerTC(idx_0).Player = whoseMove;
                                        _e.BuildHpC(idx_0).Health = Building_Values.MaxHealth(BuildingTypes.Teleport);
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.Teleport:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.NeedForAbility(ability))
                            {
                                if (_e.BuildingTC(idx_0).Is(BuildingTypes.Teleport))
                                {
                                    var idx_start = _e.StartTeleportIdxC.Idx;
                                    var idx_end = _e.EndTeleportIdxC.Idx;

                                    if (_e.EndTeleportIdxC.Idx > 0 && idx_start == idx_0)
                                    {
                                        if (!_e.UnitTC(idx_end).HaveUnit)
                                        {
                                            _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);

                                            //Teleport(idx_end, ents);
                                        }
                                    }
                                    else if (_e.StartTeleportIdxC.Idx > 0 && idx_end == idx_0)
                                    {
                                        if (!_e.UnitTC(idx_start).HaveUnit)
                                        {
                                            _e.UnitStepC(idx_0).Steps -= UnitStep_Values.NeedForAbility(ability);

                                            //Teleport(idx_start, _e);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
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
                _eventsUI.LeftCityEventUI.BuyBuilding_Master(buildT, sender);
            }

            else if (obj is MarketBuyTypes marketBuy) _eventsUI.CenterBuildingEnventsUI.Buy_Master(marketBuy, sender);

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
                        _e.RpcPoolEs.Done();
                        break;

                    case RpcMasterTypes.Shift:
                        _e.RpcPoolEs.ShiftUnitME.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Attack:
                        _e.RpcPoolEs.AttackME.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
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
                                        _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                                    }

                                    else if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.FOR_TOGGLE_CONDITION_UNIT)
                                    {
                                        _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitStepC(idx_0).Steps -= UnitStep_Values.FOR_TOGGLE_CONDITION_UNIT;
                                        _e.UnitConditionTC(idx_0).Condition = condT;
                                    }

                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                    break;


                                case ConditionUnitTypes.Relaxed:
                                    if (_e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                                    {
                                        _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                                    }

                                    else if (_e.UnitStepC(idx_0).Steps >= UnitStep_Values.FOR_TOGGLE_CONDITION_UNIT)
                                    {
                                        _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                                        _e.UnitConditionTC(idx_0).Condition = condT;
                                        _e.UnitStepC(idx_0).Steps -= UnitStep_Values.FOR_TOGGLE_CONDITION_UNIT;
                                    }

                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                    break;


                                default:
                                    throw new Exception();
                            }


                        }
                        break;

                    case RpcMasterTypes.SetUnit:
                        _e.RpcPoolEs.SetUnitME.Set((byte)objects[_idx_cur++], (UnitTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.GetHero:
                        _e.RpcPoolEs.GetHeroTC.Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.Melt:
                        _eventsUI.CenterBuildingEnventsUI.Melt_Master(sender);
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
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _e.RpcPoolEs.OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
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


            _e.RpcPoolEs.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            _e.RpcPoolEs.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
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
            _runAfterDoing.Invoke();
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